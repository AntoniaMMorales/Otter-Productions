using OtterProductions_CapstoneProject.DAL.Abstract;
using OtterProductions_CapstoneProject.Models;
using OtterProductions_CapstoneProject.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OtterProductions_CapstoneProject.DAL.Concrete
{
    public class MapAppUsersRepository : Repository<MapAppUser>, IMapAppUsersRepository
    {
        private readonly MapAppDbContext _context;
        public MapAppUsersRepository(MapAppDbContext ctx) : base(ctx)
        {
            _context = ctx;
        }

        public IQueryable<MapAppUser> ListMapAppUsers()
        {
            var mapAppUserList = GetAll();

            return mapAppUserList;
        }
    }
}