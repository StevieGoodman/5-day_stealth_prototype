using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace StealthPrototype.Characters.Movement {

public class EnemyMovementController : MonoBehaviour, IMovementController {
    
    [Header("Asset Fields")]
    [SerializeField] private MovementConfig movement_config;
    
    [Header("Target Transforms")]
    [SerializeField] private Transform move_target;
    [SerializeField] private Transform look_target;
    
    [Header("Class Fields")] 
    [SerializeField] private Transform patrol_nodes_parent;
    [SerializeField] private bool can_move = true;
    private readonly TimeSpan patrol_delay = new(0, 0, 0, 1);
    private Queue<Transform> patrol_nodes;

    private void Awake() {
        patrol_nodes = new Queue<Transform>();
        foreach (Transform patrol_node in patrol_nodes_parent)
            patrol_nodes.Enqueue(patrol_node);
        set_next_target();
    }

    private async void FixedUpdate() {
        float distance_to_target = Vector2.Distance(move_target.position, transform.position);
        if (distance_to_target < 0.1f) {
            can_move = false;
            set_next_target();
            await Task.Delay(patrol_delay);
            can_move = true;
        }

        increment_position();
        increment_rotation();
    }

    public void increment_position() {
        if (!can_move) return;
        Vector3 move_vector = (move_target.position - transform.position).normalized;
        transform.position += move_vector * (movement_config.moveSpeed * Time.deltaTime);
    }

    public void increment_rotation() {
        Vector3 look_vector = (look_target.position - transform.position).normalized;
        if (look_vector == Vector3.zero) return;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(look_vector),
            movement_config.rotateSpeed
        );
    }

    /// <summary>
    /// Sets <see cref="move_target" /> & <see cref="look_target" /> to the position of the next patrol node.
    /// </summary>
    private void set_next_target() {
        Transform next_target = patrol_nodes.Dequeue();
        patrol_nodes.Enqueue(next_target);
        move_target = next_target;
        look_target = next_target;
    }

}

}