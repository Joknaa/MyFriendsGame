using System;
using UnityEngine;

namespace Reality {
    public class UIController : MonoBehaviour {
        public GameObject HUD;
        public GameObject GameOverMenu;
        public GameObject GameWonMenu;
        public GameObject PauseMenu;

        private GameStateController.GameState lastPlayingState;
        private void Start() {
            GameStateController.Instance.SetState_Playing_FirstHalf();
            lastPlayingState = GameStateController.Instance.GetState();
        }

        private void Update() {
            if (GameStateController.Instance.IsPlaying_SecondHalf()) {
                lastPlayingState = GameStateController.GameState.Playing_SecondHalf;
            }
            if (GameStateController.Instance.IsPlaying_FirstHalf()) {
                lastPlayingState = GameStateController.GameState.Playing_FirstHalf;
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                GameStateController.Instance.TogglePause(lastPlayingState);
            }
            
            switch (GameStateController.Instance.GetState()) {
                case GameStateController.GameState.Playing_FirstHalf: Activate(hud: true);
                    break;
                case GameStateController.GameState.Playing_SecondHalf: Activate(hud: true);
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