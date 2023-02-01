using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class MainMenuInteractions : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
            PauseMenu.PauseMenu.GameIsPaused = false;
        }

        public void Quit() => Application.Quit();
    }
}
