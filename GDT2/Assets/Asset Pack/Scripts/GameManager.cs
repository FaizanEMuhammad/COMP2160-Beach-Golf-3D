using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] int parIncreasePerLevel = 2;
    private int par = 3;
    private int toatalPar = 0;
    static public int TotalPar
    {
        get
        {
            return Instance.toatalPar;
        }
    }

    static public int Par
    {
        get
        {
            return Instance.par;
        }
    }

    [SerializeField] private int level = -1;

    static public int Level
    {
        get
        {
            return Instance.level;
        }
    }

    private int kicks;
    private int totalKicks;

    static public int TotalKicks
    {
        get
        {
            return Instance.totalKicks;
        }
    }

    static public int Kicks
    {
        get
        {
            return Instance.kicks;
        }
        set
        {
            Instance.kicks = value;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextLevel()
    {
        level++;
        SceneManager.LoadScene(level);
        par = level + parIncreasePerLevel;
        toatalPar += par;
        ResetKicks();
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(level);
    }

    public void Kick()
    {
        kicks++;
        totalKicks++;
    }

    public void ResetKicks()
    {
        kicks = 0;
    }

}
