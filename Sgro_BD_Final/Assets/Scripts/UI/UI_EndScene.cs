using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EndScene : MonoBehaviour
{
    [SerializeField] UI_AnimationComponent endScene_HUD = null;
    [SerializeField] List<TextMeshProUGUI> scoreTexts = null;
    [SerializeField] TextMeshProUGUI scoreText = null;

    List<SessionData> rankingData = new List<SessionData>();

    private void Awake()
    {
        StartCoroutine(SQL_Connection.SendScoreRegister());
        SQL_Connection.OnSendScoreRequestEnded += StartGettingHighScore;
    }

    private void Start()
    {
        endScene_HUD.TransitionIn();
        SessionData currentData = LoaderManager.Get().GetSessionData();
        scoreText.text = currentData.username + "\nScore: " + currentData.score + "\nDeaths: " + currentData.deaths;
    }

    void StartGettingHighScore(string text)
    {
        StartCoroutine(SQL_Connection.RankingRegister());
        SQL_Connection.OnGetRankingRequestEnded += SetHighScore;
    }

    void SetHighScore(string scoreText)
    {
        rankingData = new List<SessionData>();
        var newString = scoreText;
        var HighScoreStringArray = newString.Split('_');
        for (int i = 0; i < HighScoreStringArray.Length-1; i++)
        {
            var scoreComponent = HighScoreStringArray[i].Split('-');
            rankingData.Add(new SessionData());
            rankingData[i].username = scoreComponent[0];
            
            rankingData[i].deaths = Convert.ToInt32(scoreComponent[1]);
            rankingData[i].score = Convert.ToInt32(scoreComponent[2]);
        }

        for (int i = 0; i < rankingData.Count; i++)
        {
            scoreTexts[i].text = rankingData[i].username + " " + rankingData[i].deaths + " " + rankingData[i].score;
        }
    }

    public void BackToMenu()
    {
        LoaderManager.Get().LoadSceneAsync("Start Scene");
    }

    public void PlayAgain()
    {
        LoaderManager.Get().LoadSceneAsync("Main Game");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        SQL_Connection.OnGetRankingRequestEnded -= SetHighScore;
        SQL_Connection.OnSendScoreRequestEnded -= StartGettingHighScore;
    }

}
