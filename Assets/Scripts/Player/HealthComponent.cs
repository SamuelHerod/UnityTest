using HUD;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class HealthComponent : MonoBehaviour
    {
        private Animator _animator;
        
        [SerializeField] private float playerHealth = 100f;
        
        [SerializeField]private bool _isDead;
        
        private static readonly int IsDead = Animator.StringToHash("isDead");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (_isDead)
            {
                KillPlayer();
            }
        }
        
        void Damage(float damageValue)
        {
            playerHealth -= damageValue;
            CheckIsDead();
        }

        void Heal(float healValue)
        {
            playerHealth += healValue;
        }

        private void CheckIsDead()
        {
            if (playerHealth <= 0)
            {
                _isDead = true;
            }
        }
        
        private void KillPlayer()
        { 
            _animator.SetBool(IsDead, true);
        }
    }
}