using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Configuration.Interfaces
{
    public interface IPiNameValuePair
    {
        string Name { get; set; }
        string Value { get; set; }
    }
}
