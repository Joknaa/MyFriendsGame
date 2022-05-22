using System;
using UnityEngine;

namespace Reality {
    public class UIController : MonoBehaviour {
        public GameObject HUD;
        public GameObject GameOverMenu;
        public GameObject GameWonMenu;
        public GameObject PauseMenu;

        private void Start() {
            GameStateController.Instance.SetState_Playing();
        }

        private void Update() {
            switch (GameStateController.Instance.GetState()) {
                case GameStateController.GameState.Playing: Activate(hud: true);
                    break;
                case GameStateController.GameState.Paused: Activate(pause: true);
                    break;
                case GameStateController.GameState.PhoneCall: Activate(hud: true);
                    break;
                case GameStateController.GameState.CutScene: Activate(hud: true);
                    break;
                case GameStateController.GameState.GameWon: Activate(gameWon: true);
                    break;
                case GameStateController.GameState.GameOver: Activate(gameOver: true);
                    break;
                
            }
        }


        private void Activate(bool hud = false, bool pause = false,  bool gameOver = false, bool gameWon = false) {
            HUD.SetActive(hud);
            GameOverMenu.SetActive(gameOver);
            GameWonMenu.SetActive(gameWon);
            PauseMenu.SetActive(pause);
        }
    }
}