using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header ("Player Controller")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InventoryController inventoryController;

    [Header ("Pause Menu Stuff")]
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject runeListOverlay;
    [SerializeField] private GameObject itemListOverlay;
    [SerializeField] private float pauseFadeMultiplier;

    private Image pauseOverlayImage;
    private bool hasRun = false;

    void Start()
    {
        pauseOverlayImage = pauseOverlay.GetComponent<Image>();
        pauseOverlayImage.color = new Color (pauseOverlayImage.color.r, pauseOverlayImage.color.g, pauseOverlayImage.color.b, 0f);
        pauseOverlay.SetActive(true);
        runeListOverlay.SetActive(true);
        itemListOverlay.SetActive(true);
    }

    void Update()
    {
        PauseMenuState();
    }

    private void PauseMenuState()
    {
        if (playerController.isPausing)
        {
            Time.timeScale = 0;
            playerController.DisablePlayerInput();

            ShowInventoryItems();
            FadeIn();
            
        }
        else if (!playerController.isPausing)
        {
            Time.timeScale = 1;
            playerController.EnablePlayerInput();

            DisableInventoryItems();
            FadeOut();            
        }
    }

    private void FadeIn()
    {
        if (pauseOverlayImage.color.a < 0.35f)
        {
            pauseOverlayImage.color = new Color (pauseOverlayImage.color.r, pauseOverlayImage.color.g, pauseOverlayImage.color.b, pauseOverlayImage.color.a + 0.1f * pauseFadeMultiplier * Time.unscaledDeltaTime);
        }

    }

    private void FadeOut()
    {
        if (pauseOverlayImage.color.a > 0f)
        {
            pauseOverlayImage.color = new Color (pauseOverlayImage.color.r, pauseOverlayImage.color.g, pauseOverlayImage.color.b, pauseOverlayImage.color.a - 0.1f * pauseFadeMultiplier * Time.unscaledDeltaTime);
        }

    }

    void ShowInventoryItems()
    {
        if (!hasRun)
        {
            foreach (Transform item in transform.GetComponentsInChildren<Transform>(true))
            {
                Debug.Log(item.name);

                if ((inventoryController.itemList.Contains(item.name) && item.CompareTag("Item")) || (inventoryController.runeList.Contains(item.name) && item.CompareTag("Rune")))
                {
                    item.gameObject.SetActive(true); 
                }

            }

            hasRun = true;
        }
    }

    void DisableInventoryItems()
    {
        if (hasRun)
        {
            foreach (Transform item in transform.GetComponentsInChildren<Transform>(true))
            {
                Debug.Log(item.name);

                if ((inventoryController.itemList.Contains(item.name) && item.CompareTag("Item")) || (inventoryController.runeList.Contains(item.name) && item.CompareTag("Rune")))
                {
                    item.gameObject.SetActive(false); 
                }

            }

            hasRun = false;
        }

    }
}
