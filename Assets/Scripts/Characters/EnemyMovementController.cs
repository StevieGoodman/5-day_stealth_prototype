using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace StealthPrototype.Characters
{
    class EnemyMovementController : MonoBehaviour, IMovementController
    {
        [Header("Asset Fields")]
        [SerializeField] private MovementConfig movementConfig;
        
        [Header("Target Transforms")]
        [SerializeField] private Transform moveTarget;
        [SerializeField] private Transform lookTarget;
        
        [Header("Class Fields")]
        [SerializeField] private Transform patrolNodesParent;
        [SerializeField] private bool canMove = true;
        private Queue<Transform> _patrolNodes;
        private readonly TimeSpan _patrolDelay = new (0, 0, 0, 1);
        
        private void Awake()
        {
            _patrolNodes = new Queue<Transform>();
            foreach (Transform patrolNode in patrolNodesParent)
            {
                _patrolNodes.Enqueue(patrolNode);
            }
            SetNextTarget();
        }

        private async void FixedUpdate()
        {
            float distanceToTarget = Vector2.Distance(moveTarget.position, transform.position);
            if (distanceToTarget < 0.1f)
            {
                canMove = false;
                SetNextTarget();
                await Task.Delay(_patrolDelay);
                canMove = true;
            }
            IncrementPosition();
            IncrementRotation();
        }
        
        /// <summary>
        /// Sets <see cref="moveTarget"/> & <see cref="lookTarget"/> to the position of the next patrol node.
        /// </summary>
        private void SetNextTarget()
        {
            Transform nextTarget = _patrolNodes.Dequeue();
            _patrolNodes.Enqueue(nextTarget);
            moveTarget = nextTarget;
            lookTarget = nextTarget;
        }

        public void IncrementPosition()
        {
            if (!canMove) return;
            Vector3 moveVector = (moveTarget.position - transform.position).normalized;
            transform.position += moveVector * (movementConfig.moveSpeed * Time.deltaTime);
        }
        
        public void IncrementRotation()
        {
            Vector3 lookVector = (lookTarget.position - transform.position).normalized;
            if (lookVector == Vector3.zero) return; 
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(lookVector), 
                movementConfig.rotateSpeed
            );
        }
    }
}