using UnityEngine;

public static class SaveSystem
{
    public static void DeleteAllSavings()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SetPlayerPosition(Vector2 position)
    {
        PlayerPrefs.SetFloat("PlayerPositionX", position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", position.y);        
    }

    public static Vector3 GetPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            return new Vector2(
                PlayerPrefs.GetFloat("PlayerPositionX"),
                PlayerPrefs.GetFloat("PlayerPositionY"));
        }
        else
        {
            return new Vector2(1f, 0.35f);
        }
    }        
}
