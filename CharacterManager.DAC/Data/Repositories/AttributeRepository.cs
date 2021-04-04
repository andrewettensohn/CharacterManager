using CharacterManager.DAC.Data.Contracts;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly ApplicationDbContext _context;

        public AttributeRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Attributes> AddAttributes(Attributes attributes)
        {
            await _context.AddAsync(attributes);
            await _context.SaveChangesAsync();

            return attributes;
        }

        public async Task<Attributes> GetCharacterAttributes(Guid characterId)
        {
            return await _context.Attributes.FirstOrDefaultAsync(x => x.CharacterId == characterId);
        }
    }
}
