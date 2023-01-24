using UnityEngine;
using UnityEngine.InputSystem;

namespace StealthPrototype.Characters {

/// <summary>
/// Controls the player character's movement.
/// </summary>
public class PlayerMovementController : MonoBehaviour, IMovementController {

    // Asset fields
    [Header("Asset Fields")] 
    [SerializeField] private MovementConfig movement_config;

    // Fields
    [Header("Class Fields")] 
    [SerializeField] private Vector3 move_direction;

    private void FixedUpdate() {
        increment_position();
        increment_rotation();
    }

    public void increment_position() {
        transform.position += move_direction * (movement_config.moveSpeed * Time.deltaTime);
    }

    public void increment_rotation() {
        if (move_direction == Vector3.zero) return;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(move_direction),
            movement_config.rotateSpeed
        );
    }

    public void OnMovementInput(InputAction.CallbackContext context) {
        Vector2 input = context.ReadValue<Vector2>();
        move_direction = new Vector3(input.x, 0, input.y);
    }

}

}