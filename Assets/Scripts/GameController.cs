using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [Header("Game Object")]
    [SerializeField] List<GameObject> lines;
    [SerializeField] List<Text> letters;
    [SerializeField] List<GameObject> buttons, btnEn, btnTR;
    [SerializeField] List<Sprite> bgColor;
    [SerializeField] List<RuntimeAnimatorController> anims;
    [SerializeField] List<GameObject> keyboards;

    [SerializeField] GameObject test;

    [Header("Menu Object")]
    [SerializeField] List<GameObject> winPanel;
    [SerializeField] List<GameObject> losePanel;
    [SerializeField] List<GameObject> menuButtons;
    [SerializeField] List<GameObject> images;

    [Header("Scripts Variables")]
    int activeLine, letCount = 0;
    string playerWord = "";
    bool win = false;

    static bool isCB;
    private void Start()
    {


        ChangeKeyboard(PlayerPrefs.GetString("language"));

    }

    public void ChangeKeyboard(string language)
    {
        if (language != null)
        {
            PlayerPrefs.SetString("language", language);
        }
        else
            language = PlayerPrefs.GetString("language");


        if (language == "en")
        {
            keyboards[1].SetActive(true);
            keyboards[0].SetActive(false);

            buttons = btnEn;
        }
        else
        {
            keyboards[1].SetActive(false);
            keyboards[0].SetActive(true);
            buttons = btnTR;
        }
    }

    public void StartGame()
    {
        Debug.Log(PlayerPrefs.GetInt("ActiveLine") + "      " + GameManager.continueGame);

        if (PlayerPrefs.GetInt("ActiveLine") > 0 && GameManager.continueGame == true)
        {
            activeLine = PlayerPrefs.GetInt("ActiveLine");
            Debug.Log(activeLine);
            SetLastWords(activeLine);
        }
        else
        {
            activeLine = 0;
        }
        SetActiveLine();
    }


    private void SetLastWords(int activeLinex)
    {
        activeLine = 0;
        for (int i = 0; i < activeLinex; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                lines[i].transform.GetChild(j).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("Line_" + i.ToString())[j].ToString();
            }
            LoadLastWord();
        }
    }

    public void AddLetter(string letter)
    {
        if (letCount < 5)
        {
            playerWord += letter;
            letters[letCount].text = letter;
            letCount++;
        }

    }

    public void ClearAllLine()
    {
        SceneManager.LoadScene(0);
    }


    public void SetActiveLine()
    {
        letCount = 0;
        playerWord = "";
        letters.Clear();
        for (int i = 0; i < lines[activeLine].transform.childCount; i++)
        {
            letters.Add(lines[activeLine].transform.GetChild(i).GetChild(0).GetComponent<Text>());
        }
    }


    public void NextLevel()
    {
        this.gameObject.GetComponent<GameManager>().SelectLanguageMode(GameManager.gameMode);
        ClearAllLine();
    }
    public void ControlWord()
    {
        int count = 0;
        string correctLetters = "";
        Debug.Log(activeLine);
        for (int i = 0; i < playerWord.Length; i++)
        {
            for (int j = 0; j < GameManager.word.Length; j++)
            {

                if (GameManager.word[j] == playerWord[i])
                {
                    lines[activeLine].transform.GetChild(i).GetComponent<Image>().color = Color.yellow;
                    for (int x = 0; x < buttons.Count; x++)
                    {
                        if (playerWord[i].ToString() == buttons[x].transform.name)
                        {
                            correctLetters += playerWord[i].ToString();
                            buttons[x].GetComponent<Image>().sprite = bgColor[1];
                            break;
                        }
                    }
                }

            }
        }



        for (int i = 0; i < playerWord.Length; i++)
        {

            if (playerWord[i] == GameManager.word[i])
            {
                lines[activeLine].transform.GetChild(i).GetComponent<Image>().color = Color.green;

                for (int x = 0; x < buttons.Count; x++)
                {
                    if (playerWord[i].ToString() == buttons[x].transform.name)
                    {
                        buttons[x].GetComponent<Image>().sprite = bgColor[0];
                        break;
                    }
                }
                count++;
                continue;
            }
        }


        string disableLetters = playerWord.Replace(correctLetters, "");
        for (int i = 0; i < disableLetters.Length; i++)
        {
            for (int x = 0; x < buttons.Count; x++)
            {
                if (disableLetters[i].ToString() == buttons[x].transform.name)
                {
                    buttons[x].GetComponent<Image>().sprite = bgColor[2];
                 //   buttons[x].GetComponent<Button>().interactable = false;
                }
            }
        }

        if (count == playerWord.Length)
        {
            //WinScreen();
        }
        else
        {
            if (activeLine < lines.Count - 1)
            {
                activeLine++;
                PlayerPrefs.SetInt("ActiveLine", activeLine);
                SetActiveLine();
            }
            else
            {
                Debug.Log("try again tomorrow");
            }
        }


    }


    public void ChangeColorBlind(int x)
    {
        if (isCB == false) { isCB = true; images[1].SetActive(true); images[0].SetActive(false); }
        else { isCB = false; images[0].SetActive(true); images[1].SetActive(false); }
    }


    public void DeleteLetter()
    {
        if (playerWord.Length > 0 && playerWord != "")
        {
            letters[letCount - 1].text = "";

            if (letCount != 0)
            {
                letCount--;
            }

            playerWord = "";
            for (int i = 0; i < letters.Count; i++)
            {
                if (letters[i].text != "" || letters[i].text != " ")
                {
                    playerWord += letters[i].text;
                }
            }
        }

        Debug.Log(playerWord.Length);

    }
    public void CheckWord()
    {

        StartCoroutine(WaitAnim());
        #region Time Wordle
        //count = 0;
        //string correctLetters = "";

        //for (int i = 0; i < playerWord.Length; i++)
        //{

        //    if (playerWord[i] == GameManager.word[i])
        //    {
        //        lines[activeLine].transform.GetChild(i).GetComponent<Image>().color = Color.green;
        //       // StartCoroutine(WaitAnim(lines[activeLine].transform.GetChild(i).gameObject));

        //         for (int x = 0; x < buttons.Count; x++)
        //        {
        //            if (playerWord[i].ToString() == buttons[x].transform.name)
        //            {
        //                correctLetters += playerWord[i].ToString();
        //                buttons[x].GetComponent<Image>().sprite = bgColor[0];
        //                break;
        //            }
        //        }

        //        count++;
        //        continue;
        //    }            

        //    for (int j = 0; j < GameManager.word.Length; j++)
        //    {
        //        if (playerWord[i] == GameManager.word[j])
        //        {
        //            lines[activeLine].transform.GetChild(i).GetComponent<Image>().color =Color.yellow;
        //           // StartCoroutine(WaitAnim(lines[activeLine].transform.GetChild(i).gameObject));

        //            for (int x = 0; x < buttons.Count; x++)
        //            {
        //                if (playerWord[i].ToString() == buttons[x].transform.name)
        //                {
        //                    correctLetters += playerWord[i].ToString();
        //                    buttons[x].GetComponent<Image>().sprite = bgColor[1];

        //                    break;
        //                }
        //            }
        //            break;
        //        }
        //    }

        //}

        //string disableLetters = playerWord.Replace(correctLetters, "");

        //Debug.Log(correctLetters + "  " + playerWord + "  " + disableLetters);

        //for (int i = 0; i < playerWord.Length; i++)
        //{
        //    for (int x = 0; x < buttons.Count; x++)
        //    {
        //        if (disableLetters[i].ToString() == buttons[x].transform.name)
        //        {

        //            buttons[x].GetComponent<Image>().color = Color.gray;
        //            buttons[x].GetComponent<Button>().interactable = false;
        //        }
        //    }
        //}


        //if (count == playerWord.Length)
        //{
        //    WinScreen();
        //}
        //else
        //{
        //    if (activeLine < lines.Count - 1)
        //    {
        //        activeLine++;
        //        PlayerPrefs.SetInt("ActiveLine", activeLine);
        //        SetActiveLine();
        //    }
        //    else
        //    {
        //        Debug.Log("try again tomorrow");
        //    }
        //}
        #endregion
    }
    public void WinScreen()
    {

        AdsController.Instance.ShowTransition();
        PlayerPrefs.SetInt("ActiveLine", 0);
        winPanel[0].SetActive(true);
        win = true;
        GameManager.isPlay = true;
        TurnStringToInt(gameObject.GetComponent<GameManager>());

        // winPanel[1].GetComponent<Text>().text = TimeCalculate(); // remaining time calculation

    }

    void TurnStringToInt(GameManager gm)
    {
        List<int> dataInt = new List<int>();

        for (int i = 0; i < gm.playerData.Count; i++)
        {
            dataInt.Add(int.Parse(gm.playerData[i]));
        }
        if (win == true)
        {
            AddAndShowData(dataInt);
        }
        else
        {
            LoseData(dataInt);
        }


    }

    void AddAndShowData(List<int> data)
    {
        data[0] = data[0] + 1;
        data[2] = data[2] + 1;
        data[1] = data[1] + (100 + ((data[2] - 1) * 10));
        data[3] = data[3] < data[2] ? data[2] : data[3];
        SaveData(data);
    }

    void LoseData(List<int> data)
    {
        data[0] = data[0] + 1;
        data[2] = 0;
        data[3] = data[3] < data[2] ? data[2] : data[3];
        SaveData(data);
    }

    void SaveData(List<int> intList)
    {

        for (int i = 0; i < gameObject.GetComponent<GameManager>().playerData.Count; i++)
        {
            gameObject.GetComponent<GameManager>().playerData[i] = intList[i].ToString();
            if (win == true) { winPanel[i + 1].GetComponent<Text>().text = gameObject.GetComponent<GameManager>().playerData[i]; }
            else if (win == false) { losePanel[i + 1].GetComponent<Text>().text = gameObject.GetComponent<GameManager>().playerData[i]; }

        }
        PlayerPrefs.SetString("playerData", (intList[0].ToString() + "-" + intList[1].ToString() + "-" + intList[2].ToString() + "-" + intList[3].ToString()));
    }

    public void LoseScreen()
    {
        AdsController.Instance.ShowTransition();
        PlayerPrefs.SetInt("ActiveLine", 0);
        losePanel[0].SetActive(true);
        win = false;
        GameManager.isPlay = true;
        TurnStringToInt(gameObject.GetComponent<GameManager>());
    }



    #region Time Calculated
    //public string TimeCalculate()
    //{
    //    string girisZamani = PlayerPrefs.GetString("Oclock");
    //    string cikisZamani = "23:59";
    //    TimeSpan girisCikisFarki = DateTime.Parse(cikisZamani).Subtract(DateTime.Parse(girisZamani));

    //    List<string> time = new List<string>();
    //    time.AddRange(girisCikisFarki.ToString().Split(":"[0]));

    //    return time[0] + "hr :" + time[1] + "min";
    //}

    #endregion

    IEnumerator WaitAnim()
    {
        if (playerWord.Length > 4)
        {
            if (ControlWordisDB(playerWord) == true)
            {
                int count = 0;
                string correctLetters = "";
                bool grey = false;
                PlayerPrefs.SetString("Line_" + activeLine.ToString(), playerWord);
                for (int i = 0; i < playerWord.Length; i++)
                {

                    grey = false;
                    if (playerWord[i] == GameManager.word[i])
                    {
                        grey = true;
                        //  lines[activeLine].transform.GetChild(i).GetComponent<Image>().color = GameManager.isColorBlind == true ? new Color (1f,.5f,0) : Color.green;
                        for (int x = 0; x < buttons.Count; x++)
                        {
                            if (playerWord[i].ToString() == buttons[x].transform.name)
                            {
                                correctLetters += playerWord[i].ToString();
                                lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger(isCB == false ? "isCorret" : "isCorrectCB");
                                buttons[x].GetComponent<Image>().sprite = bgColor[0];
                                count++;
                                break;

                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < GameManager.word.Length; j++)
                        {
                            if (playerWord[i] == GameManager.word[j])
                            {
                                grey = true;
                                lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger(isCB == false ? "isGuess" : "isGuessCB");
                                lines[activeLine].transform.GetChild(i).GetComponent<Image>().color = GameManager.isColorBlind == true ? Color.cyan : Color.yellow;
                                for (int x = 0; x < buttons.Count; x++)
                                {
                                    if (playerWord[i].ToString() == buttons[x].transform.name)
                                    {
                                        correctLetters += playerWord[i].ToString();
                                        buttons[x].GetComponent<Image>().sprite = bgColor[1];
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }

                    if (grey == false)
                    {
                        lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger("isWrong");
                        lines[activeLine].transform.GetChild(i).GetComponent<Image>().color = Color.gray;
                        for (int x = 0; x < buttons.Count; x++)
                        {
                            if (playerWord[i].ToString() == buttons[x].transform.name)
                            {

                                buttons[x].GetComponent<Image>().sprite = bgColor[2];
                                //buttons[x].GetComponent<Button>().interactable = false;
                            }
                        }
                    }

                    yield return new WaitForSeconds(0.9f);

                }


                if (count == GameManager.word.Length - 1)
                {
                    Debug.Log("in here");
                    StartCoroutine(WinAnim());
                }
                else
                {
                    if (activeLine < lines.Count - 1)
                    {
                        activeLine++;
                        PlayerPrefs.SetInt("ActiveLine", activeLine);
                        SetActiveLine();
                    }
                    else
                    {
                        LoseScreen();
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger("isNot");
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger("isNot");
            }
        }




    }

    private bool ControlWordisDB(string word)
    {
        for (int i = 0; i < GameManager.eachLine.Count; i++)
        {

            if (word == GameManager.eachLine[i].Trim())
            {
                return true;
            }
        }

        return false;
    }

    void LoadLastWord()
    {
        playerWord = "";
        int count = 0;
        string correctLetters = "";
        bool grey = false;

        for (int y = 0; y < PlayerPrefs.GetInt("ActiveLine"); y++)
        {
            playerWord = PlayerPrefs.GetString("Line_" + y.ToString());

            for (int i = 0; i < playerWord.Length; i++)
            {
                grey = false;

                if (playerWord[i] == GameManager.word[i])
                {
                    grey = true;
                    lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger(isCB == false ? "isCorret" : "isCorrectCB");
                    lines[y].transform.GetChild(i).GetComponent<Image>().color = isCB == true ? new Color(1f, .5f, 0) : Color.green;
                    for (int x = 0; x < buttons.Count; x++)
                    {
                        if (playerWord[i].ToString() == buttons[x].transform.name)
                        {
                            correctLetters += playerWord[i].ToString();
                            buttons[x].GetComponent<Image>().sprite = bgColor[0];
                            count++;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < GameManager.word.Length; j++)
                    {
                        Debug.Log(playerWord[i] + "    " + GameManager.word[j]);
                        if (playerWord[i] == GameManager.word[j])
                        {
                            grey = true;
                            lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger(isCB == false ? "isGuess" : "isGuessCB");
                            lines[y].transform.GetChild(i).GetComponent<Image>().color = Color.yellow;
                            for (int x = 0; x < buttons.Count; x++)
                            {
                                if (playerWord[i].ToString() == buttons[x].transform.name)
                                {
                                    correctLetters += playerWord[i].ToString();
                                    buttons[x].GetComponent<Image>().sprite = bgColor[1];
                                    break;
                                }
                            }
                            break;
                        }
                    }

                }

                if (grey == false)
                {

                    lines[y].transform.GetChild(i).GetComponent<Image>().color = Color.gray;
                    for (int x = 0; x < buttons.Count; x++)
                    {
                        if (playerWord[i].ToString() == buttons[x].transform.name)
                        {

                            buttons[x].GetComponent<Image>().sprite = bgColor[2];
                            //buttons[x].GetComponent<Button>().interactable = false;
                        }
                    }
                }

            }
        }

        if (count == GameManager.word.Length - 1)
        {
            Debug.Log("in here"); LoadLastWord();
        }
        else
        {
            if (activeLine < lines.Count - 1)
            {
                activeLine++;
                PlayerPrefs.SetInt("ActiveLine", activeLine);
                SetActiveLine();
            }
            else
            {
                LoseScreen();
            }
        }



    }


    IEnumerator WinAnim()
    {
        for (int i = 0; i < lines[activeLine].transform.childCount; i++)
        {
            lines[activeLine].transform.GetChild(i).GetComponent<Animator>().SetTrigger(isCB == false ? "isWin" : "isWinCB");
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(3f);
        WinScreen();

    }



}
