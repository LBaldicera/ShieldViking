using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlScreen : MonoBehaviour
{
    private EventSystem eventSystem;
    [SerializeField] private Button selectedButton;
    private bool isVisible = true;
    private float lastBlinkTime;
    public float blinkInterval = 0.5f; // Adjust the blink speed here
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadSceneAsync(2);
        }

        // Blink the selected button
        if (Time.time - lastBlinkTime >= blinkInterval)
        {
            isVisible = !isVisible;
            selectedButton.gameObject.SetActive(isVisible);
            lastBlinkTime = Time.time;
        }
    }
}
