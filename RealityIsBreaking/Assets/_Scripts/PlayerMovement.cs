using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 5;


    [Header("Collision Detection")]
    [SerializeField] private float GroundCollisionRange;
    [SerializeField] private LayerMask GroundLayer;

    private BoxCollider2D playerCollider;

    private float horizontalVelocity = 0f;
    private Rigidbody2D body;
    private bool canJump = false;
    private bool queueJump = true;
    private float queueTimer = 0f;
    private float queueJumpLimit = 0.2f;

    



   

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalVelocity = 0f;

        updateVelocity();

    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalVelocity, body.velocity.y);

    }

    void updateVelocity()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalVelocity = moveSpeed;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalVelocity += -moveSpeed;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            queueJump = true;
            canJump = detectGround();
            if (canJump)
            {
                //Debug.Log("jumping");
                body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                queueJump = false;
                queueTimer = 0f;
            }
        }
        

        // This is used to queue up the next jump if the player presses jump right before landing
        if (queueJump)
        {
            if (queueTimer < queueJumpLimit)
            {
                canJump = detectGround();
                if (canJump)
                {
                    //Debug.Log("jumping");
                    body.velocity = new Vector2(body.velocity.x, 0f) ;
                    body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    queueTimer = 0f;
                    queueJump = false;
                }
                Debug.Log(queueTimer);
                queueTimer += Time.deltaTime;
            }
        }

    }
    
    private bool detectGround()
    {
        Bounds SquareBounds = playerCollider.bounds;
        float Distance = SquareBounds.extents.y + GroundCollisionRange;

        RaycastHit2D GroundRaycast = Physics2D.Raycast(SquareBounds.center, Vector2.down, Distance, GroundLayer);
        Debug.DrawRay(SquareBounds.center, Vector2.down * Distance);
        return GroundRaycast;
    }
}
