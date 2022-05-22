using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Reality {
    public class Gun : MonoBehaviour {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float bulletForce = 20f;
        [SerializeField] private float knockBackForce = 5f;
        


        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] SFX;

        private GameObject player;
        private Rigidbody2D playerRigidbody;


        private Vector3 mousePosition;
        SpriteRenderer cursorSprite;
        private Camera cam;


        void Start() {
            cam = Camera.main;
            player = transform.parent.gameObject;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            cursorSprite = GetComponent<SpriteRenderer>();
        }

        void Update() {
            if (!GameStateController.Instance.IsPlaying()) return;

            cursorSprite.enabled = true;
            MoveCursor();
            FireBullet();
            //MoveGun();
        }

        void MoveCursor() {
            mousePosition = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            mousePosition.z = transform.position.z;

            //Sets cursor position
            transform.position = mousePosition;

            //Debug.DrawLine(player.transform.position, mousePosition, Color.red);
        }


        private void FireBullet() {
            if (!GameStateController.Instance.IsPlaying_SecondHalf()) return;
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
            bulletRb.AddForce(((Vector2) transform.position - (Vector2) player.transform.position).normalized * bulletForce, ForceMode2D.Impulse);


            //KNOCKBACK
            //float knockBackDirection = -(transform.position.x - player.transform.position.x);

            //if (knockBackDirection > 0)
            //{
            //    StartCoroutine(KnockBackVelocity(1));
            //    //playerRigidbody.AddForce(new Vector2( knockBackForce, 0f), ForceMode2D.Impulse);
            //}
            //else
            //{
            //    //playerRigidbody.AddForce(new Vector2( -knockBackForce, 0f), ForceMode2D.Impulse);
            //    StartCoroutine(KnockBackVelocity(-1));
            //}
            

            _source.PlayOneShot(SFX[0]);
        }

        IEnumerator KnockBackVelocity(int direction)
        {
            float i = 0.01f;
            while (knockBackForce > i)
            {
                playerRigidbody.velocity = new Vector2((direction*knockBackForce )/ i, playerRigidbody.velocity.y);
                i = i + Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            playerRigidbody.velocity = Vector2.zero;
            yield return null;
        }

        IEnumerator KnockBackTransform(int direction)
        {
            float i = 0.01f;
            while (knockBackForce > i)
            {
                playerRigidbody.velocity = new Vector2((direction * knockBackForce) / i, playerRigidbody.velocity.y);
                i = i + Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            playerRigidbody.velocity = Vector2.zero;
            yield return null;
        }


        private float getShootingDirection() {
            Vector2 lookDir = (Vector2) transform.position - (Vector2) player.transform.position;
            return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }
    }
}