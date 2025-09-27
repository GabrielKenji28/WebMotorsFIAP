using Domain.DTOs;
using Infra.DataContext;
using Infra.Models;
using Infra.Repository;
using Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebMotors.Service.Interface;

namespace Domain.Service;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepository;

    public UserService(AppDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByAuth0IdAsync(string auth0UserId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.AuthUserId == auth0UserId);
    }
    
    // Método para criar um novo usuário localmente se ele não existir
    public async Task<User?> CreateOrGetUserAsync(string auth0UserId)
    {
        var user = await _userRepository.GetByAuth0UserId(auth0UserId);
        
        if (user != null)
        {
            return user;
        }
        
        user = ToUser(auth0UserId);
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        user.IsActive = true;
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    private static User ToUser(string auth0UserId)
    {
        return new User()
        {
            AuthUserId = auth0UserId,

        };
    }
}