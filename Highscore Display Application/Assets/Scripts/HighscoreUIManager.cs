using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighscoreUIManager : MonoBehaviour
{
    [Header("Highscore UI Settings:")]
    [SerializeField] float _updateInterval = 1.0f;
    [SerializeField] float _rotationInterval = 5.0f;
    [Space(10)]
    [SerializeField] private int _currentDay = 0;
    [SerializeField] GameObject _overwriteDayUI;
    [SerializeField] TMP_Dropdown _overwriteDayInputField;
    private DateTime _startDate = new DateTime(2023, 08, 23);


    [Header("Daily-Highscore Interfaces:")]
    [Tooltip("The position at which this should play in the interface carousel.")]
    [SerializeField] private int _dayInterfaceSlot = 1;  // Default is 1 (second position in the carousel)
    [Space(10)]
    [SerializeField] private GameObject _dayOneInterface;
    [SerializeField] private GameObject _dayTwoInterface;
    [SerializeField] private GameObject _dayThreeInterface;
    [SerializeField] private GameObject _dayFourInterface;
    [SerializeField] private GameObject _dayFiveInterface;

    [Header("Overall Highscores UI")]
    [SerializeField] private TextMeshProUGUI[] _overallSpeedNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _overallSpeedEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _overallSpeedTimesUI;
    [SerializeField] private TextMeshProUGUI[] _overallSpeedCrownsUI;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI[] _overallCrownsNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _overallCrownsEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _overallCrownsTimesUI;
    [SerializeField] private TextMeshProUGUI[] _overallCrownsCrownsUI;

    [Header("Day One Highscores UI")]
    [SerializeField] private TextMeshProUGUI[] _dayOneSpeedNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayOneSpeedEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayOneSpeedTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayOneSpeedCrownsUI;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI[] _dayOneCrownsNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayOneCrownsEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayOneCrownsTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayOneCrownsCrownsUI;

    [Header("Day Two Highscores UI")]
    [SerializeField] private TextMeshProUGUI[] _dayTwoSpeedNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayTwoSpeedEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayTwoSpeedTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayTwoSpeedCrownsUI;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI[] _dayTwoCrownsNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayTwoCrownsEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayTwoCrownsTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayTwoCrownsCrownsUI;

    [Header("Day Three Highscores UI")]
    [SerializeField] private TextMeshProUGUI[] _dayThreeSpeedNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayThreeSpeedEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayThreeSpeedTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayThreeSpeedCrownsUI;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI[] _dayThreeCrownsNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayThreeCrownsEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayThreeCrownsTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayThreeCrownsCrownsUI;

    [Header("Day Four Highscores UI")]
    [SerializeField] private TextMeshProUGUI[] _dayFourSpeedNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayFourSpeedEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayFourSpeedTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayFourSpeedCrownsUI;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI[] _dayFourCrownsNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayFourCrownsEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayFourCrownsTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayFourCrownsCrownsUI;

    [Header("Day Five Highscores UI")]
    [SerializeField] private TextMeshProUGUI[] _dayFiveSpeedNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayFourSpeedEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayFiveSpeedTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayFiveSpeedCrownsUI;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI[] _dayFiveCrownsNamesUI;
    //[SerializeField] private TextMeshProUGUI[] _dayFourCrownsEmailsUI;
    [SerializeField] private TextMeshProUGUI[] _dayFiveCrownsTimesUI;
    [SerializeField] private TextMeshProUGUI[] _dayFiveCrownsCrownsUI;

    [SerializeField] GameObject[] _interfaceCarousel;

    HighscoreDataGatherer _highscoreDataGatherer;

    private void OnEnable()
    {
        // instructions:
        Debug.Log("Instructions: You can switch the monitor where any app/game is displayed by pressing WIN + Shift + left/right arrow.");
        Debug.Log("Ctrl+Shift+1 will toggle the UI that displays the emails of daily victors in a s");
        Debug.Log("Ctrl+Shift+2 will toggle the UI that allows manual overwrite of the day-id. " +
            "NOTE: This is done automatically too, so first check if the correct day is not already selected (read here in log)");

        _highscoreDataGatherer = FindObjectOfType<HighscoreDataGatherer>();

        StartCoroutine(UpdateHighscoreDisplay());

        if (_interfaceCarousel != null)
        {
            StartCoroutine(UIRotation());
        }

        if (_currentDay == 0)
        {
            SetDayAutomatically();
        }
    }
    void SetDayAutomatically()
    {
        DateTime now = DateTime.Now;
        TimeSpan durationSinceStart = now - _startDate;
        int daysSinceStart = durationSinceStart.Days;

        if (daysSinceStart >= 0) // ensure we're not before the start date
        {
            _currentDay = daysSinceStart + 1; // +1 because if it's the start date, it should be day 1, not day 0
            if (_currentDay > 5) // 5 days event
            {
                _currentDay = 5;
            }
        }else // the event hasn't started yet
        {
            _currentDay = 1;
        }

        Debug.Log("Current day: " + _currentDay + ", if this is incorrect, manually overwrite!");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleIdentificationUI();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleManualDayOverwrite();
        }
    }
    private void ToggleManualDayOverwrite()
    {
        if (!_overwriteDayUI.activeInHierarchy)
        {
            _overwriteDayUI.SetActive(true);
        }else
        {
            _overwriteDayUI.SetActive(false);
        }
    }
    public void SetDateManually()
    {
        _currentDay = _overwriteDayInputField.value + 1; // accounting for the value starting at 0!
        Debug.Log("current day has been manually overwritten by: " + _currentDay);
    }
    private void ToggleIdentificationUI()
    {
        //_identificationUIActive = !_identificationUIActive;

        //if (_identificationUIActive)
        //{
            // Print the email values for each day based on the data we have:
            PrintDailyWinnerMail("Day 1 Speed Winner", _highscoreDataGatherer.GetDayOneSpeedHighscores());
            PrintDailyWinnerMail("Day 1 Crowns Winner", _highscoreDataGatherer.GetDayOneCrownsHighscores());

            PrintDailyWinnerMail("Day 2 Speed Winner", _highscoreDataGatherer.GetDayTwoSpeedHighscores());
            PrintDailyWinnerMail("Day 2 Crowns Winner", _highscoreDataGatherer.GetDayTwoCrownsHighscores());

            PrintDailyWinnerMail("Day 3 Speed Winner", _highscoreDataGatherer.GetDayThreeSpeedHighscores());
            PrintDailyWinnerMail("Day 3 Crowns Winner", _highscoreDataGatherer.GetDayThreeCrownsHighscores());

            PrintDailyWinnerMail("Day 4 Speed Winner", _highscoreDataGatherer.GetDayFourSpeedHighscores());
            PrintDailyWinnerMail("Day 4 Crowns Winner", _highscoreDataGatherer.GetDayFourCrownsHighscores());

        PrintDailyWinnerMail("Day 5 Speed Winner", _highscoreDataGatherer.GetDayFiveSpeedHighscores());
        PrintDailyWinnerMail("Day 5 Crowns Winner", _highscoreDataGatherer.GetDayFiveCrownsHighscores());
        //}
    }

    private void PrintDailyWinnerMail(string title, List<HighscoreEntry> entries)
    {
        if (entries != null && entries.Count > 0)
        {
            if (entries[0].email != "test@test.test" && entries[0].name != "[name]" && entries[0].time > 0)
            {
                Debug.Log(title + ": " + entries[0].email);  // assuming the top entry is the winner for the day
            }else
            {
                Debug.Log(title + ": [none]");
            }
        }
        else
        {
            Debug.Log(title + ": [none]");
        }
    }

    IEnumerator UpdateHighscoreDisplay()
    {
        UpdateUIForList(_overallSpeedNamesUI, _overallSpeedTimesUI, _overallSpeedCrownsUI, _highscoreDataGatherer.GetOverallSpeedHighscores());
        UpdateUIForList(_overallCrownsNamesUI, _overallCrownsTimesUI, _overallCrownsCrownsUI, _highscoreDataGatherer.GetOverallCrownsHighscores());

        UpdateUIForList(_dayOneSpeedNamesUI, _dayOneSpeedTimesUI, _dayOneSpeedCrownsUI, _highscoreDataGatherer.GetDayOneSpeedHighscores());
        UpdateUIForList(_dayOneCrownsNamesUI, _dayOneCrownsTimesUI, _dayOneCrownsCrownsUI, _highscoreDataGatherer.GetDayOneCrownsHighscores());

        UpdateUIForList(_dayTwoSpeedNamesUI, _dayTwoSpeedTimesUI, _dayTwoSpeedCrownsUI, _highscoreDataGatherer.GetDayTwoSpeedHighscores());
        UpdateUIForList(_dayTwoCrownsNamesUI, _dayTwoCrownsTimesUI, _dayTwoCrownsCrownsUI, _highscoreDataGatherer.GetDayTwoCrownsHighscores());

        UpdateUIForList(_dayThreeSpeedNamesUI, _dayThreeSpeedTimesUI, _dayThreeSpeedCrownsUI, _highscoreDataGatherer.GetDayThreeSpeedHighscores());
        UpdateUIForList(_dayThreeCrownsNamesUI, _dayThreeCrownsTimesUI, _dayThreeCrownsCrownsUI, _highscoreDataGatherer.GetDayThreeCrownsHighscores());

        UpdateUIForList(_dayFourSpeedNamesUI, _dayFourSpeedTimesUI, _dayFourSpeedCrownsUI, _highscoreDataGatherer.GetDayFourSpeedHighscores());
        UpdateUIForList(_dayFourCrownsNamesUI, _dayFourCrownsTimesUI, _dayFourCrownsCrownsUI, _highscoreDataGatherer.GetDayFourCrownsHighscores());

        UpdateUIForList(_dayFiveSpeedNamesUI, _dayFiveSpeedTimesUI, _dayFiveSpeedCrownsUI, _highscoreDataGatherer.GetDayFiveSpeedHighscores());
        UpdateUIForList(_dayFiveCrownsNamesUI, _dayFiveCrownsTimesUI, _dayFiveCrownsCrownsUI, _highscoreDataGatherer.GetDayFiveCrownsHighscores());

        yield return new WaitForSeconds(_updateInterval);
        StartCoroutine(UpdateHighscoreDisplay());
    }

    void UpdateUIForList(TextMeshProUGUI[] namesUI, TextMeshProUGUI[] timesUI, TextMeshProUGUI[] crownsUI, List<HighscoreEntry> entries)
    {
        for (int i = 0; i < namesUI.Length && i < entries.Count; i++)
        {
            namesUI[i].text = entries[i].name;
            //emailsUI[i].text = entries[i].email;
            timesUI[i].text = FormatTime(entries[i].time);
            crownsUI[i].text = entries[i].crowns.ToString();
        }
    }

    /*
    IEnumerator UpdateHighscoreDisplay()
    {
        UpdateUIForList(_overallSpeedNamesUI, _overallSpeedEmailsUI, _overallSpeedTimesUI, _overallSpeedCrownsUI, _highscoreDataGatherer.GetOverallSpeedHighscores());
        UpdateUIForList(_overallCrownsNamesUI, _overallCrownsEmailsUI, _overallCrownsTimesUI, _overallCrownsCrownsUI, _highscoreDataGatherer.GetOverallCrownsHighscores());

        UpdateUIForList(_dayOneSpeedNamesUI, _dayOneSpeedEmailsUI, _dayOneSpeedTimesUI, _dayOneSpeedCrownsUI, _highscoreDataGatherer.GetDayOneSpeedHighscores());
        UpdateUIForList(_dayOneCrownsNamesUI, _dayOneCrownsEmailsUI, _dayOneCrownsTimesUI, _dayOneCrownsCrownsUI, _highscoreDataGatherer.GetDayOneCrownsHighscores());

        UpdateUIForList(_dayTwoSpeedNamesUI, _dayTwoSpeedEmailsUI, _dayTwoSpeedTimesUI, _dayTwoSpeedCrownsUI, _highscoreDataGatherer.GetDayTwoSpeedHighscores());
        UpdateUIForList(_dayTwoCrownsNamesUI, _dayTwoCrownsEmailsUI, _dayTwoCrownsTimesUI, _dayTwoCrownsCrownsUI, _highscoreDataGatherer.GetDayTwoCrownsHighscores());

        UpdateUIForList(_dayThreeSpeedNamesUI, _dayThreeSpeedEmailsUI, _dayThreeSpeedTimesUI, _dayThreeSpeedCrownsUI, _highscoreDataGatherer.GetDayThreeSpeedHighscores());
        UpdateUIForList(_dayThreeCrownsNamesUI, _dayThreeCrownsEmailsUI, _dayThreeCrownsTimesUI, _dayThreeCrownsCrownsUI, _highscoreDataGatherer.GetDayThreeCrownsHighscores());

        UpdateUIForList(_dayFourSpeedNamesUI, _dayFourSpeedEmailsUI, _dayFourSpeedTimesUI, _dayFourSpeedCrownsUI, _highscoreDataGatherer.GetDayFourSpeedHighscores());
        UpdateUIForList(_dayFourCrownsNamesUI, _dayFourCrownsEmailsUI, _dayFourCrownsTimesUI, _dayFourCrownsCrownsUI, _highscoreDataGatherer.GetDayFourCrownsHighscores());

        yield return new WaitForSeconds(_updateInterval);
        StartCoroutine(UpdateHighscoreDisplay());
    }

    void UpdateUIForList(TextMeshProUGUI[] namesUI, TextMeshProUGUI[] emailsUI, TextMeshProUGUI[] timesUI, TextMeshProUGUI[] crownsUI, List<HighscoreEntry> entries)
    {
        for (int i = 0; i < namesUI.Length && i < entries.Count; i++)
        {
            namesUI[i].text = entries[i].name;
            //emailsUI[i].text = entries[i].email;
            timesUI[i].text = FormatTime(entries[i].time);
            crownsUI[i].text = entries[i].crowns.ToString();
        }
    }*/

    string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }


    IEnumerator UIRotation()
    {
        GameObject currentDayUI = GetCurrentDayUI();  // Get the UI for the current day

        for (int i = 0; i < _interfaceCarousel.Length; i++)
        {
            if (i == _dayInterfaceSlot)  // If this slot is for the current day's UI
            {
                currentDayUI.SetActive(true);
                yield return new WaitForSeconds(_rotationInterval);
                currentDayUI.SetActive(false);
            }else
            {
                _interfaceCarousel[i].SetActive(true);
                yield return new WaitForSeconds(_rotationInterval);
                _interfaceCarousel[i].SetActive(false);
            }
        }
        StartCoroutine(UIRotation());
    }

    // Utility function to get the UI GameObject for the current day
    private GameObject GetCurrentDayUI()
    {
        switch (_currentDay)
        {
            case 1:
                return _dayOneInterface;
            case 2:
                return _dayTwoInterface;
            case 3:
                return _dayThreeInterface;
            case 4:
                return _dayFourInterface;
            case 5:
                return _dayFiveInterface;
            default:
                return null; // or some default UI
        }
    }
    /*
    IEnumerator UIRotation()
    {
        for (int i = 0; i < _interfaceCarousel.Length; i++)
        {
            _interfaceCarousel[i].SetActive(true);
            yield return new WaitForSeconds(_rotationInterval);
            _interfaceCarousel[i].SetActive(false);
        }
        StartCoroutine(UIRotation());
    }*/
}

    



