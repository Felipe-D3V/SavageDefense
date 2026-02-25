using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject ConfigMenu;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ConfigMenu.activeSelf)
            {
                ConfigMenu.SetActive(false);
            }
        }
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene("Arena");
    }

    public void IniciarCoop()
    {
        SceneManager.LoadScene("CharacterChoose");
       
    }

    public void Settings()
    {
        ConfigMenu.SetActive(true);
    }



    public void FecharMenu()
    {
        ConfigMenu.SetActive(false);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}
