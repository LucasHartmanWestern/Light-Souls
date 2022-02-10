using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator animator; // Reference to the players animator
    int horizontal;
    int vertical;

    // Called right before Start() method
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get animator of object script is attached to
        horizontal = Animator.StringToHash("Horizontal"); // Set int to be the value of horizontal in the animator
        vertical = Animator.StringToHash("Vertical"); // Set int to be the value of vertical in the animator
    }

    // Get the values from the input handler and pass them into the animation controller
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        // Add animation snapping
        #region Snapped Horizontal
        float snappedHorizontal;
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            snappedHorizontal = 0.5f;
        else if (horizontalMovement > 0.55f)
            snappedHorizontal = 1;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            snappedHorizontal = -0.5f;
        else if (horizontalMovement < -0.55f)
            snappedHorizontal = -1;
        else
            snappedHorizontal = 0;
        #endregion
        #region Snapped Vertical
        float snappedVertical;
        if (verticalMovement > 0 && verticalMovement < 0.55f)
            snappedVertical = 0.5f;
        else if (verticalMovement > 0.55f)
            snappedVertical = 1;
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
            snappedVertical = -0.5f;
        else if (verticalMovement < -0.55f)
            snappedVertical = -1;
        else
            snappedVertical = 0;
        #endregion

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime); // Set the horizontal float in the animator
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime); // Set the vertical float in the animator
    }
}
