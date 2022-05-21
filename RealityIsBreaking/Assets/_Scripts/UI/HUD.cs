using System;
using _Scripts;
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
                    HPBar.SetActive(false);
                    break;
                case GameStateController.GameState.CutScene:
                    HPBar.SetActive(false);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}