using Raspberry_Pi_Trebuchet.RestUp.Configuration.Context;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Interfaces;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Raspberry_Pi_Trebuchet.RestUp.Configuration.Services
{
    public class PiNameValuePairDBSettings : IPiNameValuePairDBSettings
    {

        public void CopyKeyValuePair(IPiNameValuePair from, IPiNameValuePair to)
        {
            to.name = from.name;
            to.value = from.value;
        }


        public bool DeleteNameValuePair(string PairName)
        {
            using (var db = new PiGeneralContext())
            {

                db.Database.Migrate();

                var PairToDelete = (from ValuePairs in db.PiNameValuePairs
                                    where ValuePairs.name.ToUpper() == PairName.ToUpper()
                                    select ValuePairs).FirstOrDefault();

                if (PairToDelete != null)
                {
                    db.PiNameValuePairs.Remove(PairToDelete);
                    return true;
                }
            }

            return false;
        }

        public List<IPiNameValuePair> GetAllNameValuePairs()
        {
            List<IPiNameValuePair> retValues;
            using (var db = new PiGeneralContext())
            {
                db.Database.Migrate();

                retValues = db.PiNameValuePairs.ToList<IPiNameValuePair>();
            }
            return retValues;
        }




        public PiNameValuePair GetPiNameValuePair(string PairName)
        {
            using (var db = new PiGeneralContext())
            {
                db.Database.Migrate();

                var allPairs = db.PiNameValuePairs.ToList<PiNameValuePair>();
                var PairToFind = (from ValuePairs in allPairs
                                  where ValuePairs.name.ToUpper() == PairName.ToUpper()
                                  select ValuePairs).FirstOrDefault();

                if (PairToFind != null)
                {
                    return PairToFind;
                }

            }
            return null;
        }


        public PiNameValuePair GetPiNameValuePair(string PairName, string DefaultValue)
        {
            var PairToFind = GetPiNameValuePair(PairName);
            if (PairToFind != null)
            {
                return PairToFind;
            }
            else
            {
                PairToFind = new PiNameValuePair() { name = PairName, value = DefaultValue };
            }

            return null;
        }

        public bool SetAllNameValuePairs(List<IPiNameValuePair> AzureValuePairs)
        {

            foreach (var AzureValuePair in AzureValuePairs)
            {
                var PairToFind = GetPiNameValuePair(AzureValuePair.name);
                if (PairToFind != null)
                {
                    if (SetNameValuePair(AzureValuePair.name, AzureValuePair.value) == false)
                        return false;
                }
            }

            return true;
        }

        public bool SetNameValuePair(string PairName, string Value)
        {
            using (var db = new PiGeneralContext())
            {
                db.Database.Migrate();

                var PairToModify = GetPiNameValuePair(PairName);

                if (PairToModify != null)
                {
                    PairToModify.value = Value;
                    db.PiNameValuePairs.Update(PairToModify);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    PairToModify = new PiNameValuePair();
                    PairToModify.value = Value;
                    PairToModify.name = PairName;
                    db.PiNameValuePairs.Add(PairToModify);
                    db.SaveChanges();
                }

            }
            return false;
        }




        /// <summary>
        /// Sets the value of the key value pair if one does not
        /// already exists vpn
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns> flase if value already exists returns true if added a new value</returns>
        public bool SetValueIfOneDoesNotExist(string PairName, string Value)
        {
            var PairToFind = GetPiNameValuePair(PairName);
            if (PairToFind == null)
            {
                SetNameValuePair(PairName, Value);
            }
            return false;
        }
    }
}
