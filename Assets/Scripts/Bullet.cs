using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float lifetime = 5f; // Time before the bullet is destroyed
    public int damage = 1; // Damage dealt by the bullet

    private void Start()
    {
        if (IsServer)
        {
            // Destroy the bullet after a set lifetime to avoid clutter
            Destroy(gameObject, lifetime);
        }
    }

    private void Update()
    {
        if (IsServer)
        {
            // Move the bullet forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsServer)
        {
            // Check if the collision is with a player that is not the local player
            if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<NetworkObject>().IsOwner)
            {
                // Get the PlayerController component and apply damage
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }

                // Destroy the bullet upon collision
                Destroy(gameObject);
            }
        }
    }
}