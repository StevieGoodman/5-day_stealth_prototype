using UnityEngine;
using UnityEngine.InputSystem;

namespace StealthPrototype.Characters
{
    /// <summary>
    /// Controls the player character's movement.
    /// </summary>
    public class PlayerMovementController : MonoBehaviour, IMovementController
    {
        // Asset fields
        [Header("Asset Fields")]
        [SerializeField] private MovementConfig movementConfig;
        
        // Fields
        [Header("Class Fields")]
        [SerializeField] private Vector3 moveDirection;

        private void FixedUpdate()
        {
            IncrementPosition();
            IncrementRotation();
        }

        public void IncrementPosition()
        {
            transform.position += moveDirection * (movementConfig.moveSpeed * Time.deltaTime);
        }
        
        public void IncrementRotation()
        {
            if (moveDirection == Vector3.zero) return; 
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(moveDirection), 
                movementConfig.rotateSpeed
            );
        }
        
        public void OnMovementInput(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            moveDirection = new Vector3(input.x, 0, input.y);
        }
    }
}