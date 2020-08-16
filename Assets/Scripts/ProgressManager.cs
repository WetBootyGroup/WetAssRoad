using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    static ProgressManager instance = null;
    public static ProgressManager Instance;

    // time
    public Text timeText;
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

    void Start()
    {
        gameSpeed = 4;
    }

    void FixedUpdate()
    {
        while(timeRanOut == false){
            if(gameTime > 0){

                gameTime -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(gameTime / 60); 
                float seconds = Mathf.FloorToInt(gameTime % 60);

                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            }
            else{

                print("Game Over! You won!");
                timeRanOut = true;
            }
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
}
