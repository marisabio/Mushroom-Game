using UnityEngine;
using UnityEngine.Events;

public class CheckItemEvent : MonoBehaviour
{
    [Header ("Inventory Controller")]
    [SerializeField] private InventoryController inventoryController;

    [Header ("Item Name")]
    [SerializeField] private string itemName;

    [Header ("Item Events")]
    [SerializeField] private UnityEvent rightItemEvent;
    [SerializeField] private UnityEvent wrongItemEvent;

    private InteractableController interactableController;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
    }

    public void UsingItem()
    {
        if (inventoryController.itemList.Contains(itemName))
        {
            inventoryController.RemoveItem(itemName);

            gameObject.tag = "Untagged";

            rightItemEvent.Invoke();

            Destroy(interactableController);
            Destroy(this);
        }
        else
        {
            wrongItemEvent.Invoke();
        }
        
    }
}
