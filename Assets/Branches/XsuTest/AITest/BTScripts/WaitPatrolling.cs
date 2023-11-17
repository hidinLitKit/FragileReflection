using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class WaitPatrolling : ActionNode
{
    public float duration = 1;
    float startTime;
    protected override void OnStart() {
        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.canSeePlayer)
            return State.Failure;

        float timeRemaining = Time.time - startTime;
        if (timeRemaining > duration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
