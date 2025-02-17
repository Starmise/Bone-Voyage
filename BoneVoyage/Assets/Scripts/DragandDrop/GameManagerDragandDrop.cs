using UnityEngine;

public class GameManagerDragandDrop : MonoBehaviour
{
    public DropZone[] dropZones; // Array de DropZones
    public GameObject winText; // Texto de victoria

    void Start()
    {
        // Asegúrate de que el texto de victoria esté oculto al inicio
        if (winText != null)
        {
            winText.SetActive(false);
        }
    }

    void Update()
    {
        // Revisa si todas las zonas de caída han sido llenadas correctamente
        if (CheckWinCondition())
        {
            Win();
        }
    }

    bool CheckWinCondition()
    {
        // Comprobamos si todas las DropZones están correctamente ocupadas
        foreach (var zone in dropZones)
        {
            // Si alguna zona no tiene el cubo correcto o está vacía, no hemos ganado
            if (!zone.IsCompleted)
            {
                return false;
            }
        }
        return true; // Si todas las zonas están llenas correctamente, ganamos
    }

    void Win()
    {
        // Activar el mensaje de victoria
        if (winText != null)
        {
            winText.SetActive(true);
        }

        // Aquí podrías hacer más cosas, como detener el juego, mostrar animaciones, etc.
        Time.timeScale = 0; // Detener el juego (opcional)
    }
}
