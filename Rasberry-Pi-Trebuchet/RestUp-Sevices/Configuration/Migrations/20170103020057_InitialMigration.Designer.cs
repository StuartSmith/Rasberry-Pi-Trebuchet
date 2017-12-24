using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Context;

namespace Raspberry_Pi_Trebuchet.RestUp.Configuration.Migrations
{
    [DbContext(typeof(PiGeneralContext))]
    [Migration("20170103020057_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Raspberry_Pi_Trebuchet.RestUp.Configuration.Models.PiNameValuePair", b =>
                {
                    b.Property<int>("NameValuePairId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("NameValuePairId");

                    b.ToTable("PiNameValuePairs");
                });
        }
    }
}
