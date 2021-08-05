using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

public class UI_MainMenu : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] UI_AnimationComponent mainMenu_HUD = null;

    [Header("Log In/Sign Up")]
    [SerializeField] UI_AnimationComponent LogIn_HUD = null;
    [SerializeField] TextMeshProUGUI usernameText = null;
    [SerializeField] TextMeshProUGUI passwordText = null;
    [SerializeField] TextMeshProUGUI errorText = null;


    private void Start()
    {
        LogIn_HUD.TransitionOut();
        mainMenu_HUD.TransitionIn();
    }

    public void StartMainMenu()
    {
        mainMenu_HUD.TransitionIn();
        LogIn_HUD.TransitionOut();
    }

    public void StartLogInMenu()
    {
        LogIn_HUD.TransitionIn();
        mainMenu_HUD.TransitionOut();
    }

    public void CallLoginRegister()
    {
        StopAllCoroutines();
        StartCoroutine(SQL_Connection.LoginRegister(usernameText.text, passwordText.text, errorText));
    }

    public void CallSignUpRegister()
    {
        StopAllCoroutines();
        StartCoroutine(SQL_Connection.SignUpRegister(usernameText.text, passwordText.text, errorText));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

