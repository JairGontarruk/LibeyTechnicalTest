using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IUbigeoAggregate
    {
        List<RegionResponse> FindAllRegionResponse();
        List<ProvinceResponse> FindAllProvinceResponse(string regionCode);
        List<UbigeoResponse> FindAllUbigeoResponse(string regionCode, string provinceCode);
    }
}
