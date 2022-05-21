using System;
using UnityEngine;

namespace Reality {
    public class HUD : MonoBehaviour {
        public GameObject HPBar;


        private void Update() {
            switch (GameStateController.Instance.GetState()) {
                case GameStateController.GameState.Playing:
                    HPBar.SetActive(true);
                    break;
                case GameStateController.GameState.Paused:
                case GameStateController.GameState.CutScene:
                case GameStateController.GameState.GameOver:
                case GameStateController.GameState.GameWon:
                    HPBar.SetActive(false);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}