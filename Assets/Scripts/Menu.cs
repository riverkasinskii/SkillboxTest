using UnityEngine;

public class Menu : MonoBehaviour
{
    private SavingWrapper savingSystem;

    private void Awake()
    {
        savingSystem = FindObjectOfType<SavingWrapper>();
    }
        
    public void PlayButtonClickSound()
    {
        SoundEffects.Instance.audioSource.PlayOneShot(SoundEffects.Instance.buttonClick);
    }

    public void NewGame()
    {        
        savingSystem.DeleteAllSaves();
    }
        
    public void Exit()
    {
        Application.Quit();
    }
}
