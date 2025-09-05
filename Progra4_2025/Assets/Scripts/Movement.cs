using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;

    private Vector2 m_moveAim;
    private Vector2 m_lookAim;

    private Rigidbody2D m_Rigidbody;

    public float moveSpd;
    public float rotateSpd;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");

        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        m_moveAim = m_moveAction.ReadValue<Vector2>();
        m_lookAim = m_lookAction.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }
    private void Walking()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + (Vector2)transform.up * m_moveAim.y * moveSpd * Time.deltaTime);
    }
    private void Rotating()
    {
        if(m_moveAim.x != 0)
        {
            float rotationAmount = -m_moveAim.x * rotateSpd * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);
        }
    }
}
