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

        private void SimpleAttackTrigger()
        {
            if (_playerHealthManager != null)
                _playerHealthManager?.TakeDamage(_meleeSimpleAttack.GetDamage());
        }
    }
}