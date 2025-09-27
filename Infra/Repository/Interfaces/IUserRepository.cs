using Infra.Models;

namespace Infra.Repository.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByAuth0UserId(string auth0UserId);
}