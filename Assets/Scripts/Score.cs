using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    public const int TOTAL_MATCHES = 8;
    public Text textField;
    public int numCorrect = 0;
    public int numIncorrect = 0;
    public GameObject submissionPanel;


    /**
     * The score is not stored in a variable but instead we read the textfield showing the score
     * The variable increment is passed in to let the function know whether to increment the score or decrement it 
     * We then do the proper operation then update the text field 
     * We keep track of the total number the user got correct and incorrect for future purposes such as knowing when the user have completed the game and for the instructor to see how well the student did
    **/ 
    public void UpdateScore(bool increment)
    {
        int currentScore = int.Parse(textField.text);

        if(increment)
        {
            currentScore++;
            numCorrect++;
            textField.text = currentScore.ToString();
        }
        else
        {
            numIncorrect++;
            if(textField.text == "0")
            {
                return; // we don't want a negative score
            }
            else
            {
                currentScore--;
                textField.text = currentScore.ToString();
            }
        }


        // When the user has gotten all of the matches we need to prompt them to submit their score
        if(numCorrect == TOTAL_MATCHES)
        {
            submissionPanel.SetActive(true);
        }

    }


}
