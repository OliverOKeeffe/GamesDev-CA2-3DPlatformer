using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLevel1()
    {
        
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2()
    {
        
        SceneManager.LoadScene("Level2");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); 
    }
}
