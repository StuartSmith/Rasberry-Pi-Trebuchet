using Raspberry_Pi_Trebuchet.RestUp.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Configuration.Interfaces
{
    public interface IPiNameValuePairDBSettings
    {
        bool DeleteNameValuePair(string PairName);
        PiNameValuePair GetPiNameValuePair(string PairName);
        bool SetNameValuePair(string PairName, string Value);
    }
}
