using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private int _healthMax;
        
        private int _currentHealth;
        
        public bool IsAlive => _currentHealth >= 1;
        
        private void Start() => _currentHealth = _healthMax;

        public int GetCurrentHealth() => _currentHealth;

        public void SetCurrentHealth(int damageAmount)
        {
            int tempHealth = GetCurrentHealth() - damageAmount;

            if (_currentHealth != tempHealth && tempHealth <= _healthMax && tempHealth > 0)
                _currentHealth = tempHealth;
            else if (tempHealth <= 0)
                _currentHealth = 0;
            else if (_currentHealth >= _healthMax)
                _currentHealth = _healthMax;
        }
    }
}