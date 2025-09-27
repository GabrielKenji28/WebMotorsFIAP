using Infra.DataContext;
using Infra.Models;
using Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByAuth0UserId(string auth0UserId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.AuthUserId == auth0UserId);
    }
    
    
}