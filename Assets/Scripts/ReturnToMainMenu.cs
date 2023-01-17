using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnToMainMenu : MonoBehaviour
{
    public GameObject startPanel;
    public Score player;
    public Slider loader;
    public GameObject loaderObject;
    public Text progressIndicator;
    public Button startButton;
    public InputField dataEntry;
    public GameObject confirmPanel;


    /**
     * If the user decides to return to the main menu we need to reset all of the player's game stats and activate the start panel
     * Next we need to destroy the image and words associated with the current game since the player is not playing this current game anymore
    **/ 
    public void MainMenu()
    {
        startPanel.SetActive(true);
        player.numIncorrect = 0;
        player.numCorrect = 0;
        player.textField.text = "0";
        loader.value = 0f;
        loaderObject.SetActive(false);
        progressIndicator.text = "";
        dataEntry.text = "";
        startButton.interactable = true;

        for (int i = 0; i < 8; i++)
        {
            if (GameObject.Find("Word " + i.ToString()) != null || GameObject.Find("Image " + i.ToString()) != null)
            {
                GameObject.Destroy(GameObject.Find("Word " + i.ToString()));
                GameObject.Destroy(GameObject.Find("Image " + i.ToString()));
            }
        }


        confirmPanel.SetActive(false);
    }

    public void HidePanel()
    {
        confirmPanel.SetActive(false);
    }

    public void ShowPanel()
    {
        confirmPanel.SetActive(true);
    }


}
