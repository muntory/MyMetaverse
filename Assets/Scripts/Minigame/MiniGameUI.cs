using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MiniGameUI : MonoBehaviour
{
    bool hasInput = false;
    bool isOver = false;

    [SerializeField]
    GameObject startUI;
    [SerializeField]
    GameObject gameOverUI;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI bestScoreText;


    [SerializeField]
    MiniGamePlayer player;

    private PlayerInput playerInput;
    void Start()
    {
        Time.timeScale = 0f;
        playerInput = player.GetComponent<PlayerInput>();
        if (playerInput != null )
        {
            playerInput.actions.Disable();
        }

        bestScoreText.text = $"Best: {GameManager.Instance.GetBestScore()}"; 
        player.OnPlayerDead += OnGameOver;
        player.OnScoreChanged += UpdateScoreText;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick(InputValue value)
    {
        
        if (!hasInput)
        {
            hasInput = true;
            playerInput.actions.Enable();
            Time.timeScale = 1f;
            startUI.SetActive(false);
        }
        
        if (isOver)
        {
            SceneManager.LoadScene("MainScene");
        }
        
    }

    public void OnGameOver()
    {
        isOver = true;
        gameOverUI.SetActive(true);

    }


    void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }


    private void OnDestroy()
    {
        player.OnScoreChanged -= UpdateScoreText;
        player.OnPlayerDead -= OnGameOver;

    }
}
