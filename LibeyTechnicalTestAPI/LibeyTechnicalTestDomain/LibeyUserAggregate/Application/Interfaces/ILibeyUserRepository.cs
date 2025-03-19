using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        LibeyUserResponse FindResponse(string documentNumber);
        void Create(LibeyUser libeyUser);
        List<LibeyUsersResponse> FindAllResponses(string? documentNumber);
        bool Update(string documentNumber, LibeyUser libeyUser);
        bool Remove(string documentNumber);
    }
}
