using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreUIManager : MonoBehaviour
{
    [Header("Highscore UI Settings:")]
    [SerializeField] float _updateInterval = 1.0f;
    //[SerializeField] float _rotationInterval = 15.0f;

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

    HighscoreData _highscoreData;

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
        //_highscoreData = FindObjectOfType<HighscoreDataGatherer>().currentHighscores;

        StartCoroutine("UpdateHighscoreDisplay");
    }

    IEnumerator UpdateHighscoreDisplay()
    {
        _highscoreData = FindObjectOfType<HighscoreDataGatherer>().currentHighscores;

        // update highscore information:
        _speedName = _highscoreData.speedName;
        _speedEmail = _highscoreData.speedEmail;
        _speedTime = _highscoreData.speedTime;
        _speedCrowns = _highscoreData.speedCrowns;

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
}
