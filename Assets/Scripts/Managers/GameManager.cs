using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int currentScore = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        if (instance && instance != this)
        {
            Destroy(this);
        }
    }


    public void SetCurrentScore(int score)
    {
        currentScore = score;
        UpdateBestScore(currentScore);
    }


    public void AddScore(int score)
    {
        currentScore += score;
    }



    public int GetBestScore()
    {
        int ret = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            ret = PlayerPrefs.GetInt("BestScore");
        }

        return ret;
    }


    void UpdateBestScore(int score)
    {
        int bestScore = GetBestScore();
        if (score > bestScore)
        {
            bestScore = score;
        }

        PlayerPrefs.SetInt("BestScore", bestScore);
        UIManager.Instance.SetBestScore(bestScore);
    }
}
