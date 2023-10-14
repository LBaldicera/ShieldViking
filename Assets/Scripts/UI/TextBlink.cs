using System.Collections;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.5f; // Adjust the blinking speed here
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            textMesh.enabled = !textMesh.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
