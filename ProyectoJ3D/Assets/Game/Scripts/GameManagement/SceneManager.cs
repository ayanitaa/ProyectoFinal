using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class SceneManager : MonoBehaviour
    {
        [Header("Scene Names")]
        public string mainMenuScene = "Menu";
        public string gameScene = "Scene1";
        public string controlsScene = "Controls";

        // Iniciar el juego → cargar escena 1
        public void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameScene);
        }

        // Ir a controles
        public void OpenControls()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(controlsScene);
        }

        // Volver al menú principal
        public void BackToMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
        }

        // Salir del juego
        public void QuitGame()
        {
            Debug.Log("Saliendo del juego...");

            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
