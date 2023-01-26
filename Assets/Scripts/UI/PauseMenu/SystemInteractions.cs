using System;
using UnityEngine;

namespace UI.PauseMenu
{
    public class SystemInteractions : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleActive();
            }
        }

        private void ToggleActive()
        {
            _pauseMenu.gameObject.SetActive(!_pauseMenu.gameObject.activeSelf);
            _pauseMenu.ResetMenu();
            
            if (Time.timeScale >= 1f)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }
}