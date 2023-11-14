using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class PlayerAnimController : MonoBehaviour
    {
        [SerializeField] Animator playerAnim;
        private const string aimingAnimation = "isAiming";
        private const string walkingAnimation = "isWalking";
        private const string sprintAnimation = "isRunning";
        public void Aiming(float aim)
        {
            playerAnim.SetBool(aimingAnimation, aim == 1);
        }
        public void Walking(float walk)
        {
            playerAnim.SetBool(walkingAnimation, walk != 0);
        }
        public void Running(float run)
        {
            playerAnim.SetBool(sprintAnimation, run == 1);
        }
    }
}
