using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private int m_Points = 0;
    [SerializeField]
    private TMP_Text m_PointsText;
    [SerializeField]
    ScrollingBackground scrollingBacground;
    [SerializeField]
    List <ObstacleSpawner> obstacleSpawners = new List<ObstacleSpawner>();

    public List<int> Records = new List<int>();
    private float speedMultiplier = 0.01f;

    public event System.Action<int> OnGameFinished;

    private void Start()
    {
        Time.timeScale = 0;
        LoadRecords();
    }

    public void StopGame() 
    {
        OnGameFinished.Invoke(m_Points);
        ManageRecords();
        SaveRecords();
        Time.timeScale = 0;
    }

    public void AddPoints() 
    {
        m_Points += 10;
        UpdatePoints();
        UpdateGameSpeed();
    }

    private void UpdatePoints() 
    {
        m_PointsText.text= "Points: " + m_Points;
    }

    private void UpdateGameSpeed() 
    {
        scrollingBacground.speed += scrollingBacground.speed * speedMultiplier;
        foreach(var spawner in obstacleSpawners) 
        {
            spawner.obstacleSpawnigTime -= spawner.obstacleSpawnigTime * speedMultiplier;
            spawner.obstacleSpeedMultiplier += spawner.obstacleSpeedMultiplier * speedMultiplier;
        }
        //obstacleSpawner.obstacleSpawnigTime -= obstacleSpawner.obstacleSpawnigTime * speedMultiplier;
        //obstacleSpawner.obstacleSpeedMultiplier += obstacleSpawner.obstacleSpeedMultiplier * speedMultiplier;
    }

    private void ManageRecords() 
    {
        Records.Add(m_Points);
        Records = Records.OrderByDescending(num => num).ToList();
    }
    private void SaveRecords()
    {
        for (int i = 0; i < Records.Count; i++) 
        {
            PlayerPrefs.SetInt(i.ToString(), Records[i]);
        }
    }
    private void LoadRecords() 
    {
        for (int i = 0; i < 10; i++) 
        {
            Records.Add(PlayerPrefs.GetInt(i.ToString()));
        }
    }
}
