using Events.Interfaces;

namespace Events
{
    public class CharacterSelectionEvent : IEvent
    {
        public int CharacterId { get; }

        public CharacterSelectionEvent(int characterId)
        {
            CharacterId = characterId;
        }
    }
}
