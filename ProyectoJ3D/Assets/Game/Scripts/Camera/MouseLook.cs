using UnityEngine;
using UnityEngine.InputSystem;  // New Input System

public class MouseLook : MonoBehaviour
{
    [Header("Refs")]
    public Transform playerBody;       // el objeto que rota horizontalmente (Player)

    [Header("Sensibilidad")]
    public float sensitivityX = 200f;
    public float sensitivityY = 200f;

    [Header("Límites verticales")]
    public float minY = -80f;
    public float maxY = 80f;

    float xRotation = 0f; // rotación acumulada vertical

    void Start()
    {
        // Bloquea el cursor en el centro y lo oculta
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // Delta del mouse este frame
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * sensitivityX * Time.deltaTime;
        float mouseY = mouseDelta.y * sensitivityY * Time.deltaTime;

        // Rotación vertical (mirar arriba/abajo) en la CÁMARA
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minY, maxY);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (girar a los lados) en el CUERPO del jugador
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
