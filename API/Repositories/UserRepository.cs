using Subman.Models;
using Subman.Database;
using MongoDB.Driver;

namespace Subman.Repositories;
public class UserRepository : BaseRepository<User> {
    private readonly IMongoCollection<User> _users;

    public UserRepository(MongoDbContext dbContext) : base(dbContext, "users") {
        _users = dbContext.Database.GetCollection<User>("users");
    }

    public async Task<User?> GetByEmailAsync(string email) {
        return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username) {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }
}