using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    [Header("Pre Exisiting Objects")]
    [SerializeField] GameObject rock = null;
    [SerializeField] SavePoint savePoint = null;
    [SerializeField] List<GameObject> platforms = null;
    [SerializeField] LoseLifeTrigger triggerBelowPlatforms = null;
    Vector3 playerStartingPosition = Vector3.zero;

    [Header("Coins")]
    [SerializeField] GameObject coinPrefab = null;
    [SerializeField] Vector3 coinsOffsetFromPlatforms = Vector3.zero;
    [SerializeField] float timeBetweenCoins = 5f;
    List<int> usedPositions;

    [Header("Gameplay General")]
    [SerializeField] float gameTime = 120f;
    [SerializeField] float playerRespawnTime = 3f;
    [SerializeField] int playerMaxLifePoints = 3;
    int playerCurrentLifePoints;
    float currentTime = 0;

    public Action<int> OnPlayerGotPoints;
    public Action<int> OnPlayerSavedPoints;
    public Action<int> OnPlayerRecieveDamage;
    public Action<int> OnPlayerDeath;
    public Action<float> OnTimeUpdate;

    int savedScore = 0;
    int unSavedScore = 0;
    int deaths = 0;

    private void Awake()
    {
        usedPositions = new List<int>(platforms.Count);
        playerStartingPosition = rock.transform.position;
        playerCurrentLifePoints = playerMaxLifePoints;
        triggerBelowPlatforms.OnTriggerActivated += PlayerRecievedDamage;
        savePoint.OnPlayerCollided += SaveScore;
    }

    private void Start()
    {
        StartCoroutine(CoinsCreationLogic());
        StartCoroutine(EndGameTimer());
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        OnTimeUpdate?.Invoke(currentTime);
    }

    IEnumerator EndGameTimer()
    {
        yield return new WaitForSeconds(gameTime);
        LoaderManager.Get().SetSessionData(LoaderManager.Get().GetSessionData().username, savedScore, deaths);
        LoaderManager.Get().LoadSceneAsync("End Scene");
    }

    void SaveScore()
    {
        savedScore += unSavedScore;
        unSavedScore = 0;
        OnPlayerSavedPoints?.Invoke(savedScore);
    }

    void PlayerRecievedDamage()
    {
        rock.GetComponent<Rock>().StopMovement();
        playerCurrentLifePoints--;
        OnPlayerRecieveDamage?.Invoke(playerCurrentLifePoints);
        StartCoroutine(RespawnCoroutine());
        if(playerCurrentLifePoints > 0)
        {
            CameraController.instance.ShakeCamera();
        }
        else
        {
            playerCurrentLifePoints = playerMaxLifePoints;
            PlayerDied();
        }

    }

    void PlayerDied()
    {
        CameraController.instance.StrongShakeCamera();
        deaths++;
        unSavedScore /= 2;
        OnPlayerDeath(deaths);
        OnPlayerGotPoints?.Invoke(unSavedScore);
    }

    void CoinGrabbed(bool redcoin, int score)
    {
        if (redcoin)
        {
            PlayerRecievedDamage();
        }
        else
        {
            unSavedScore += score;
            OnPlayerGotPoints?.Invoke(unSavedScore);
        }
    }

    IEnumerator CoinsCreationLogic()
    {
        do
        {
            bool openSpaceFound = false;
            int pos = 0;
            while (!openSpaceFound)
            {
                pos = UnityEngine.Random.Range(0, platforms.Count);
                if (!usedPositions.Contains(pos))
                {
                    usedPositions.Add(pos);
                    openSpaceFound = true;
                }
            }
            GameObject go = Instantiate(coinPrefab, platforms[pos].transform.position + coinsOffsetFromPlatforms, Quaternion.identity, transform);
            go.GetComponent<Coin>().OnCollision += CoinGrabbed;
            yield return new WaitForSeconds(timeBetweenCoins);
        } while (true);
    }

    IEnumerator RespawnCoroutine()
    {
        CameraController.instance.StopFollowingPlayer();
        yield return new WaitForSeconds(playerRespawnTime);
        rock.transform.position = playerStartingPosition;
        rock.GetComponent<Rock>().StartMovement();
        CameraController.instance.StartFollowingPlayer();
    }

}
