using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class LibeyUserRepository : ILibeyUserRepository
    {
        private readonly Context _context;
        public LibeyUserRepository(Context context)
        {
            _context = context;
        }
        public void Create(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Add(libeyUser);
            _context.SaveChanges();
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {

        
            var q = from user in _context.LibeyUsers
                        join ubigeo in _context.Ubigeo
                        on user.UbigeoCode equals ubigeo.UbigeoCode into ubigeoGroup
                        from ubigeo in ubigeoGroup.DefaultIfEmpty()

                        join province in _context.Province
                        on ubigeo.ProvinceCode equals province.ProvinceCode into provinceGroup
                        from province in provinceGroup.DefaultIfEmpty()

                        join region in _context.Region
                        on province.RegionCode equals region.RegionCode into regionGroup
                        from region in regionGroup.DefaultIfEmpty()

                        join document in _context.DocumentType
                        on user.DocumentTypeId equals document.DocumentTypeId into documentGroup
                        from document in documentGroup.DefaultIfEmpty()

                    where user.DocumentNumber == documentNumber
                    select new LibeyUserResponse
                        {
                            DocumentNumber = user.DocumentNumber,
                            Active = user.Active,
                            Address = user.Address,
                            DocumentTypeId = user.DocumentTypeId,
                            Email = user.Email,
                            FathersLastName = user.FathersLastName,
                            MothersLastName = user.MothersLastName,
                            Name = user.Name,
                            Password = user.Password,
                            Phone = user.Phone,

                            // Nuevos campos obtenidos
                            UbigeoCode = user.UbigeoCode,
                            ProvinceCode = ubigeo != null ? ubigeo.ProvinceCode : "N/A",
                            RegionCode = province != null ? province.RegionCode : "N/A"
                        };

            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();
        }

        public List<LibeyUsersResponse> FindAllResponses(string? documentNumber)
        {
            var query = from user in _context.LibeyUsers

                        join ubigeo in _context.Ubigeo
                        on user.UbigeoCode equals ubigeo.UbigeoCode into ubigeoGroup
                        from ubigeo in ubigeoGroup.DefaultIfEmpty()

                        join document in _context.DocumentType
                        on user.DocumentTypeId equals document.DocumentTypeId into documentGroup
                        from document in documentGroup.DefaultIfEmpty()

                        select new LibeyUsersResponse
                        {
                            DocumentNumber = user.DocumentNumber,
                            Active = user.Active,
                            Address = user.Address,
                            DocumentTypeDescription = document != null ? document.DocumentTypeDescription : "N/A",
                            Email = user.Email,
                            FathersLastName = user.FathersLastName,
                            MothersLastName = user.MothersLastName,
                            Name = user.Name,
                            Password = user.Password,
                            Phone = user.Phone,

                            // Nuevos campos obtenidos
                            UbigeoDescription = ubigeo.UbigeoDescription
                        };

            if (!string.IsNullOrEmpty(documentNumber))
            {
                query = query.Where(u => u.DocumentNumber.Contains(documentNumber)); // LIKE '%documentNumber%'
            }

            return query.ToList();
        }

        public bool Update(string documentNumber,LibeyUser libeyUser)
        {
            //var user = _context.LibeyUsers.AsNoTracking().FirstOrDefault(x => x.DocumentNumber == libeyUser.DocumentNumber);

            //if (user == null)
            //{
            //    return false; // Usuario no encontrado
            //}

            //_context.LibeyUsers.Update(libeyUser);
            //_context.SaveChanges();
            //return true;

            var user = _context.LibeyUsers.AsNoTracking().FirstOrDefault(x => x.DocumentNumber == documentNumber);

            if (user == null)
            {
                return false; // Usuario no encontrado
            }
            //_context.LibeyUsers.Update(libeyUser);
            // Si el número de documento cambió, se elimina el usuario viejo y se inserta uno nuevo
            if (documentNumber != libeyUser.DocumentNumber)
            {
                _context.LibeyUsers.Remove(user);
                _context.SaveChanges();

                _context.LibeyUsers.Add(libeyUser);
            }
            else
            {
                _context.LibeyUsers.Update(libeyUser);
            }

            _context.SaveChanges();
            return true;

        }

        public bool Remove(string documentNumber)
        {
            var user = _context.LibeyUsers.AsNoTracking().FirstOrDefault(x => x.DocumentNumber == documentNumber);

            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            _context.LibeyUsers.Remove(user);
            _context.SaveChanges();
            return true;
        }

    }
}