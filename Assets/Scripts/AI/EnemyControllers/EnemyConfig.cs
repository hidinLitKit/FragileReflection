using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [CreateAssetMenu(fileName = "Data", menuName = "AIConfigs/RegularEnemy", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Enemy settings")]
        [Space]
        [Header("Detecting settings")]
        public LayerMask playerLayer;
        public LayerMask obstacleLayers;
        [Header("Attack")]
        public float attackDistance;
        public float attackDamage;
        [Header("Speed")]
        public float moveSpeed;
        public float chaseSpeed;
        [Header("Animation timers")]
        public float deathDuration;
        [Header("Chase delay")]
        public float chaseDelay;
    }
}
