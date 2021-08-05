using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

static public class SQL_Connection
{

    static public Action<string> OnLoginRequestEnded;
    static public Action<string> OnSignUpRequestEnded;
    static public Action<string> OnSendScoreRequestEnded;
    static public Action<string> OnGetRankingRequestEnded;

    public static IEnumerator LoginRegister(string username, string password, TextMeshProUGUI errorText)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/shooter/User Read.php", form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success && www.downloadHandler.text[0] != '*')
        {
            errorText.color = Color.green;
            errorText.text = "Logged In. Have Fun!";
            LoaderManager.Get().SetSessionData(username, 0, 0);
            LoaderManager.Get().LoadSceneAsync("Main Game");
        }
        else
        {
            errorText.color = Color.red;
            errorText.text = "Can't login with current username/password.";
        }
        OnLoginRequestEnded?.Invoke(www.downloadHandler.text);
    }

    public static IEnumerator SignUpRegister(string username, string password, TextMeshProUGUI errorText)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/shooter/User Create.php", form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success && www.downloadHandler.text[0] != '*')
        {
            errorText.color = Color.green;
            errorText.text = "Username created correctly.";

        }
        else
        {
            errorText.color = Color.red;
            errorText.text = "Can't sign up current username/password.";
        }
        OnSignUpRequestEnded?.Invoke(www.downloadHandler.text);
    }

    public static IEnumerator SendScoreRegister()
    {
        SessionData currentSessionData = LoaderManager.Get().GetSessionData();

        WWWForm form = new WWWForm();
        form.AddField("username", currentSessionData.username);
        form.AddField("score", currentSessionData.score);
        form.AddField("deaths", currentSessionData.deaths);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/shooter/Score Insert.php", form);

        yield return www.SendWebRequest();
        yield return new WaitForSeconds(2);
        
        OnSendScoreRequestEnded?.Invoke(www.downloadHandler.text);
    }

    public static IEnumerator RankingRegister()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/shooter/Score Ranking.php", form);

        yield return www.SendWebRequest();

        OnGetRankingRequestEnded?.Invoke(www.downloadHandler.text);
    }

}
