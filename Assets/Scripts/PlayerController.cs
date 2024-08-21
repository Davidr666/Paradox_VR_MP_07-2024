using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public Transform firePoint; // Transform from where bullets are fired
    public GameObject bulletPrefab; // Bullet prefab
    public NetworkVariable<int> playerHealth = new NetworkVariable<int>(100); // Player health

    private void Start()
    {
        if (IsOwner)
        {
            //UpdateHealthText();
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        // Handle movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // Handle fire input
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (IsServer)
        {
            // Create and spawn the bullet on the server
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletInstance.GetComponent<NetworkObject>().Spawn();
            Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * 10f; // Bullet speed
        }
        else
        {
            ShootServerRpc(firePoint.position, firePoint.rotation);
        }
    }

    [ServerRpc]
    void ShootServerRpc(Vector3 position, Quaternion rotation)
    {
        // Create and spawn the bullet on the server
        GameObject bulletInstance = Instantiate(bulletPrefab, position, rotation);
        bulletInstance.GetComponent<NetworkObject>().Spawn();
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 10f; // Bullet speed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !IsOwner)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsServer)
        {
            playerHealth.Value -= damage;
            //UpdateHealthText();
        }
    }

}
