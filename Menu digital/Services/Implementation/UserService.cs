namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}
