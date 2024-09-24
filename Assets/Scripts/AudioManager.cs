using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioClip jumpSound;
    public AudioClip fallSound;

    // Propiedad para acceder a la instancia
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Busca la instancia en la escena
                instance = FindObjectOfType<AudioManager>();

                // Si no hay ninguna, crea un nuevo objeto
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(AudioManager).Name);
                    instance = singletonObject.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void PlayFallSound()
    {
        if (fallSound != null)
        {
            audioSource.PlayOneShot(fallSound);
        }
    }

    private void Awake()
    {
        // Asegúrate de que no haya otra instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evita que el objeto se destruya al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Destruye la nueva instancia
        }
    }

    // Aquí puedes agregar referencias a AudioSource, listas de pistas, etc.
    private AudioSource audioSource;

    void Start()
    {
        // Inicializa el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Método para reproducir un sonido
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
