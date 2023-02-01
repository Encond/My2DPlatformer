using Enemy.AnimationsManager;
using UnityEngine;

namespace Enemy
{
    public class EnemyPatrol : MonoBehaviour
    {
        [Header("Patrol Points")]
        [SerializeField] private Transform _leftEdge;
        [SerializeField] private Transform _rightEdge;

        [Header("Enemy")]
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Animator _animator;
        [SerializeField] private CapsuleCollider2D _collider;

        [Header("Movement parameters")]
        [SerializeField] private float _speed;

        [Header("Target")]
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _player;

        [Header("Target chase properties")]
        [SerializeField] private float _rangeX;
        [SerializeField] private float _rangeOffsetX;
        [SerializeField] private float _colliderDistance;
        [SerializeField] private float _howCloseMovingOffset;

        private Rigidbody2D _enemyRigidbody2D;
        private bool _isMovingLeft;
        private float _scaleX;
        private float _currentSpeed;

        private void Start()
        {
            _enemyRigidbody2D = _enemy.GetComponent<Rigidbody2D>();
            _scaleX = _enemy.transform.localScale.x;
            _currentSpeed = _speed;
        }

        private void Update()
        {
            if (!_enemy.IsAlive) return;
            
            if (PlayerInChaseSight())
                TargetChase();
            else
            {
                MoveInDirection();
                ChangeDirection();
            }

            SpeedControl();
        }

        private void ChangeDirection()
        {
            if (_enemy.transform.position.x <= _leftEdge.position.x)
            {
                _isMovingLeft = false;
                LookingToDirection(_scaleX);
            }
            else if (_enemy.transform.position.x >= _rightEdge.position.x)
            {
                _isMovingLeft = true;
                LookingToDirection(-_scaleX);
            }
        }

        private void MoveInDirection()
        {
            _animator.SetBool(EnemyAnimations.GetRunning(), true);
            _currentSpeed = _speed;
            
            float tempDirection = _enemy.transform.position.x <= 0 ? -1 : 1;
            float tempPosition = _isMovingLeft ? -_enemy.transform.position.x : _enemy.transform.position.x;

            _enemyRigidbody2D.velocity += new Vector2(tempPosition * Time.deltaTime * tempDirection * _currentSpeed, 0f);
        }

        private void SpeedControl()
        {
            Vector2 flatVel = new Vector2(_enemyRigidbody2D.velocity.x, _enemyRigidbody2D.velocity.y);

            if (flatVel.magnitude > _currentSpeed)
            {
                Vector2 limitedVel = flatVel.normalized * _currentSpeed;
                _enemyRigidbody2D.velocity = new Vector2(limitedVel.x, limitedVel.y);
            }
        }

        private void LookingToDirection(float scaleX)
        {
            Vector2 localScale = _enemy.gameObject.transform.localScale;
            _enemy.gameObject.transform.localScale = new Vector2(scaleX, localScale.y);
        }

        private void TargetChase()
        {
            if (_enemy.transform.position.x <= _player.transform.position.x + _howCloseMovingOffset)
                _isMovingLeft = false;
            else if (_enemy.transform.position.x >= _player.transform.position.x + _howCloseMovingOffset)
                _isMovingLeft = true;

            float tempPosition = _isMovingLeft ? -_player.transform.position.x : _player.transform.position.x;

            if (_player.transform.position.x <= 0)
                tempPosition = -tempPosition;

            IsReachedOffset(ref tempPosition);

            if (tempPosition is > 0f or < 0f)
            {
                _animator.SetBool(EnemyAnimations.GetRunning(), true);
                _currentSpeed = _speed;
                _enemyRigidbody2D.velocity += new Vector2(tempPosition * Time.deltaTime * _currentSpeed, 0f);
            }
            else
            {
                _animator.SetBool(EnemyAnimations.GetRunning(), false);
                _currentSpeed = 0f;
            }
        }

        private void IsReachedOffset(ref float tempPosition)
        {
            if (_enemy.transform.position.x >= (_player.transform.position.x - _howCloseMovingOffset) && !_isMovingLeft ||
                _enemy.transform.position.x <= (_player.transform.position.x + _howCloseMovingOffset) && _isMovingLeft)
            {
                tempPosition = 0f;
            }
        }

        private bool PlayerInChaseSight()
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                _collider.bounds.center + transform.right * (_rangeX * (_enemy.transform.localScale.x < 0 ? -1 : 1) * _colliderDistance),
                new Vector2(_collider.bounds.size.x * _rangeX + _rangeOffsetX, _collider.bounds.size.y),
                0, Vector2.left, 0, _playerLayer);

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(
                _collider.bounds.center + transform.right * _rangeX * (_enemy.transform.localScale.x < 0 ? -1 : 1) *
                _colliderDistance,
                new Vector2(_collider.bounds.size.x * _rangeX + _rangeOffsetX, _collider.bounds.size.y));
        }
    }
}