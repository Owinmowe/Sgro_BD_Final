using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Gameplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameplayManager gm = null;
    [Header("General HUD Components")]
    [SerializeField] UI_AnimationComponent gameplay_HUD = null;
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI unSavedScoreText = null;
    [SerializeField] TextMeshProUGUI savedScoreText = null;
    [SerializeField] TextMeshProUGUI currentLifeText = null;
    [SerializeField] TextMeshProUGUI currentDeathsText = null;
    [SerializeField] TextMeshProUGUI timeText = null;

    void Start()
    {
        gameplay_HUD.TransitionIn();
        gm.OnPlayerGotPoints += UpdateUnsavedScoreText;
        gm.OnPlayerSavedPoints += UpdateSavedScoreText;
        gm.OnPlayerDeath += UpdateDeathsText;
        gm.OnTimeUpdate += UpdateTimeText;
        gm.OnPlayerRecieveDamage += UpdateLifeText;
    }


    void UpdateUnsavedScoreText(int score)
    {
        unSavedScoreText.text = "Unsaved Score: " + score;
    }

    void UpdateSavedScoreText(int score)
    {
        savedScoreText.text = "Saved Score: " + score;
        unSavedScoreText.text = "Unsaved Score: " + 0;
    }

    void UpdateDeathsText(int deaths)
    {
        currentDeathsText.text = "Deaths: " + deaths;
    }

    void UpdateTimeText(float time)
    {
        timeText.text = "Time: " + Mathf.RoundToInt(time);
    }

    void UpdateLifeText(int currentLife)
    {
        currentLifeText.text = "Life: " + currentLife;
    }

}
