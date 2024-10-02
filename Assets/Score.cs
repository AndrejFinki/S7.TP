using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{

    public static int place;
    public static int correct;
    public static int wrong;
    public static int tiles;
    public static int maxTiles;

    public Text placeText;
    public Text correctText;
    public Text wrongText;
    public Text pointsText;
    public Text tilesText;

    void Start()
    {
        placeText.text = string.Format(placeText.text, place);
        float percent = ((float)correct) / (correct + wrong);
        int correctPoints = (int)(percent * 30);
        correctText.text = string.Format(correctText.text, percent.ToString("0.00"), 30, correctPoints);
        int wrongPoints = wrong * -5;
        wrongText.text = string.Format(wrongText.text, wrong, -5, wrongPoints);
        int tilePoints = tiles * 2;
        tilesText.text = string.Format(tilesText.text, tiles, 2, tilePoints);
        int points = correctPoints + wrongPoints + tilePoints;
        pointsText.text = string.Format(pointsText.text, points);
    }

}
