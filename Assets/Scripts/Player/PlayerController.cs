using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [Header("Elements")]
    [SerializeField] private Joystick joystick;
    [Header("Settings")]
    [SerializeField] private float speed = 15f;
    private Rigidbody2D rb;
    private InputAction moveAction;


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb.linearVelocity = Vector2.right;
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update() {

    }
    void FixedUpdate() {
        rb.linearVelocity = GetMovementInput() * speed;
    }
    private Vector2 GetMovementInput() {
        Vector2 joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        Vector2 keyboardInput = moveAction.ReadValue<Vector2>();

        if (joystickInput.sqrMagnitude > 0.01f) return joystickInput;
        return keyboardInput;
    }
}
