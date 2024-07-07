using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Hubs;
using RenoCare.Core.Hubs.Models;
using RenoCare.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Base
{
    public class MedReqCheckBackgroundService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<ChatHub> _hub;

        public MedReqCheckBackgroundService(IServiceProvider serviceProvider, IHubContext<ChatHub> hub)
        {
            _serviceProvider = serviceProvider;
            _hub = hub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckMedReqs, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void CheckMedReqs(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _medReqRepo = scope.ServiceProvider.GetRequiredService<IRepository<MedicationRequest>>();
                var _patientRepo = scope.ServiceProvider.GetRequiredService<IRepository<Patient>>();
                var _unitRepo = scope.ServiceProvider.GetRequiredService<IRepository<DialysisUnit>>();

                //gat medReqs where the appointment time has exceeded the current time and it's status is upcoming or pending
                var pending_medReqs = _medReqRepo.Table.Include(x => x.Session).Where(x => x.AppointmentDate > DateTime.Now && x.StatusId == 1).ToList();

                foreach (var medReq in pending_medReqs)
                {
                    medReq.StatusId = 4;



                    //notify the user that the request has passed ir's time with no response
                    var unit_name = _unitRepo.Table.Where(x => x.Id == medReq.DialysisUnitId)
                            .Select(x => x.Name).FirstOrDefault();

                    var notification = new Notification
                    {
                        UserId = _patientRepo.Table.Where(x => x.Id == medReq.PatientId).Select(x => x.UserId).FirstOrDefault(),
                        Title = "Pending with no respond",
                        Body = $"Your appointment at {unit_name} booked for ${medReq.AppointmentDate:dd-MMM-yyyy} at ${medReq.Session.Time:hh:mm tt} didn't receive any response."
                    };

                    await _hub.Clients.User(notification.UserId).SendAsync("OnNotified", notification);
                }

                var upcoming_medReqs = _medReqRepo.Table.Include(x => x.Session).Where(x => x.AppointmentDate > DateTime.Now && x.StatusId == 2).ToList();

                foreach (var medReq in upcoming_medReqs)
                {
                    medReq.StatusId = 3;

                    //weekly booking
                    if (medReq.TypeId == 2)
                    {
                        var next_weekMedReq = new MedicationRequest
                        {
                            PatientId = medReq.PatientId,
                            DialysisUnitId = medReq.DialysisUnitId,
                            SessionId = medReq.SessionId,
                            PatientProblem = medReq.PatientProblem,
                            TypeId = medReq.TypeId,
                            StatusId = 2,
                            Treatment = medReq.Treatment,
                            AppointmentDate = medReq.AppointmentDate.AddDays(7),
                        };

                        await _medReqRepo.InsertAsync(next_weekMedReq);

                        //notify the user with the new made med req
                        var unit_name = _unitRepo.Table.Where(x => x.Id == medReq.DialysisUnitId)
                            .Select(x => x.Name).FirstOrDefault();

                        var notification = new Notification
                        {
                            UserId = _patientRepo.Table.Where(x => x.Id == medReq.PatientId).Select(x => x.UserId).FirstOrDefault(),
                            Title = "Next week session has been booked",
                            Body = $"A new session at {unit_name} was booked for ${next_weekMedReq.AppointmentDate:dd-MMM-yyyy} at ${medReq.Session.Time:hh:mm tt}."
                        };

                        await _hub.Clients.User(notification.UserId).SendAsync("OnNotified", notification);
                    }

                }

                await _medReqRepo.SaveAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
