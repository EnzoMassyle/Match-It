using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class LoadData : MonoBehaviour
{
    public GameObject panel;
    public Score player;
    public GameControl dataSetter;
    public GameObject sendBtn;
    public GameObject inputField;
    public GameObject confirmation;
    public GameObject newGameBtn;
    public GameObject instructions;


    /**
     * Going into a new game, many game objects need to be activated an deactivated
     * The existing image and word objects are then destroyed to make room for the new images and words
     * finally reset the score and display the new images and words in a new random order
    **/ 
    public void NewGame()
    {
        player.numCorrect = 0;
        player.numIncorrect = 0;
        panel.SetActive(false);
        confirmation.SetActive(false);
        newGameBtn.SetActive(false);
        sendBtn.SetActive(true);
        inputField.SetActive(true);
        instructions.SetActive(true);

        for (int i = 0; i < 8; i++)
        {
            if(GameObject.Find("Word " + i.ToString()) != null || GameObject.Find("Image " + i.ToString()) != null)
            {
                GameObject.Destroy(GameObject.Find("Word " + i.ToString()));
                GameObject.Destroy(GameObject.Find("Image " + i.ToString()));
            }
        }

        player.textField.text = "0";

        dataSetter.DisplayImages();
        dataSetter.DisplayWords();
    }


}
