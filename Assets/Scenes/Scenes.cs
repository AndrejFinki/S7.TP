using UnityEngine;
using System.Collections;

public class Scenes : MonoBehaviour
{

    public enum Scene
    {
        START = 0,
        GAME,
        SCORE
    }

    public static void LoadScene(Scene scene)
    {
        Application.LoadLevel((int)scene);
    }


    public Scene scene;

    public void Load()
    {
        Scenes.LoadScene(scene);
    }
}
