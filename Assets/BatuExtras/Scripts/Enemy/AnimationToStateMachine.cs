using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public AttackState attackState;

    private void TriggerAttack()
    {

        attackState.TriggerAttack();
    }

    private void FinishrAttack()
    {
        attackState.FinishAttack();

    }
}
