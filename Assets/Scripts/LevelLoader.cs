using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadLevel();       
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadAsync());
    }
        
    private IEnumerator LoadAsync()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = operation.progress;
            slider.value = progress;
            yield return null; 
            loadingScreen.SetActive(false);            
        }        
    }        
}
