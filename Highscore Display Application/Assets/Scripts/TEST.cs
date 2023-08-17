using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class TEST : MonoBehaviour
{
}
    /*[Tooltip("Enter the base file path where the highscore-stats Jsons are saved on the gaming-device.")]
    [SerializeField] private string baseFilePath = "xxx";

    [Space(10)]
    [Tooltip("This determines the length of the intervals to check for new highscores. Adjust as needed.")]
    [SerializeField] float _updateInterval = 30.0f;

    [Space(30)]
    [Header("DO NOT TOUCH:")]

    public List<HighscoreEntry> overallSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> overallCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayOneSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayOneCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayTwoSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayTwoCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayThreeSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayThreeCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayFourSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayFourCrownsHighscores = new List<HighscoreEntry>();

    private void Start()
    {
        if (DataAvailable())
        {
            StartCoroutine("CheckData");
        }
    }

    bool DataAvailable()
    {
        if (File.Exists(baseFilePath + "/OverallSpeedHighscores.json"))
        {
            Debug.Log("During test: Overall speed-highscore found (representing all data-paths)!");
            return true;
        }
        else
        {
            Debug.Log("No data has been saved yet, or you are using an incorrect path!");
            return false;
        }
    }

    IEnumerator CheckData()
    {
        LoadAllHighscoreListsFromJson();

        yield return new WaitForSeconds(_updateInterval);
        StartCoroutine("CheckData");
    }

    public void LoadAllHighscoreListsFromJson()
    {
        overallSpeedHighscores = LoadListFromJson("/OverallSpeedHighscores.json");
        overallCrownsHighscores = LoadListFromJson("/OverallCrownsHighscores.json");

        dayOneSpeedHighscores = LoadListFromJson("/DayOneSpeedHighscores.json");
        dayOneCrownsHighscores = LoadListFromJson("/DayOneCrownsHighscores.json");

        dayTwoSpeedHighscores = LoadListFromJson("/DayTwoSpeedHighscores.json");
        dayTwoCrownsHighscores = LoadListFromJson("/DayTwoCrownsHighscores.json");

        dayThreeSpeedHighscores = LoadListFromJson("/DayThreeSpeedHighscores.json");
        dayThreeCrownsHighscores = LoadListFromJson("/DayThreeCrownsHighscores.json");

        dayFourSpeedHighscores = LoadListFromJson("/DayFourSpeedHighscores.json");
        dayFourCrownsHighscores = LoadListFromJson("/DayFourCrownsHighscores.json");
    }

    private List<HighscoreEntry> LoadListFromJson(string filePath)
    {
        string fullPath = baseFilePath + filePath;

        if (File.Exists(fullPath))
        {
            string jsonData = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<SerializableList<HighscoreEntry>>(jsonData).items;
        }

        return new List<HighscoreEntry>();
    }

    public void TestOutput()
    {
        Debug.Log("---- Top 5 Overall Speed Highscores ----");
        for (int i = 0; i < Mathf.Min(5, overallSpeedHighscores.Count); i++)
        {
            Debug.Log("Rank " + (i + 1) + ": " + overallSpeedHighscores[i].name + " | Time: " + overallSpeedHighscores[i].time + "s | Crowns: " + overallSpeedHighscores[i].crowns);
        }

        Debug.Log("---- Top 5 Overall Crowns Highscores ----");
        for (int i = 0; i < Mathf.Min(5, overallCrownsHighscores.Count); i++)
        {
            Debug.Log("Rank " + (i + 1) + ": " + overallCrownsHighscores[i].name + " | Time: " + overallCrownsHighscores[i].time + "s | Crowns: " + overallCrownsHighscores[i].crowns);
        }

        Debug.Log("---- Top 5 Day One Speed Highscores ----");
        for (int i = 0; i < Mathf.Min(5, dayOneSpeedHighscores.Count); i++)
        {
            Debug.Log("Rank " + (i + 1) + ": " + dayOneSpeedHighscores[i].name + " | Time: " + dayOneSpeedHighscores[i].time + "s | Crowns: " + dayOneSpeedHighscores[i].crowns);
        }

        Debug.Log("---- Top 5 Day One Crowns Highscores ----");
        for (int i = 0; i < Mathf.Min(5, dayOneCrownsHighscores.Count); i++)
        {
            Debug.Log("Rank " + (i + 1) + ": " + dayOneCrownsHighscores[i].name + " | Time: " + dayOneCrownsHighscores[i].time + "s | Crowns: " + dayOneCrownsHighscores[i].crowns);
        }

        Debug.Log("---- Top 5 Day Two Speed Highscores ----");
        for (int i = 0; i < Mathf.Min(5, dayTwoSpeedHighscores.Count); i++)
        {
            Debug.Log("Rank " + (i + 1) + ": " + dayTwoSpeedHighscores[i].name + " | Time: " + dayTwoSpeedHighscores[i].time + "s | Crowns: " + dayTwoSpeedHighscores[i].crowns);
        }

        Debug.Log("---- Top 5 Day Two Crowns Highscores ----");
        for (int i = 0; i < Mathf.Min(5, dayTwoCrownsHighscores.Count); i++)
        {
            Debug.Log("Rank " + (i + 1) + ": " + dayTwoCrownsHighscores[i].name + " | Time: " + dayTwoCrownsHighscores[i].time + "s | Crowns: " + dayTwoCrownsHighscores[i].crowns);
        }
    }
}

[System.Serializable]
public class HighscoreEntry
{
    public string name;
    public string email;
    public float time;
    public int crowns;
}

[System.Serializable]
public class SerializableList<T>
{
    public List<T> items;

    public SerializableList(List<T> list)
    {
        items = list;
    }
}*/
