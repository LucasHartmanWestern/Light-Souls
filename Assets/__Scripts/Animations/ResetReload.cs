using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetReload : StateMachineBehaviour
{


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindObjectOfType<PlayerGeneral>().isReloading = false; // Set that player isn't reloading
        FindObjectOfType<PlayerGeneral>().startedReload = false; // Set that player isn't reloading
    }
}
