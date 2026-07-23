using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Input Settings")] 
    public InputAction primaryMouseAction;
    public InputAction secondaryMouseAction;
    public InputAction enterDrawMode;

    [Header ("Gameplay Mode")]
    public bool drawMode;

    private NavMeshAgent agent;
    private Vector2 tapPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        EnableCharacterControl();
    }

    // Update is called once per frame
    void Update()
    {
        if (!drawMode)
        {
            PointControl();
        }

        if (enterDrawMode.WasPressedThisFrame() && !drawMode)
        {
            drawMode = true;
        }
        else if (enterDrawMode.WasPressedThisFrame() && drawMode)
        {
            drawMode = false;
        }
    }

    private void PointControl()
    {
        tapPoint = Pointer.current.position.ReadValue();

        RaycastHit hitInfo;

        if (primaryMouseAction.WasPressedThisFrame())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(tapPoint), out hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

    public void EnableCharacterControl()
    {
        primaryMouseAction.Enable();
        secondaryMouseAction.Enable();
        enterDrawMode.Enable();
    }

}
