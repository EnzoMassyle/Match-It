using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;
using System;

public class GameControl : MonoBehaviour
{

    public MainImage image;
    public MainWord word;
    public Score score;
    public GameObject startPanel;
    public Slider loader;
    public Text progressIndicator;
    public GameObject errorMessage;
    public GameObject loaderObject;
    public Button sendBtn;
    public GameObject wordBank;
    public GameObject imageBank;

    const string SPREADSHEET_ID = "1rr9mAVWdhz1ZlRqClq09KOY0E3otQgP17cEr1yVQaPA";
    const int STARTING_INDEX_OF_ID = 39;
    const int ID_LENGTH = 44;

    public string sheetDataName;
    public string sheetDataID;

    public static System.Random randomIndex = new System.Random(); //our random number generator;
    public int imageShuffler = 0;
    public int wordShuffler = 0;
    int selectedImage = -1;
    int selectedWord = -1;

    const int NUM_ELEMENTS = 8;
    public void ShowGameScreen() // Gets rid of the starting screen
    {
        startPanel.SetActive(false);
    }


    /**
     * In DisplayImages(), we are creating the game objects that will hold the image sprites on our game screen
     * To ensure a random order, we are making use of a random number generator
     * The first thing we do is initialize a list of ints from 0-7 (representing the eight difference images), I used a list because of the List.Remove function which would allow us to remove a given index from the list after it has been used, this ensures no duplicate images
     * We already have an outline for our game object in the form of image which is a MainImage type 
     * The Instantiate method is used to perform the cloning with Instantiate(GameObject, transform)
     * After the image Gameobject is created, we need to assign the image a sprite which is what SetImage() is for
    **/
    public void DisplayImages()
    {
        List<int> imageIndicies = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

        for (int i = 0; i < NUM_ELEMENTS; i++)
        {
            imageShuffler = randomIndex.Next(0, imageIndicies.Count); // will produce a random number between 0 and the number of elements in our list

            MainImage tempImage = Instantiate(image, imageBank.transform);
            tempImage.name = "Image " + imageIndicies[imageShuffler].ToString();

            tempImage.imageIndex = imageIndicies[imageShuffler];

            imageIndicies.Remove(imageIndicies[imageShuffler]); //will remove that specific element from our list to ensure we do not get duplicates when creating these images

            tempImage.SetImage();

        }
    }


    /**
     * DisplayWords() is similar to DisplayImages() in almost all aspects
     * The only difference is we are Instantiating words with the MainWord data type and assinging a text box text instead of assigning an image a sprite
    **/
    public void DisplayWords()
    {
        List<int> wordIndicies = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        for (int i = 0; i < NUM_ELEMENTS; i++)
        {
            wordShuffler = randomIndex.Next(0, wordIndicies.Count);

            MainWord tempWord = Instantiate(word, wordBank.transform);
            tempWord.name = "Word " + wordIndicies[wordShuffler].ToString();

            tempWord.wordIndex = wordIndicies[wordShuffler];

            wordIndicies.Remove(wordIndicies[wordShuffler]);

            tempWord.SetWord();


            ShowGameScreen();
  
        }
    }


    // this Shuffles an array, Not in use as of right now...
    /*private void Shuffle(string[] arr)
    {
        int shuffledIndex = 0;
        for (int i = arr.Length - 1; i != 0; i--)
        {
            shuffledIndex = randomIndex.Next(0, i);

            string temp = arr[i];
            arr[i] = arr[shuffledIndex];
            arr[shuffledIndex] = temp;


        }
    }*/

    /**
     * GetSelectedImage is called from the MainImage script and will occur when the user clicks on an image
     * We first need to check if the user already has an image selected (denoted by the selected image not being -1)
     * If this is the case then we need to deselect the previously selected image, all we need to do is make the color of the image white
     * After we have checked the initial condition, we will then assign the selected image index to the image the user just clicked on
     * We will then check for a match, This is done after every click on an image
    **/
    public void GetSelectedImage(int imageIndex)
    {
        if (selectedImage != -1)
        {
            MainImage currentImage = GameObject.Find("Image " + selectedImage.ToString()).GetComponent<MainImage>();
            currentImage.image.color = Color.white;
        }
        selectedImage = imageIndex; //we are getting the index of what image the user clicked on with their mouse
        CheckForMatch();
    }

    /**
     * GetSelectedWord is similar to GetSelectedImage but we are going it for a textfield instead of an image
    **/
    public void GetSelectedWord(int wordIndex)
    {
        if (selectedWord != -1)
        {
            GameObject currentWord = GameObject.Find("Word " + selectedWord.ToString());

            currentWord.GetComponent<MainWord>().textField.color = Color.white;

        }

        selectedWord = wordIndex;
        CheckForMatch();
    }

