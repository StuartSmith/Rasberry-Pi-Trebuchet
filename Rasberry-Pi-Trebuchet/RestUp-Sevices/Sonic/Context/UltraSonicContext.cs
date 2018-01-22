using Microsoft.EntityFrameworkCore;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Sonic.Context
{
    /// <summary>
    /// To add a migration run following for example..
    ///
    /// Add-Migration InitialMigration -Verbose -project  Raspberry-Pi-Trebuchet.Sonic
    /// 
    /// *****************************************************************************************************************
    /// Packages to install to get sql lite working
    /// Update-Package Microsoft.NETCore.UniversalWindowsPlatform
    /// 
    /// Install-Package Microsoft.EntityFrameworkCore.Sqlite
    /// Install-Package Microsoft.EntityFrameworkCore.Tools –Pre
    /// *****************************************************************************************************************
    /// </summary>
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
