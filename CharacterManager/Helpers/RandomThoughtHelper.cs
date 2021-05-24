using CharacterManager.UserMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Helpers
{
    public static class RandomThoughtHelper
    {
        private static Random rnd = new Random();

        public static string GetRandomThought()
        {
            int randomNum = rnd.Next(ImperiumThoughts.Thoughts.Count);
            return ImperiumThoughts.Thoughts[randomNum];
        }
    }
}
