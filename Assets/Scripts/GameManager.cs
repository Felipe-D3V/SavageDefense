using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject ConfigMenu;
    public bool IsPaused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ConfigMenu.activeSelf)
            {
                ConfigMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                ConfigMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    public void ToggleMenuConfig()
    {
        bool isActive = ConfigMenu.activeSelf;

        ConfigMenu.SetActive(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0f;
            IsPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            IsPaused = false;
        }
    }
}
