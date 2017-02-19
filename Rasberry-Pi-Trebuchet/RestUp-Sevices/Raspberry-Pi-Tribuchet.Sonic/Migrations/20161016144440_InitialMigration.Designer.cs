using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Context;

namespace Raspberry_Pi_Trebuchet.RestUp.Sonic.Migrations
{
    [DbContext(typeof(UltraSonicContext))]
    [Migration("20161016144440_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Raspberry_Pi_Trebuchet.RestUp.Sonic.Models.UltraSonicSensorRun", b =>
                {
                    b.Property<int>("SonicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MeasurementIn");

                    b.Property<int>("PinGPIOEcho");

                    b.Property<int>("PinGPIOTrigger");

                    b.Property<bool>("RequestSentToAzure");

                    b.Property<DateTime>("RunDate");

                    b.Property<string>("SonicGUID");

                    b.Property<double>("TotalDistance");

                    b.HasKey("SonicId");

                    b.ToTable("UltraSonicSensorRuns");
                });

            modelBuilder.Entity("Raspberry_Pi_Trebuchet.RestUp.Sonic.Models.UltraSonicSensorRunMeasurement", b =>
                {
                    b.Property<int>("SonicMeasurementId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("MeasurementDistance");

                    b.Property<string>("MeasurementGUID");

                    b.Property<string>("SonicGUID");

                    b.Property<DateTime>("TimeOfMeasurment");

                    b.Property<int>("UltraSonicSensorRunId");

                    b.HasKey("SonicMeasurementId");

                    b.HasIndex("UltraSonicSensorRunId");

                    b.ToTable("UltraSonicSensorRunMeasurements");
                });

            modelBuilder.Entity("Raspberry_Pi_Trebuchet.RestUp.Sonic.Models.UltraSonicSensorRunMeasurement", b =>
                {
                    b.HasOne("Raspberry_Pi_Trebuchet.RestUp.Sonic.Models.UltraSonicSensorRun", "Run")
                        .WithMany("SonicMeasurements")
                        .HasForeignKey("UltraSonicSensorRunId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
