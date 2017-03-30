using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.RestViewModels
{
    public class MsgContentToAndFromAzure:MsgContentToAzure
    {
        public string Response { get; set; }

        public MsgContentToAndFromAzure() { }

        public MsgContentToAndFromAzure(MsgContentToAzure obj)
         : base(obj)
    {
            this.Response = "";
        }
        public MsgContentToAndFromAzure(MsgContentToAndFromAzure other)
         : base(other)
        {
            this.Response = other.Response;
        }

    }
}
