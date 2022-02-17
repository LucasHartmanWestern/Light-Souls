using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator; // Reference to the players animator
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
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
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

        if (isSprinting && Mathf.Abs(verticalMovement) > 0.55f) { snappedVertical = 2; } // Set vertical to 2 if player is sprinting to change the animation

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime); // Set the horizontal float in the animator
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime); // Set the vertical float in the animator
    }

    // Play any animation sent via the targetAnimation parameter
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting); // Set the isInteracting variable on the Animator to match the parameter
        animator.CrossFade(targetAnimation, 0.2f); // Play animation specified by parameter
    }

    // Change the weight of the animation layer for aiming
    public void PlayerAim(float layerWeight)
    {
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), layerWeight, Time.deltaTime * 10f)); // Smoothly transition animation to aiming or not aiming
    }

    // Apply root motion of animation (only triggered for certain animations)
    public void AplpyRootMotion()
    {
        animator.applyRootMotion = true;
    }

    // Check if an animation is playing
    public bool AnimationPlaying(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(1).IsName(animationName); // Check if an animation is playing
    }
}
