using Microsoft.EntityFrameworkCore;
using Models;
using Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IContactoService
    {
        Task<bool> Create(Contacto model);
        Task<List<Contacto>> Get();
    }

    public class ContactoService : IContactoService
    {
        private ApplicationDbContext _context;

        public ContactoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Contacto model)
        {
            try
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Contacto>> Get() 
        {
            return await _context.Contactos.ToListAsync();
        }
    }
}
