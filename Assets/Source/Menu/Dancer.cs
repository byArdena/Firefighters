using UnityEngine;

namespace Menu
{
    [RequireComponent(typeof(Animator))]
    public class Dancer : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.Play(0);
        }
    }
}