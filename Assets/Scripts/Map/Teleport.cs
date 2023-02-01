using UI.GuideText;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public class Teleport : MonoBehaviour
    {
        [Header("Text properties")]
        [SerializeField] private GuideText _guideText;
        [SerializeField] private TeleportPoint _positionTo;
        
        public event UnityAction<Player.Player, TeleportPoint> OnTeleporting;
        
        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player.Player player))
                OnTeleporting?.Invoke(player, _positionTo);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player.Player player))
                _guideText.gameObject.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player.Player player))
                _guideText.gameObject.SetActive(false);
        }
    }
}
