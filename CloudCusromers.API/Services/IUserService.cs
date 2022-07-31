using CloudCusromers.API.Models;

namespace CloudCusromers.API.Services
{
    public interface IUserService
    {
        public  Task<List<User>> GetAllUsers();
    }
}
