using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;

public class SensitiveDataManager : MonoBehaviour
{
    [SerializeField] private string _dataPath = "xxx";

    private static readonly string[] JsonFiles =
    {
        "OverallSpeedHighscores.json",
        "OverallCrownsHighscores.json",
        "DayOneSpeedHighscores.json",
        "DayOneCrownsHighscores.json",
        "DayTwoSpeedHighscores.json",
        "DayTwoCrownsHighscores.json",
        "DayThreeSpeedHighscores.json",
        "DayThreeCrownsHighscores.json",
        "DayFourSpeedHighscores.json",
        "DayFourCrownsHighscores.json",
        "DayFiveSpeedHighscores.json",
        "DayFiveCrownsHighscores.json"
    };

    private static readonly List<string> CommonProviders = new List<string>
    {
        "@gmail.com",
        "@yahoo.com",
        "@hotmail.com",
        "@outlook.com",
        "@web.de",
        "@aol.com",
        "@msn.com",
        "@live.com",
        "@comcast.net",
        "@icloud.com"
    };

    private List<string> sortedEmails = new List<string>();
    private Dictionary<string, string> emailNamePairs = new Dictionary<string, string>();

    private string ConstructFullPath(string fileName)
    {
        return Path.Combine(_dataPath, fileName);
    }

    public void GatherData()
    {
        foreach (string file in JsonFiles)
        {
            string fullPath = ConstructFullPath(file);
            if (File.Exists(fullPath))
            {
                string jsonData = File.ReadAllText(fullPath);
                List<HighscoreEntry> entries = JsonUtility.FromJson<HighscoreEntriesWrapper>(jsonData).items;

                foreach (var entry in entries)
                {
                    emailNamePairs[entry.email] = entry.name;
                }
            }
        }

        CompareData(emailNamePairs.Keys.ToList());
        Debug.Log($"Total email-name pairs gathered: {emailNamePairs.Count}");
    }

    private void CompareData(List<string> gatheredEmails)
    {
        HashSet<string> existingHashset = ReadEmailsFromHashList(_dataPath + "/GamescomEmails.json");
        if (existingHashset == null)
        {
            return;
        }

        foreach (var email in gatheredEmails)
        {
            existingHashset.Add(email);
        }

        foreach (var email in existingHashset)
        {
            emailNamePairs.TryAdd(email, "[no name entered]");  // Note: TryAdd requires C# 8.0; replace with ContainsKey check for earlier versions

            //alternative to work with version < C#8:
            /*if (!emailNamePairs.ContainsKey(email)) // Use ContainsKey for compatibility with C# < 8.0
            {
                emailNamePairs[email] = "[no name entered]";
            }*/
        }

        SortData(existingHashset.ToList());
    }

    private HashSet<string> ReadEmailsFromHashList(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        string jsonData = File.ReadAllText(path);
        var emailWrapper = JsonUtility.FromJson<SerializableList<string>>(jsonData);

        return emailWrapper?.items == null ? null : new HashSet<string>(emailWrapper.items);
    }

    private void SortData(List<string> emailList)
    {
        var commonEmails = emailList.Where(email => CommonProviders.Any(provider => email.EndsWith(provider))).ToList();
        var uncommonEmails = emailList.Except(commonEmails).ToList();

        commonEmails.Sort();
        uncommonEmails.Sort();

        sortedEmails = commonEmails.Concat(uncommonEmails).ToList();
    }

    public void WriteCSVList()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // for the email & name CSV:
        string csvFileWithNamesPath = Path.Combine(desktopPath, "Gamescom Name-Email-List (confidential).csv");
        List<string> csvContentWithNames = new List<string> { "Email,     Name" }; // 5x space for readability

        // for the email-only CSV:
        string csvFileEmailOnlyPath = Path.Combine(desktopPath, "Gamescom Email-List (confidential).csv");
        List<string> csvContentEmailOnly = new List<string> { "Email" };

