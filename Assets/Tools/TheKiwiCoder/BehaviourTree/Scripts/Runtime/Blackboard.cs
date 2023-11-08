using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
	public class Blackboard
	{
        [Header("Monster 1")]
		public Transform target;
		public Vector3 moveToPosition;
        public bool isStruggled;
        public float viewRadius;
        public float viewAngle;
        public float attackDistance;
        public float moveSpeed;
        public float chaseSpeed;

        [Space]
        public GameCore game;

        public bool CanSeePlayer(Transform pos)
        {
            Vector3 direction = game.GetPlayer().transform.position - pos.position;
            float angle = Vector3.Angle(direction, pos.forward);
            if (direction.magnitude < viewRadius && angle < viewAngle)
            {
                return true;
            }
            return false;
        }

        public bool CanAttackPlayer(Transform pos)
        {
            Vector3 direction = game.GetPlayer().transform.position - pos.position;
            if (direction.magnitude < attackDistance)
            {
                return true;
            }
            return false;
        }
    }
}