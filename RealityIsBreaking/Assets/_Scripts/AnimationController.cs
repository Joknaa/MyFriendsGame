using UnityEngine;

namespace Reality {
    public enum PlayerState {
        Idle,
        Jump,
        Walk,
        Run
    }

    public class AnimationController : MonoBehaviour {
        [SerializeField] private Sprite playerJumpSprite;
        private PlayerState CurrentState;
        private PlayerMovement MovementController;
        private Animator PlayerAnimator;
        private SpriteRenderer PlayerSprite;

        private void Start() {
            PlayerSprite = GetComponent<SpriteRenderer>();
            PlayerAnimator = GetComponent<Animator>();
            MovementController = GetComponent<PlayerMovement>();
            //CharacterController2D = GetComponent<CharacterController2D>();
        }

        private void Update() {
            var moveDirection = MovementController.GetHorizontalMovement();
            var isGrounded = MovementController.isGrounded();

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