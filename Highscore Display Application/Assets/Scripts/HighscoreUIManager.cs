using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreUIManager : MonoBehaviour
{
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
}
