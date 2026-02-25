using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject graphicsPan;
    // Start is called before the first frame update
    public void GraphicsPanel()
    {
        graphicsPan.SetActive(true);
    }
}
