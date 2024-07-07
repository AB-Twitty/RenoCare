using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RenoCare.Core.Conatracts.Mail;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Hubs;
using RenoCare.Core.Hubs.Models;
using RenoCare.Domain;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Base
{
    public class Notify24HourBeforeBooking : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<ChatHub> _hub;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webEnv;

        public Notify24HourBeforeBooking(IServiceProvider serviceProvider, IHubContext<ChatHub> hub, IEmailSender emailSender, IWebHostEnvironment webEnv)
        {
            _serviceProvider = serviceProvider;
            _hub = hub;
            _emailSender = emailSender;
            _webEnv = webEnv;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Notify24HourBefore, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void Notify24HourBefore(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _medReqRepo = scope.ServiceProvider.GetRequiredService<IRepository<MedicationRequest>>();
                var _patientRepo = scope.ServiceProvider.GetRequiredService<IRepository<Patient>>();
                var _unitRepo = scope.ServiceProvider.GetRequiredService<IRepository<DialysisUnit>>();


                DateTime currentTime = DateTime.Now;
                DateTime targetTime = currentTime.AddHours(24);

                // Query appointments where the appointment date is exactly 24 hours from the current time
                var appointments = _medReqRepo.Table.Include(x => x.Session)
                    .Where(a => a.AppointmentDate > currentTime && a.AppointmentDate <= targetTime)
                    .ToList();


                foreach (var appointment in appointments)
                {
                    var patient = _patientRepo.Table.Include(x => x.User).FirstOrDefault(x => x.Id == appointment.PatientId);
                    var unit = _unitRepo.Table.FirstOrDefault(x => x.Id == appointment.DialysisUnitId);

                    var notification = new Notification
                    {
                        UserId = patient.UserId,
                        Title = "Appointment Reminder",
                        Body = $"This is a reminder that you have an upcoming appointment booked with RenoCare for {appointment.AppointmentDate:dd-MMM-yyyy} at {appointment.Session.Time:hh:mm tt}. The details are as follows:\n\n- Dialysis Unit: {unit.Name}\n- Address: {unit.Address}, {unit.City}, {unit.Country}\n\nPlease ensure you are prepared for your appointment and arrive on time."
                    };

                    await _hub.Clients.User(notification.UserId).SendAsync("OnNotified", notification);


                    try
                    {
                        var pathToTemplate = _webEnv.WebRootPath + Path.DirectorySeparatorChar.ToString()
                                + "Templates" + Path.DirectorySeparatorChar.ToString() + "Mail" + Path.DirectorySeparatorChar.ToString()
                                + "Email_Reminder_Template.cshtml";

                        var subject = "Reminder: Your Appointment with RenoCare";
                        var bodyHtml = "";

                        using (StreamReader streamReader = File.OpenText(pathToTemplate))
                        {
                            bodyHtml = streamReader.ReadToEnd();
                        }

                        var emailValues = new
                        {
                            EmailHeader = "This is a reminder that you have an upcoming appointment booked with RenoCare:",
                            FullName = $"{patient.User.FirstName} {patient.User.LastName}",
                            DialysisUnitName = unit.Name,
                            Address = $"{unit.Address}, {unit.Country}, {unit.City}",
                            Date = appointment.AppointmentDate,
                            Time = appointment.Session.Time,
                            CurrentYear = DateTime.Now.Year.ToString(),
                            EmailFooter = "Please ensure you are prepared for your appointment and arrive on time."
                        };

                        var email = new Email
                        {
                            ToName = $"{patient.User.FirstName} {patient.User.LastName}",
                            ToAddress = $"{patient.User.Email}",
                            Subject = subject,
                            Body = bodyHtml
                        };

                        await _emailSender.SendEmailAsync(email, emailValues);
                    }
                    catch
                    {

                    }
                }

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
