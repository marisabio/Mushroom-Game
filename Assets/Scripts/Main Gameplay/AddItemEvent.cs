using UnityEngine;

public class AddItemEvent : MonoBehaviour
{
    [Header ("Inventory Controller")]
    [SerializeField] private InventoryController inventoryController;

    [Header ("Item Name")]
    [SerializeField] private string itemName;

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

        Destroy(meshRenderer);
        Destroy(interactableController);
        Destroy(this);
    }

}
