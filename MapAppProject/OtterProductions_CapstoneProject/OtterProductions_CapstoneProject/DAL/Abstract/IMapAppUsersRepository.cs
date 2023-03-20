using OtterProductions_CapstoneProject.DAL.Abstract;
using OtterProductions_CapstoneProject.Models;

namespace OtterProductions_CapstoneProject.DAL.Abstract
{
    public interface IMapAppUsersRepository : IRepository<MapAppUser>
    {
        IQueryable<MapAppUser> ListMapAppUsers();
    }
}