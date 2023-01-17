using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject helpPanel;

    public void EnableHelpScreen()
    {
        helpPanel.SetActive(true);
    }

    public void DisableHelpScreen()
    {
        helpPanel.SetActive(false);
    }
}
