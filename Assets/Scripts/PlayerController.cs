using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 15f;
    [SerializeField] private Joystick joystick;
    private Rigidbody2D _rb;
    private InputAction _moveAction;


    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _rb.linearVelocity = Vector2.right;
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update() {

    }
    void FixedUpdate() {
        _rb.linearVelocity = GetMovementInput() * speed;
    }
    private Vector2 GetMovementInput() {
        Vector2 joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        Vector2 keyboardInput = _moveAction.ReadValue<Vector2>();

        if (joystickInput.sqrMagnitude > 0.01f) return joystickInput;
        return keyboardInput;
    }
}
