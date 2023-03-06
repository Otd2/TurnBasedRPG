namespace Character
{
    public class CharacterSelectionEventManager
    {
        public delegate void OnCharacterSelectionEvent(int id);
        public event OnCharacterSelectionEvent OnCharacterSelection;

        public void OnCharacterSelected(int id)
        {
            OnCharacterSelection?.Invoke(id);
        }
        
        public delegate void OnCharacterDeselectedEvent(int id);
        public event OnCharacterDeselectedEvent OnCharacterDeselect;

        public void OnCharacterDeselected(int id)
        {
            OnCharacterDeselect?.Invoke(id);
        }
    }
}