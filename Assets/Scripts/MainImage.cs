using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System;

public class MainImage : MonoBehaviour
{
    public GameControl controller;
    public int imageIndex;
    public Sprite[] images = new Sprite[8];
    public Texture2D imageTexture;
    public Image image;

    public string[] urls = new string[8];

    public LoadGameScreen progressIndicator;

    public Sprite testingSprite;

    // This preset the image with an image and making the color of the image white
    public void SetImage()
    {
        image.sprite = images[imageIndex];
        image.color = Color.white;
    }


    /**
     * The way we determine whether an image is already selected, is if its color is not white
     * If the image's color is white, then we know the user is selecting that image, thus we change the color to yellow indicating that it has been selected and tell our game controller the image that got selected
     * If the image's color is not white, then we know the user is deselecting that image, so we change the color back to white and tell our game controller the user deselected the image (with -1)
    **/ 
    public void OnMouseDown()
    {
        if (image.color == Color.white) // Selecting the image
        {
            image.color = Color.yellow;
            controller.GetSelectedImage(imageIndex);
        

        }
        else //Deselecting the image
        {
            image.color = Color.white;
            controller.GetSelectedImage(-1);
        }
    }

}
