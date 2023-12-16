using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class TakeDamageDecorator : DecoratorNode
{
    EnemyController controller;

    protected override void OnStart() {
        controller = context.agent.GetComponent<EnemyController>();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (!controller.IsStuggled())
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
