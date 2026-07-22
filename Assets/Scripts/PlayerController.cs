using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Input Settings")] 
    public InputAction mouseAction;

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
        tapPoint = Pointer.current.position.ReadValue();
        Debug.Log(tapPoint);

        RaycastHit hitInfo;

        if (mouseAction.WasPressedThisFrame())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(tapPoint), out hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

    public void EnableCharacterControl()
    {
        mouseAction.Enable();
    }

}
