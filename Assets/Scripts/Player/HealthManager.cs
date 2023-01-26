using Player.AnimationsManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Player
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Image _healthBarFill;

        private PlayerInteractions _playerInteractions;
        
        public event UnityAction OnPlayerDied;

        private void Start()
        {
            _playerInteractions = _player.GetComponent<PlayerInteractions>();
        }

        public void TakeDamage(int damageAmount)
        {
            _playerAnimator.SetTrigger(PlayerAnimations.GetIsDamaged());
            
            _player.SetCurrentHealth(damageAmount);

            _healthBarFill.rectTransform.localScale = new Vector3(_player.GetHealthPercent(),
                _healthBarFill.rectTransform.localScale.y,
                _healthBarFill.rectTransform.localScale.z);

            if (_player.GetCurrentHealth() < 0)
            {
                _player.SetCurrentHealth(0);
                OnPlayerDied?.Invoke();
            }
        }

        public void MakeDamage(Enemy.Enemy enemy, int damage)
        {
            enemy.SetCurrentHealth(damage);
        }

        // public void TakeHeal(int healAmount)
        // {
        //     _player.CurrentHealth += healAmount;
        //     if (_player.CurrentHealth > _player.GetHealthMax()) _player.CurrentHealth = _player.GetHealthMax();
        // }
    }
}