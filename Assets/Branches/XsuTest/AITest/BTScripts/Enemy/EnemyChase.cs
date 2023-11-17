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
        enemyController = context.gameObject.GetComponent<EnemyController>();
        player = enemyController.player;
        context.agent.destination = player.position;
        animator = enemyController.animator;
        animator.SetBool("Run", true);
    }

    protected override void OnStop()
    {
        context.agent.isStopped = true;
        animator.SetBool("Run", false);
    }

    protected override State OnUpdate()
    {
        context.agent.destination = player.position;

        //if (context.agent.pathPending)
        //{
        //    return State.Running;
        //}
        //if ( blackboard.CanAttackPlayer(context.agent.transform))
        //{
        //    return State.Success;
        //}
        if (enemyController.CanAttackPlayer())
        {
            return State.Success;
        }
        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Running;
    }
}

