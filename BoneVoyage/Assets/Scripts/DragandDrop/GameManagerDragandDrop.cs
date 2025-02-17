using UnityEngine;

public class GameManagerDragandDrop : MonoBehaviour
{
    public DropZone[] dropZones; // Array de DropZones
    public GameObject winText; // Texto de victoria

    void Start()
    {
        // Aseg�rate de que el texto de victoria est� oculto al inicio
        if (winText != null)
        {
            winText.SetActive(false);
        }
    }

    void Update()
    {
        // Revisa si todas las zonas de ca�da han sido llenadas correctamente
        if (CheckWinCondition())
        {
            Win();
        }
    }

    bool CheckWinCondition()
    {
        // Comprobamos si todas las DropZones est�n correctamente ocupadas
        foreach (var zone in dropZones)
        {
            // Si alguna zona no tiene el cubo correcto o est� vac�a, no hemos ganado
            if (!zone.IsCompleted)
            {
                return false;
            }
        }
        return true; // Si todas las zonas est�n llenas correctamente, ganamos
    }

    void Win()
    {
        // Activar el mensaje de victoria
        if (winText != null)
        {
            winText.SetActive(true);
        }

        // Aqu� podr�as hacer m�s cosas, como detener el juego, mostrar animaciones, etc.
        Time.timeScale = 0; // Detener el juego (opcional)
    }
}
