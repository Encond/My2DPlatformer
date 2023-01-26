using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _healthMax;
        // [SerializeField] private int _damage = 15;

        private int _currentHealth;

        // public int Damage => _damage;
        public bool IsAlive => _currentHealth >= 1;

        // private void OnDestroy() => Destroy(gameObject);

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

        // public event UnityAction OnPlayerInChaseSight;
        //
        // private void OnTriggerEnter2D(Collider2D col)
        // {
        //     if (col.TryGetComponent(out Player.Player player))
        //     {
        //         OnPlayerInChaseSight?.Invoke();
        //     }
        // }
        //
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (other.TryGetComponent(out Player.Player player))
        //     {
        //         OnPlayerInChaseSight?.Invoke();
        //     }
        // }

        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.collider.CompareTag("Player"))
        //     {
        //         OnCollidingPlayer?.Invoke();
        //     }
        // }
    }
}