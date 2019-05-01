using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public ObstacleSpawner spawner;

    public uint highscore;
    public uint score;

    public uint highestStreak;
    public uint currentHighestStreak;
    public uint streak;

    public Text ScoreText;
    public Text GameOverText;
    public Text HighScoreText;
    public Text HighestStreakTest;
    public Text CurrentHighestStreakTest;
    public Text StreakText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance!=this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);

        Debug.Log(Application.persistentDataPath);

        Load();

        GameOverText.gameObject.SetActive(false);

        UpdateScoreTexts();

        UpdateStreakTexts();
        //UpdateHighScoreText();
    }

    private void Update()
    {
        //CheckScore();
    }

    [System.Serializable]
    class GameData
    {
        public uint _highScore;
        public uint _streak;

        public GameData()
        {
            _highScore = 0;
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/GameData.dat");

        GameData data = new GameData();

        data._highScore = highscore;
        data._streak = highestStreak;

        bf.Serialize(file, data);

        file.Close();

        Debug.Log("Data Saved ");


    }

    public void Load()
    {

        if(File.Exists(Application.persistentDataPath + "/GameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);

            file.Close();

            highscore = data._highScore;
            highestStreak = data._streak;

            Debug.Log("Data Loaded");
        }
        else
        {
            Debug.Log("Couldn't Load Data");
        }

    }

    public void CheckScore()
    {
        if(score > highscore)
        {
            highscore = score;
            Save();        
        }
    }

    public void CheckStreak(bool AreColorsEqual)
    {
        if(AreColorsEqual)
        {
            streak++;

            if (streak > currentHighestStreak)
                currentHighestStreak = streak;

            if (currentHighestStreak > highestStreak)
                highestStreak = currentHighestStreak;
        }
        else
        {
            streak = 1;
        }
    }

    public void UpdateScore(uint value, bool AreColorsEqual)
    {
        score+=value;

        CheckScore();
        CheckStreak(AreColorsEqual);

        UpdateScoreTexts();
        UpdateStreakTexts();
        //UpdateHighScoreText();
    }

    void UpdateScoreTexts()
    {
        ScoreText.text = score.ToString();
        HighScoreText.text = "HS : " + highscore.ToString();

    }

    void UpdateStreakTexts()
    {
        StreakText.text = "Streak : " + streak.ToString();

        CurrentHighestStreakTest.text = "CHSt : " + currentHighestStreak.ToString();

        HighestStreakTest.text = "HSt : " + highestStreak.ToString();
    }

    public void GameOver()
    {
        spawner.gameObject.SetActive(false);

        GameOverText.gameObject.SetActive(true);
    }
}
