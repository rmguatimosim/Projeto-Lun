using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    //Singleton
    public static ScoreManager Instance { get; private set; }

    //Constants
private static readonly string KEY_HIGH_SCORE = "HighScore";
private static readonly string KEY_PLAYER_NAME = "PlayerName";


    //score
    private string playerName;
    private int highestScore;
    private string highestScorePlayer;


    void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this;
        }

        //retrieve high score and profile from player prefs
        if (PlayerPrefs.HasKey(KEY_HIGH_SCORE))
        {
            highestScore = PlayerPrefs.GetInt(KEY_HIGH_SCORE);
        }
        else
        {
            highestScorePlayer = "00000";
        }

        if (PlayerPrefs.HasKey(KEY_PLAYER_NAME))
        {
            highestScorePlayer = PlayerPrefs.GetString(KEY_PLAYER_NAME);
        }
        else
        {
            highestScorePlayer = "Sem pontuação";
        }
    }

    public bool IsNewRecord(int score)
    {
        return score > highestScore;
    }


    public string PrintHighScore()
    {
        return highestScore.ToString();
    }
    public string PrintHighScorePlayer()
    {
        return highestScorePlayer;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }
    public string GetPlayerName()
    {
        return playerName;
    }

    public void SaveNewRecord(int score)
    {
        //save highest score
        PlayerPrefs.SetInt(KEY_HIGH_SCORE, score);
        PlayerPrefs.SetString(KEY_PLAYER_NAME, playerName);
        PlayerPrefs.Save();
    }




}
