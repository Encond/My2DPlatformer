using UnityEngine;

namespace Map
{
    public class TeleportManager : MonoBehaviour
    {
        [Header("Teleports")]
        [SerializeField] private Teleport _aboveGroundTeleport;
        [SerializeField] private Teleport _underGroundTeleport;
        
        [Header("Properties")]
        [SerializeField] private float _teleportCooldown;

        private bool _keyPressed;
        private float _keyPressCooldownCount;

        private void OnEnable()
        {
            _aboveGroundTeleport.OnTeleporting += OnTeleport;
            _underGroundTeleport.OnTeleporting += OnTeleport;
        }
        
        private void OnDisable()
        {
            _aboveGroundTeleport.OnTeleporting -= OnTeleport;
            _underGroundTeleport.OnTeleporting -= OnTeleport;
        }

        private void Update()
        {
            if (_keyPressCooldownCount < _teleportCooldown)
                _keyPressCooldownCount += Mathf.Abs(Time.deltaTime);
            
            if (Input.GetKeyDown(KeyCode.E))
                _keyPressed = true;
        }

        private void OnTeleport(Player.Player player, TeleportPoint positionTo)
        {
            if (_keyPressed && _keyPressCooldownCount >= _teleportCooldown)
            {
                player.transform.position = positionTo.gameObject.transform.position;

                _keyPressed = false;
                _keyPressCooldownCount = 0;
            }
        }
    }
}
