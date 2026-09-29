using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    [SerializeField] int levelIndex;
    [SerializeField] private GameObject portalOverlay;
    [SerializeField] private float portalFadeMultiplier;

    private Image portalOverlayImage;
    private bool newScene;
    private bool beginSceneTransition = false;
    private bool isNextSceneReady = false;

    void Start()
    {
        portalOverlayImage = portalOverlay.GetComponent<Image>();
        portalOverlayImage.color = new Color (portalOverlayImage.color.r, portalOverlayImage.color.g, portalOverlayImage.color.b, 1f);
        portalOverlay.SetActive(true);

        newScene = true;
    }

    void Update()
    {
        if (newScene)
        {
            PortalFadeIn();
        }

        if (beginSceneTransition)
        {
            PortalFadeOut();

            if (isNextSceneReady)
            {
                SceneManager.LoadSceneAsync(levelIndex);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            beginSceneTransition = true;
        }
    }

    private void PortalFadeOut()
    {
        if (portalOverlayImage.color.a < 1f && !isNextSceneReady)
        {
            portalOverlayImage.color = new Color (portalOverlayImage.color.r, portalOverlayImage.color.g, portalOverlayImage.color.b, portalOverlayImage.color.a + 0.1f * portalFadeMultiplier * Time.deltaTime); 
            
            if (portalOverlayImage.color.a >= 1f)
            {
                isNextSceneReady = true;
            }

        }
    }

    private void PortalFadeIn()
    {
        if (portalOverlayImage.color.a > 0f)
        {
            portalOverlayImage.color = new Color (portalOverlayImage.color.r, portalOverlayImage.color.g, portalOverlayImage.color.b, portalOverlayImage.color.a - 0.1f * portalFadeMultiplier * Time.deltaTime); 
        }

        if (portalOverlayImage.color.a <= 0f)
        {
            newScene = false;
            Debug.Log(newScene);
        }
        
    }

}
