using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

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

    public TextMeshProUGUI watchOutText; // Referencia al objeto de texto de "watch out"
    public float textDisplayDuration = 2.0f;
    private TeleportationProvider teleportationProvider;
    private bool isTeleporting = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        teleportationProvider = FindObjectOfType<TeleportationProvider>();

        // Vincular la acción de salto
        jumpAction.action.performed += ctx => Jump();

        watchOutText.gameObject.SetActive(false);
        if (teleportationProvider != null)
        {
            teleportationProvider.beginLocomotion += OnBeginTeleportation;
            teleportationProvider.endLocomotion += OnEndTeleportation;
        }
    }

    void Update()
    {
        if (isTeleporting)
            return;
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

        if (transform.position.y < fallThreshold)
        {
            ShowWatchOutText();
            AudioManager.Instance.PlayFallSound();
            Respawn();
        }
        if (Input.GetMouseButtonDown(0)) // Por ejemplo, clic izquierdo para atacar
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f)) // Alcance del ataque: 2 unidades
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(10); // Daño que el jugador inflige al enemigo
                    }
                }
            }
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            // Aplicar la fuerza de salto
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);

            AudioManager.Instance.PlayJumpSound();
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

    private void ShowWatchOutText()
    {
        watchOutText.gameObject.SetActive(true);
        Invoke("HideWatchOutText", textDisplayDuration);
    }

    private void HideWatchOutText()
    {
        watchOutText.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // Desenlazar las acciones
        jumpAction.action.performed -= ctx => Jump();

        if (teleportationProvider != null)
        {
            teleportationProvider.beginLocomotion -= OnBeginTeleportation;
            teleportationProvider.endLocomotion -= OnEndTeleportation;
        }
    }

    private void OnBeginTeleportation(LocomotionSystem locomotionSystem)
    {
        isTeleporting = true;
        characterController.enabled = false; // Desactivar el CharacterController al comenzar la teletransportación
    }


    private void OnEndTeleportation(LocomotionSystem locomotionSystem)
    {
        isTeleporting = false;
        characterController.enabled = true;
    }
}