// old but working for single entries:
    /*
    [Header("Highscore UI Settings:")]
    [SerializeField] float _updateInterval = 1.0f;
    [SerializeField] float _rotationInterval = 15.0f;

    [Space(10)]
    [SerializeField] TextMeshProUGUI _speedNameUI;
    [SerializeField] TextMeshProUGUI _speedEmailUI;
    [SerializeField] TextMeshProUGUI _speedTimeUI;
    [SerializeField] TextMeshProUGUI _speedCrownsUI;

    [Space(10)]
    [SerializeField] TextMeshProUGUI _crownsNameUI;
    [SerializeField] TextMeshProUGUI _crownsEmailUI;
    [SerializeField] TextMeshProUGUI _crownsTimeUI;
    [SerializeField] TextMeshProUGUI _crownsCrownsUI;

    [Space(10)]
    //List<GameObject> _interfaceCarousel = new List<GameObject>();
    [SerializeField] GameObject[] _interfaceCarousel;

    SpeedHighscoreData _speedHighscoreData;
    CrownsHighscoreData _crownsHighscoreData;

    HighscoreDataGatherer _highscoreDataGatherer;

    string _speedName;
    string _speedEmail;
    float _speedTime;
    int _speedCrowns;

    string _crownsName;
    string _crownsEmail;
    float _crownsTime;
    int _crownsCrowns;

    private void OnEnable()
    {
        // instructions:
        Debug.Log("Instructions: You can switch the monitor where any app/game is to be displayed by pressing WIN + Shift + left/right arrow.");

        _highscoreDataGatherer = FindObjectOfType<HighscoreDataGatherer>();

        StartCoroutine("UpdateHighscoreDisplay");

        if(_interfaceCarousel != null)
        {
            StartCoroutine(UIRotation());
        }
    }

    /// <summary>
    /// Retrieves the highscore data from the HighscoreDataGatherer-script and displays it.
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateHighscoreDisplay()
    {
        // retrieve newest highscore-data:
        _speedHighscoreData = _highscoreDataGatherer.currentSpeedHighscores;
        _crownsHighscoreData = _highscoreDataGatherer.currentCrownsHighscores;

        // check for new speed-run record:
        _speedName = _speedHighscoreData.speedName;
        _speedEmail = _speedHighscoreData.speedEmail;
        _speedTime = _speedHighscoreData.speedTime;
        _speedCrowns = _speedHighscoreData.speedCrowns;
        // check for new crown-run record:
        _crownsName = _crownsHighscoreData.crownsName;
        _crownsEmail = _crownsHighscoreData.crownsEmail;
        _crownsTime = _crownsHighscoreData.crownsTime;
        _crownsCrowns = _crownsHighscoreData.crownsCrowns;

        // update highscore UI:
        _speedNameUI.text = _speedName;
        _speedEmailUI.text = _speedEmail;
        float _rawTimeS = _speedTime;
        int minutesS = (int)_rawTimeS / 60;
        int secondsS = (int)_rawTimeS - 60 * minutesS;
        int millisecondsS = (int)(1000 * (_rawTimeS - minutesS * 60 - secondsS));
        _speedTimeUI.text = string.Format("{0:00}:{1:00}:{2:000}", minutesS, secondsS, millisecondsS);
        //_speedTimeUI.text = _speedTime.ToString();
        _speedCrownsUI.text = _speedCrowns.ToString();

        _crownsNameUI.text = _crownsName;
        _crownsEmailUI.text = _crownsEmail;
        float _rawTimeC = _crownsTime;
        int minutesC = (int)_rawTimeC / 60;
        int secondsC = (int)_rawTimeC - 60 * minutesC;
        int millisecondsC = (int)(1000 * (_rawTimeC - minutesC * 60 - secondsC));
        _crownsTimeUI.text = string.Format("{0:00}:{1:00}:{2:000}", minutesC, secondsC, millisecondsC);
        //_crownsTimeUI.text = _crownsTime.ToString();
        _crownsCrownsUI.text = _crownsCrowns.ToString();

        yield return new WaitForSeconds(_updateInterval);

        StartCoroutine("UpdateHighscoreDisplay");
    }

    /// <summary>
    /// Rotates the UI-carousel at an adjustable interval, looping through every UI in the array.
    /// This only happens if there is more than one UI in the array.
    /// </summary>
    /// <returns></returns>
    IEnumerator UIRotation()
    {
        for(int i = 0; i < _interfaceCarousel.Length; i++)
        {
            // turn UI on:
            _interfaceCarousel[i].SetActive(true);

            // wait for set time (secs):
            yield return new WaitForSeconds(_rotationInterval);

            // turn UI off and move on to the next one:
            _interfaceCarousel[i].SetActive(false);
        }

        // start over:
        StartCoroutine(UIRotation());
    }
}*/
