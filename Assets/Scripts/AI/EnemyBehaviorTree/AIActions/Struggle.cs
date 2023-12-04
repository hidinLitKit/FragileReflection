using Dan;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

[System.Serializable]
public class Struggle : ActionNode
{
    EnemyController controller;
    Animator animator;

    protected override void OnStart()
    {
        controller = context.gameObject.GetComponent<EnemyController>();
        animator = controller.animator;

        if(!controller.CanSee() || Random.Range(0, 10) == 1)
        {
            context.agent.isStopped = true;
            animator.SetTrigger("Stuggle");

            controller._audioController.PlayAudio(false, FragileReflection.EnemySounds.Stuggle, true);
        }
            

        controller.DetectPlayer();
        blackboard.wasStuggled = true;
    }

    protected override void OnStop()
    {
        context.agent.isStopped = false;
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}

