using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int maxLives = 3;    // Máximo número de vidas del jugador
    private int currentLives;   // Vidas actuales del jugador
    public int playerHealth = 100; // Salud máxima del jugador
    private int currentHealth;  // Salud actual del jugador

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentLives = maxLives;    // Inicializar vidas
        currentHealth = playerHealth; // Inicializar salud
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player recibió daño. Salud actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        currentLives--;
        Debug.Log("Player perdió una vida. Vidas restantes: " + currentLives);

        if (currentLives > 0)
        {
            Respawn();
        }
        else
        {
            GameOver();
        }
    }

    private void Respawn()
    {
        // Restablecer salud del jugador
        currentHealth = playerHealth;
        // Implementar la lógica para mover al jugador a un punto de respawn si es necesario
        Debug.Log("Respawning player...");
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        // Implementar lógica para reiniciar la escena o mostrar una pantalla de Game Over
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        // Reiniciar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetLives()
    {
        return currentLives;
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
