using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;

    [Header("Camera / Look")]
    [Tooltip("Cámara del jugador (normalmente hija del Player).")]
    public Camera playerCamera;

    // Sensibilidad fija, no modificable
    private const float mouseSensitivityX = 9f;
    private const float mouseSensitivityY = 9f;

    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;

    private CharacterController controller;
    private Animator anim;

    private Vector2 moveInput;
    private Vector3 velocity;

    private float cameraPitch = 0f;

    [SerializeField] private float animDamp = 0.05f;
    private float velXCur, velYCur;
    private float velXVel, velYVel; // velocidades internas para SmoothDamp

    private static readonly int VelX = Animator.StringToHash("velX");
    private static readonly int VelY = Animator.StringToHash("velY");

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Input de movimiento (New Input System)
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    // Input de salto
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (controller.isGrounded)
        {
            // Física del salto
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            // Animación de salto
            if (anim != null)
                anim.SetTrigger("Jump");
        }
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleGravity();
        HandleAnimations();
    }

    // Rotación con el mouse
    void HandleMouseLook()
    {
        if (Mouse.current == null || playerCamera == null) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // 0.02f para evitar que sea ultra rápido
        float mouseX = mouseDelta.x * mouseSensitivityX * 0.02f;
        float mouseY = mouseDelta.y * mouseSensitivityY * 0.02f;

        // Rotación horizontal del cuerpo
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical de la cámara
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    // Movimiento con CharacterController
    void HandleMovement()
    {
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y);

        // Movimiento relativo a la orientación del jugador
        Vector3 moveWorld = transform.TransformDirection(input);
        moveWorld.y = 0f;
        moveWorld.Normalize();

        controller.Move(moveWorld * moveSpeed * Time.deltaTime);
    }

    // Gravedad + grounded
    void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Informar al Animator si estamos en el suelo
        if (anim != null)
            anim.SetBool("IsGrounded", controller.isGrounded);
    }

    // Parámetros del Blend Tree (velX, velY)
    void HandleAnimations()
    {
        velXCur = Mathf.SmoothDamp(velXCur, moveInput.x, ref velXVel, animDamp);
        velYCur = Mathf.SmoothDamp(velYCur, moveInput.y, ref velYVel, animDamp);

        if (anim != null)
        {
            anim.SetFloat(VelX, velXCur);
            anim.SetFloat(VelY, velYCur);
        }
    }
}
