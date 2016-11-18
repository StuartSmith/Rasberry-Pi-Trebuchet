using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Trebuchet.Context;

namespace Trebuchet.Migrations
{
    [DbContext(typeof(PiConfigurationContext))]
    [Migration("20161118133346_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Trebuchet.Models.PiSettingsAndConfiguration", b =>
                {
                    b.Property<int>("PiConfigid")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LedLightColor");

                    b.Property<string>("Name");

                    b.Property<string>("PiIP");

                    b.Property<string>("PiName");

                    b.Property<bool>("SendToast");

                    b.Property<bool>("UserAzure");

                    b.Property<bool>("isConfigurationSetting");

                    b.HasKey("PiConfigid");

                    b.ToTable("PiConfigurationSettings");
                });
        }
    }
}
