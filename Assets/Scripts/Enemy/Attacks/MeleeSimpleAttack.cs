using Enemy.AnimationsManager;
using UnityEngine;

namespace Enemy.Attacks
{
    public class MeleeSimpleAttack : Attack
    {
        [Header("Player properties")]
        [SerializeField] private LayerMask _playerLayer;

        [Header("Enemy properties")]
        [SerializeField] private Animator _enemyAnimator;
        
        private Enemy _enemy;
        private CapsuleCollider2D _collider;
        private Vector3 _rayDirection;
        private float _cooldownTimer = Mathf.Infinity;

        private void Start()
        {
            _enemy = GetComponentInParent<Enemy>();
            _collider = _enemy.GetComponent<CapsuleCollider2D>();
            _rayDirection = transform.right;
        }

        private void Update()
        {
            if (!_enemy.IsAlive) return;
            
            _cooldownTimer += Time.deltaTime;

            if (PlayerInSight())
                if (_cooldownTimer >= base._attackCooldown)
                {
                    Attack();
                    ResetCooldownTimer();
                }
        }

        public int GetDamage() => _damage;

        private void ResetCooldownTimer() => _cooldownTimer = 0;

        private float GetDirection() => _enemy.transform.localScale.x < 0 ? -1 : 1;
        
        private void Attack() => _enemyAnimator.SetTrigger(EnemyAnimations.GetSimpleAttack());

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                _collider.bounds.center + _rayDirection * (_rangeX * GetDirection() * base._colliderDistance),
                new Vector2(_rayDirection.x * _rangeX + _rangeOffsetX, _collider.bounds.size.y + _rangeOffsetY));
        }

        private bool PlayerInSight()
        {
            if (_collider is not null)
            {
                RaycastHit2D hit = Physics2D.BoxCast(
                    _collider.bounds.center + _rayDirection * (_rangeX * GetDirection() * base._colliderDistance),
                    new Vector2(_rayDirection.x * _rangeX + _rangeOffsetX, _collider.bounds.size.y + _rangeOffsetY),
                    0, Vector2.left, 0, _playerLayer);

                if (hit.collider is not null)
                    return true;
            }

            return false;
        }
    }
}