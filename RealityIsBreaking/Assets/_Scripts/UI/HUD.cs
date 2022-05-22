using System;
using UnityEngine;

namespace Reality {
    public class HUD : MonoBehaviour {
        public GameObject HPBar;
        public GameObject phoneCallWindow;

        private void Update() {
            switch (GameStateController.Instance.GetState()) {
                case GameStateController.GameState.Playing_FirstHalf: Activate(hpBar: true);
                    break;
                case GameStateController.GameState.Paused: Activate(hpBar: true);
                    break;
                case GameStateController.GameState.PhoneCall: Activate(hpBar: true, phoneCall: true);
                    break;
                case GameStateController.GameState.CutScene: Activate(phoneCall: true);
                    break;
                case GameStateController.GameState.GameWon: Activate(hpBar: true);
                    break;
                case GameStateController.GameState.GameOver: Activate(hpBar: true);
                    break;
            }
        }


        private void Activate(bool hpBar = false, bool phoneCall = false) {
            HPBar.SetActive(hpBar);
            phoneCallWindow.SetActive(phoneCall);
        }
    }
}