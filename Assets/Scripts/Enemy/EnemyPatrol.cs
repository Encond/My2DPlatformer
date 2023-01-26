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

        private Rigidbody2D _rigidbody2D;
        private bool _isMovingLeft;
        private float _movingUntilOffset;
        private float _scaleX;

        private void Start()
        {
            _rigidbody2D = _enemy.GetComponent<Rigidbody2D>();
            _movingUntilOffset = 1f;
            _scaleX = _enemy.transform.localScale.x;
        }

        private void Update()
        {
            if (_enemy.IsAlive)
            {
                _animator.SetBool("Running", true);
                
                if (PlayerInChaseSight())
                    TargetChase();
                else
                {
                    MoveInDirection();
                    ChangeDirection();
                }

                SpeedControl();
            }
            else
            {
                _animator.SetBool("Running", false);
            }
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
            float tempDirection = _enemy.transform.position.x <= 0 ? -1 : 1;
            float tempPosition = _isMovingLeft ? -_enemy.transform.position.x : _enemy.transform.position.x;

            _rigidbody2D.velocity += new Vector2(tempPosition * Time.deltaTime * tempDirection * _speed, 0f);
        }

        private void SpeedControl()
        {
            Vector2 flatVel = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y);

            if (flatVel.magnitude > _speed)
            {
                Vector2 limitedVel = flatVel.normalized * _speed;
                _rigidbody2D.velocity = new Vector2(limitedVel.x, limitedVel.y);
            }
        }

        private void LookingToDirection(float scaleX)
        {
            Vector2 localScale = _enemy.gameObject.transform.localScale;
            _enemy.gameObject.transform.localScale = new Vector2(scaleX, localScale.y);
        }

        private void TargetChase()
        {
            if (_enemy.transform.position.x <= _player.transform.position.x + _movingUntilOffset)
                _isMovingLeft = false;
            else if (_enemy.transform.position.x >= _player.transform.position.x + _movingUntilOffset)
                _isMovingLeft = true;

            float tempPosition = _isMovingLeft ? -_player.transform.position.x : _player.transform.position.x;
            if (_player.transform.position.x <= 0)
                tempPosition = -tempPosition;

            _rigidbody2D.velocity +=
                new Vector2(tempPosition * Time.deltaTime * _speed, 0f);
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

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.cyan;
        //
        //     Gizmos.DrawWireCube(_enemy.GetComponent<BoxCollider2D>().bounds.center,
        //         new Vector3(_enemy.GetComponent<BoxCollider2D>().bounds.size.x,
        //             _enemy.GetComponent<BoxCollider2D>().bounds.size.y / 2,
        //             _enemy.GetComponent<BoxCollider2D>().bounds.size.z));
        // }

        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireCube(
        //         _collider.bounds.center + transform.right * base._rangeX *
        //         (_enemy.transform.localScale.x < 0 ? -1 : 1) * base._colliderDistance,
        //         new Vector2(_collider.bounds.size.x * base._rangeX, _collider.bounds.size.y));
    }
}