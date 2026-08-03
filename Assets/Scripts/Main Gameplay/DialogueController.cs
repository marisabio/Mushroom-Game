using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;
    public PlayerController playerController;

    public string[] lines;
    public string[] names;
    public CinemachineCamera[] cameras;
    
    public float textSpeed;

    private int index;

    public void StartDialogue()
    {
        if (!dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);
            playerController.isPointControlEnabled = false;
            speechText.text = string.Empty;
            index = 0;
            StartCoroutine(TypeLine());
        }
        else if (speechText.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            speechText.text = lines[index];
        }
    }

    private IEnumerator TypeLine()
    {
        actorNameText.text = names[index];
        CameraDialogueOverride(cameras[index]);
        foreach (char c in lines[index].ToCharArray())
        {
            speechText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        CameraDialogueDisable(cameras[index]);
        
        if (index <lines.Length -1)
        {
            index++;
            speechText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            playerController.isPointControlEnabled = true;
            dialogueBox.SetActive(false);
        }
    }

    private void CameraDialogueOverride(CinemachineCamera camera)
    {
        camera.Priority = 15;
    }

    private void CameraDialogueDisable(CinemachineCamera camera)
    {
        camera.Priority = 0;
    }
}
