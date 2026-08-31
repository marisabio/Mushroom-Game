using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header ("Player Controller")]
    [SerializeField] private PlayerController playerController;

    [Header ("Pause Menu Stuff")]
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject runeListOverlay;
    [SerializeField] private GameObject itemListOverlay;
    [SerializeField] private float pauseFadeMultiplier;

    private Image pauseOverlayImage;

    void Start()
    {
        pauseOverlayImage = pauseOverlay.GetComponent<Image>();
        pauseOverlayImage.color = new Color (pauseOverlayImage.color.r, pauseOverlayImage.color.g, pauseOverlayImage.color.b, 0f);
        pauseOverlay.SetActive(true);
        runeListOverlay.SetActive(false);
        itemListOverlay.SetActive(false);
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
            runeListOverlay.SetActive(true);
            itemListOverlay.SetActive(true);

            FadeIn();
            
        }
        else if (!playerController.isPausing)
        {
            Time.timeScale = 1;
            playerController.EnablePlayerInput();
            runeListOverlay.SetActive(false);
            itemListOverlay.SetActive(false);

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
}
