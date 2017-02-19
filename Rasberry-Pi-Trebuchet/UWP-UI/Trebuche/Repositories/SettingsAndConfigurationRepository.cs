using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trebuchet.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Trebuchet.Repositories
{

    public class SettingsAndConfigurationRepository : Repository<IPiSettingsAndConfiguration>
    {
       SettingsAndConfigurationRepository(DbContext dataContext):base(dataContext)
       {

       }

        public IQueryable<IPiSettingsAndConfiguration> GetPIConfigureationByName(string Name  )
        {
            return DbSet.Where(h=> h.Name.ToUpper() == Name.ToUpper());
        }


    }
}
