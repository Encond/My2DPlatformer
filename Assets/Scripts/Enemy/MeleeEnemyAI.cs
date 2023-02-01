using Enemy.Attacks;
using Player;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Enemy))]
    public class MeleeEnemyAI : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private Enemy _enemy;
        
        [Header("Health Manager")]
        [SerializeField] private HealthManager _playerHealthManager;

        [Header("Attacks")]
        [SerializeField] private MeleeSimpleAttack _meleeSimpleAttack;

        private Rigidbody2D _enemyRigidbody2D;

        private void Start() => _enemyRigidbody2D = _enemy.GetComponent<Rigidbody2D>();

        private void SimpleAttackTrigger()
        {
            _enemyRigidbody2D.simulated = false;
            
            if (_playerHealthManager != null)
                _playerHealthManager?.TakeDamage(_meleeSimpleAttack.GetDamage());

            _enemyRigidbody2D.simulated = true;
        }
    }
}