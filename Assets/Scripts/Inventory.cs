using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject menuInventory;

    public GameObject menuWeapons;
    public GameObject menuItens;
    public GameObject menuPerks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuInventory.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuInventory.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuInventory.SetActive(true);
            Time.timeScale = 0f;
        }


        if (menuWeapons.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuWeapons.SetActive(false);
                menuInventory.SetActive(true);
            }
        }
        if (menuPerks.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuPerks.SetActive(false);
                menuInventory.SetActive(true);
            }
        }
        if (menuItens.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuItens.SetActive(false);
                menuInventory.SetActive(true);
            }
        }

    }

    public void Weapons()
    {
        menuInventory.SetActive(false);
        menuWeapons.SetActive(true);
    }

    public void Items()
        {
        menuInventory.SetActive(false);
        menuItens.SetActive(true);
        }
    
    public void Perks()
    {
        menuInventory.SetActive(false);
        menuPerks.SetActive(true);
    }
}
