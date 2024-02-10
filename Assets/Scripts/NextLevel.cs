using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
                
        SceneManager.LoadScene(nextSceneIndex);
    }
}
