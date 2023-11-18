using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CanAttackDecorator : DecoratorNode
{
    EnemyController enemyController;
    protected override void OnStart() {
        enemyController = context.gameObject.GetComponent<EnemyController>();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (!enemyController.CanAttackPlayer())
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
