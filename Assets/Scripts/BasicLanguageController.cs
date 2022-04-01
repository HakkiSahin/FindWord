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

Her tahmin 5 harfli doðru bir kelime olmalýdýr. Göndermek için enter'a basýn.

Her tahminden sonra kutucuklarýn renkleri tahmininizin yakýnlýðýna göre deðiþecektir.

Örnekler";

            infoLetters[1].text = "Tahmin sonrasýnda kutu'da Yeþil Yanýyorsa harfi ve yerini Doðru tahmin etmiþindir.";
            infoLetters[2].text = "Tahmin sonrasýnda kutu'da Sarý Yanýyorsa harfi  doðru ve yerini yanlýþ tahmin etmiþindir.";
            infoLetters[3].text = "Tahmin sonrasýnda kutu'da Gri Yanýyorsa harfi  ve yerini yanlýþ tahmin etmiþindir.";
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
