using Player.AnimationsManager;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Player
{
    public class HealthManager : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private Player _player;
        [SerializeField] private Animator _playerAnimator;
        
        [Header("UI Elements")]
        [SerializeField] private Image _healthBarFill;
        
        [Header("Timelines")]
        [SerializeField] private PlayableDirector _endingGameTimeline;

        [Header("Sounds")]
        [SerializeField] private AudioSource _hurtSound;

        public void TakeDamage(int damageAmount)
        {
            _playerAnimator.SetTrigger(PlayerAnimations.GetIsDamaged());

            _hurtSound.Play();

            _player.SetCurrentHealth(damageAmount);

            var localScale = _healthBarFill.rectTransform.localScale;
            localScale = new Vector3(_player.GetHealthPercent(), localScale.y, localScale.z);
            _healthBarFill.rectTransform.localScale = localScale;

            if (_player.GetCurrentHealth() <= 0)
            {
                _player.SetCurrentHealth(0);

                if (!_endingGameTimeline.gameObject.activeSelf && _endingGameTimeline.state == PlayState.Paused)
                {
                    _endingGameTimeline.gameObject.SetActive(true);
                    _endingGameTimeline.Play();
                }
            }
        }

        public void MakeDamage(Enemy.Enemy enemy, int damage) => enemy.SetCurrentHealth(damage);
    }
}