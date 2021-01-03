namespace CharacterManager.Models.CharacterLinks
{
    public class CharacterActionLink
    {
        public int CharacterActionLinkId { get; set; }

        public int CharacterActionId { get; set; }

        public int? CharacterId { get; set; }

        public int? CharacterClassId { get; set; }
    }
}
