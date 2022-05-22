using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Fungus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reality {
    public class EmailWindow: MonoBehaviour {

        public GameObject playButton;
        public float vibrationStrength = 10f;
        public float vibrationDuration = 0.5f;
        public float vibrationCooldown = 0.5f;

        private void Start() {
            ClickArrowAnimation();
            playButton.GetComponent<Button>().onClick.AddListener(() => {
                ScenesController.Instance.LoadMainMenu();
            });
        }
        
        private void ClickArrowAnimation() {
            playButton.transform
                .DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength))
                .SetDelay(vibrationCooldown)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}