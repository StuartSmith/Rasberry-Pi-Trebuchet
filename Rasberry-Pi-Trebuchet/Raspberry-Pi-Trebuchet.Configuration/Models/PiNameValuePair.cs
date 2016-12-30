using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Raspberry_Pi_Trebuchet.Configuration.Interfaces;

namespace Raspberry_Pi_Trebuchet.Configuration.Models
{
    public class PiNameValuePair : IPiNameValuePair
    {
        [Key]
        public int NameValuePairId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
