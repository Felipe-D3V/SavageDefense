using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    static bool Multiplayer = false;
    public GameObject ConfigMenu;
 public void IniciarJogo()
    {
        SceneManager.LoadScene("CharacterChoose");
    }

    public void IniciarMultiplayer()
    {
        SceneManager.LoadScene("CharacterChoose");
        Multiplayer = true;
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
