using System;
using UnityEngine;

namespace Reality {
    public class UIDesktopController : MonoBehaviour {
        public GameObject emailIcon;
        public GameObject emailWindow;

        public GameObject emptyTaskBar;
        public GameObject emailTaskBar;
        
        
        private void Start() {
            GameStateController.Instance.SetState_Desktop();
        }

        private void Update() {
            switch (GameStateController.Instance.GetState()) {
                case GameStateController.GameState.Desktop: Activate(emailIcon: true);
                    break;
                case GameStateController.GameState.EmailReading: Activate(emailWindow: true);
                    break; 
            }
        }

        public void ShowEmailTaskbar() {
            emailTaskBar.SetActive(true);
        }

        private void Activate(bool emailIcon = false, bool emailWindow = false) {
            this.emailIcon.SetActive(emailIcon);
            this.emailWindow.SetActive(emailWindow);
        }
    }
}