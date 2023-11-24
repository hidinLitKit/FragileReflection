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
    bool isAttacking;

    protected override void OnStart()
    {
        context.agent.isStopped = true;
        context.agent.speed = 0;
        context.agent.stoppingDistance = blackboard.attackDistance;
        controller = context.gameObject.GetComponent<EnemyController>();
        context.agent.destination = controller.player.position;
        animator = controller.animator;
        //if(animator.GetBool("Attack") != true)
        //    animator.SetBool("Attack", true);
        animator.SetTrigger("Attack");
        controller.AttackPlayer();
        controller.DetectPlayer();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(controller.IsStuggled() || !controller.CanSee())
        {
            return State.Failure;
        }

        if(isAttacking)
            return State.Running;
        else
            return State.Success;
    }
}

