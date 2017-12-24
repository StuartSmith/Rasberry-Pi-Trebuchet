using Microsoft.EntityFrameworkCore;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Raspberry_Pi_Trebuchet.RestUp.Configuration.Context
{
    /// <summary>
    /// To add a migration run following for example..
    ///
    /// Add-Migration InitialMigration -Verbose -project  Raspberry-Pi-Trebuchet.Configuration
    /// 
    /// *****************************************************************************************************************
    /// Packages to install to get sql lite working
    /// Update-Package Microsoft.NETCore.UniversalWindowsPlatform
    /// 
    /// Install-Package Microsoft.EntityFrameworkCore.Sqlite
    /// Install-Package Microsoft.EntityFrameworkCore.Tools –Pre
    /// *****************************************************************************************************************
    /// </summary>
    public class PiGeneralContext : DbContext
    {

        public DbSet<PiNameValuePair> PiNameValuePairs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=General.db");
        }
    }
}

