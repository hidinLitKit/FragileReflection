using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class TakeDamageDecorator : DecoratorNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (!blackboard.isStruggled)
        {
            if (child is { state: State.Running })
            {
                child.Abort();
            }

            return State.Failure;
        }

        return child?.Update() ?? State.Success;
    }
}
