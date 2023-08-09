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

    HighscoreData _highscoreData;
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
        _highscoreData = _highscoreDataGatherer.currentHighscores;
        // check for new speed-run record:
        _speedName = _highscoreData.speedName;
        _speedEmail = _highscoreData.speedEmail;
        _speedTime = _highscoreData.speedTime;
        _speedCrowns = _highscoreData.speedCrowns;
        // check for new crown-run record:
        _crownsName = _highscoreData.crownsName;
        _crownsEmail = _highscoreData.crownsEmail;
        _crownsTime = _highscoreData.crownsTime;
        _crownsCrowns = _highscoreData.crownsCrowns;

        // update highscore UI:
        _speedNameUI.text = _speedName;
        _speedEmailUI.text = _speedEmail;
        _speedTimeUI.text = _speedTime.ToString();
        _speedCrownsUI.text = _speedCrowns.ToString();

        _crownsNameUI.text = _crownsName;
        _crownsEmailUI.text = _crownsEmail;
        _crownsTimeUI.text = _crownsTime.ToString();
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
