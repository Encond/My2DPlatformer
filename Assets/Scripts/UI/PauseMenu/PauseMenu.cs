using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace UI.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private protected Canvas _pauseMenu;
        [SerializeField] private protected Canvas _optionsMenu;

        [Header("Timeline")]
        [SerializeField] private PlayableDirector _startGameTimeline;

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
            _startGameTimeline.gameObject.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void ResetMenu()
        {
            if (!GameIsPaused)
                GameIsPaused = true;
            else
                GameIsPaused = false;
            
            _pauseMenu.gameObject.SetActive(true);
            _optionsMenu.gameObject.SetActive(false);
        }
    }
}
