using CharacterManager.DAC.Data.Contracts;
using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Repositories
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly ApplicationDbContext _context;

        public WeaponRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Character> AddExistingWeaponToCharacter(Character character, Weapon weapon)
        {
            await CreateLink(character.CharacterId, weapon.WeaponId);
            return character;
        }

        public async Task AddNewWeapon(Weapon weapon)
        {
            await _context.AddAsync(weapon);
            await _context.SaveChangesAsync();
        }

        public async Task<Character> AddWeapon(Character character, Weapon weapon)
        {
            await _context.AddAsync(weapon);
            await _context.SaveChangesAsync();

            await CreateLink(character.CharacterId, weapon.WeaponId);

            return character;
        }

        public async Task<List<Weapon>> GetWeapons()
        {
            return await _context.Weapon.ToListAsync();
        }

        public async Task<List<Weapon>> GetWeaponsForCharacter(Guid characterId)
        {
            List<Weapon> weapons = new List<Weapon>();
            List<WeaponLink> weaponLinks = await _context.WeaponLink.Where(x => x.CharacterId == characterId).ToListAsync();

            if (!weaponLinks.Any())
            {
                return weapons;
            }

            foreach (WeaponLink link in weaponLinks)
            {
                Weapon linkedWeapon = await _context.Weapon.FirstOrDefaultAsync(x => x.WeaponId == link.WeaponId);

                if (linkedWeapon != null)
                {
                    weapons.Add(linkedWeapon);
                }
            }

            return weapons;
        }

        public async Task<Character> RemoveWeaponFromCharacter(Character character, Weapon weapon)
        {
            character.Weapons.Remove(weapon);
            WeaponLink link = await _context.WeaponLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            await RemoveExistingLink(link);

            return character;
        }

        public async Task UpdateWeapon(Weapon weapon)
        {
            _context.Update(weapon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWeapons(List<Weapon> weapons)
        {
            _context.UpdateRange(weapons);
            await _context.SaveChangesAsync();
        }

        private async Task<WeaponLink> CreateLink(Guid characterId, Guid weaponId)
        {
            WeaponLink link = new WeaponLink
            {
                WeaponId = weaponId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingLink(WeaponLink link)
        {
            _context.WeaponLink.Remove(link);
            await _context.SaveChangesAsync();
        }
    }
}
