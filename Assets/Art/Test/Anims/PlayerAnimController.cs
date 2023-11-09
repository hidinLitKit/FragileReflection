using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class PlayerAnimController : MonoBehaviour
    {
        [SerializeField] Animator playerAnim;
        
        public void Aiming(float aim)
        {
            if (aim == 1) playerAnim.SetBool("isAiming", true);
            else playerAnim.SetBool("isAiming", false);
        }

        public void Walking(float walk)
        {
            if (walk == 1) 
                playerAnim.SetBool("isWalk", true);
            else 
                playerAnim.SetBool("isWalk", false);
        }
    }
}
