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
        WavingWandAnimation();
    }

    private void WalkingAnimationState()
    {
        if (!playerController.agent.pathPending)
        {
            animator.SetBool("isWalking", true);
            
            if (playerController.agent.remainingDistance <= playerController.agent.stoppingDistance)
            {
                if (!playerController.agent.hasPath || playerController.agent.velocity.sqrMagnitude == 0f || playerController.agent.isPathStale)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
    }

    public void TakingItemAnimationState()
    {
        animator.Play("Taking Item");
        StartCoroutine(TakingItemAnimationTimer());
        itemCount = inventoryController.itemList.Count;
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
