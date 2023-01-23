using UnityEngine;
using UnityEngine.InputSystem;

namespace StealthPrototype.Characters
{
    /// <summary>
    /// Controls the player character's movement.
    /// </summary>
    public class PlayerCharacterMovementController : MonoBehaviour, ICharacterController
    {
        // Component cache fields
        private Camera _camera;

        // Asset fields
        [Header("Asset Fields")]
        [SerializeField] private MovementConfig movementConfig;
        
        // Fields
        [Header("Class Fields")]
        [SerializeField] private Vector3 movementDirection;

        private void FixedUpdate()
        {
            IncrementPosition();
            AdjustRotation();
        }

        private void IncrementPosition()
        {
            transform.position += movementDirection * (movementConfig.speed * Time.deltaTime);
        }
        private void AdjustRotation()
        {
            if (movementDirection == Vector3.zero) return; 
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(movementDirection), 
                360f/60f
            );
        }
        
        public void OnMovementInput(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementDirection = new Vector3(input.x, 0, input.y);
        }
    }
}