using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Fungus;
using TMPro;
using UnityEngine;

namespace Reality {
    public class EmailIcon: MonoBehaviour {

        public GameObject emailRinging;
        public float vibrationStrength = 180f;
        public float vibrationDuration = 0.5f;
        public float vibrationCooldown = 0.5f;
        public GameObject clickArrow;
        public float arrowMoveDistance;
        public float arrowAnimationDuration = 0.5f;

        private TweenerCore<Vector3, Vector3, VectorOptions> clickArrowAnimation;
        private Tweener phoneVibratingAnimation;
        private void Start() {
            ClickArrowAnimation();
        }

        private void ClickArrowAnimation() {
            var originalPosition = clickArrow.transform.position;
            
            clickArrowAnimation = clickArrow.transform
                .DOMoveY(originalPosition.y + arrowMoveDistance, arrowAnimationDuration)
                .SetLoops(-1, LoopType.Yoyo);

            phoneVibratingAnimation = emailRinging.transform
                .DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength))
                .SetDelay(vibrationCooldown)
                .SetLoops(-1, LoopType.Restart);
        }
        
        public void OpenEmail() {
            GameStateController.Instance.SetState_EmailReading();
            phoneVibratingAnimation.Kill();
            emailRinging.transform.rotation = Quaternion.identity;
            emailRinging.SetActive(false); 
            
            clickArrowAnimation.Complete();
            clickArrow.SetActive(false);
            
            GameObject.FindGameObjectWithTag("GameController").GetComponent<UIDesktopController>().ShowEmailTaskbar();
        }

        public void EndPhoneCall() {
            gameObject.SetActive(false);
            GameStateController.Instance.SetState_Playing();
        }

    }
}