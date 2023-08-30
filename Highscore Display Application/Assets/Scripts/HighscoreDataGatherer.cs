using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class HighscoreDataGatherer : MonoBehaviour
{
    [Tooltip("Enter the base file path where the highscore-stats Jsons are saved on the gaming-device.")]
    [SerializeField] private string baseFilePath = "xxx";

    [Space(10)]
    [Tooltip("This determines the length of the intervals to check for new highscores. Adjust as needed.")]
    [SerializeField] float _updateInterval = 1.0f;

    [Space(30)]
    [Header("DO NOT TOUCH:")]
    List<HighscoreEntry> overallSpeedHighscores = new List<HighscoreEntry>();
    List<HighscoreEntry> overallCrownsHighscores = new List<HighscoreEntry>();

    List<HighscoreEntry> dayOneSpeedHighscores = new List<HighscoreEntry>();
    List<HighscoreEntry> dayOneCrownsHighscores = new List<HighscoreEntry>();

    List<HighscoreEntry> dayTwoSpeedHighscores = new List<HighscoreEntry>();
    List<HighscoreEntry> dayTwoCrownsHighscores = new List<HighscoreEntry>();

    List<HighscoreEntry> dayThreeSpeedHighscores = new List<HighscoreEntry>();
    List<HighscoreEntry> dayThreeCrownsHighscores = new List<HighscoreEntry>();

    List<HighscoreEntry> dayFourSpeedHighscores = new List<HighscoreEntry>();
    List<HighscoreEntry> dayFourCrownsHighscores = new List<HighscoreEntry>();

    List<HighscoreEntry> dayFiveSpeedHighscores = new List<HighscoreEntry>();
    List<HighscoreEntry> dayFiveCrownsHighscores = new List<HighscoreEntry>();

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

        dayFiveSpeedHighscores = LoadListFromJson("/DayFiveSpeedHighscores.json");
        dayFiveCrownsHighscores = LoadListFromJson("/DayFiveCrownsHighscores.json");
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

    /// <summary>
    /// This script has getter methods added to return the highscore lists.
    /// </summary>
    /// <returns></returns>
    public List<HighscoreEntry> GetOverallSpeedHighscores() => overallSpeedHighscores;
    public List<HighscoreEntry> GetOverallCrownsHighscores() => overallCrownsHighscores;

    public List<HighscoreEntry> GetDayOneSpeedHighscores() => dayOneSpeedHighscores;
    public List<HighscoreEntry> GetDayOneCrownsHighscores() => dayOneCrownsHighscores;

    public List<HighscoreEntry> GetDayTwoSpeedHighscores() => dayTwoSpeedHighscores;
    public List<HighscoreEntry> GetDayTwoCrownsHighscores() => dayTwoCrownsHighscores;

    public List<HighscoreEntry> GetDayThreeSpeedHighscores() => dayThreeSpeedHighscores;
    public List<HighscoreEntry> GetDayThreeCrownsHighscores() => dayThreeCrownsHighscores;

    public List<HighscoreEntry> GetDayFourSpeedHighscores() => dayFourSpeedHighscores;
    public List<HighscoreEntry> GetDayFourCrownsHighscores() => dayFourCrownsHighscores;

    public List<HighscoreEntry> GetDayFiveSpeedHighscores() => dayFiveSpeedHighscores;
    public List<HighscoreEntry> GetDayFiveCrownsHighscores() => dayFiveCrownsHighscores;

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
}