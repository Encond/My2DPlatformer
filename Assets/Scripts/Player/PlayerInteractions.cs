using Enemy.AnimationsManager;
using Enemy.Attacks;
using Player.AnimationsManager;
using UI.PauseMenu;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : Attack
    {
        [Header("Damage")]
        [SerializeField] private int _chargedDamageAttack;

        [Header("Player properties")]
        [SerializeField] private Animator _animator;
        [SerializeField] private BoxCollider2D _playerCollider;

        [Header("Enemy")]
        [SerializeField] private LayerMask _enemyLayer;

        [Header("Health Manager")]
        [SerializeField] private HealthManager _healthManager;

        [Header("Sounds")]
        [SerializeField] private AudioSource _simpleAttackSound;
        [SerializeField] private AudioSource _simpleAttackNoTargetSound;
        [SerializeField] private AudioSource _chargedAttackSound;
        [SerializeField] private AudioSource _chargedAttackNoTargetSound;
        
        private RaycastHit2D _hit;
        private Enemy.Enemy _targetToAttack;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _playerCollider = GetComponent<BoxCollider2D>();
            _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!PauseMenu.GameIsPaused)
            {
                IsTargetAlive();

                Attacks();
            }
        }

        private void Attacks()
        {
            _animator.SetBool(PlayerAnimations.GetChargedAttack(),
                _animator.GetBool(PlayerAnimations.GetSimpleAttack()) && !Input.GetMouseButton(0));
            _animator.SetBool(PlayerAnimations.GetSimpleAttack(), Input.GetMouseButton(0));

            if ((_animator.GetBool(PlayerAnimations.GetSimpleAttack()) ||
                 _animator.GetBool(PlayerAnimations.GetChargedAttack())) &&
                _animator.GetBool(PlayerAnimations.GetIsGrounded()))
            {
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void SimpleAttackTrigger()
        {
            if (EnemyInSight())
            {
                _simpleAttackSound.Play();
                _targetToAttack = _hit.collider.GetComponent<Enemy.Enemy>();
                _healthManager.MakeDamage(_targetToAttack, base._damage);
            }
            else
            {
                _targetToAttack = null;
                _simpleAttackNoTargetSound.Play();
            }
        }

        private void ChargedAttackTrigger()
        {
            if (EnemyInSight())
            {
                _chargedAttackSound.Play();
                _targetToAttack = _hit.collider.GetComponent<Enemy.Enemy>();
                _healthManager.MakeDamage(_targetToAttack, _chargedDamageAttack);
            }
            else
            {
                _targetToAttack = null;
                _chargedAttackNoTargetSound.Play();
            }
        }

        private void DamagedTrigger() => _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        private void DamagedTriggerEnded() => _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        private void IsTargetAlive()
        {
            if (_targetToAttack is not null && !_targetToAttack.IsAlive)
            {
                Animator targetAnimator = _targetToAttack.GetComponent<Animator>();

                if (targetAnimator.GetBool(EnemyAnimations.GetIsAlive()))
                {
                    targetAnimator.SetBool(EnemyAnimations.GetIsAlive(), false);
                    _targetToAttack.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }

        private bool EnemyInSight()
        {
            Vector3 direction = transform.right;
            
            _hit = Physics2D.BoxCast(
                _playerCollider.bounds.center + direction * (_rangeX * GetDirection() * base._colliderDistance),
                new Vector2(direction.x * _rangeX + _rangeOffsetX, _playerCollider.bounds.size.y + _rangeOffsetY),
                0, Vector2.left, 0, _enemyLayer);

            return _hit.collider != null;
        }

        private float GetDirection() => gameObject.transform.localScale.x < 0 ? -1 : 1;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(
                _playerCollider.bounds.center + transform.right * (_rangeX * GetDirection() * base._colliderDistance),
                new Vector2(transform.right.x * _rangeX + _rangeOffsetX,
                    _playerCollider.bounds.size.y + _rangeOffsetY));
        }
    }
}