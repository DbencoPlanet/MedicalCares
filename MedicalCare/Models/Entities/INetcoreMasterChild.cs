using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class INetcoreMasterChild
    {
        //never used to store data, just a mark for master detail
        public string HasChild { get; set; }
    }
}
