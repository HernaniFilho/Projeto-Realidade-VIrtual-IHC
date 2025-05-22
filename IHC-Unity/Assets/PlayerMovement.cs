using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player
    public float jumpForce = 5f; // Jump force of the player
    public Transform cameraTransform;

    private InputSystem_Actions inputActions;
    private Rigidbody rb;
    private Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Travar rotação nos 3 eixos
        rb.freezeRotation = true;
        inputActions = new InputSystem_Actions();

        // Registrar callbacks para a a��o Move
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // Movimento relativo � c�mera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignorar o eixo y para o movimento planar
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * moveInput.y + right * moveInput.x;

        if (move.magnitude > 1f)
            move.Normalize();

        Vector3 velocity = move * speed;
        velocity.y = rb.linearVelocity.y; // mantem a gravidade

        rb.linearVelocity = velocity;
    }
}
