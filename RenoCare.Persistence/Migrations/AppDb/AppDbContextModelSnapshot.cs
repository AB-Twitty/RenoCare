﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RenoCare.Persistence;

namespace RenoCare.Persistence.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RenoCare.Domain.Chat.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SendingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId")
                        .IsUnique();

                    b.HasIndex("SenderId")
                        .IsUnique();

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RenoCare.Domain.DiabetesType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Diabetes_Types");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "No diabetes and fall within the normal range of blood sugar levels.",
                            Name = "Non-diabetic"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Type 1 diabetes is where the blood glucose (sugar) level is too high because the body can’t make a hormone called insulin. The body still breaks down the carbohydrate from food and drink and turns it into glucose. But when the glucose enters the bloodstream, there’s no insulin to allow it into the body’s cells. More and more glucose then builds up in the bloodstream, leading to high blood sugar levels.",
                            Name = "Type 1 diabetes"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Type 2 diabetes is where the insulin the pancreas makes can’t work properly, or the pancreas can’t make enough insulin. This means the blood glucose (sugar) levels keep rising. Having type 2 diabetes without treatment means that high sugar levels in the blood can seriously damage parts of the body, including the eyes, heart and feet. These are called the complications of diabetes. But with the right treatment and care, the patient can live well with type 2 diabetes and reduce the risk of developing them.",
                            Name = "Type 2 diabetes"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Gestational diabetes is diabetes that can develop during pregnancy. It affects women who haven't been affected by diabetes before. It means she has high blood sugar and needs to take extra care of herself and her bump. This will include eating well and keeping active. It usually goes away again after giving birth. It is usually diagnosed from a blood test 24 to 28 weeks into pregnancy.",
                            Name = "Gestational diabetes"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Monogenic diabetes is a rare condition, different from both type 1 and type 2 diabetes. It’s caused by a mutation in a single gene. If a parent has this mutation, their children have a 50p per cent chance of inheriting it. In some cases of monogenic diabetes, the condition can be managed with specific tablets and doesn’t require insulin treatment.",
                            Name = "Monogenic diabetes"
                        });
                });

            modelBuilder.Entity("RenoCare.Domain.DialysisUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsHDFSupported")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHDSupported")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Dialysis_Units");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "the street where the unit is located",
                            City = "Paris",
                            Country = "France",
                            Description = "this is the description for the dialysis unit",
                            IsDeleted = false,
                            IsHDFSupported = true,
                            IsHDSupported = true,
                            Name = "Dialysis unit name",
                            PhoneNumber = "123456789",
                            UserId = "30aaf317-be57-4870-9768-2af3599936v2"
                        });
                });

            modelBuilder.Entity("RenoCare.Domain.HypertensionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Hypertension_Types");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Systolic blood pressure less than 120 mm Hg and diastolic blood pressure less than 80 mm Hg.",
                            Name = "Normal"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Systolic blood pressure between 120-129 mm Hg and diastolic blood pressure less than 80 mm Hg.",
                            Name = "Elevated"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Systolic blood pressure consistently ranging from 130-139 mm Hg or diastolic blood pressure consistently ranging from 80-89 mm Hg.",
                            Name = "Hypertension Stage 1"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Systolic blood pressure of 140 mm Hg or higher or diastolic blood pressure of 90 mm Hg or higher.",
                            Name = "Hypertension Stage 2"
                        },
                        new
                        {
                            Id = 5,
                            Description = " Blood pressure readings exceeding 180/120 mm Hg, requiring immediate medical attention.",
                            Name = "Hypertensive Crisis:"
                        });
                });

            modelBuilder.Entity("RenoCare.Domain.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("RenoCare.Domain.MedicationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppointmentHour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DialysisUnitId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("PatientProblem")
                        .HasColumnType("text");

                    b.Property<int?>("ReportId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DialysisUnitId")
                        .IsUnique();

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.HasIndex("ReportId")
                        .IsUnique()
                        .HasFilter("[ReportId] IS NOT NULL");

                    b.HasIndex("StatusId")
                        .IsUnique();

                    b.HasIndex("TypeId")
                        .IsUnique();

                    b.ToTable("Medication_Requests");
                });

            modelBuilder.Entity("RenoCare.Domain.MedicationRequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LabelClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Medication_Request_Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Indicates that the medication request is pending / awaiting to be reviewed by the healthcare provider.",
                            LabelClass = "#f0ad4e",
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Indicates that the medication request is upcoming / reviewed by the healthcare provider and approved it.",
                            LabelClass = "#20809D",
                            Name = "Upcoming"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Indicates that the medication request is completed.",
                            LabelClass = "#5cb85c",
                            Name = "Completed"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Indicates that the medication request is rejected / reviewed by the healthcare provider and declined it.",
                            LabelClass = "#A72925",
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Indicates that the medication request is either cancelled by the patient or its time has passed without reviewing it.",
                            LabelClass = "#d9534f",
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("RenoCare.Domain.MedicationRequestType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Medication_Request_Types");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Book for only one time.",
                            IsActive = true,
                            Name = "Just Once"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Automatically book the same medication request every week.",
                            IsActive = true,
                            Name = "Weekly"
                        });
                });

            modelBuilder.Entity("RenoCare.Domain.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeletionReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiabetesTypeId")
                        .HasColumnType("int");

                    b.Property<int>("HypertensionTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("KidneyFailureCause")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SmokingStatusId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DiabetesTypeId")
                        .IsUnique();

                    b.HasIndex("HypertensionTypeId")
                        .IsUnique();

                    b.HasIndex("SmokingStatusId")
                        .IsUnique()
                        .HasFilter("[SmokingStatusId] IS NOT NULL");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DiabetesTypeId = 1,
                            HypertensionTypeId = 1,
                            IsDeleted = false,
                            KidneyFailureCause = "Hypertension",
                            UserId = "a6d6f491-1957-4e70-98c7-997eb0d3255f"
                        });
                });

            modelBuilder.Entity("RenoCare.Domain.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Albumin")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<double>("Creatinine")
                        .HasColumnType("float");

                    b.Property<double>("DialysisDuration")
                        .HasColumnType("float");

                    b.Property<int>("DialysisFrequency")
                        .HasColumnType("int");

                    b.Property<int>("DialysisUnitId")
                        .HasColumnType("int");

                    b.Property<string>("DialyzerType")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("DryWeight")
                        .HasColumnType("float");

                    b.Property<string>("DuringBloodPressure")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("FluidRemovalRate")
                        .HasColumnType("float");

                    b.Property<int>("HeartRate")
                        .HasColumnType("int");

                    b.Property<double>("Hematocrit")
                        .HasColumnType("float");

                    b.Property<double>("Hemoglobin")
                        .HasColumnType("float");

                    b.Property<double>("Kt_V")
                        .HasColumnType("float");

                    b.Property<DateTime>("LastModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicationRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Nephrologist")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("PostBloodPressure")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("PostUrea")
                        .HasColumnType("float");

                    b.Property<double>("PostWeight")
                        .HasColumnType("float");

                    b.Property<double>("Potassium")
                        .HasColumnType("float");

                    b.Property<string>("PreBloodPressure")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("PreUrea")
                        .HasColumnType("float");

                    b.Property<double>("PreWeight")
                        .HasColumnType("float");

                    b.Property<double>("TotalFluidRemoval")
                        .HasColumnType("float");

                    b.Property<double>("UreaReductionRatio")
                        .HasColumnType("float");

                    b.Property<double>("UrineOutput")
                        .HasColumnType("float");

                    b.Property<string>("VascularAccessType")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DialysisUnitId")
                        .IsUnique();

                    b.HasIndex("MedicationRequestId")
                        .IsUnique();

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.ToTable("Session_Reports");
                });

            modelBuilder.Entity("RenoCare.Domain.SmokingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Smoking_Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Individuals who have never smoked.",
                            Name = "Non Smoker"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Individuals who used to smoke but have successfully quit.",
                            Name = "Former Smoker"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Individuals who currently smoke.",
                            Name = "Current Smoker"
                        });
                });

            modelBuilder.Entity("RenoCare.Domain.Chat.ChatMessage", b =>
                {
                    b.HasOne("RenoCare.Domain.Identity.AppUser", "Receiver")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Chat.ChatMessage", "ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.Identity.AppUser", "Sender")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Chat.ChatMessage", "SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("RenoCare.Domain.DialysisUnit", b =>
                {
                    b.HasOne("RenoCare.Domain.Identity.AppUser", "User")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.DialysisUnit", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RenoCare.Domain.MedicationRequest", b =>
                {
                    b.HasOne("RenoCare.Domain.DialysisUnit", "DialysisUnit")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.MedicationRequest", "DialysisUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.Patient", "Patient")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.MedicationRequest", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.Report", "Report")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.MedicationRequest", "ReportId");

                    b.HasOne("RenoCare.Domain.MedicationRequestStatus", "Status")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.MedicationRequest", "StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.MedicationRequestType", "Type")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.MedicationRequest", "TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DialysisUnit");

                    b.Navigation("Patient");

                    b.Navigation("Report");

                    b.Navigation("Status");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("RenoCare.Domain.Patient", b =>
                {
                    b.HasOne("RenoCare.Domain.DiabetesType", "DiabetesType")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Patient", "DiabetesTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.HypertensionType", "HypertensionType")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Patient", "HypertensionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.SmokingStatus", "SmokingStatus")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Patient", "SmokingStatusId");

                    b.HasOne("RenoCare.Domain.Identity.AppUser", "User")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Patient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiabetesType");

                    b.Navigation("HypertensionType");

                    b.Navigation("SmokingStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RenoCare.Domain.Report", b =>
                {
                    b.HasOne("RenoCare.Domain.DialysisUnit", "DialysisUnit")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Report", "DialysisUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.MedicationRequest", "MedicationRequest")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Report", "MedicationRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RenoCare.Domain.Patient", "Patient")
                        .WithOne()
                        .HasForeignKey("RenoCare.Domain.Report", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DialysisUnit");

                    b.Navigation("MedicationRequest");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
