using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;

public class ChoseDifficulty : MonoBehaviour
{
    public static double validDate;

    public static double Timestamp()
    {
        return (DateTime.Now -
        new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalMilliseconds;
    }

    public bool checkTeacher;
    public ScriptableDeck[] decks;
    public TeacherDialog dialog;
    public YesNoDialog processing;
    public YesNoDialog error;
    private int level;

    public void StartLevel(int level)
    {
        this.level = level;
        if (level == 0 || !checkTeacher)
        {
            Load();
            return;
        }
        double currentTime = Timestamp();
        if (currentTime < validDate)
        {
            Load();
        }
        else
        {
            dialog.ShowDialog(Teacher);
        }

    }

    private void Teacher(string teacher)
    {
        StartCoroutine("GetTeacher", teacher);
    }

    private IEnumerator GetTeacher(string teacher)
    {   
        processing.ShowDialog(null, null);
        string teacherUrl = "https://digitmile.firebaseio.com/users.json?print=pretty&orderBy=\"email\"&equalTo=\"" + teacher + "\"";
        WWW teacherWWW = new WWW(teacherUrl);
        yield return teacherWWW;
        try
        {
            var teacherJSON = JSON.Parse(teacherWWW.text);
            if (teacherJSON == null || teacherJSON.Count != 1)
            {
                ShowError();
                yield break;
                // return true;
            }
            foreach (string id in teacherJSON.ChildNames)
            {
                StartCoroutine("GetClasses", id);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            ShowError();
        }
    }

    private IEnumerator GetClasses(string id)
    {
        string classesUrl = "https://digitmile.firebaseio.com/classes/" + id + ".json";
        WWW classesWWW = new WWW(classesUrl);
        yield return classesWWW;
        try
        {
            var classesJSON = JSON.Parse(classesWWW.text);
            if (classesJSON == null)
            {
                ShowError();
                yield break;
                // return true;
            }
            double currentTime = Timestamp();
            double classLength = 90 * 60 * 1000;
            foreach (JSONNode c in classesJSON.Childs)
            {
                double start = c["startTime"].AsDouble;
                if (start < currentTime && start + classLength > currentTime)
                {
                    validDate = start + classLength;
                    Load();
                    yield break;
                    // return true;
                }
            }
            ShowError();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            ShowError();
        }
    }

    private void ShowError()
    {
        processing.HideDialog();
        error.ShowDialog(null, null);
    }

    private void Load()
    {
        Deck.deck = decks[level];
        Scenes.LoadScene(Scenes.Scene.GAME);
    }

}
