using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        LibeyUserResponse FindResponse(string documentNumber);
        void Create(UserUpdateorCreateCommand command);
        List<LibeyUsersResponse> FindAllResponse(string? documentNumber);
        bool Update(string documentNumber, UserUpdateorCreateCommand command);
        bool Remove(string documentNumber);
    }
}