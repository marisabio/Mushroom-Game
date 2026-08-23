using UnityEngine;

public class AddItemEvent : MonoBehaviour
{
    [Header ("Inventory Controller")]
    [SerializeField] private InventoryController inventoryController;

    [Header ("Item Settings")]
    [SerializeField] private string itemName;
    [SerializeField] private float itemDestroyTimer = 1f;

    private InteractableController interactableController;
    private MeshRenderer meshRenderer;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void TakingItem()
    {
        inventoryController.AddItem(itemName);

        gameObject.tag = "Untagged";

        Invoke(nameof(DestroyItem), itemDestroyTimer);
    }

    private void DestroyItem()
    {
        Destroy(meshRenderer);
        Destroy(interactableController);
        Destroy(this);
    }

}
