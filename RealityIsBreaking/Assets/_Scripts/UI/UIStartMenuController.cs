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
    }
}