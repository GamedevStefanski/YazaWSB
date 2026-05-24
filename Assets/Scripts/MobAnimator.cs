using UnityEngine;
using Spine.Unity;

public class MobAnimator : MonoBehaviour
{
    [Header("Component References")]
    public SkeletonAnimation skeletonAnim;
    public MobMovement movementScript;
    public MobCombat combatScript;
    public MobStats statsScript;

    [Header("Exact Spine Animation Names")]
    public string idleAnim = "idle";
    public string walkAnim = "walk";
    public string attackAnim = "attack";
    public string deathAnim = "death";

    // Variable holding the currently playing animation
    private string currentAnimation;
    private bool isDead = false;

    void Awake()
    {
        // Automatically assign components if you forget to drag them in the Inspector
        if (skeletonAnim == null) skeletonAnim = GetComponent<SkeletonAnimation>();
        if (movementScript == null) movementScript = GetComponent<MobMovement>();
        if (combatScript == null) combatScript = GetComponent<MobCombat>();
        if (statsScript == null) statsScript = GetComponent<MobStats>();
        if (skeletonAnim != null)
        {
            skeletonAnim.timeScale = Random.Range(0.9f, 1.1f);
        }
    }

    void Update()
    {
        // If the character is dead, ignore the rest of the logic
        if (isDead) return;

        // 1. Check for death
        if (statsScript.currentHealth <= 0)
        {
            ChangeAnimation(deathAnim, false); // false, because the death animation should only play once
            isDead = true;
            return;
        }

        // 2. Check for combat
        if (combatScript.fighting)
        {
            ChangeAnimation(attackAnim, true); // true, character loops attacks as long as combat lasts
        }
        // 3. Check for movement (if not fighting and not stopped)
        else if (!movementScript.isStopped && movementScript.moveSpeed > 0f)
        {
            ChangeAnimation(walkAnim, true);
        }
        // 4. Idle state - default when nothing else is happening
        else
        {
            ChangeAnimation(idleAnim, true);
        }
    }

    // Helper function that prevents calling the same animation every frame
    private void ChangeAnimation(string newAnimName, bool loop)
    {
        // If the new animation is the same as the current one, do nothing
        if (currentAnimation == newAnimName) return;

        // Play the animation in Spine and update the variable
        skeletonAnim.AnimationState.SetAnimation(0, newAnimName, loop);
        currentAnimation = newAnimName;
    }
}