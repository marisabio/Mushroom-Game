using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue Elements")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private TextMeshProUGUI actorNameText;
    [SerializeField] private PlayerController playerController;

    [Header("Dialogue Sets")]
    [SerializeField] private DialogueSet[] dialogueSets;
    [SerializeField] private int currentIndex;

    public float textSpeed;
    private int index;

    private DialogueLine[] CurrentLine => dialogueSets[currentIndex].lines;

    public void SelectDialogueSet(int index)
    {
        currentIndex = index;
    }

    public void StartDialogue()
    {
        if (dialogueSets == null || dialogueSets.Length == 0) return;

        if (!dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);
            playerController.DisableMouseInput();
            playerController.gameObject.transform.LookAt(gameObject.transform);
            speechText.text = string.Empty;
            index = 0;
            StartCoroutine(TypeLine());
        }
        else if (speechText.text == CurrentLine[index].text)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            speechText.text = CurrentLine[index].text;
        }
    }

    private IEnumerator TypeLine()
    {
        DialogueLine line = CurrentLine[index];
        actorNameText.text = line.actor;
        CameraDialogueOverride(line.camera);

        foreach (char c in line.text)
        {
            speechText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        CameraDialogueDisable(CurrentLine[index].camera);

        if (index < CurrentLine.Length - 1)
        {
            index++;
            speechText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            playerController.EnableMouseInput();
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
