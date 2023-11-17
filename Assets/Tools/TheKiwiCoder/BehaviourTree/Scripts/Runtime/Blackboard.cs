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
        public bool canAttack;
        public bool canSeePlayer;
        //public bool canAttack;
        //public bool canSeePlayer;
        public float moveSpeed;
        public float chaseSpeed;

    }
}