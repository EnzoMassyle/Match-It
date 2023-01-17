using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWord : MonoBehaviour
{

    public GameControl controller;
    public string [] words = new string [8];
    public int wordIndex;
    public Text textField;

    public AudioSource source;
    public AudioClip[] audioClip = new AudioClip[8];
    public string[] audioUrls = new string[8];


    // To play the sound bit
    public void StartSound()
    {
        source.Play();
    }

    // Setting the the word with a word, a color of white and it's respective audio bit
    public void SetWord()
    {
        textField.text = words[wordIndex];
        textField.color = Color.white;
        source.clip = audioClip[wordIndex];
    }

    /**
     * The way we determine whether a word is already selected, is if its color is not white
     * If the word's color is white, then we know the user is selecting that word, thus we change the color to yellow indicating that it has been selected and tell our game controller the word that got selected
     * If the word's color is not white, then we know the user is deselecting that word, so we change the color back to white and tell our game controller the user deselected the word (with -1)
    **/
    public void ClickOnWord()
    {
        if (textField.color == Color.white)
        {
            textField.color = Color.yellow;
            controller.GetSelectedWord(wordIndex);
        }
        else
        {
            textField.color = Color.white;
            controller.GetSelectedWord(-1);

        }
    }

}
