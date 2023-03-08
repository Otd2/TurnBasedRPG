using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterAttribute", menuName = "Character", order = 0)]
    public class CharacterAttributesSo : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private CharacterAttributes attributes;

        public int ID => id;
        public CharacterAttributes Attributes => attributes;
    }
}