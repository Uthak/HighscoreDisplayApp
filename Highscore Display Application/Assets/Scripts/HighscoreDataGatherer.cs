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
    public List<HighscoreEntry> overallCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayOneSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayOneCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayTwoSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayTwoCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayThreeSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayThreeCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayFourSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayFourCrownsHighscores = new List<HighscoreEntry>();

    public List<HighscoreEntry> dayFiveSpeedHighscores = new List<HighscoreEntry>();
    public List<HighscoreEntry> dayFiveCrownsHighscores = new List<HighscoreEntry>();

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























/*{
    // tutorial on Json-savings: https://www.youtube.com/watch?v=pVXEUtMy_Hc 

    [Tooltip("Enter the exact file path where the highscore-stats Json gets saved on the gaming-device here! (Exact means EXACT!)")]
    [SerializeField] string _filePathSpeed = "xxx";
    [SerializeField] string _filePathCrowns = "xxx";

    [Space(10)]
    [Tooltip("Day 1 is the 24th; day 2 is the 25th; day 3 is the 26th; day 4 is 27th. Switch at the start of every day!")]
    [SerializeField] int _day = 1;

    [Space(10)]
    [Tooltip("This determins the length of the intervals to check for new highscores. Adjust as needed.")]
    [SerializeField] float _updateInterval = 30.0f;

    [Space(30)]
    [Header("DO NOT TOUCH:")]
    public SpeedHighscoreData currentSpeedHighscores = new SpeedHighscoreData(); // this contains the most recent highscores of the ongoing day.
    public CrownsHighscoreData currentCrownsHighscores = new CrownsHighscoreData(); // this contains the most recent highscores of the ongoing day.


    private void Start()
    {
        if (DataAvailable())
        {
            // Start checking the highscores at an interval:
            StartCoroutine("CheckData");

            // Load data of previous days:
            if (_day != 1)
            {
                //LoadPreviousDays();
            }
        }
    }

    /// <summary>
    /// Check at the desired location (path) if a Json (save-file) has already been created. If not, return.
    /// </summary>
    /// <returns></returns>
    bool DataAvailable()
    {
        if (System.IO.File.ReadAllText(_filePathSpeed) != null && System.IO.File.ReadAllText(_filePathCrowns) != null)
        {
            Debug.Log("Data found!");
            return true;
        }else 
        {
            Debug.Log("No data has been saved yet, or you are using an incorrect path!");
            return false;
        }
    }

    /// <summary>
    /// Retrieves the highscore data from the host game, reads it and saves it in public variables
    /// within this script. This happens on an adjustable interval.
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckData()
    {
        LoadCurrentHighscoresFromJson();

        yield return new WaitForSeconds(_updateInterval);

        StartCoroutine("CheckData");
    }

    public void LoadCurrentHighscoresFromJson()
    {
        //string highscoreData = System.IO.File.ReadAllText(_filePath);
        //currentHighscores = JsonUtility.FromJson<HighscoreData>(highscoreData);

        string speedHighscoreData = System.IO.File.ReadAllText(_filePathSpeed);
        currentSpeedHighscores = JsonUtility.FromJson<SpeedHighscoreData>(speedHighscoreData);
        
        string crownsHighscoreData = System.IO.File.ReadAllText(_filePathCrowns);
        currentCrownsHighscores = JsonUtility.FromJson<CrownsHighscoreData>(crownsHighscoreData);

        // testing:
        //Debug.Log("Load Complete!");
    }
}

[System.Serializable]
public class SpeedHighscoreData
{
    [Header("Speed Highscore:")]
    public string speedName;
    public string speedEmail;
    public float speedTime;
    public int speedCrowns;
}

[System.Serializable]
public class CrownsHighscoreData
{
    [Header("Crowns Highscore:")]
    public string crownsName;
    public string crownsEmail;
    public float crownsTime;
    public int crownsCrowns;
}

/// <summary>
/// Use this to save the daily data (best-of-day) in a list of entries which gets sorted by 
/// </summary>
[System.Serializable]
public class DailyData
{
    public List<IndividualEntry> individualEntries = new List<IndividualEntry>();
}

[System.Serializable]
public class IndividualEntry
{
    public string name;
    public string email;

    public float time;
    public int crowns;
}*/