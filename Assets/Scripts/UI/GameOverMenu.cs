using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Button continueButton;
    public Button endButton;
    public float blinkInterval = 0.5f; // Adjust the blink speed here

    private EventSystem eventSystem;
    private Button selectedButton;
    private Button lastSelectedButton;
    private bool isVisible = true;
    private float lastBlinkTime;

    private void Start()
    {
        eventSystem = EventSystem.current;
        lastSelectedButton = continueButton; // Initialize the last selected button

        // Set the default button to be selected when the scene starts.
        eventSystem.SetSelectedGameObject(continueButton.gameObject);
        selectedButton = continueButton; // Start with "Continue" button selected
        lastBlinkTime = Time.time;
    }

    private void Update()
    {
        // Use controller input to navigate the buttons.
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
        {
            SetSelectedButton(continueButton);
        }
        else if (verticalInput < 0)
        {
            SetSelectedButton(endButton);
        }

        // Blink the selected button
        if (Time.time - lastBlinkTime >= blinkInterval)
        {
            isVisible = !isVisible;
            selectedButton.gameObject.SetActive(isVisible);
            lastBlinkTime = Time.time;
        }
    }

    private void SetSelectedButton(Button newSelectedButton)
    {
        // Disable the visibility of the previously selected button
        lastSelectedButton.gameObject.SetActive(true);

        // Set the new selected button
        eventSystem.SetSelectedGameObject(newSelectedButton.gameObject);
        selectedButton = newSelectedButton;

        // Update the last selected button
        lastSelectedButton = newSelectedButton;

        // Hide the new selected button if it's currently invisible
        newSelectedButton.gameObject.SetActive(isVisible);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1); // Load your game scene (adjust the scene index as needed).
    }

    public void End()
    {
        Application.Quit(); // Close the application. Make sure to handle this gracefully on all platforms.
    }
}
