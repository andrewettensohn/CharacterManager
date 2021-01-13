using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillsRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Skills> AddSkills(Skills skills)
        {
            await _context.AddAsync(skills);
            await _context.SaveChangesAsync();

            return skills;
        }

        public async Task<Skills> GetCharacterSkills(int id)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.CharacterId == id);
        }
    }
}
