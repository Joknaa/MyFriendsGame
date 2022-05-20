using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoMovements : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 400f;

    [Header("Collision Detection")] 
    [SerializeField] private float groundCollisionRange;
    [SerializeField] private LayerMask groundLayer;
    
    private float _moveDirection;
    private BoxCollider2D _playerCollider;
    private Rigidbody2D _playerRigidbody2D;
    private SpriteRenderer _playerSpriteRenderer;


    private void Start() {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        _moveDirection = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        _playerRigidbody2D.velocity = new Vector2(speed * Time.deltaTime * _moveDirection, _playerRigidbody2D.velocity.y);;
        
        _playerSpriteRenderer.flipX = _moveDirection < 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Respawn")) SceneManager.LoadScene(0);
    }


    private bool HasDetectedGround() {
        var SquareBounds = _playerCollider.bounds;
        var Distance = SquareBounds.extents.y + groundCollisionRange;

        var GroundRaycast = Physics2D.Raycast(SquareBounds.center, Vector2.down, Distance, groundLayer);
        Debug.DrawRay(SquareBounds.center, Vector2.down * Distance);
        return GroundRaycast;
    }
}