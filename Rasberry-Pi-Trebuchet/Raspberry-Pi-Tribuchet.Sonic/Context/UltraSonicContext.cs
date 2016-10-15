using Microsoft.EntityFrameworkCore;
using Raspberry_Pi_Tribuchet.Sonic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Context
{
    public class UltraSonicContext:DbContext
    {
        public DbSet<UltraSonicSensorRun> UltraSonicSensorRuns { get; set; }
        public DbSet<UltraSonicSensorRunMeasurement> UltraSonicSensorRunMeasurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=UltraSonic.db");
        }
    }
}
