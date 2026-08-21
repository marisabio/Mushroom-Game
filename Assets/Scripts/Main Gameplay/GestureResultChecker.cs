using PDollarGestureRecognizer;
using UnityEngine;
using UnityEngine.Events;

public class GestureResultChecker : MonoBehaviour
{
    [Header ("Expected Gesture Result")]
    [SerializeField] private string gestureName;
    [SerializeField] private float gesturePrecision;

    [Header ("Player Gesture Controller")]
    [SerializeField] private GestureController gestureController;

    [Header ("Draw Mode Event")]
    [SerializeField] private UnityEvent startDrawMode;
    [SerializeField] private UnityEvent checkGestureResult;
    [SerializeField] private UnityEvent onResultMatch;

    private bool isResultBeingChecked;
    private InteractableController interactableController;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
    }

    void Update()
    {
        if (gestureController.isDrawModeOn && isResultBeingChecked)
        {
            checkGestureResult.Invoke();
        }
    }

    public void StartDrawModeOnGestureChecker()
    {
        startDrawMode.Invoke();
        isResultBeingChecked = true;
    }

    public void CheckFinalGestureResult()
    {
        if (gestureController.isCheckingResult)
        {
            if (gestureController.finalGestureResult == gestureName && gestureController.finalGestureScore >= gesturePrecision)
            {
                Debug.Log("Result matched!");

                gestureController.finalGestureResult = null;
                gestureController.finalGestureScore = 0;
                
                gameObject.tag = "Untagged";
                isResultBeingChecked = false;
                onResultMatch.Invoke();

                Destroy(interactableController);
                Destroy(this);
            }
            else
            {
                Debug.Log("Wrong gesture :c");
                isResultBeingChecked = false;
            }
        }
    }
}
