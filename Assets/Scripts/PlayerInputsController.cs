using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 lookInput;
    [SerializeField] private PlayerInput playerInput;

    private void OnEnable()
    {
        movementInput = new Vector2(0.09f, 0.01f);
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Look"].performed += OnLook;
        playerInput.actions["Look"].canceled += OnLook;
        //playerInput.actions["Fire"].performed += OnFire;
        //playerInput.actions["Fire"].canceled += OnFire;
    }

    private void OnDisable()
    {
        playerInput.actions["Look"].performed -= OnLook;
        playerInput.actions["Look"].canceled -= OnLook;
        //playerInput.actions["Fire"].performed -= OnFire;
        //playerInput.actions["Fire"].canceled -= OnFire;
    }

    private void Update()
    {
        Move(movementInput);
        Rotate();
    }
    private void Move(Vector2 moveVector)
    {
        moveVector.Normalize();
        transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Rotate()
    {
        // Rotate around the Y-axis based on mouse delta X
        float rotationAmount = lookInput.x * lookSpeed;
        transform.Rotate(Vector3.up, rotationAmount);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Read the mouse delta
        lookInput = context.ReadValue<Vector2>();
    }
    //public void OnFire(InputAction.CallbackContext context)
    //{
    //    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    //}
}