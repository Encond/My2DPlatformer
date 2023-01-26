using UnityEngine;

namespace Enemy.Attacks
{
    public class MeleeSimpleAttack : Attack
    {
        [SerializeField] private LayerMask _playerLayer;

        private Enemy _enemy;
        private Animator _enemyAnimator;
        private CapsuleCollider2D _collider;
        private Vector3 _rayDirection;
        private float _cooldownTimer = Mathf.Infinity;

        public int GetDamage() => _damage;
        
        private void Awake() => _enemy = GetComponentInParent<Enemy>();

        private void Start()
        {
            _collider = _enemy.GetComponent<CapsuleCollider2D>();
            _enemyAnimator = _enemy.GetComponent<Animator>();
            _rayDirection = transform.right;
        }

        private void Update()
        {
            if (_enemy.IsAlive)
            {
                _cooldownTimer += Time.deltaTime;

                if (PlayerInSight())
                    if (_cooldownTimer >= base._attackCooldown)
                    {
                        Attack();
                        ResetCooldownTimer();
                    }
            }
            else
            {
                if (_attackCooldown != 0)
                    _attackCooldown = 0;
            }
        }

        private void ResetCooldownTimer() => _cooldownTimer = 0;

        private void Attack() => _enemyAnimator.SetTrigger("SimpleAttack");

        private float GetDirection() => _enemy.transform.localScale.x < 0 ? -1 : 1;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                _collider.bounds.center + _rayDirection * (_rangeX * GetDirection() * base._colliderDistance),
                new Vector2(_rayDirection.x * _rangeX + _rangeOffsetX, _collider.bounds.size.y + _rangeOffsetY));
        }

        private bool PlayerInSight()
        {
            if (_collider != null)
            {
                RaycastHit2D hit = Physics2D.BoxCast(
                    _collider.bounds.center + _rayDirection * (_rangeX * GetDirection() * base._colliderDistance),
                    new Vector2(_rayDirection.x * _rangeX + _rangeOffsetX, _collider.bounds.size.y + _rangeOffsetY),
                    0, Vector2.left, 0, _playerLayer);

                if (hit.collider != null)
                    return true;
            }

            return false;
        }
    }
}