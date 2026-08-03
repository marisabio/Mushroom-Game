using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header ("Player Camera")]
    [SerializeField] private CinemachineCamera playerCamera;

    [Header ("Temporary Camera")]
    [SerializeField] private CinemachineCamera tempCamera;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            playerCamera.Priority = 0;
            tempCamera = other.GetComponentInChildren<CinemachineCamera>();
            tempCamera.Priority = 10;
        }
        if (other.CompareTag("Untagged"))
        {
            playerCamera.Priority = 10;
            tempCamera = other.GetComponentInChildren<CinemachineCamera>();
            tempCamera.Priority = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            playerCamera.Priority = 10;
            tempCamera = other.GetComponentInChildren<CinemachineCamera>();
            tempCamera.Priority = 0;
        }
    }
}