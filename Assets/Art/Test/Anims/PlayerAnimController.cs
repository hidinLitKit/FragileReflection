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
        private const string walkingBlendX = "WalkXdir";
        private const string walkingBlendY = "WalkYdir";
        private const string sprintAnimation = "isRunning";
        private const string crouchAnimation = "isCrouching";

        private const string deathAnimation = "isDeath";
        public void Aiming(bool aim)
        {
            playerAnim.SetBool(aimingAnimation, aim );
        }
        public void Walking(bool walk)
        {
            playerAnim.SetBool(walkingAnimation, walk);
        }
        public void WalkDir(float dirx, float diry)
        {
            float blendX = 1;
            blendX += dirx;
            float blendY = diry >0.5f ? 1 : 0;

            
            playerAnim.SetFloat(walkingBlendX, blendX, 0.25f , Time.deltaTime);
            playerAnim.SetFloat(walkingBlendY, blendY, 0.5f,  Time.deltaTime);
        }
        public void Sprinting(bool run)
        {
            playerAnim.SetBool(sprintAnimation, run);
        }
        public void Crouching(bool crouch)
        {
            playerAnim.SetBool(crouchAnimation, crouch);
        }

        public void Death(bool die)
        {
            playerAnim.SetBool(deathAnimation, die);
        }

        public void HandleAnimations()
        {

        }
    }
}
