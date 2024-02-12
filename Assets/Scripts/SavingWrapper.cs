using UnityEngine;

[RequireComponent(typeof(SavingSystem))]
public class SavingWrapper : MonoBehaviour
{
    private const string DEFAULT_SAVE_FILE = "save";

    private void Awake()
    {        
        DontDestroyOnLoad(this);        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Save();
        }        
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(DEFAULT_SAVE_FILE);
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(DEFAULT_SAVE_FILE);
    }

    public void DeleteAllSaves()
    {
        GetComponent<SavingSystem>().DeleteAllSaves(DEFAULT_SAVE_FILE);
    }
}
