using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    static ProgressManager instance = null;

    public static ProgressManager Instance
    {
        get
        {
            return instance;
        }
    }

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

    // time
    float gameTime;
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
        gameTime -= Time.deltaTime;
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
