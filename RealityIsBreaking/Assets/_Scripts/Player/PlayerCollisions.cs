using System;
using UnityEngine;

namespace Reality {
    public class PlayerCollisions : MonoBehaviour{
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip winSFX;
        private AudioSource MusicManager;

        private void Start()
        {
            MusicManager = GameObject.Find("Controllers/MusicManager").GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collidedWith) {
            if (collidedWith.CompareTag("Chest")) {
                collidedWith.enabled = false;
                GameStateController.Instance.SetState_PhoneCall();
            }
            if (collidedWith.CompareTag("Spike")) {
                GetComponent<PlayerHealth>().takeDamage(1);
            }
            if (collidedWith.CompareTag("PitFall")) {
                GetComponent<PlayerHealth>().takeDamage(10);
            }
            
            if (collidedWith.CompareTag("FinishLine")) {
                MusicManager.Stop();
                _source.PlayOneShot(winSFX);
                GameStateController.Instance.SetState_GameWon();
            }
        }
    }
}