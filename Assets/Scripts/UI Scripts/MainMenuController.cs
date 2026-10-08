using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private DataPersistanceManager dataPersistanceManager;

    void Start()
    {
        dataPersistanceManager = GetComponent<DataPersistanceManager>();
    }

    public void OnNewGame()
    {
        dataPersistanceManager.NewGame();
        SceneManager.LoadSceneAsync(1);
    }

    public void OnContinueGame()
    {
        dataPersistanceManager.LoadGame();
        SceneManager.LoadSceneAsync(dataPersistanceManager.gameData.currentScene);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

}
