using Raspberry_Pi_Trebuchet.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Configuration.RestViewModels
{
    [DebuggerDisplay( "Name={name} Value={value} ")]
    public class ViewModelRestNameValuePair: IPiNameValuePair
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
