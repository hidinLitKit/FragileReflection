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

        enemyController._audioController.PlayAudio(true, FragileReflection.EnemySounds.Chasing, true);

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
        if(context.agent.remainingDistance <= context.agent.stoppingDistance)
        {
            context.agent.updateRotation = false;
            Vector3 lookPos = context.agent.destination - context.agent.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            context.agent.transform.rotation = Quaternion.Slerp(context.agent.transform.rotation, rotation, 10*Time.deltaTime);

        }
        else
        {
            context.agent.updateRotation = true;
        }
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

