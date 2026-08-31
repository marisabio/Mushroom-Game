using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuController : MonoBehaviour
{
    [Header ("Player Controller")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerController playerController;

    private bool hasRun;

    void Start()
    {

    }

    void Update()
    {
        if (playerController.isPausing && !hasRun)
        {
            foreach (Transform item in transform.GetComponentsInChildren<Transform>())
            {
                Debug.Log(item.name);

                if (inventoryController.itemList.Contains(item.name) || inventoryController.runeList.Contains(item.name))
                {
                    item.gameObject.SetActive(true); 
                }
                else
                {
                    item.gameObject.SetActive(false); 
                }
            }

            hasRun = true;
        }
        else if (!playerController.isPausing)
        {
            hasRun = false;
        }

    }
}
