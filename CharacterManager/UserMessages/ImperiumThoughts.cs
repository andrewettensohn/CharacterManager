using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.UserMessages
{
    public static class ImperiumThoughts
    {
        private static List<string> Thoughts = new List<string> 
        {
            "A broad mind lacks focus.",
            "A coward always seeks compromise",
            "A moment of laxity spawns a lifetime of heresy.",
            "A small mind is easily filled with faith.",
            "Blessed is the mind too small for doubt",
            "To admit defeat is to blaspheme against the Emperor.",
            "Only in death does duty end.",
            "Fear denies faith.",
            "To Question is to doubt.",
            "Truth is Subjective.",
            "It is better to die for the Emperor than to live for yourself.",
            "An open mind is like a fortress with its gates unbarred and unguarded.",
            "A suspicious mind is a healthy mind.",
            "Cowards die in shame.",
            "Faith without deeds is worthless.",
            "It is better to die for the Emperor than to live for yourself.",
            "Life is the Emperor's currency, spend it well",
        };

        private static Random rnd = new Random();

        public static string GetRandomThought()
        {
            int randomNum = rnd.Next(Thoughts.Count);
            return Thoughts[randomNum];
        }
    }
}
