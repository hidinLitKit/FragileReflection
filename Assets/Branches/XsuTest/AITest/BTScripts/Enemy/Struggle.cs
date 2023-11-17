using Dan;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

[System.Serializable]
public class Struggle : ActionNode
{

    Animator animator;

    protected override void OnStart()
    {
        context.agent.isStopped = true;
        animator = context.gameObject.GetComponent<EnemyController>().animator;
    }

    protected override void OnStop()
    {
        context.agent.isStopped = false;
    }

    protected override State OnUpdate()
    {
        if (!blackboard.isStruggled)
        {
            animator.SetBool("Struggle", false);
            return State.Failure;
        }
        else
        {
            animator.SetBool("Struggle", true);
            return State.Success;
        }
    }
}

