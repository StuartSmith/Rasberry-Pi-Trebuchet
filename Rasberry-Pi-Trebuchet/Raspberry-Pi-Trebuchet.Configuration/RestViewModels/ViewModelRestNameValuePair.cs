using Raspberry_Pi_Trebuchet.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Configuration.RestViewModels
{
    public class ViewModelRestNameValuePair: IPiNameValuePair
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
