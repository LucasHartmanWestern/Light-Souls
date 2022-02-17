using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    public string isInteractingBool; // Get name of Animator parameter
    public bool isInteractingStatus; // Get status of Animator parameter

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isInteractingBool, isInteractingStatus); // Set the Animator paramater pased on the function parameters
    }
}
