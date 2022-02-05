using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    
}
