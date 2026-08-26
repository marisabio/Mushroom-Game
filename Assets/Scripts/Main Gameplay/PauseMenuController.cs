using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject pauseOverlay;

    void Start()
    {
        
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
            pauseOverlay.SetActive(true);
        }
        else if (!playerController.isPausing)
        {
            Time.timeScale = 1;
            playerController.EnablePlayerInput();
            pauseOverlay.SetActive(false);
        }
    }
    
}
