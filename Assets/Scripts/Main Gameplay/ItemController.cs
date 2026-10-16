using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header ("Inventory Controller")]
    [SerializeField] private InventoryController inventoryController;

    [Header ("Item Settings")]
    [SerializeField] private string itemName;
    [SerializeField] private bool isItem;
    [SerializeField] private bool isRune;
    [SerializeField] private float itemDestroyTimer = 1f;
    [SerializeField] private GameObject itemObject;

    private InteractableController interactableController;
    
    private SphereCollider sphereCollider;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
        sphereCollider = GetComponent<SphereCollider>();

        ItemInInventoryCheck();
    }

    public void TakingItem()
    {
        inventoryController.AddItem(itemName);

        gameObject.tag = "Untagged";

        Invoke(nameof(DestroyItem), itemDestroyTimer);
    }

    public void LearningRune()
    {
        inventoryController.AddRune(itemName);

        gameObject.tag = "Untagged";

        Invoke(nameof(DestroyItem), itemDestroyTimer);
    }

    private void DestroyItem()
    {
        Destroy(itemObject);
        Destroy(interactableController);
        Destroy(sphereCollider);
    }

    private void ItemInInventoryCheck()
    {
        if (inventoryController.itemList.Contains(itemName))
        {
            gameObject.tag = "Untagged";

            Destroy(itemObject);
            Destroy(interactableController);
            Destroy(sphereCollider);
        }

        if (inventoryController.runeList.Contains(itemName))
        {
            gameObject.tag = "Untagged";

            Destroy(itemObject);
            Destroy(interactableController);
            Destroy(sphereCollider);
        }
    }

}