    /**
     * ResetColor will run when the user makes an incorrect match
     * selectedRedImage will hold the incorrect image and selectedRedWord will hold the incorrect word 
     * We simply need to change the color component of each object back to white
    **/
    public IEnumerator ResetColor(MainImage currentImage, MainWord currentWord)
    {
        yield return new WaitForSecondsRealtime(1.0f);

        currentImage.image.color = Color.white;
        currentWord.textField.color = Color.white;

    }


    /**
     * CheckForMatch will be the the algorithm to determine whether the user has made a correct match or an incorrect match
     * This should only run if the user has selected a word and an image, which is why we first check the int value of selectedImage and selectedWord to ensure they are not -1. If this is not true, the algorithm is done
     * If the statement above is true, we need to find the game objects for the word and the image the user selected
     * The way we determine whether the user has made a correct or incorrect match is by the int value of selectedImage and selectedWord, these two values are in parallel so if they are equal, it means they are a match
     * If they are a match, we make the color of the image and the word green to indicate it is correct. If incorrect, we make the color red
     * We then reset the selectedImage and selected word to -1, indicating the user has just tried to make a match and now has no image or word selected
     * We then update the score
    **/
    private void CheckForMatch()
    {
        if (selectedImage != -1 && selectedWord != -1)
        {
            MainImage currentImage = GameObject.Find("Image " + selectedImage.ToString()).GetComponent<MainImage>();
            MainWord currentWord = GameObject.Find("Word " + selectedWord.ToString()).GetComponent<MainWord>();

            if (selectedImage == selectedWord)
            {
                currentImage.image.color = Color.green;
                currentWord.textField.color = Color.green;


                score.UpdateScore(true);

                currentImage.GetComponent<Button>().interactable = false;
                currentWord.GetComponent<Button>().interactable = false;

                selectedImage = -1;
                selectedWord = -1;

            }
            else
            {
                currentImage.image.color = Color.red;
                currentWord.textField.color = Color.red;

                selectedImage = -1;
                selectedWord = -1;

                score.UpdateScore(false);

                StartCoroutine(ResetColor(currentImage, currentWord));
            }
        }
    }

    /**
     * This is the beginning of gathering the data from our master spreadsheet
     * First make the submit button on our start screen non-interactable to not make multiple online requests for the game data
     * Next we start our first Coroutine the find the data set from the user's input
    **/
    public void GetCsvData()
    {
        sendBtn.interactable = false;
        StartCoroutine(RetrieveDataSet());
    }



    /**
     * We are going to export the master spreadsheet as a csv file first
     * Next we process the csv data loop through the parsed list to find the data set the user entered
     * If the data set is not found, break out of the function and tell the user that the data set entered does not exist
     * If the data set is found, we move to the next step which is retrieving the content from the content spreadsheet 
    **/ 
    IEnumerator RetrieveDataSet()
    {
        bool successfullRequest = false;
        string spreadsheet = "https://docs.google.com/spreadsheets/d/" + SPREADSHEET_ID + "/export?format=csv";
        UnityWebRequest retriever = UnityWebRequest.Get(spreadsheet);
        yield return retriever.SendWebRequest();

        if (retriever.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(retriever.error);


        }
        else
        {
            string dataFeedback = retriever.downloadHandler.text;

            List<string> processedData = ProcessData(dataFeedback);


            for (int i = 0; i < processedData.Count; i += 2)
            {
                if (processedData[i] == sheetDataName)
                {
                    successfullRequest = true;
                    sheetDataID = processedData[i + 1].Substring(STARTING_INDEX_OF_ID, ID_LENGTH);
                }
            }

            if (successfullRequest == false)
            {
                errorMessage.SetActive(true);
                sendBtn.interactable = true;
                yield break;
            }
            else
            {
                loaderObject.SetActive(true);
                progressIndicator.text = "gathering game data...";
            }
            StartCoroutine(RetrieveContent());
        }
    }


    /**
     * in this function we will need to Process the data from the main spreadsheet
     * our goal is to find a match with the name of the data set the user entered and load that data spreadsheet as a csv so we can access the correct data
     * 
     **/
    public List<string> ProcessData(string data)
    {
        List<string> words = new List<string>();
        string[] split = data.Split(',', '\n', '\r');

        for (int i = 3; i < split.Length; i++)
        {
            if (split[i] != "")
            {
                words.Add(split[i]);
            }
        }
        return words;

    }

