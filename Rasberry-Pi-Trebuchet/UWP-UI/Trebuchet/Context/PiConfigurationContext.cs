using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trebuchet.Models;

namespace Trebuchet.Context
{
    /// <summary>
    /// To add a migration run following for example..
    ///
    /// Add-Migration InitialMigration -Verbose -project  Trebuchet
    /// 
    /// *****************************************************************************************************************
    /// Packages to install to get sql lite working
    /// Update-Package Microsoft.NETCore.UniversalWindowsPlatform
    /// 
    /// Install-Package Microsoft.EntityFrameworkCore.Sqlite
    /// Install-Package Microsoft.EntityFrameworkCore.Tools –Pre
    /// *****************************************************************************************************************
    /// </summary>
    public class PiConfigurationContext:DbContext
    {
        public DbSet<PiSettingsAndConfiguration> PiConfigurationSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Trebuchet.db");
        }
    }
}
