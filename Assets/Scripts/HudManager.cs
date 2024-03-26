using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }

    public GameObject gameOverObject;
    public Text scoreGameOverText;

    public GameObject winObject;
    public Text scoreWinText;

    public Slider hpBar;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {

            Instance = this;
        }
    }




    public  void gameOver()
    {
        Time.timeScale = 0;
        ScoreManager.Instance.scoreText.gameObject.SetActive(false);
        scoreGameOverText.text = ScoreManager.Instance.score.ToString();
        gameOverObject.SetActive(true);
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ShotUp");
    }


    public void win()
    {
        Time.timeScale = 0;
        ScoreManager.Instance.scoreText.gameObject.SetActive(false);
        scoreWinText.text = ScoreManager.Instance.score.ToString();
        winObject.SetActive(true);
    }

    public void playerHp()
    {
        hpBar.maxValue = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().maxHp;
        hpBar.value = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().hp;
    }
}
