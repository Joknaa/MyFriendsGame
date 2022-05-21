using System;
using System.Collections;
using System.Timers;
using _Scripts;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Reality {
    public class PhoneWindow : MonoBehaviour {

        public GameObject phoneTalking;
        public GameObject phoneRinging;
        public float vibrationStrength = 180f;
        public float vibrationDuration = 0.5f;
        public float vibrationCooldown = 0.5f;
        public TMP_Text callCounter;
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

            phoneVibratingAnimation = phoneRinging.transform.DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength))
                .SetDelay(vibrationCooldown)
                .SetLoops(-1, LoopType.Restart);
        }

        private IEnumerator Vibrate() {
            while (true) {
                var tween = phoneRinging.transform.DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength))
                    .SetDelay(vibrationCooldown)
                    .SetLoops(-1, LoopType.Restart);
                yield return tween.WaitForCompletion();
                yield return new WaitForSeconds(vibrationCooldown);
            }
        }


        public void StartPhoneCall() {
            GameStateController.Instance.SetState_CutScene();
            phoneVibratingAnimation.Kill();
            phoneRinging.transform.rotation = Quaternion.identity;
            phoneRinging.SetActive(false); 
            phoneTalking.SetActive(true);
            
            callCounter.gameObject.SetActive(true);
            StartCoroutine(UpdateCallCounter());
            
            clickArrowAnimation.Complete();
            clickArrow.SetActive(false);
        }

        public void EndPhoneCall() {
            gameObject.SetActive(false);
        }

        private IEnumerator UpdateCallCounter() {
            float time = 0;
            while (GameStateController.Instance.IsCutScene()) {
                float minutes = Mathf.FloorToInt(time / 60); 
                float seconds = Mathf.FloorToInt(time % 60);
                callCounter.text = $"{minutes:00}:{seconds:00}";
                yield return new WaitForSeconds(1f);
                time += 1;
            }
        }
    }
}