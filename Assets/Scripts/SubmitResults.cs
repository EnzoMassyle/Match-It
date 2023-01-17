using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SubmitResults : MonoBehaviour
{
    public Text playerScore;
    public GameObject confirmation;
    public InputField studentID;
    public Score numCorrectResponses;
    public Score numIncorrectResponses;
    public GameObject submitBtn;
    public GameObject newGameBtn;
    public GameObject instructions;
    public GameObject inputField;
    public const string FORM_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeRlzdT369EZuhzRkE21YSQS7aZ1MhNek0N7-vRjTSpf-E6dQ/formResponse";

    public string dataID;

    // Here we are setting the strings of the data we are going to submit to the google form and then pushing that data through
    public void SendToGoogleForm()
    {
        string score = playerScore.text;
        string id = studentID.text;
        string numCorrect = (Math.Round(((numIncorrectResponses.numIncorrect * 1.0f) / numCorrectResponses.numCorrect),3)).ToString();
        string numIncorrect = numIncorrectResponses.numIncorrect.ToString();

        StartCoroutine(PushData(score, numCorrect, numIncorrect, id));
    }


    /**
     * To find the entry ids, we need to inspect the webpage of the the google form to find these
     * We add fill in these entry fields with the corresponding data
     * We then post these responses to the google form
    **/ 
    public IEnumerator PushData(string score, string numCorrect, string numIncorrect, string id)
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.695786423", score);
        form.AddField("entry.1785884302", numCorrect);
        form.AddField("entry.1950437179", numIncorrect);
        form.AddField("entry.1248285287", id);
        form.AddField("entry.1546201251", dataID);

        UnityWebRequest googleForm = UnityWebRequest.Post(FORM_URL, form);
       
        yield return googleForm.SendWebRequest();

        if(googleForm.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("error" + googleForm.error);
        }

        studentID.text = "";
        Debug.Log("Successfully Submitted");
        confirmation.SetActive(true);
        newGameBtn.SetActive(true);
        submitBtn.SetActive(false);
        instructions.SetActive(false);
        inputField.SetActive(false);

    }
}

