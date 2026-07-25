using System;
using PDollarGestureRecognizer;
using UnityEngine;

public class GestureResultChecker : MonoBehaviour
{
    [Header ("Expected Gesture Result")]
    [SerializeField] private String gestureName;
    [SerializeField] private float gesturePrecision;

    [Header ("Player Gesture Controller")]
    [SerializeField] private GestureController gestureController;

    private InteractableController interactableController;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
    }

    void Update()
    {
        if (gestureController.finalGestureResult == gestureName && gestureController.finalGestureScore >= gesturePrecision)
        {
            Debug.Log("Result matched!");

            gestureController.finalGestureResult = null;
            gestureController.finalGestureScore = 0;
            
            gameObject.tag = "Untagged";

            Destroy(interactableController);
            Destroy(this);
        }
        else
        {
            // Debug.Log("Wrong gesture :c");
        }
    }
}
