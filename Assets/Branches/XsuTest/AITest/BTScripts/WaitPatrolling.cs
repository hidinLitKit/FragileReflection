using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class WaitPatrolling : ActionNode
{
    public float duration = 1;
    float startTime;
    EnemyController controller;
    Animator animator;

    protected override void OnStart() {
        startTime = Time.time;
        controller = context.agent.GetComponent<EnemyController>();
        animator = controller.animator;
        animator.SetBool("Run", false);
        animator.SetBool("Move", false);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(controller.IsStuggled())
        {
            return State.Failure;
        }
        if (controller.CanSee())
            return State.Failure;

        float timeRemaining = Time.time - startTime;
        if (timeRemaining > duration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
