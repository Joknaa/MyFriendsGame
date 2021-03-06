using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Fungus;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Reality {
    public class PhoneWindow : MonoBehaviour {
        public GameObject phoneTaskBar;
        public GameObject phoneTalking;
        public GameObject phoneRinging;
        public float vibrationStrength = 180f;
        public float vibrationDuration = 0.5f;
        public float vibrationCooldown = 0.5f;
        public TMP_Text callCounter;
        public GameObject clickArrow;
        public float arrowMoveDistance;
        public float arrowAnimationDuration = 0.5f;

        public AudioClip secondSong;
        public AudioClip ringingSFX;
        private AudioSource MusicManager;


        private TweenerCore<Vector3, Vector3, VectorOptions> clickArrowAnimation;
        private Tweener phoneVibratingAnimation;

        private void Start() {
            MusicManager = GameObject.Find("Controllers/MusicManager").GetComponent<AudioSource>();
            phoneTaskBar.SetActive(true);
            ClickArrowAnimation();
        }

        void PlaySong(AudioClip clippy) {
            //if (!GameStateController.Instance.IsPhoneCall()) return;

            MusicManager.Stop();
            MusicManager.clip = clippy;
            MusicManager.loop = true;
            MusicManager.Play();
        }

        private void OnEnable() {
            if (!GameStateController.Instance.IsPhoneCall()) return;
            if (MusicManager != null) MusicManager = GameObject.FindGameObjectWithTag("MusicController").GetComponent<AudioSource>();

            PlaySong(ringingSFX);
        }

        private void ClickArrowAnimation() {
            var originalPosition = clickArrow.transform.position;

            clickArrowAnimation = clickArrow.transform
                .DOMoveY(originalPosition.y + arrowMoveDistance, arrowAnimationDuration)
                .SetLoops(-1, LoopType.Yoyo);

            phoneVibratingAnimation = phoneRinging.transform
                .DOShakeRotation(vibrationDuration, new Vector3(0, 0, vibrationStrength))
                .SetDelay(vibrationCooldown)
                .SetLoops(-1, LoopType.Restart);
        }

        public void StartPhoneCall() {
            //Stop Ringing
            MusicManager.Stop();

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
            //Start second song
            PlaySong(secondSong);
            GameStateController.Instance.SetState_Playing_SecondHalf();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>().StartSecondHalfLevels();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>().enabled = true;
            phoneTaskBar.SetActive(false);
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