using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Configuration.Interfaces
{
    public interface IPiNameValuePair
    {
        string name { get; set; }
        string value { get; set; }
    }
}
