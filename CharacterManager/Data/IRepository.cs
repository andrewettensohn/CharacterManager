using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data
{
    public interface IRepository
    {
        #region

        #endregion

        #region Character

        public Task<List<Character>> ListCharacters();

        public Task AddCharacter(Character character);

        public Task UpdateCharacter(Character character);

        public Task DeleteCharacter(Character character);

        #endregion

        #region CharacterAction

        public Task<List<CharacterAction>> ListCharacterActions();

        public Task AddCharacterAction(CharacterAction characterAction);

        public Task UpdateCharacterAction(CharacterAction characterAction);

        public Task DeleteCharacterAction(CharacterAction characterAction);

        #endregion

        #region CharacterClass

        public Task<List<CharacterClass>> ListCharacterClasses();

        public Task AddCharacterClass(CharacterClass characterClass);

        public Task UpdateCharacterClass(CharacterClass characterClass);

        public Task DeleteCharacterClass(CharacterClass characterClass);

        #endregion

        #region Race

        public Task<List<Race>> ListRaces();

        public Task AddRace(Race characterClass);

        public Task UpdateRace(Race characterClass);

        public Task DeleteRace(Race characterClass);

        #endregion
    }
}
