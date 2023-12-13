using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        private const string pistolShoot = "PistolShoot";
        private const string pistolReload = "PistolReload";
        private const string meleeAttack = "MeleeAttack";

        /* int id_attack = Animator.StringToHash("Attack");
        animator.SetFloat(id_attack, 1); */

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
        public void Death()
        {
            playerAnim.SetTrigger(deathAnimation);
        }
        public void PistolShoot()
        {
            playerAnim.SetTrigger(pistolShoot);
        }
        public void PistolReload()
        {
            playerAnim.SetTrigger(pistolReload);
        }
        public void MeleeAttack()
        {
            playerAnim.SetTrigger(meleeAttack);
        }
        public void HandleAnimations()
        {

        }
    }
}
