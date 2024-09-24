using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint;        // Punto desde donde se dispara
    public float shootForce = 500f;     // Fuerza del disparo

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Verifica si se presiona el botón de disparo
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Instanciar el proyectil
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Aplicar una fuerza al proyectil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootPoint.forward * shootForce);
        }
    }
}
