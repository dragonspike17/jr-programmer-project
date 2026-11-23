
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Singleton instance
    public static MainManager Instance { get; private set; }

    // Selected forklift color
    public Color TeamColor = Color.white;

    // Resource database for the warehouse
    public ResourceDatabase ResourceDatabase;

    private void Awake()
    {
        // Prevent duplicate MainManager objects
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the singleton instance
        Instance = this;

        // Keep MainManager between scenes
        DontDestroyOnLoad(gameObject);

        // Initialize the resource database
        if (ResourceDatabase != null)
        {
            ResourceDatabase.Init();
        }
        else
        {
            Debug.LogWarning(
                "MainManager: ResourceDatabase is not assigned!"
            );
        }

        // Load the previously saved forklift color
        LoadColor();
    }

    // Class used to store save data as JSON
    [System.Serializable]
    private class SaveData
    {
        public Color TeamColor;
    }

    // Save the selected color to a file
    public void SaveColor()
    {
        SaveData data = new SaveData();

        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        string path = Path.Combine(
            Application.persistentDataPath,
            "savefile.json"
        );

        File.WriteAllText(path, json);

        Debug.Log("Forklift color saved successfully!");
    }

    // Load the previously saved color
    public void LoadColor()
    {
        string path = Path.Combine(
            Application.persistentDataPath,
            "savefile.json"
        );

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data =
                JsonUtility.FromJson<SaveData>(json);

            if (data != null)
            {
                TeamColor = data.TeamColor;

                Debug.Log(
                    "Saved forklift color loaded successfully!"
                );
            }
        }
        else
        {
            // Default color when no save file exists
            TeamColor = Color.white;

            Debug.Log(
                "No saved color found. Using white."
            );
        }
    }

    // Update the selected forklift color
    public void SetTeamColor(Color newColor)
    {
        TeamColor = newColor;
    }

    // Save the selected color when quitting
    private void OnApplicationQuit()
    {
        SaveColor();
    }
}
