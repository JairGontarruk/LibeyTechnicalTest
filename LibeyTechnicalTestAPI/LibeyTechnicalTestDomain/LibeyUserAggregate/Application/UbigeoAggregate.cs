using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class UbigeoAggregate : IUbigeoAggregate
    {
        private readonly IUbigeoRepository _repository;
        public UbigeoAggregate(IUbigeoRepository repository)
        {
            _repository = repository;
        }

        public List<RegionResponse> FindAllRegionResponse()
        {
            var row = _repository.FindAllRegionResponses();
            return row;
        }

        public List<ProvinceResponse> FindAllProvinceResponse(string regionCode)
        {
            var row = _repository.FindAllProvinceResponses(regionCode);
            return row;
        }

        public List<UbigeoResponse> FindAllUbigeoResponse(string regionCode, string provinceCode)
        {
            var row = _repository.FindAllUbigeoResponses(regionCode, provinceCode);
            return row;
        }
    }
}
