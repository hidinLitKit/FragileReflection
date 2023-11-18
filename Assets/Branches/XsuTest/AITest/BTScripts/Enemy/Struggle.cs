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
        context.agent.isStopped = true;
        controller = context.gameObject.GetComponent<EnemyController>();
        animator = controller.animator;
        animator.SetTrigger("Stuggle");
        controller.DetectPlayer();
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

