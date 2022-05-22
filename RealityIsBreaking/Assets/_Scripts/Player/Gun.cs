using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Reality {
    public class Gun : MonoBehaviour {
        
        [SerializeField] private GameObject bullet;
        [SerializeField] private float bulletForce = 20f;

        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] SFX;

        private GameObject player;


        private Vector3 mousePosition;
        private Camera cam;
        


        void Start() {
            cam = Camera.main;
            player = transform.parent.gameObject;
        }

        void Update() {
            if (!GameStateController.Instance.IsPlaying()) return;
            
            MoveCursor();
            FireBullet();
            //MoveGun();
        }

        void MoveCursor() {
            mousePosition = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            mousePosition.z = transform.position.z;

            //Sets cursor position
            transform.position = mousePosition;

            Debug.DrawLine(player.transform.position, mousePosition, Color.red);
        }


        private void FireBullet() {
            if (!Input.GetMouseButtonDown(0)) return;
            float angle = getShootingDirection();

            GameObject bulletInstance = Instantiate(bullet, transform.parent.parent);
            Bullet bulletScript = bulletInstance.AddComponent<Bullet>();
            bulletScript.isPlayerBullet = true;
            bulletInstance.transform.position = player.transform.position;
            bulletInstance.name = "Bullet";
            bulletScript.SetInstance(bulletInstance);
                
            Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
            bulletRb.rotation = angle;
            bulletRb.AddForce(((Vector2)transform.position - (Vector2)player.transform.position).normalized * bulletForce, ForceMode2D.Impulse);

            _source.PlayOneShot(SFX[0]);
        }

        private float getShootingDirection() {
            Vector2 lookDir = (Vector2)transform.position - (Vector2)player.transform.position;
            return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }
    }
}