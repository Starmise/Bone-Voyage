using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class Goal : MonoBehaviour
{
    public string menuSceneName = "LevelsMenu"; // Nombre de la escena del menú

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador toca la meta
        {
            Debug.Log("¡Ganaste! Volviendo al menú...");
            SceneManager.LoadScene(menuSceneName); // Cargar la escena del menú
        }
    }
}
