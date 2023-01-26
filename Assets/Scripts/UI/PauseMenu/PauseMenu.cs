using UnityEngine;
using UnityEngine.Rendering;

namespace UI.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private protected Canvas _pauseMenu;
        [SerializeField] private protected Canvas _optionsMenu;

        public static bool GameIsPaused;
        
        public void Resume()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            GameIsPaused = false;
            Time.timeScale = 1f;
        }

        public void Options()
        {
            _pauseMenu.gameObject.SetActive(!_pauseMenu.gameObject.activeSelf);
            _optionsMenu.gameObject.SetActive(!_optionsMenu.gameObject.activeSelf);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ResetMenu()
        {
            GameIsPaused = true;
            _pauseMenu.gameObject.SetActive(true);
            _optionsMenu.gameObject.SetActive(false);
        }
    }
}
