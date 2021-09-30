using Microsoft.EntityFrameworkCore;
using Models;
using Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IUbicacionServicio
    {
        Task<List<Ubicacion>> Get();

        Task<Ubicacion> Get(int id);
    }

    public class UbicacionServicio: IUbicacionServicio
    {
        private ApplicationDbContext _context;

        public UbicacionServicio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ubicacion>> Get()
        {
            return await _context.Ubicacions.ToListAsync();
        }

        public async Task<Ubicacion> Get(int id)
        {
            return await _context.Ubicacions.Where(x => x.Id == id).SingleOrDefaultAsync();
        }
    }
}
