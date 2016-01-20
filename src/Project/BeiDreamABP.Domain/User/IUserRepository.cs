using System.Collections.Generic;
using Abp.Domain.Repositories;

namespace BeiDreamAbp.Domain.User
{
    public interface IUserRepository: IRepository<User, long>
    {
        List<User> GetUsers(string name);   
    }
}