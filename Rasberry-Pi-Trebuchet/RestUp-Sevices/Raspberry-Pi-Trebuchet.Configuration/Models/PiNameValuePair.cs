using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Interfaces;

namespace Raspberry_Pi_Trebuchet.RestUp.Configuration.Models
{
    public class PiNameValuePair : IPiNameValuePair
    {
        [Key]
        public int NameValuePairId { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
