using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
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

        public async Task AddRace(Race race)
        {
            await _context.AddAsync(race);
            await _context.AddAsync(race.RaceModifer);

            StatLink link = new StatLink { RaceId = race.RaceId, StatId = race.RaceModifer.StatId };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();
        }

        public Task DeleteRace(Race race)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Race>> ListRaces()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> RaceLookup(string raceName)
        {
            return await _context.Races.FirstOrDefaultAsync(x => x.Name.Contains(raceName));
        }

        public Task UpdateRace(Race race)
        {
            throw new NotImplementedException();
        }
    }
}
