using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public List<TextAsset> dictionaryTextFile;
    private string theWholeFileAsOneLongString;
    public List<string> playerData;
    public static List<string> eachLine;
    string date, netTime, newWord;
    bool isWork = false;
    [SerializeField] GameObject gamePanel, menuPanel;

    public static string word;
    public static int gameMode;
    public static bool isPlay;
    public static bool continueGame;
    public static bool isColorBlind;

    void Start()
    {
       
        if (isPlay == true)
        {
            CreateNewLevel();
            isPlay = false;
        }

        //  StartCoroutine(GetInternetTime());
        //for (int i = 0; i < findWord.Count; i++)
        //{
        //    Debug.Log(findWord[i]);
        //}
        SetAllData();
    }

    public void ColorBlind()
    {
        isColorBlind = true;
    }


    public void CreateNewLevel()
    {
        if (PlayerPrefs.HasKey("isBlind") == false) isColorBlind = false;
        else isColorBlind = true;

        if (PlayerPrefs.GetInt("ActiveLine") > 0 && continueGame == true)
        {
            word = PlayerPrefs.GetString("Words");
            Debug.Log(word);
        }



        gamePanel.SetActive(true);
        menuPanel.SetActive(false);

        if (PlayerPrefs.GetInt("ActiveLine") > 0 && continueGame == true)
        {
            word = PlayerPrefs.GetString("Words");
        }
        else
        {
            CreateEndlessLevel();
        }

        this.gameObject.GetComponent<GameController>().StartGame();
    }

    public void SelectLanguageMode(int gameAssetIndex)
    {

        Debug.Log(PlayerPrefs.GetInt("LastMode") + " " + gameAssetIndex);
        if (PlayerPrefs.HasKey("LastMode") == false)
        {
            continueGame = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("LastMode") != gameAssetIndex)
            {
                continueGame = false;
                PlayerPrefs.GetInt("ActiveLine", 0);
            }
            else
            {
                continueGame = true;
            }
        }


        gameMode = gameAssetIndex >= 0 ? gameAssetIndex : 0;

        theWholeFileAsOneLongString = dictionaryTextFile[gameMode].text;

        eachLine = new List<string>();
        eachLine.AddRange(
                    theWholeFileAsOneLongString.Split("\n"[0]));


        PlayerPrefs.SetInt("LastMode", gameMode);
        CreateNewLevel();

    }
    private void SetAllData()
    {
        playerData = new List<string>();
        if (PlayerPrefs.GetString("playerData") == "")
        {
            PlayerPrefs.SetString("playerData", "0-0-0-1");
            SetAllData();
        }
        else playerData.AddRange(PlayerPrefs.GetString("playerData").Split("-"[0]));
    }

    public void CreateEndlessLevel()
    {
        CreateNewWords();
    }
    void CreateLevel()
    {

        date = PlayerPrefs.GetString("NewDate");



        if (date == PlayerPrefs.GetString("Date"))
        {
            SetWord();
        }
        else
        {
            PlayerPrefs.SetInt("ActiveLine", 0);
            PlayerPrefs.SetString("Date", date);
            SetWord();
        }
    } // For Time

    void SetWord()
    {

        if (PlayerPrefs.GetString("Words") == "")
        {
            CreateNewWords();
        }
        else
        {
            word = PlayerPrefs.GetString("Words");
        }
    }

    private void Update()
    {
        //if (PlayerPrefs.GetString("NewDate") != "" && isWork == false)
        //{
        //    isWork = true;
        //    CreateLevel();
        //}
    }
    void CreateNewWords()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString("Line_" + i.ToString(), "");
        }
        word = eachLine[UnityEngine.Random.Range(0, eachLine.Count + 1)];
        PlayerPrefs.SetString("Words", word);
        Debug.Log(word);

    }
    #region Time
    //public IEnumerator GetInternetTime()
    //{
    //    UnityWebRequest myHttpWebRequest = UnityWebRequest.Get("http://www.microsoft.com");
    //    yield return myHttpWebRequest.Send();

    //    netTime = myHttpWebRequest.GetResponseHeader("date");
    //    dateTime = new List<string>();
    //    dateTime.AddRange(
    //        netTime.Split(" "[0])
    //        );
    //    PlayerPrefs.SetString("NewDate", dateTime[1] + dateTime[2] + dateTime[3]);

    //    List<string> time = new List<string>();
    //    time.AddRange(dateTime[4].Split(":"[0]));

    //    PlayerPrefs.SetString("Oclock", time[0] + ":" + time[1]);
    //}
    #endregion
    public enum LanguageMode
    {
        Simple,
        Hard
    }
}

