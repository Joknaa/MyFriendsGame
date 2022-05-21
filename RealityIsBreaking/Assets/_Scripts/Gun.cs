using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Reality
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D gun;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float bulletForce = 20f;
        private GameObject player;


        


        private Vector3 mousePosition;
        private Camera cam;
        private bool canShoot;


        void Start()
        {
            cam = Camera.main;
            player = transform.parent.gameObject;
        }
        void Update()
        {
            MoveCursor();
            
            FireBullet();
            //MoveGun();

        }

        void MoveCursor()
        {
            mousePosition = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            mousePosition.z = transform.position.z;

            //Sets cursor position
            transform.position = mousePosition;

            Debug.DrawLine(player.transform.position, mousePosition, Color.red);

        }

        void MoveGun()
        {
            gun.rotation = getShootingDirection();
            
        }

        private void FireBullet()
        {
            if (Input.GetMouseButtonDown(0))
            {
                float angle = getShootingDirection(); 

                bullet = Instantiate(bullet, transform.parent.transform.parent);
                //bullet.transform.position = gun.transform.GetChild(0).transform.position;
                bullet.transform.position = player.transform.position;
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.rotation = angle;
                bulletRb.AddForce((transform.position - player.transform.position).normalized * bulletForce, ForceMode2D.Impulse);
            }
        }

        private float getShootingDirection()
        {
            Vector2 lookDir = transform.position - player.transform.position;
            return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }


    }
}
