using UnityEngine;

public class ItemController : MonoBehaviour, IDataPersistance
{
    [Header ("Inventory Controller")]
    [SerializeField] private InventoryController inventoryController;

    [Header ("Item Settings")]
    [SerializeField] private string itemName;
    [SerializeField] private bool isItem;
    [SerializeField] private bool isRune;
    [SerializeField] private float itemDestroyTimer = 1f;

    private InteractableController interactableController;
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
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
        Destroy(meshRenderer);
        Destroy(interactableController);
        Destroy(sphereCollider);
    }

    public void LoadData(GameData data)
    {
        if (data.itemsCollected.Contains(itemName))
        {
            inventoryController.AddItem(itemName);
            gameObject.tag = "Untagged";

            Destroy(meshRenderer);
            Destroy(interactableController);
            Destroy(sphereCollider);
        }

        if (data.runesCollected.Contains(itemName))
        {
            inventoryController.AddRune(itemName);
            gameObject.tag = "Untagged";

            Destroy(meshRenderer);
            Destroy(interactableController);
            Destroy(sphereCollider);
        }
    }

    public void SaveData(GameData data) { }

}
