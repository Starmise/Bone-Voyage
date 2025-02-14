using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerWithMenu : MonoBehaviour
{
    public GameObject LevelsMenu;  // Asigna el menú desde el inspector

    void Start()
    {
        levelMenu.SetActive(false);  // Asegúrate de que el menú esté oculto al inicio
    }

    public void ShowLevelMenu()
    {
        levelMenu.SetActive(true);  // Muestra el menú de niveles
    }

    public void HideLevelMenu()
    {
        levelMenu.SetActive(false);  // Oculta el menú de niveles
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);  // Cambia a la escena seleccionada
    }
}
