using UnityEngine;

namespace Character
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public void PlayAnimation(string animationText)
        {
            _animator.Play(animationText);
        }
    }
}