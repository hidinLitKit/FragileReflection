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


    protected override void OnStart()
    {
        context.agent.isStopped = false;
        context.agent.speed = blackboard.chaseSpeed;
        player = context.gameObject.GetComponent<EnemyController>().player;
        context.agent.destination = player.position;
        animator = context.gameObject.GetComponent<EnemyController>().animator;
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
        //if (blackboard.CanSeePlayer(context.agent.transform))
        //{
        //    return State.Running;
        //}
        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Failure;
    }
}

