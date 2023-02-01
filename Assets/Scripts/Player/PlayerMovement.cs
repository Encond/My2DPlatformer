using Player.AnimationsManager;
using Sounds;
using UI.PauseMenu;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player properties")]
        [SerializeField] private Player _player;
        [SerializeField] private Rigidbody2D _rigidbody2D;
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

        [Header("Sounds")]
        [SerializeField] private AudioSource _jump;
        [SerializeField] private AudioSource _landing;

        private Vector2 _moveDirection;

        private Vector2 _playerScale;
        private bool _isOnGround;
        private bool _isLookingLeft;
        private bool _landingSoundShouldPlay;
        private int _jumpsCount = 1;
        private readonly int _normalizedMovementSpeed = 2;

        private void Start()
        {
            Vector2 tempLocalScale = _player.transform.localScale;
            _playerScale.x = tempLocalScale.x;
            _playerScale.y = tempLocalScale.y;
        }

        private void Update()
        {
            if (PauseMenu.GameIsPaused) return;
            
            if (_player.GetCurrentHealth() <= 0)
                GameOver();
            else
            {
                GetMovementInputs();
                Jump();
            }
        }

        private void FixedUpdate()
        {
            if (PauseMenu.GameIsPaused) return;
            
            IsGrounded();

            if (_player.GetCurrentHealth() <= 0)
                GameOver();
            else
                Move();
        }

        private void GetMovementInputs()
        {
            _moveDirection = Vector2.zero;
            
            if (Input.GetKey(_keyLeft))
            {
                _animator.SetBool(PlayerAnimations.GetRunning(), true);

                if (_rigidbody2D.velocity.magnitude <= _movementSpeed)
                    _moveDirection = -transform.right;

                TurnThePlayer(-_playerScale.x);

            }
            else if (Input.GetKey(_keyRight))
            {
                _animator.SetBool(PlayerAnimations.GetRunning(), true);

                if (_rigidbody2D.velocity.magnitude <= _movementSpeed)
                    _moveDirection = transform.right;

                TurnThePlayer(_playerScale.x);
            }
            else
            {
                _animator.SetBool(PlayerAnimations.GetRunning(), false);

                _moveDirection = Vector2.zero;
                _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            }

        }

        private void Move() => _rigidbody2D.AddForce(_moveDirection * (_movementSpeed * _normalizedMovementSpeed), ForceMode2D.Force);

        private void Jump()
        {
            if (_jumpsCount < _jumpsCountLimit && Input.GetKeyDown(_keyJump) &&
                !_animator.GetBool(PlayerAnimations.GetSimpleAttack()) &&
                !_animator.GetBool(PlayerAnimations.GetChargedAttack()))
            {
                _jump.Play();
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
            _isOnGround = Physics2D.OverlapPoint(_groundCheckPosition.position, _whatIsGround);
            
            _animator.SetBool(PlayerAnimations.GetIsGrounded(), _isOnGround);

            if (_isOnGround == false)
            {
                _animator.SetBool(PlayerAnimations.GetFalling(), true);
                _landingSoundShouldPlay = true;
            }
            else if (_isOnGround)
            {
                if (_landingSoundShouldPlay)
                {
                    _landing.Play();
                    _landingSoundShouldPlay = false;
                }
                
                _animator.SetBool(PlayerAnimations.GetJumping(), false);
                _animator.SetBool(PlayerAnimations.GetFalling(), false);
                
                _jumpsCount = 1;
            }

            if (_player.transform.position.y < -20f)
            {
                if (!SoundManager.Instance._isPlayingMainMusic || !SoundManager.Instance._shouldPlayMusic) return;
                
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayUnderGroundMusic();
            }
            else
            {
                if (SoundManager.Instance._isPlayingMainMusic || !SoundManager.Instance._shouldPlayMusic) return;
                
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMainMusic();
            }
        }

        private void TurnThePlayer(float scaleX) =>
            _player.gameObject.transform.localScale = new Vector2(scaleX, _playerScale.y);

        private void GameOver()
        {
            _animator.Play(PlayerAnimations.GetIsAlive());
            _animator.SetBool(PlayerAnimations.GetIsAlive(), false);
            _player.gameObject.layer = 0;
        }
    }
}