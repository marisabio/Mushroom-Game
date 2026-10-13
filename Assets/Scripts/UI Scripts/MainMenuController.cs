using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject NewGameButton;
    [SerializeField] private GameObject YesNewGameButton;
    [SerializeField] private GameObject NoNewGameButton;
    [SerializeField] private GameObject ConfirmationTextNewGame;

    private DataPersistanceManager dataPersistanceManager;

    void Start()
    {
        dataPersistanceManager = GetComponent<DataPersistanceManager>();
    }

    public void OnNewGame()
    {
        if (dataPersistanceManager.gameData.freshStart == true)
        {
            dataPersistanceManager.NewGame();
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            NewGameButton.SetActive(false);
            YesNewGameButton.SetActive(true);
            NoNewGameButton.SetActive(true);
            ConfirmationTextNewGame.SetActive(true);
        }
        
    }

    public void OnNewGameClickConfirmation()
    {
        dataPersistanceManager.NewGame();
        SceneManager.LoadSceneAsync(1);
    }

    public void OnNewGameClickCancelation()
    {
        NewGameButton.SetActive(true);
        YesNewGameButton.SetActive(false);
        NoNewGameButton.SetActive(false);
        ConfirmationTextNewGame.SetActive(false);
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
