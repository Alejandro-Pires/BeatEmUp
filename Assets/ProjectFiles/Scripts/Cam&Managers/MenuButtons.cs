using ProjectFiles.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void Quit() => Application.Quit();
    public void MainMenu() => SceneManager.LoadScene("MainMenu"); 
    public void NewGame() => SceneManager.LoadScene("Level1");
}