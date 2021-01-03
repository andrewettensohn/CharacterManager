using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public Task AddRace(Race characterClass)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRace(Race characterClass)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Race>> ListRaces()
        {
            return await _context.Races.ToListAsync();
        }

        public Task UpdateRace(Race characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
