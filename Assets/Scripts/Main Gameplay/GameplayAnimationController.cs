using UnityEngine;

public class GameplayAnimationController : MonoBehaviour
{
    PlayerController playerController;
    Animator animator;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerController.agent.pathPending)
        {
            animator.SetBool("isWalking", true);
            if (playerController.agent.remainingDistance <= playerController.agent.stoppingDistance)
            {
                if (!playerController.agent.hasPath || playerController.agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
    }
}
