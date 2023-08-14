using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreDataGatherer : MonoBehaviour
{
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
    /*
    void LoadPreviousDays()
    {
        switch (_day)
        {
            case 1: // 24th
                // nothing to load.
                break;

            case 2: // 25th
                LoadDayOneJson();
                break;

            case 3: // 26th
                LoadDayOneJson();
                LoadDayTwoJson();
                break;

            case 4: // 27th
                LoadDayOneJson();
                LoadDayTwoJson();
                LoadDayThreeJson();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Button to save a days data-set at the end of a day to be loaded up seperately next time.
    /// </summary>
    public void SaveThisDaysData()
    {
        switch (_day)
        {
            case 1: // 24th
                SaveDayOneToJson();
                break;

            case 2: // 25th
                SaveDayTwoToJson();
                break;

            case 3: // 26th
                SaveDayThreeToJson();
                break;

            case 4: // 27th
                SaveDayFourToJson(); // redundant?
                break;

            default:
                break;
        }
    }

    //TESTING!
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToJson();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadCurrentHighscoresFromJson();
        }
    }

    public void SaveDayOneToJson()
    {
        string inventoryData = JsonUtility.ToJson(currentHighscores);
        string filePath = Application.persistentDataPath + "/DayOneData.json"; // this could be anything "x" with ".json" in the end!
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("savefile has been created");
    }

    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(currentHighscores);
        string filePath = Application.persistentDataPath + "/InventoryData.json"; // this could be anything "x" with ".json" in the end!
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("savefile has been created");
    }

    */
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
}


















    /*
    //TESTING!
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToJson();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadCurrentHighscoresFromJson();
        }
    }

    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(highscoreData);
        string filePath = Application.persistentDataPath + "/InventoryData.json"; // this could be anything "x" with ".json" in the end!
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("savefile has been created");
    }

    public void LoadCurrentHighscoresFromJson()
    {
        string highscoreData = System.IO.File.ReadAllText(_filePath);
        this.highscoreData = JsonUtility.FromJson<HighscoreData>(highscoreData);
        Debug.Log("Load Complete!");

        /*
        //string filePath = Application.persistentDataPath + "/InventoryData.json"; // this could be anything "x" with ".json" in the end!

        //string filePath = _filePath;
        string inventoryData = System.IO.File.ReadAllText(_filePath);
        

        inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        Debug.Log("Load Complete!");
    }
}

[System.Serializable]
public class HighscoreData
{
    // overal:
    // fastest
    // most crowns

    // day 1:
    // fastest
    // most crowns

    // day 2: 
    // fastest
    // most crowns

    // day 3:
    // fastest
    // most crowns

    // day 4:
    // fastest
    // most crowns

    // day 5:
    // fastest
    // most crowns

    public int gold;
    public bool isFulL;
    public List<Items> items = new List<Items>();
}

[System.Serializable]
public class IndividualEntry
{
    public string name;
    public string email;

    public float time;
    public int crowns;
}*/

/*
[System.Serializable]
public class Inventory
{
    public int gold;
    public bool isFulL;
    public List<Items> items = new List<Items>();
}

[System.Serializable]
public class Items
{
    public string name;
    public string description;
}*/