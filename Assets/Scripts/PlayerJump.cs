using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5.0f;      // Fuerza del salto
    public float moveSpeed = 5.0f;      // Velocidad de movimiento
    private CharacterController characterController;
    private Vector3 velocity;            // Velocidad vertical del jugador
    private bool isGrounded = true;      // Verifica si está en el suelo
    public Transform respawnPoint;       // Punto de respawn
    public float fallThreshold = 0f;
    // Referencias a las acciones de salto y movimiento (Input Action)
    public InputActionReference jumpAction;
    public InputActionReference moveAction;
    public Transform cameraTransform;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Vincular la acción de salto
        jumpAction.action.performed += ctx => Jump();
    }

    void Update()
    {
        // Verificar si el jugador está en el suelo
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Un pequeño valor negativo para mantener el personaje en el suelo
        }

        // Aplicar gravedad manualmente
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // Obtener la entrada de movimiento
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = cameraTransform.right * input.x + cameraTransform.forward * input.y;
        move.y = 0;

        // Mover al jugador
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Aplicar la gravedad
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            // Aplicar la fuerza de salto
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }
    }
    private void Respawn()
    {
        // Mover al jugador al punto de respawn
        characterController.enabled = false; // Desactivar el CharacterController para evitar problemas de colisión
        transform.position = respawnPoint.position;
        characterController.enabled = true;  // Volver a activar el CharacterController
        velocity = Vector3.zero; // Restablecer la velocidad
    }

    private void OnDisable()
    {
        // Desenlazar las acciones
        jumpAction.action.performed -= ctx => Jump();
    }
}