        foreach (var email in sortedEmails)
        {
            // add only the email to the email-only list:
            csvContentEmailOnly.Add($"\"{email}\"");

            if (emailNamePairs.TryGetValue(email, out string name))
            {
                csvContentWithNames.Add($"\"{email}\",     \"{name}\""); // 5x space for readability
            }else
            {
                csvContentWithNames.Add($"\"{email}\",     \"[no name entered]\""); // 5x space for readability
            }
        }

        // write to the csv-files:
        File.WriteAllLines(csvFileWithNamesPath, csvContentWithNames);
        File.WriteAllLines(csvFileEmailOnlyPath, csvContentEmailOnly);
    }

    public void DeleteData()
    {
        foreach (string file in JsonFiles)
        {
            string fullPath = ConstructFullPath(file);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        if (File.Exists(_dataPath + "/GamescomEmails.json"))
        {
            File.Delete(_dataPath + "/GamescomEmails.json");
        }
    }
}

[Serializable]
class HighscoreEntriesWrapper
{
    public List<HighscoreEntry> items;
}

// this worked! just take away the commenting!
/*
private List<string> sortedEmails = new List<string>();

private Dictionary<string, string> emailNamePairs = new Dictionary<string, string>();


public void GatherData()
{
    List<string> allEmails = new List<string>();

    // Base directory where the JSON files are stored
    string baseDirectory = jsonFilePath;

    // Assuming the paths for your 12 JSON files look like this:
    string[] jsonPaths = 
    {
    baseDirectory + "/OverallSpeedHighscores.json",
    baseDirectory + "/OverallCrownsHighscores.json",

    baseDirectory + "/DayOneSpeedHighscores.json",
    baseDirectory + "/DayOneCrownsHighscores.json",

    baseDirectory + "/DayTwoSpeedHighscores.json",
    baseDirectory + "/DayTwoCrownsHighscores.json",

    baseDirectory + "/DayThreeSpeedHighscores.json",
    baseDirectory + "/DayThreeCrownsHighscores.json",

    baseDirectory + "/DayFourSpeedHighscores.json",
    baseDirectory + "/DayFourCrownsHighscores.json",

    baseDirectory + "/DayFiveSpeedHighscores.json",
    baseDirectory + "/DayFiveCrownsHighscores.json"
    };

    foreach (string path in jsonPaths)
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            //Debug.Log($"JSON data from {path}: {jsonData}");

            List<HighscoreEntry> entries = JsonUtility.FromJson<HighscoreEntriesWrapper>(jsonData).items;

            foreach (var entry in entries)
            {
                emailNamePairs[entry.email] = entry.name;
            }

            //Debug.Log("Number of unique email-name pairs so far: " + emailNamePairs.Count);

        }
    }

    CompareData(emailNamePairs.Keys.ToList());
    Debug.Log($"Total email-name pairs gathered: {emailNamePairs.Count}");


}

private void CompareData(List<string> gatheredEmails)
{
    string path = hashListPath + "/GamescomEmails.json";

    if (File.Exists(path))
    {
        string jsonData = File.ReadAllText(path);
        var emailWrapper = JsonUtility.FromJson<SerializableList<string>>(jsonData);

        if (emailWrapper == null || emailWrapper.items == null)
        {
            Debug.LogError("Error deserializing email data or the data is null.");
            return;
        }

        HashSet<string> existingHashset = new HashSet<string>(emailWrapper.items);

        // Loop through the hashset to ensure every email from hashset is added with a default name if it doesn't have one
        foreach (var email in existingHashset)
        {
            if (!emailNamePairs.ContainsKey(email))
            {
                emailNamePairs[email] = "[no name entered]";
            }
        }

        // Now, loop through gathered emails and make sure they are in the existing hashset
        foreach (var email in gatheredEmails)
        {
            existingHashset.Add(email);
        }

        SortData(existingHashset.ToList());
    }
}

private void SortData(List<string> emailList)
{
    // Define the common email providers
    List<string> commonProviders = new List<string>
{
    "@gmail.com",
    "@yahoo.com",
    "@hotmail.com",
    "@outlook.com",
    "@web.de",
    "@aol.com",
    "@msn.com",
    "@live.com",
    "@comcast.net",
    "@icloud.com"
};

    // Split emails into common and uncommon lists
    List<string> commonEmails = emailList.Where(email => commonProviders.Any(provider => email.EndsWith(provider))).ToList();
    List<string> uncommonEmails = emailList.Except(commonEmails).ToList();

    // Sort common emails alphabetically
    commonEmails.Sort();
    uncommonEmails.Sort();

    // Concatenate the lists (common ones first)
    sortedEmails = commonEmails.Concat(uncommonEmails).ToList();

    PrintEmailListToConsole();
    PrintHashSet();
}

public void PrintHashSet()
{
    string path = hashListPath + "/GamescomEmails.json";

    if (File.Exists(path))
    {
        string jsonData = File.ReadAllText(path);
        var emailWrapper = JsonUtility.FromJson<SerializableList<string>>(jsonData);

        if (emailWrapper == null || emailWrapper.items == null || emailWrapper.items.Count == 0)
        {
            Debug.Log("No emails found in the hashset.");
            return;
        }

        Debug.Log("Total emails in the hashset: " + emailWrapper.items.Count);

        foreach (var email in emailWrapper.items)
        {
            Debug.Log("Email found: " + email);
        }
    }
    else
    {
        Debug.Log($"File at path {path} not found.");
    }
}

private void PrintEmailListToConsole()
{ 
    if (sortedEmails != null && sortedEmails.Count > 0)
    {
        //for (int i = 0; i < sortedEmails.Count; i++)
        for (int i = 0; i < sortedEmails.Count; i++)
        {
            //Debug.Log("length of sorted email list: " + sortedEmails.Count);
            Debug.Log(sortedEmails[i]);
        }
    }
}


public void WriteCSVList()
{
    string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
    string csvFilePath = Path.Combine(desktopPath, "GamescomHardcoreEmailList.csv");

    List<string> csvContent = new List<string> { "Email,Name" };  // Header

    foreach (var email in sortedEmails)
    {
        string name;
        if (emailNamePairs.TryGetValue(email, out name))
        {
            csvContent.Add($"\"{email}\",\"{name}\"");  // Email,Name format
        }
        else
        {
            csvContent.Add($"\"{email}\",\"[no name entered]\"");  // Email with default name format
        }
    }

    File.WriteAllLines(csvFilePath, csvContent);
}

public void DeleteLists()
{
    string baseDirectory = jsonFilePath;

    // Assuming the paths for your 12 JSON files look like this:
    string[] jsonPaths =
    {
    baseDirectory + "/OverallSpeedHighscores",
    baseDirectory + "/OverallCrownsHighscores",

    baseDirectory + "/DayOneSpeedHighscores",
    baseDirectory + "/DayOneCrownsHighscores",

    baseDirectory + "/DayTwoSpeedHighscores",
    baseDirectory + "/DayTwoCrownsHighscores",

    baseDirectory + "/DayThreeSpeedHighscores",
    baseDirectory + "/DayThreeCrownsHighscores",

    baseDirectory + "/DayFourSpeedHighscores",
    baseDirectory + "/DayFourCrownsHighscores",

    baseDirectory + "/DayFiveSpeedHighscores",
    baseDirectory + "/DayFiveCrownsHighscores"
    };

    // Loop through each JSON path and delete if the file exists
    foreach (string path in jsonPaths)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    // Delete the hash list file if it exists
    if (File.Exists(hashListPath))
    {
        File.Delete(hashListPath);
    }
}
}

[Serializable]
class HighscoreEntriesWrapper
{
public List<HighscoreEntry> items;
}
*/