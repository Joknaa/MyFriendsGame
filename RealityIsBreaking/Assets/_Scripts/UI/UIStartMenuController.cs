using System;
using UnityEngine;
using UnityEngine.UI;

namespace Reality {
    public class UIStartMenuController : MonoBehaviour {
        public GameObject startMenu;
        public GameObject credits;

        public Button startButton;
        public Button creditButton;
        public Button backButton;


        private void Start() {
            startButton.onClick.AddListener(OnPlayerPressed);
            creditButton.onClick.AddListener(ShowCredit);
            backButton.onClick.AddListener(HideCredit);
        }

        private void OnPlayerPressed() {
            ScenesController.Instance.LoadActualGame();
            GameStateController.Instance.SetState_Playing_FirstHalf();
        }

        private void ShowCredit() {
            print("ShowCredit");
            credits.SetActive(true);
            startMenu.SetActive(false);
        }
        
        private void HideCredit() {
            credits.SetActive(false);
            startMenu.SetActive(true);
        }


        private void Update() {
            switch (GameStateController.Instance.GetState()) {
                case GameStateController.GameState.Desktop: Activate(startMenu: true);
                    break;
                case GameStateController.GameState.EmailReading: Activate(credits: true);
                    break; 
            }
        }
        
  

        private void Activate(bool startMenu = false, bool credits = false) {
            this.startMenu.SetActive(startMenu);
            this.credits.SetActive(credits);
        }
    }
}