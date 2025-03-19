using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Province
    {
        [Key]
        public string ProvinceCode { get; private set; }
        public string RegionCode { get; private set; }
        public string ProvinceDescription { get; private set; }

        public Province(string provinceCode,string regionCode, string provinceDescription)
        {
            ProvinceCode = provinceCode;
            RegionCode = regionCode;
            ProvinceDescription = provinceDescription;
        }
    }
}
