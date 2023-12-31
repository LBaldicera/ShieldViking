using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] CharacterStats characterStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WinConditions();
    }

    private void WinConditions()
    {
        if (characterStats.currentHealth <= 0)
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
}
