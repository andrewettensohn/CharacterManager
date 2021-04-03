using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Repositories
{
    public class TalentRepository : ITalentRepository
    {
        private readonly ApplicationDbContext _context;

        public TalentRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Character> AddExistingTalentToCharacter(Character character, Talent talent)
        {
            await CreateLink(character.CharacterId, talent.TalentId);
            //character.XP -= talent.XPCost;
            return character;
        }

        public async Task AddNewTalent(Talent talent)
        {
            await _context.AddAsync(talent);
            await _context.SaveChangesAsync();
        }

        public async Task<Character> AddTalent(Character character, Talent talent)
        {
            await _context.AddAsync(talent);
            await _context.SaveChangesAsync();

            await CreateLink(character.CharacterId, talent.TalentId);
            character.XP -= talent.XPCost;

            return character;
        }

        public async Task<List<Talent>> GetTalents()
        {
            return await _context.Talent.ToListAsync();
        }

        public async Task<List<Talent>> GetTalentsForCharacter(int characterId)
        {
            List<Talent> talents = new List<Talent>();
            List<TalentLink> talentLinks = await _context.TalentLink.Where(x => x.CharacterId == characterId).ToListAsync();
            
            if(!talentLinks.Any())
            {
                return talents;
            }

            foreach(TalentLink link in talentLinks)
            {
                Talent linkedTalent = await _context.Talent.FirstOrDefaultAsync(x => x.TalentId == link.TalentId);

                if(linkedTalent != null)
                {
                    talents.Add(linkedTalent);
                }
            }

            return talents;
        }

        public async Task RemoveTalent(Talent talent)
        {
            List<TalentLink> links = await _context.TalentLink.Where(x => x.TalentId == talent.TalentId).ToListAsync();

            _context.RemoveRange(links);
            _context.Remove(talent);
            await _context.SaveChangesAsync();
        }

        public async Task<Character> RemoveTalentFromCharacter(Character character, Talent talent)
        {
            character.Talents.Remove(talent);
            TalentLink link = await _context.TalentLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            await RemoveExistingLink(link);

            return character;
        }

        public async Task<Talent> UpdateTalent(Talent talent)
        {
            _context.Entry(talent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return talent;
        }

        public async Task UpdateTalents(List<Talent> talents)
        {
            _context.UpdateRange(talents);
            await _context.SaveChangesAsync();
        }

        private async Task<TalentLink> CreateLink(int characterId, int talentId)
        {
            TalentLink link = new TalentLink
            {
                TalentId = talentId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingLink(TalentLink link)
        { 
            _context.TalentLink.Remove(link);
            await _context.SaveChangesAsync();
        }
    }
}
