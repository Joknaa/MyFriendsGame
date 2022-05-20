using UnityEngine;

namespace Player {
    public class MovementController : MonoBehaviour {
        [SerializeField] private float Speed;
        [SerializeField] private float JumpForce = 400f;

        [Header("Collision Detection")] [SerializeField]
        private float GroundCollisionRange;

        [SerializeField] private LayerMask GroundLayer;
        public bool IsJumping;
        public bool IsGrounded = true;
        private BoxCollider2D playerCollider;
        private Rigidbody2D playerRigidbody2D;

        private SpriteRenderer playerSpriteRenderer;

        public float get_MoveDirection { get; private set; }

        public bool get_IsGrounded => IsGrounded;
        public bool get_IsJumping => IsJumping;

        private void Start() {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            playerRigidbody2D = GetComponent<Rigidbody2D>();
            playerCollider = GetComponent<BoxCollider2D>();
        }

        private void Update() {
            get_MoveDirection = Input.GetAxisRaw("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space)) IsJumping = true;
        }

        private void FixedUpdate() {
            IsGrounded = HasDetectedGround();
            var moveVelocity = new Vector2(Speed * Time.deltaTime * get_MoveDirection, playerRigidbody2D.velocity.y);
            playerRigidbody2D.velocity = moveVelocity;

            if (IsJumping && IsGrounded) {
                var jumpVelocity = new Vector2(0f, JumpForce);
                playerRigidbody2D.AddForce(jumpVelocity, ForceMode2D.Force);
                IsJumping = false;
            }

            playerSpriteRenderer.flipX = get_MoveDirection < 0;
        }

        private bool HasDetectedGround() {
            var SquareBounds = playerCollider.bounds;
            var Distance = SquareBounds.extents.y + GroundCollisionRange;

            var GroundRaycast = Physics2D.Raycast(SquareBounds.center, Vector2.down, Distance, GroundLayer);
            Debug.DrawRay(SquareBounds.center, Vector2.down * Distance);
            return GroundRaycast;
        }
    }
}