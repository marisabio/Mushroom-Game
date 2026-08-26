using System.Collections;
using UnityEngine;

public class GameplayAnimationController : MonoBehaviour
{
    [Header ("Animation Timers")]
    [SerializeField] private float takingItemTime;

    private PlayerController playerController;
    private InventoryController inventoryController;
    private Animator animator;

    private int itemCount;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        inventoryController = GetComponentInParent<InventoryController>();
        animator = GetComponent<Animator>();

        itemCount = inventoryController.itemList.Count;
    }

    void Update()
    {
        WalkingAnimationState();
        TakingItemAnimationState();
        WavingWandAnimation();
    }

    private void WalkingAnimationState()
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

    private void TakingItemAnimationState()
    {
        int currentItemCount = inventoryController.itemList.Count;

        if (itemCount != currentItemCount)
        {
            animator.SetBool("isTakingItem", true);
            animator.Play("Taking Item");
            StartCoroutine(TakingItemAnimationTimer());
            itemCount = inventoryController.itemList.Count;
        }
        else if (itemCount == currentItemCount)
        {
            animator.SetBool("isTakingItem", false);
        }

    }

    private IEnumerator TakingItemAnimationTimer()
    { 
        playerController.DisableMouseInput();
        yield return new WaitForSeconds(takingItemTime);
        playerController.EnableMouseInput();
    }

    private void WavingWandAnimation()
    {
        if (playerController.drawMode)
        {
            animator.SetBool("isWaving", true);
        }
        else
        {
            animator.SetBool("isWaving", false);
        }
    }

}
