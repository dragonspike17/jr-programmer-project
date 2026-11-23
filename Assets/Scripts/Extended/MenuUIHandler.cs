
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    private void Start()
    {
        if (ColorPicker != null)
        {
            // Initialize the color picker
            ColorPicker.Init();

            // Listen for color changes
            ColorPicker.onColorChanged += NewColorSelected;

            // Restore the saved color when opening the menu
            if (MainManager.Instance != null)
            {
                ColorPicker.SelectColor(
                    MainManager.Instance.TeamColor
                );
            }
        }
        else
        {
            Debug.LogWarning("ColorPicker is not assigned!");
        }
    }

    // Store the color selected by the player
    public void NewColorSelected(Color color)
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.TeamColor = color;
        }
    }

    // Start the warehouse simulation
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    // Save the color and exit the application
    public void Exit()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.SaveColor();
        }

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Called by the Save Color button
    public void SaveColorClicked()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.SaveColor();
            Debug.Log("Color saved successfully!");
        }
    }

    // Called by the Load Color button
    public void LoadColorClicked()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.LoadColor();

            ColorPicker.SelectColor(
                MainManager.Instance.TeamColor
            );

            Debug.Log("Color loaded successfully!");
        }
    }

    private void OnDestroy()
    {
        if (ColorPicker != null)
        {
            ColorPicker.onColorChanged -= NewColorSelected;
        }
    }
}
