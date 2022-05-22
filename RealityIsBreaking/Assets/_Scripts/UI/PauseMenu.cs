using System;
using UnityEngine;
using UnityEngine.UI;

namespace Reality {
    public class PauseMenu : MonoBehaviour {
        public Button PlayerAgainButton;
        public Button MainMenuButton;
        
        public void Start() {
            PlayerAgainButton.onClick.AddListener(() => {
                ScenesController.Instance.RestartLevel();
            });
            MainMenuButton.onClick.AddListener(() => {
                ScenesController.Instance.LoadMainMenu();
            });
        }
    }
}