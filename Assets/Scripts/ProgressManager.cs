using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    static ProgressManager instance = null;
    public static ProgressManager Instance;

    // time
    public Text timeText;
    public Text pussyCount;
    public bool timeRanOut = false;
    public float gameTime = 60;

    //{
    //    get
    //    {
    //        return instance;
    //    }
    //}

    void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    
    // killed cats
    int killCount;

    // game speed (difficulty)
    public float gameSpeed;

    public GameObject gameOverGood;
    public GameObject gameOverBad;

    void Start()
    {
        gameSpeed = 4;
    }

    void FixedUpdate()
    {
        if (timeRanOut == false){
            if(gameTime > 0){

                gameTime -= Time.deltaTime;

            }
            else{
                gameTime = 0;
                timeRanOut = true;
            }

            float minutes = Mathf.FloorToInt(gameTime / 60); 
            float seconds = Mathf.FloorToInt(gameTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        pussyCount.text = killCount.ToString();

        if (timeRanOut || killCount >= 10) {
            StartCoroutine("gameOverScreen");
        }
    }

    public void IncreaseKillCount() {
        killCount += 1;
    }

    public int GetKillCount() {
        return killCount;
    }

    public float GetGameSpeed() {
        return gameSpeed;
    }

    IEnumerator gameOverScreen() {
        //show game over screen
        if (killCount >= 10) {
            gameOverBad.SetActive(true);
        }
        else if (timeRanOut) {
            gameOverGood.SetActive(true);
        }

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("TitleScreen");
    }
}
