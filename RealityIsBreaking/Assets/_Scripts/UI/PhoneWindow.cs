using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Reality {
    public class PhoneWindow : MonoBehaviour {

        public GameObject phone;
        public float vibrationStrength = 180f;
        public float vibrationDuration = 0.5f;
        public float vibrationCooldown = 0.5f;
        public GameObject clickArrow;
        public float arrowMoveDistance;
        public float arrowAnimationDuration = 0.5f;

        private TweenerCore<Vector3, Vector3, VectorOptions> clickArrowAnimation;
        private Tweener phoneVibratingAnimation;
        private void Start() {
            // StartCoroutine(Vibrate());
            ClickArrowAnimation();
        }

        private void ClickArrowAnimation() {
            var originalPosition = clickArrow.transform.position;

            
            clickArrowAnimation = clickArrow.transform
                .DOMoveY(originalPosition.y + arrowMoveDistance, arrowAnimationDuration)
                .SetLoops(-1, LoopType.Yoyo);

            phoneVibratingAnimation = phone.transform.DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength))
                .SetDelay(vibrationCooldown)
                .SetLoops(-1, LoopType.Restart);
        }

        private IEnumerator Vibrate() {
            while (true) {
                var tween = phone.transform.DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength))
                    .SetDelay(vibrationCooldown)
                    .SetLoops(-1, LoopType.Restart);
                yield return tween.WaitForCompletion();
                yield return new WaitForSeconds(vibrationCooldown);
            }
        }


        public void StartPhoneCall() {
            print("Gatcha");
            phoneVibratingAnimation.Kill();
            phone.transform.rotation = Quaternion.identity;
            clickArrowAnimation.Complete();
            clickArrow.SetActive(false);
        }
    }
}