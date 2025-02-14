using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerWithMenu : MonoBehaviour
{
    public GameObject LevelsMenu;  // Asigna el men� desde el inspector

    void Start()
    {
        levelMenu.SetActive(false);  // Aseg�rate de que el men� est� oculto al inicio
    }

    public void ShowLevelMenu()
    {
        levelMenu.SetActive(true);  // Muestra el men� de niveles
    }

    public void HideLevelMenu()
    {
        levelMenu.SetActive(false);  // Oculta el men� de niveles
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);  // Cambia a la escena seleccionada
    }
}
