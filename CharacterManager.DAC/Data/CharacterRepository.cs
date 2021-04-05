using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
using CharacterManager.Sync.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Character> GetCharacter(Guid id)
        {
            return await _context.Character.FirstOrDefaultAsync(x => x.CharacterId == id);
        }

        public async Task<List<Character>> ListCharacters()
        {
            return await _context.Character.ToListAsync();
        }

        public async Task<Character> NewCharacter(Character character)
        {
            await _context.AddAsync(character);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(NewCharacter), character.CharacterId);

            return character;
        }

        public async Task UpdateCharacter(Character character)
        {

            if (character.XP == 0 && character.Name == "New Character")
            {
                character = ModifyXPForTier(character);
            }

            _context.Update(character);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(NewCharacter), character.CharacterId);
        }

        private static Character ModifyXPForTier(Character character)
        {
            if (character.Tier == 1)
            {
                character.XP = 100;
            }
            else if (character.Tier == 2)
            {
                character.XP = 200;
            }
            else if (character.Tier == 3)
            {
                character.XP = 300;
            }

            return character;
        }

        public async Task AddNewArchetype(Archetype archetype)
        {
            await _context.AddAsync(archetype);
            await _context.SaveChangesAsync();
        }

        public async Task<Archetype> GetArchetypeForCharacter(Guid characterId)
        {
            ArchetypeLink link = await _context.ArchetypeLink.FirstOrDefaultAsync(x => x.CharacterId == characterId);

            if (link is null)
            {
                return null;
            }

            return await _context.Archetype.FirstOrDefaultAsync(x => x.ArchetypeId == link.ArchetypeId);
        }

        public async Task<List<Archetype>> GetArchetypes()
        {
            return await _context.Archetype.ToListAsync();
        }

        public async Task<Character> UpdateArchetype(Character character)
        {

            ArchetypeLink link = await _context.ArchetypeLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            if (link != null)
            {
                await ResetCharacterXP(link, character);
                await RemoveExistingArchetypeLink(character.CharacterId);
            }

            UpdateCharacterXPForNewArchetype(character);

            await CreateLink(character.CharacterId, character.Archetype.ArchetypeId);

            _context.Entry(character.Archetype).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return character;
        }

        private static Character UpdateCharacterXPForNewArchetype(Character character)
        {

            if (character.Archetype.AttributeBonus > 0)
            {
                character.XP += character.Archetype.AttributeBonus * 4;
            }

            if (character.Archetype.SkillBonus > 0)
            {
                character.XP += character.Archetype.SkillBonus * 2;
            }

            character.XP -= character.Archetype.XPCost;

            return character;
        }

        private async Task<Character> ResetCharacterXP(ArchetypeLink link, Character character)
        {
            Archetype oldArchetype = await _context.Archetype.FirstOrDefaultAsync(x => x.ArchetypeId == link.ArchetypeId);

            if (oldArchetype.AttributeBonus > 0)
            {
                character.XP -= oldArchetype.AttributeBonus * 4;
            }

            if (oldArchetype.SkillBonus > 0)
            {
                character.XP -= oldArchetype.SkillBonus * 2;
            }

            character.XP += oldArchetype.XPCost;

            return character;
        }


        private async Task<ArchetypeLink> CreateLink(Guid characterId, Guid archetypeId)
        {
            ArchetypeLink link = new ArchetypeLink
            {
                ArchetypeId = archetypeId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingArchetypeLink(Guid characterId)
        {

            ArchetypeLink link = _context.ArchetypeLink.FirstOrDefault(x => x.CharacterId == characterId);

            _context.ArchetypeLink.Remove(link);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewArmor(Armor armor)
        {
            await _context.AddAsync(armor);
            await _context.SaveChangesAsync();
        }

        public async Task<Armor> AddArmor(Character character)
        {
            await _context.AddAsync(character.Armor);
            await _context.SaveChangesAsync();

            ArmorLink link = await _context.ArmorLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            if (link != null)
            {
                await RemoveExistingArmorLink(character.CharacterId);
            }

            await CreateLink(character.CharacterId, character.Archetype.ArchetypeId);

            return character.Armor;
        }

        public async Task<Armor> GetArmorForCharacter(Guid characterId)
        {
            ArmorLink link = await _context.ArmorLink.FirstOrDefaultAsync(x => x.CharacterId == characterId);

            if (link is null)
            {
                return null;
            }

            return await _context.Armor.FirstOrDefaultAsync(x => x.ArmorId == link.ArmorId);
        }

        public async Task<List<Armor>> GetArmorList()
        {
            return await _context.Armor.ToListAsync();
        }

        public async Task UpdateArmor(Character character)
        {

            ArmorLink link = _context.ArmorLink.FirstOrDefault(x => x.CharacterId == character.CharacterId);

            if (link != null)
            {
                await RemoveExistingArmorLink(character.CharacterId);
            }

            await CreateLink(character.CharacterId, character.Armor.ArmorId);

            _context.Entry(character.Armor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task<ArmorLink> CreateArmorLink(Guid characterId, Guid armorId)
        {
            ArmorLink link = new ArmorLink
            {
                ArmorId = armorId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingArmorLink(Guid characterId)
        {

            ArmorLink link = _context.ArmorLink.FirstOrDefault(x => x.CharacterId == characterId);

            _context.ArmorLink.Remove(link);
            await _context.SaveChangesAsync();
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

        public async Task<Character> AddExistingGearToCharacter(Character character, Gear Gear)
        {
            await CreateLink(character.CharacterId, Gear.GearId);
            return character;
        }

        public async Task AddNewGear(Gear gear)
        {
            await _context.AddAsync(gear);
            await _context.SaveChangesAsync();
        }

        public async Task<Character> AddGear(Character character, Gear Gear)
        {
            await _context.AddAsync(Gear);
            await _context.SaveChangesAsync();

            await CreateLink(character.CharacterId, Gear.GearId);

            return character;
        }

        public async Task<List<Gear>> GetGearList()
        {
            return await _context.Gear.ToListAsync();
        }

        public async Task<List<Gear>> GetGearListForCharacter(Guid characterId)
        {
            List<Gear> Gears = new List<Gear>();
            List<GearLink> GearLinks = await _context.GearLink.Where(x => x.CharacterId == characterId).ToListAsync();

            if (!GearLinks.Any())
            {
                return Gears;
            }

            foreach (GearLink link in GearLinks)
            {
                Gear linkedGear = await _context.Gear.FirstOrDefaultAsync(x => x.GearId == link.GearId);

                if (linkedGear != null)
                {
                    Gears.Add(linkedGear);
                }
            }

            return Gears;
        }

        public async Task<Character> RemoveGearFromCharacter(Character character, Gear Gear)
        {
            character.Gear.Remove(Gear);
            GearLink link = await _context.GearLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            await RemoveExistingGearLink(link);

            return character;
        }

        public async Task UpdateGearList(List<Gear> Gears)
        {
            _context.UpdateRange(Gears);
            await _context.SaveChangesAsync();
        }

        private async Task<GearLink> CreateGearLink(Guid characterId, Guid GearId)
        {
            GearLink link = new GearLink
            {
                GearId = GearId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingGearLink(GearLink link)
        {
            _context.GearLink.Remove(link);
            await _context.SaveChangesAsync();
        }

        public async Task<Skills> AddSkills(Skills skills)
        {
            await _context.AddAsync(skills);
            await _context.SaveChangesAsync();

            return skills;
        }

        public async Task<Skills> GetCharacterSkills(Guid id)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.CharacterId == id);
        }

        public async Task<Character> AddExistingTalentToCharacter(Character character, Talent talent)
        {
            await CreateLink(character.CharacterId, talent.TalentId);
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

        public async Task<List<Talent>> GetTalentsForCharacter(Guid characterId)
        {
            List<Talent> talents = new List<Talent>();
            List<TalentLink> talentLinks = await _context.TalentLink.Where(x => x.CharacterId == characterId).ToListAsync();

            if (!talentLinks.Any())
            {
                return talents;
            }

            foreach (TalentLink link in talentLinks)
            {
                Talent linkedTalent = await _context.Talent.FirstOrDefaultAsync(x => x.TalentId == link.TalentId);

                if (linkedTalent != null)
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

            await RemoveExistingTalentLink(link);

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

        private async Task<TalentLink> CreateTalentLink(Guid characterId, Guid talentId)
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

        private async Task RemoveExistingTalentLink(TalentLink link)
        {
            _context.TalentLink.Remove(link);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewTransaction(string sourceRepo, string methodName, Guid SourceId)
        {
            Transaction transaction = new Transaction
            {
                SourceRepository = sourceRepo,
                SourceMethod = methodName,
                SourceId = SourceId,
                DateTime = DateTime.Now
            };
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransactionList(List<Transaction> transactions)
        {
            await _context.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();
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

            await RemoveExistingWeaponLink(link);

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

        private async Task<WeaponLink> CreateWeaponLink(Guid characterId, Guid weaponId)
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

        private async Task RemoveExistingWeaponLink(WeaponLink link)
        {
            _context.WeaponLink.Remove(link);
            await _context.SaveChangesAsync();
        }

    }
}
