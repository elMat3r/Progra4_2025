using UnityEngine;
using UnityEngine.InputSystem;
public class TurretRotation : MonoBehaviour
{
    public InputActionAsset action;
    private InputAction rotateAction;
    public float rotateSpd;
    private Vector2 rotatePos;
    private Rigidbody2D rb;
    private void OnEnable()
    {
        action.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        action.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        rotateAction = InputSystem.actions.FindAction("RotateTurret"); //Mantener el nombre de la accion del InputSystem
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rotatePos = rotateAction.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        RotateMove();
    }
    void RotateMove()
    {
        if (rotatePos.x != 0)
        {
            float rotationPos = -rotatePos.x * rotateSpd * Time.deltaTime;
            transform.Rotate(0, 0, rotationPos);
        }
    }
}