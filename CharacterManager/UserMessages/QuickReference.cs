using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.UserMessages
{
    public class QuickReference
    {
        public static Dictionary<string, string> Actions = new Dictionary<string, string>
        {
            {"Combat Action", "Make an attack, use a Skill." },
            {"Movement", "Move up to your Speed in metres." },
            {"Simple Action", "Reload a gun, draw a sword, kick open a door, look around." },
            {"Free Action", "Roll Determination, shout a warning." },
            {"Reflexive Action", "React to something." },
        };

        public static Dictionary<string, string> AdvancedActions = new Dictionary<string, string>
        {
            {"Full-Round Action", "Sacrifice all your Actions and Movement to Charge, Sprint, or use Full Defence." },
            {"Multi-Action", "Declare all Actions you want to take. +2 DN to all Tests for every Action you take." },
            {"Multi-Attack", "+2 DN to all attacks for each attack beyond the first. Roll damage once and apply it to all targets hit." },
        };

        public static Dictionary<string, string> Attacks = new Dictionary<string, string>
        {
            {"Melee Attack", "Weapon Skill (I) Test against target’s Defence. Strength + weapon damage for total damage." },
            {"Ranged Attack", "Ballistic Skill (A) Test against target’s Defence. Check Range for modifiers." },
            {"Interaction Attack", "Roll a Skill against target’s Skill or Resolve. If you succeed, they are Vulnerable or Hindered." },
        };

        public static Dictionary<string, string> Movement = new Dictionary<string, string>
        {
            {"Run", "Use Simple Action and Movement. Move double your Speed in metres." },
            {"Sprint", "Full-Round Action. Move triple your Speed in metres." },
            {"Crawl", "Simple Action to go Prone. Move at half Speed." },
            {"Cover", "+1 Defence if less than half of you is covered. +2 Defence if more than half of you is covered." },
        };

        public static Dictionary<string, string> DamageDefence = new Dictionary<string, string>
        {
            {"Determination", "Roll your Determination. Every Icon converts 1 Wound to 1 Shock." },
            {"Dying", "You are Prone, and can only Crawl, Fall Back, or take a basic Combat Action. Whenever you would take any number of Wounds, you take a Traumatic Injury instead." },
            {"Full Defence", "Full Round Action. Roll your Initiative dice pool; every Icon increases your Defence by +1 until end of yout next Turn." },
            {"Wounded", "If you have any Wounds, +1 DN to all Tests." },
            {"Wounds", "If an attack does more damage than your Resilience, you suffer the difference in Wounds. If you suffer more Wounds than your Max Wounds, you are Dying." },
        };

        public static Dictionary<string, string> Melee = new Dictionary<string, string>
        {
            {"All-Out Attack", "+2 bonus dice to all melee attacks. -2 Defence until the start of your next Turn." },
            {"Charge", "Full-Round Action to Run and make a melee attack with +1 bonus dice to the attack Test." },
            {"Fall Back", "Combat Action to stop an enemy using a Reflexive Attack." },
            {"Grapple", "Opposed Strength Test with an Engaged target. If you succeed, they are Restrained." },
            {"Pistols in Melee", "Target gains +2 Defence." },
            {"Unarmed", "Strength + 1 ED damage." },
        };

        public static Dictionary<string, string> Ranged = new Dictionary<string, string>
        {
            {"Short Range", "+ 1 bonus dice to ranged attack Tests." },
            {"Long Range", "Target gains +2 Defence." },
            {"Aim", "+ 1 bonus dice to ranged attack Tests. You can’t move." },
            {"Brace", "Ignore the Heavy weapon penalty. You can’t move." },
            {"Called Shot", "+ 1 ED for every + 1 you add to target’s Defence." },
            {"Disarming Shot", "Target makes a Strength Test, DN = half the damage from your ranged attack Test. If they fail, they drop their weapon." },
            {"Firing into Melee", "If you roll a Complication, shot hits a random unintended target." },
            {"Grenades & AOE", "DN 3 Ballistic Skill Test to hit a point in range. All targets in Blast are hit." },
            {"Reloading", "Use a Simple Action and spend 1 Ammo to Reload your weapon." },
            {"Salvo Options", "Improve ranged attack Tests. Weapon must be Reloaded after use." },
            {"Scattering", "If an AOE Ballistic Skill Test fails, roll 1d6 x2 for distance and 1d6 for direction." },
            {"Shoot through Cover", "Add Cover bonus to target’s Resilience." },
            {"Pinning Attack", "Roll Ballistic Skill against target’s Resolve to inflict Pinned. Weapon must be Reloaded after use." },
        };

        public static Dictionary<string, string> Situations = new Dictionary<string, string>
        {
            {"Difficult Terrain", "Your Speed is halved." },
            {"Dodging AOE", "Use Full Round Defence to raise your Resilience; you lose your next Turn." },
            {"Engaged", "Within range of an enemy’s melee weapon." },
            {"Seize the Initiative", "Spend 1 Glory to act before the GM." },
            {"Surprise Attacks", "If the target doesn’t know you’re there, +2 bonus dice to the attack Test and + 2 ED" },
            {"Reflexive Attack", "If an enemy leaves Engagement without Fall Back, use Reflexive Action to attack." },
        };
    }
}
