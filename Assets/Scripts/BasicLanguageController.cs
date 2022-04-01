using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicLanguageController : MonoBehaviour
{

    public List<Text> infoLetters;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.gameMode < 2)
        {
            infoLetters[0].text = @"Oyun'da 6 denemede bulun.

Her tahmin 5 harfli do�ru bir kelime olmal�d�r. G�ndermek i�in enter'a bas�n.

Her tahminden sonra kutucuklar�n renkleri tahmininizin yak�nl���na g�re de�i�ecektir.

�rnekler";

            infoLetters[1].text = "Tahmin sonras�nda kutu'da Ye�il Yan�yorsa harfi ve yerini Do�ru tahmin etmi�indir.";
            infoLetters[2].text = "Tahmin sonras�nda kutu'da Sar� Yan�yorsa harfi  do�ru ve yerini yanl�� tahmin etmi�indir.";
            infoLetters[3].text = "Tahmin sonras�nda kutu'da Gri Yan�yorsa harfi  ve yerini yanl�� tahmin etmi�indir.";
        }
        else
        {
            infoLetters[0].text = @"Find 6 attempts in the game.

Each guess must be a correct 5-letter word. Press enter to send.

After each guess, the colors of the boxes will change according to the closeness of your guess.

Examples";

            infoLetters[1].text = "If the box is Green after guessing, you guessed the letter and its location correctly.";
            infoLetters[2].text = "If the box is Yellow after guessing, you guessed the letter correctly and the location incorrectly.";
            infoLetters[3].text = "If the box is Grayed Out after guessing, you guessed the wrong letter and location..";
        }
    }

   
}
