using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 25;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el proyectil impacta un enemigo o alguna otra cosa
        if (other.CompareTag("Enemy"))
        {
            // Suponiendo que el enemigo tenga un script que maneje daño
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject); // Destruye el proyectil al impactar
        }
    }
}
