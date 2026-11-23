
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    // Initialize the color picker when the menu starts
    private void Start()
    {
        if (ColorPicker != null)
        {
            ColorPicker.Init();

            ColorPicker.onColorChanged += NewColorSelected;
        }
        else
        {
            Debug.LogWarning("ColorPicker is not assigned!");
        }
    }

    // Called when the player selects a forklift color
    public void NewColorSelected(Color color)
    {
        if (MainManager.Instance != null)
        {
            // Store the selected forklift color
            MainManager.Instance.TeamColor = color;

            // Save the selected color to a JSON file
            MainManager.Instance.SaveColor();

            Debug.Log("Forklift color selected and saved!");
        }
        else
        {
            Debug.LogWarning("MainManager was not found!");
        }
    }

    // Start the warehouse simulation
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    // Exit the game
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Remove the color picker event when destroyed
    private void OnDestroy()
    {
        if (ColorPicker != null)
        {
            ColorPicker.onColorChanged -= NewColorSelected;
        }
    }
}
