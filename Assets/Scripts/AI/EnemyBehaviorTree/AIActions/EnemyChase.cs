using Dan;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

[System.Serializable]
public class EnemyChase : ActionNode
{
    Animator animator;
    Transform player;
    EnemyController enemyController;

    protected override void OnStart()
    {
        context.agent.isStopped = false;
        context.agent.speed = blackboard.chaseSpeed;
        context.agent.stoppingDistance = blackboard.attackDistance;
        enemyController = context.gameObject.GetComponent<EnemyController>();
        player = enemyController.player;
        context.agent.destination = player.position;
        animator = enemyController.animator;
        if (animator.GetBool("Run") != true)
            animator.SetBool("Run", true);
    }

    protected override void OnStop()
    {
        context.agent.isStopped = true;
        if (animator.GetBool("Run") != false)
            animator.SetBool("Run", false);
    }

    protected override State OnUpdate()
    {
        context.agent.destination = player.position;

        if (enemyController.CanAttackPlayer())
        {
            return State.Success;
        }
        if(!enemyController.CanSee())
        {
            return State.Failure;
        }
        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Running;
    }
}

