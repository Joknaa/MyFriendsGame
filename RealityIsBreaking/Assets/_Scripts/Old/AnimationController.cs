using UnityEngine;

namespace Old {
    public enum PlayerState {
        Idle,
        Jump,
        Walk,
        Run
    }

    public class AnimationController : MonoBehaviour {
        [SerializeField] private Sprite playerJumpSprite;
        private PlayerState CurrentState;
        private MovementController MovementController;
        private Animator PlayerAnimator;
        private SpriteRenderer PlayerSprite;

        private void Start() {
            PlayerSprite = GetComponent<SpriteRenderer>();
            PlayerAnimator = GetComponent<Animator>();
            MovementController = GetComponent<MovementController>();
            //CharacterController2D = GetComponent<CharacterController2D>();
        }

        private void Update() {
            var moveDirection = MovementController.get_MoveDirection;
            var isGrounded = MovementController.get_IsGrounded;
            var isJumping = MovementController.get_IsJumping;
            if (isGrounded) {
                if (moveDirection != 0) {
                    PlayerSprite.flipX = moveDirection < 0;
                    ChangeAnimationState(PlayerState.Run);
                }
                else {
                    ChangeAnimationState(PlayerState.Idle);
                }
            }
            else {
                PlayerSprite.sprite = playerJumpSprite;
                ChangeAnimationState(PlayerState.Jump);
                PlayerSprite.flipX = moveDirection < 0;
            }
        }

        public void ChangeAnimationState(PlayerState newState) {
            if (CurrentState == newState) return;
            PlayerAnimator.Play(newState.ToString());
            CurrentState = newState;
        }
    }
}