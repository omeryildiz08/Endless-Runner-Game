using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text inGameScoreText;
    public Text gameOverScoreText;
    public Text highestScoreText;
    public GameObject gameOverPanel;
    //private bool isGameover = false;
   public RoadSpawner roadSpawner;

    //json verisi
    private GameData gameData;

    private void Start()
    {
        roadSpawner = FindObjectOfType<RoadSpawner>(); // Yolu bul ve roadSpawner'a ata
        inGameScoreText.gameObject.SetActive(true);
        gameOverPanel.SetActive(false);
        gameData = SaveSystem.LoadGameData(); //JSON verisini yükle
    }
    private void Update()
    {
       
       
    }
    public void AddScore(int pointValue)
    {
        score += pointValue;
        inGameScoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
       // isGameover = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        inGameScoreText.gameObject.SetActive(false);

        gameOverScoreText.text = "Your Score:" + score;

        //eğer oyuncunun skoru yüksekse , high score u güncelle
        if (score > gameData.highestScore)
        {
            gameData.highestScore = score;
            SaveSystem.SaveGameData(gameData);
        }
        highestScoreText.text = "Highest Score:" + gameData.highestScore;

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Zamanı normale döndürüyoruz
        if (roadSpawner != null)
        {
            roadSpawner.ResetRoads();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
        
    }
}
[System.Serializable]
public class GameData
{
    public int highestScore;
}

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/gameData.JSON";

    //veriyi kaydeden fonksiyon
    public static void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    //veriyi yükleyen fonksiyon
    public static GameData LoadGameData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            //eğer dosya yoksa yeni bir game data döndür
            return new GameData { highestScore = 0 };
        }
    }













}