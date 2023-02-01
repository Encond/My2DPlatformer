using UnityEngine;

namespace UI.PauseMenu
{
    public class SystemInteractions : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ToggleActive();
        }

        private void ToggleActive()
        {
            _pauseMenu.gameObject.SetActive(!_pauseMenu.gameObject.activeSelf);
            _pauseMenu.ResetMenu();

            if (!_pauseMenu.gameObject.activeSelf)
                PauseMenu.GameIsPaused = false;

            Time.timeScale = Time.timeScale >= 1f ? 0f : 1f;
        }
    }
}