    // The only difference with this function is that we start at i = 4 in the for loop since we have 3 title columns in the content spreadsheet instead of 2 in the master spreadsheet
    public List<string> ProcessContentData(string data) //Made an extra function here to account for the extra cells in the spreadsheet
    {
        List<string> words = new List<string>();
        string[] split = data.Split(',', '\n', '\r');

        for (int i = 3; i < split.Length; i++)
        {
            if (split[i] != "")
            {
                words.Add(split[i]);
            }
        }
        return words;

    }


    /**
     * First we are going to do the same thing we did to the master spreadsheet and export it as a csv
     * After the content data has been processed, we need to parse the list of the processed data to find the image URLs, words, and Audio URLs
     * After this has been completed, we begin downloading the images
    **/ 
    IEnumerator RetrieveContent()
    {
        string url = "https://docs.google.com/spreadsheets/d/" + sheetDataID + "/export?format=csv";
        UnityWebRequest retriever = UnityWebRequest.Get(url);
        yield return retriever.SendWebRequest();

        loader.value += 0.25f;
        if (retriever.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(retriever.error);
            errorMessage.GetComponent<Text>().text = "Something was wrong with spreadsheet file... please look at google spreadsheet to ensure it is in the correct format";
            errorMessage.SetActive(true);
            yield break;
        }
        string response = retriever.downloadHandler.text;

        List<string> processedData = ProcessContentData(response);

        GetImageURLs(processedData);

        GetWords(processedData);

        //GetAudioURLs(processedData);

        StartCoroutine(RetrieveTexture(image.urls));


    }

    // parsing through the data to fill the image url array
    private void GetImageURLs(List<string> data)
    {
        for (int i = 0, j = 0; i < image.urls.Length; i++, j += 2)
        {
            image.urls[i] = data[j].ToString();
        }

    }

    // parsing through the data to fill the words array
    public void GetWords(List<string> data)
    {
        for (int i = 0, j = 1; i < word.words.Length; i++, j += 2)
        {
            word.words[i] = data[j];
        }
    }


    // parsing through the data to fill the audio urls array
    public void GetAudioURLs(List<string> data)
    {
        for (int i = 0, j = 2; i < 8; i++, j += 3)
        {
            word.audioUrls[i] = data[j];
        }
    }

    /**
     * In this function we are using the image urls the user entered in the content spreadsheet to download their images
     * We need to make a web request for each image so this portion of loading takes the longest
     * After we successfully download the image texture and assigned it to our image object, we are going to create a sprite out of it and add it to our images array
     * After all images have been filled, we are going to display them in the screen, this happens in the background while it is loading on the start screen
     * After displaying the images is complete, we will download the audio snippets
    **/
    public IEnumerator RetrieveTexture(string[] url)
    {
        progressIndicator.text = "acquiring images and words...";
        for (int i = 0; i < url.Length; i++)
        {
            loader.value += (0.5f / url.Length);
            UnityWebRequest retriever = UnityWebRequestTexture.GetTexture(url[i]);
            yield return retriever.SendWebRequest();

            if (retriever.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("error: " + retriever.error);
                errorMessage.GetComponent<Text>().text = "Something was wrong with spreadsheet file... please look at google spreadsheet to ensure it is in the correct format";
                errorMessage.SetActive(true);
                yield break;
            }
            else
            {
                image.imageTexture = ((DownloadHandlerTexture)retriever.downloadHandler).texture;
                progressIndicator.text = "downloading " + url[i];

                image.images[i] = Sprite.Create(image.imageTexture, new Rect(0f, 0f, image.imageTexture.width, image.imageTexture.height), new Vector2(0.5f, 0.5f));

            }
        }
        DisplayImages();
        DisplayWords();
        //StartCoroutine(GetAudioBit(word.audioUrls));

    }

    /**
     * This is the same process as downloading the image textures but this time we are downloading an audio clip
     * After this we have dowloaded all the content we need
    **/ 
    public IEnumerator GetAudioBit(string[] urls)
    {
        for(int i = 0; i < urls.Length; i++)
        {
            loader.value += 0.25f / urls.Length;
            UnityWebRequest retriever = UnityWebRequestMultimedia.GetAudioClip(urls[i], AudioType.MPEG);

            yield return retriever.SendWebRequest();


            if (retriever.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(retriever.error);
                errorMessage.GetComponent<Text>().text = "Something was wrong with spreadsheet file... please look at google spreadsheet to ensure it is in the correct format";
                errorMessage.SetActive(true);
                yield break;
            }

            else
            {
                word.audioClip[i] = DownloadHandlerAudioClip.GetContent(retriever);
                progressIndicator.text = "downloading " + urls[i];
                Debug.Log("Success");
            }
        }



        DisplayWords();

    }
}







