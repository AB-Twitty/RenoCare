using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

namespace RenoCare.Core.Features.Home.Mediator.Queries
{
    public class TestSendNotify24Hour : IRequest<Unit>
    {
    }

    public class TestSendNotify24HourHandler : IRequestHandler<TestSendNotify24Hour, Unit>
    {
        private readonly IRepository<MedicationRequest> _medReqRepo;
        private readonly IRepository<DialysisUnit> _unitRepo;
        private readonly IRepository<Patient> _patientRepo;
        private readonly IHubContext<ChatHub> _hub;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IEmailSender _emailSender;

        public TestSendNotify24HourHandler(IRepository<MedicationRequest> medReqRepo, IRepository<DialysisUnit> unitRepo, IRepository<Patient> patientRepo, IHubContext<ChatHub> hub, IWebHostEnvironment webEnv, IEmailSender emailSender)
        {
            _medReqRepo = medReqRepo;
            _unitRepo = unitRepo;
            _patientRepo = patientRepo;
            _hub = hub;
            _webEnv = webEnv;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(TestSendNotify24Hour request, CancellationToken cancellationToken)
        {
            var appointments = _medReqRepo.Table.Include(x => x.Session)
                .Where(a => a.Id == 6021)
                .ToList();



            foreach (var appointment in appointments)
            {
                var patient = _patientRepo.Table.Include(x => x.User).FirstOrDefault(x => x.Id == appointment.PatientId);
                var unit = _unitRepo.Table.FirstOrDefault(x => x.Id == appointment.DialysisUnitId);


                var format_date = appointment.AppointmentDate.ToString("dd-MMM-yyyy");
                var format_time = new DateTime(appointment.Session.Time.Ticks).ToString("hh:mm tt");

                var body = "This is a reminder that you have an upcoming appointment booked with RenoCare for " + format_date + " at" + format_time;
                body += ". The details are as follows:- Dialysis Unit: " + unit.Name + "- Address:" + unit.Address + ", " + unit.City + ", " + unit.Country;
                body += "Please ensure you are prepared for your appointment and arrive on time.";


                var notification = new Notification
                {
                    UserId = patient.UserId,
                    Title = "Appointment Reminder",
                    Body = body
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
                        FullName = patient.User.FirstName + " " + patient.User.LastName,
                        DialysisUnitName = unit.Name,
                        Address = unit.Address + ", " + unit.City + ", " + unit.Country,
                        Date = format_date,
                        Time = format_time,
                        CurrentYear = DateTime.Now.Year.ToString(),
                        EmailFooter = "Please ensure you are prepared for your appointment and arrive on time."
                    };

                    var email = new Email
                    {
                        ToName = patient.User.FirstName + " " + patient.User.LastName,
                        ToAddress = patient.User.Email,
                        Subject = subject,
                        Body = bodyHtml
                    };

                    await _emailSender.SendEmailAsync(email, emailValues);
                }
                catch
                {

                }
            }

            return new Unit();
        }
    }
}
