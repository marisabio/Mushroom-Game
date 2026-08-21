using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Input Settings")] 
    public InputAction primaryMouseAction;
    public InputAction secondaryMouseAction;
    public InputAction interactAction;

    [Header ("Gameplay Mode")]
    public bool drawMode;
    public bool isPointControlEnabled = true;
    
    [HideInInspector] public NavMeshAgent agent;
    private Vector2 tapPoint;
    private bool isInteracting = false;
    private bool canInteract = false;

    void OnEnable()
    {
        EnablePlayerInput();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!drawMode)
        {
            CharacterControl();
        }
    }

    private void CharacterControl()
    {
        PointControl();
        InteractControl();
    }

    private void PointControl()
    {
        tapPoint = Pointer.current.position.ReadValue();

        RaycastHit hitInfo;

        if (primaryMouseAction.WasPressedThisFrame() && isPointControlEnabled)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(tapPoint), out hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

    private void InteractControl()
    {
        if (interactAction.WasPressedThisFrame() && canInteract)
        {
            isInteracting = true;
        }
    }

    public void EnableDrawMode()
    {
        drawMode = true;
    }

    public void DisableDrawMode()
    {
        drawMode = false;
    }

    public void EnablePlayerInput()
    {
        primaryMouseAction.Enable();
        secondaryMouseAction.Enable();
        interactAction.Enable();
    }

    public void DisablePlayerInput()
    {
        primaryMouseAction.Disable();
        secondaryMouseAction.Disable();
        interactAction.Disable();
    }

    void OnDisable()
    {
        DisablePlayerInput();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = true;

            if (isInteracting)
            {
                other.GetComponent<InteractableController>().Interact();
                isInteracting = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = false;
        }
    }

}
