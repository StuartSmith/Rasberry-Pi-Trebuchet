using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trebuchet.Interfaces
{
    public interface ISettingsAndConfigurationRepository
    {
        void Add(IPiSettingsAndConfiguration p);
        void Edit(IPiSettingsAndConfiguration p);
        IPiSettingsAndConfiguration FindById(int Id);
        IPiSettingsAndConfiguration FindByName(string Name);

        IEnumerable GetConfigurationSettings();

    }
}
