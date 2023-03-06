using DefaultNamespace.Health;

namespace Character
{
    public class UnitUIModel : UnitModelBase
    {
        public bool _isUnlocked;
        public bool _isSelected;

        public UnitUIModel(int id, int level, int xp, CharacterAttributes attributes, bool isUnlocked, bool isSelected) : 
            base(id, level, xp, attributes)
        {
            
            hp = new Health(level, attributes.BaseHealth);
            _isUnlocked = isUnlocked;
            _isSelected = isSelected;
        }
    }
}