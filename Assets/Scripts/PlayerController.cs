using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health;
    [SerializeField] private float damageAmount;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Obtener el vector de movimiento del input
        moveInput = context.ReadValue<Vector2>();

    }
    private void FixedUpdate()
    {
        // Aplicar movimiento al Rigidbody2D
        rb.velocity = moveInput * speed;
    }

    void Start()
    {
        health = 100f;
    }
    private void Update()
    {
        Move();
        Fire();
        Damage(damageAmount);
    }

    public void Move()
    {
        //Debug.Log("The GameObject name is: " + gameObject.name + " | The Method name is: Move");

        var movH = Input.GetAxis("Horizontal");
        var movV = Input.GetAxis("Vertical");

        var movement = new Vector3(movH, 0, movV);
        transform.Translate(movement * speed * Time.deltaTime);
    }
    public void Fire()
    {

    }

    public void Damage(float amount)
    {
        //Debug.Log("The GameObject name is: " + gameObject.name + " | The Method name is: Damage");

        if (Input.GetKey(KeyCode.Z)) 
            health -= amount;
        
    }
}
