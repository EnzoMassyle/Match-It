using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScreen : MonoBehaviour
{
    public LoadData gameLoader;
    public GameControl startLoadData;
    public SubmitResults startResultData;
    public InputField input;
    public GameObject errorMessage;


    public void LoadGameScene()
    {
        errorMessage.SetActive(false);
        startLoadData.sheetDataName = input.text;
        startResultData.dataID = input.text;
        startLoadData.GetCsvData();
    }
}
