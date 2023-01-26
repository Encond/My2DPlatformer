using Player.AnimationsManager;
using Sounds;
using UI.PauseMenu;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private Player _player;
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _jumpForce = 20f;
        [SerializeField] private int _jumpsCountLimit = 2;

        [Header("Ground Check")]
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private Transform _groundCheckPosition;

        [Header("Input Keys")]
        [SerializeField] private KeyCode _keyLeft = KeyCode.A;
        [SerializeField] private KeyCode _keyRight = KeyCode.D;
        [SerializeField] private KeyCode _keyJump = KeyCode.Space;

        [Header("Animations")]
        [SerializeField] private Animator _animator;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveDirection;

        private float _playerScaleX;
        private bool _isOnGround;
        private bool _isLookingLeft;
        private int _jumpsCount = 1;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerScaleX = _player.transform.localScale.x;
        }

        private void Update()
        {
            if (!PauseMenu.GameIsPaused)
            {
                if (!IsAlive()) return;
                // else
                // {
                GetMovementInputs();
                Jump();
                // }
            }
        }

        private void FixedUpdate()
        {
            if (!PauseMenu.GameIsPaused)
            {
                IsGrounded();

                if (!IsAlive()) return;
                // else
                // {
                Move();
                // }
            }
        }

        private void GetMovementInputs()
        {
            _moveDirection = Vector2.zero;
            
            if (Input.GetKey(_keyLeft))
            {
                _animator.SetBool(PlayerAnimations.GetRunning(), true);

                if (_rigidbody2D.velocity.magnitude <= _movementSpeed)
                    _moveDirection = -transform.right;

                TurnThePlayer(-_playerScaleX);

            }
            else if (Input.GetKey(_keyRight))
            {
                _animator.SetBool(PlayerAnimations.GetRunning(), true);

                if (_rigidbody2D.velocity.magnitude <= _movementSpeed)
                    _moveDirection = transform.right;

                TurnThePlayer(_playerScaleX);
            }
            else
            {
                _animator.SetBool(PlayerAnimations.GetRunning(), false);

                _moveDirection = Vector2.zero;
                _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            }

        }

        private void Move() => _rigidbody2D.AddForce(_moveDirection * _movementSpeed * 2, ForceMode2D.Force);

        private void Jump()
        {
            if (_jumpsCount < _jumpsCountLimit && Input.GetKeyDown(_keyJump) && (_isOnGround || !_isOnGround))
            {
                // SoundManager.Instance.PlaySound("Jump");
                _jumpsCount++;

                _animator.SetBool(PlayerAnimations.GetJumping(), true);
                _animator.SetBool(PlayerAnimations.GetFalling(), true);

                _rigidbody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);

                _isOnGround = false;
                _animator.SetBool(PlayerAnimations.GetIsGrounded(), _isOnGround);
            }
            else
                _animator.SetBool(PlayerAnimations.GetJumping(), false);
        }

        private void IsGrounded()
        {
            // if (!_isOnGround)
            _isOnGround = Physics2D.OverlapPoint(_groundCheckPosition.position, _whatIsGround);

            _animator.SetBool(PlayerAnimations.GetIsGrounded(), _isOnGround);

            if (_isOnGround == false)
            {
                _animator.SetBool(PlayerAnimations.GetFalling(), true);
            }
            else if (_isOnGround)
            {
                _animator.SetBool(PlayerAnimations.GetJumping(), false);
                _animator.SetBool(PlayerAnimations.GetFalling(), false);
                _jumpsCount = 1;
            }

            if (_player.transform.position.y < -10f)
            {
                if (SoundManager.Instance._isPlayingMainMusic && SoundManager.Instance._shouldPlayMusic)
                {
                    SoundManager.Instance.StopMusic();
                    SoundManager.Instance.PlayUnderGroundMusic();
                }
            }
        }

        private void TurnThePlayer(float scaleX) =>
            _player.gameObject.transform.localScale = new Vector2(scaleX, 1.5f);

        private bool IsAlive() => _player.IsAlive;

        // private void Died()
        // {
        //     _animator.Play("Died");
        //     gameObject.SetActive(IsAlive());
        // }
    }
}