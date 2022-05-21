using UnityEngine;

namespace Reality {
    
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float pushDownForce = 1;

    [Header("Collision Detection")] 
    [SerializeField] private float GroundCollisionRange;
    [SerializeField] private LayerMask GroundLayer;
    
    private SpriteRenderer playerSpriteRenderer;
    private BoxCollider2D playerCollider;
    private Rigidbody2D body;
    
    private float horizontalMovement;

    private bool queueJump = false;
    private readonly float queueJumpLimit = 0.2f;
    private float queueTimer;

    private bool grounded = false;

    
    private void Start() {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        updateVelocity();
    }

    private void FixedUpdate() {
        body.velocity = new Vector2(moveSpeed * Time.deltaTime * horizontalMovement, body.velocity.y);

        if (isInApex())
        {
            pushDown();
        }
        
    }

    private void updateVelocity() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        grounded = detectGround();

        if (Input.GetKeyDown(KeyCode.Space)) {
            queueJump = true;
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pushDown();
        }

        // This is used to queue up the next jump if the player presses jump right before landing
        if (queueJump) {
            if (queueTimer < queueJumpLimit) {
                Jump();
                queueTimer += Time.deltaTime;
            }
        }
    }

    private void Jump() {
        grounded = detectGround();
        if (!grounded) return;
        
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        queueTimer = 0f;
        queueJump = false;
        grounded = false;
    }

    private bool detectGround() {
        var SquareBounds = playerCollider.bounds;
        var Distance = SquareBounds.extents.y + GroundCollisionRange;
        return Physics2D.Raycast(SquareBounds.center, Vector2.down, Distance, GroundLayer);
    }

    private void pushDown()
    {
        if(!grounded)
        {
            body.AddForce(transform.up * -pushDownForce, ForceMode2D.Impulse);
        }
    }
    
    private bool isInApex()
    {
        if(body.velocity.y < 0.1f && body.velocity.y > 0.1f)
        {
            return true;
        }
        return false;
    }

    public float GetHorizontalMovement() => horizontalMovement;
    public bool isGrounded() => grounded;
}
}