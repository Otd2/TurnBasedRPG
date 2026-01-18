using Character.Base;
using Persistence;

namespace Character.Menu
{
    public class UnitUIModel : UnitModelBase
    {
        private bool _isSelected;

        public bool IsSelected => _isSelected;

        public UnitUIModel(int id, int level, int xp, CharacterAttributes attributes
            , PersistentDataManager persistantDataManager) : 
            base(id, level, xp, attributes, persistantDataManager)
        {
            _isSelected = persistantDataManager.CurrentGameData.selectedHeroes.Contains(Id);
        }

        public void Select()
        {
            if (PersistentDataManager.CurrentGameData.selectedHeroes.Count >= 3)
                return;
            
            _isSelected = true;
            PersistentDataManager.CurrentGameData.selectedHeroes.Add(Id);
        }
        
        public void Deselect()
        {
            _isSelected = false;
            PersistentDataManager.CurrentGameData.selectedHeroes.Remove(Id);
        }
    }
}