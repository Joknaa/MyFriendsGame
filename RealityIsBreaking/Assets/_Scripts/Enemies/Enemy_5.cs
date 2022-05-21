using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reality
{
    public class Enemy_5 : MonoBehaviour
    {
        public float timeBetweenEachBullet;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 40f;


        private GameObject player;
        private float Timer = 0;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > timeBetweenEachBullet)
            {
                FireBullet();
                Timer = 0;
            }
        }


        private void FireBullet()
        {
                float angle = getShootingDirection();

                GameObject bulletInstance = Instantiate(bulletPrefab, transform);
                bulletInstance.transform.position = transform.position;
                bulletInstance.name = "EnemyBullet";
                Bullet bulletScript = bulletInstance.AddComponent<Bullet>();
                bulletScript.SetInstance(bulletInstance);

                Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
                bulletRb.rotation = angle;
                bulletRb.AddForce(((Vector2)player.transform.position - (Vector2)transform.position ).normalized * bulletForce, ForceMode2D.Impulse);
            
        }

        private float getShootingDirection()
        {
            Vector2 lookDir = (Vector2)player.transform.position - (Vector2)transform.position ;
            return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }

    }
}