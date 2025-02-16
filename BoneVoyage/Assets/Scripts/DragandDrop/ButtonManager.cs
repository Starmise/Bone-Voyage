using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject[] objectsToActivate; // Objetos que se activarán
    public GameObject[] objectsToDeactivate; // Objetos que se desactivarán

    public void SwitchObjects()
    {
        // Activar objetos
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        // Desactivar objetos
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
}
