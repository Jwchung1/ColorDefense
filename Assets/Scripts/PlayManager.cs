using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    
    public static bool isGameOver;
    public static PlayManager instance;
    private int life;
    private int gold;
    private int round;
    public bool isRound;
    public bool isTimerRunning;
    public bool isPause = false;
    public GameObject pauseUI;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI roundText; 
    public TextMeshProUGUI TimerText;
    void Awake()
    {
        life = 10;
        gold = 5000;
        round = 0;
        isGameOver = false;
        isRound = false;
        instance = this;
    }
   void Start()
   {
        InvokeRepeating("CheckRoundOver", 1.0f, 1.5f);
        StartCoroutine("GoldPerTime");
   }
   IEnumerator GoldPerTime()
   {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(3);
            EarnGold(1);
        }
   }
    void Update()
    {
        lifeText.text = "Life:"+life;
        goldText.text = "Gold:"+gold;
        roundText.text = "Round:"+round;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPause == false)
            {
                isPause = true;
                pauseUI.SetActive(true);
            }
            else if(isPause == true)
            {
                isPause = false;
                pauseUI.SetActive(false);
            }
        }
    }
    
    public bool CanBuy(int amountToUse)
    {
        if(gold < amountToUse)
            return false;
        else
            return true;
    }
    public void UseGold(int amountToUse)
    {
        gold -= amountToUse;
    }
    public void EarnGold(int amount)
    {
        gold += amount;
    }
    public void NextRound()
    {
        round++;
        isRound = true;
        
        SpawnManager.instance.StartWave(round);
    }
    void CheckRoundOver()
    {
        if(!isGameOver)
        {
            if(isRound && SpawnManager.instance.enemiesAlive <= 0)
            {
                isRound = false;
                if(round > 19)
                {
                    GameWin();
                }
            }
            else if(!isRound && !isTimerRunning)
            {
                StartCoroutine("Timer");
            }
        }
    }
    IEnumerator Timer()
    {
        isTimerRunning = true;
        int time = 10;
        TimerText.SetText(time.ToString());
        while(time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            TimerText.SetText(time.ToString());
        }
        isTimerRunning = false;
        NextRound();
    }
    public void LoseLife()
    {
        if(round == 10 || round == 20)
        {
            life -= 5;
        }
        else
        {
            life--;
        }
        if(life <= 0)
        {
            GameOver();
        }
    }
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public TextMeshProUGUI survivedRoundsText;
    void GameOver()
    {
        isGameOver = true;
        survivedRoundsText.text = (round-1).ToString();
        gameOverUI.SetActive(true);
    }
    void GameWin()
    {
        isGameOver = true;
        survivedRoundsText.text = (round-1).ToString();
        gameWinUI.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
