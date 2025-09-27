using Domain.DTOs;
using Infra.Models;

namespace WebMotors.Service.Interface;

public interface IUserService
{
    Task<User?> CreateOrGetUserAsync(string auth0UserId);
}