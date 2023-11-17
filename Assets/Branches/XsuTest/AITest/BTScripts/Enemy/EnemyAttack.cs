using Dan;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

[System.Serializable]
public class EnemyAttack : ActionNode
{
    Animator animator;
    EnemyController controller;

    protected override void OnStart()
    {
        context.agent.isStopped = true;
        controller = context.gameObject.GetComponent<EnemyController>();
        animator = controller.animator;
    }

    protected override void OnStop()
    {
        context.agent.isStopped = false;
    }

    protected override State OnUpdate()
    {
        if(blackboard.isStruggled)
        {
            return State.Failure;
        }
        //if (blackboard.CanAttackPlayer(context.transform))
        //{
        //    animator.SetBool("Attack", true);
        //    controller.Attack();
        //    return State.Success;
        //}
        else
        {
            animator.SetBool("Attack", false);
            return State.Failure;
        }
    }
}

