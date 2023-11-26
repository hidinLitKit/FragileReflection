using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class PreAgreeState : ActionNode
{
    Animator animator;
    Transform player;
    EnemyController enemyController;

    protected override void OnStart()
    {
        context.agent.isStopped = true;
        enemyController = context.gameObject.GetComponent<EnemyController>();
        player = enemyController.player;
        context.agent.destination = player.position;
        animator = enemyController.animator;
        if(blackboard.wasStuggled)
            blackboard.wasStuggled = false;
        else
            animator.SetTrigger("Angree");
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
