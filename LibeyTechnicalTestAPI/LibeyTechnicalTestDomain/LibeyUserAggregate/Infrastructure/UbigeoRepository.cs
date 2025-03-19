using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class UbigeoRepository : IUbigeoRepository
    {
        private readonly Context _context;

        public UbigeoRepository(Context context)
        {
            _context = context;
        }

        public List<RegionResponse> FindAllRegionResponses()
        {
            return _context.Region
                .Select(ubigeo => new RegionResponse
                {
                    RegionCode = ubigeo.RegionCode,
                    RegionDescription = ubigeo.RegionDescription
                }).ToList();
        }

        public List<ProvinceResponse> FindAllProvinceResponses(string regionCode)
        {
            return _context.Province
                .Where(ubigeo => ubigeo.RegionCode == regionCode) // Filtra por RegionCode
                .Select(ubigeo => new ProvinceResponse
                {
                    ProvinceCode = ubigeo.ProvinceCode,
                    RegionCode = ubigeo.RegionCode,
                    ProvinceDescription = ubigeo.ProvinceDescription
                }).ToList();
        }

        public List<UbigeoResponse> FindAllUbigeoResponses(string regionCode, string provinceCode)
        {
            return _context.Ubigeo
                .Where(ubigeo => ubigeo.RegionCode == regionCode && ubigeo.ProvinceCode == provinceCode) // Filtra por RegionCode y ProvinceCode
                .Select(ubigeo => new UbigeoResponse
                {
                    UbigeoCode = ubigeo.UbigeoCode,
                    ProvinceCode = ubigeo.ProvinceCode,
                    RegionCode = ubigeo.RegionCode,
                    UbigeoDescription = ubigeo.UbigeoDescription
                }).ToList();
        }

    }
}
