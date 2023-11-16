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
        private const string crouchAnimation = "isCrouching";
        public void Aiming(bool aim)
        {
            playerAnim.SetBool(aimingAnimation, aim );
        }
        public void Walking(bool walk)
        {
            playerAnim.SetBool(walkingAnimation, walk);
        }
        public void Sprinting(bool run)
        {
            playerAnim.SetBool(sprintAnimation, run);
        }
        public void Crouching(bool crouch)
        {
            playerAnim.SetBool(crouchAnimation, crouch);
        }

        public void HandleAnimations()
        {

        }
    }
}
