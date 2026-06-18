using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI startParText;
    [SerializeField] private TextMeshProUGUI kicksText;
    [SerializeField] private TextMeshProUGUI endParText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private TextMeshProUGUI totalText;

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
    void Start()
    {
        InitializeUI();
        RegisterButtonListeners();
    }
    void Update()
    {
        //if time is zero show the start pannel
        if (Time.time == 0)
        {
            ShowStartPanel();
        }
        UpdateKicksText();

    }

    private void InitializeUI()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        lvlText.text = "Level " + GameManager.Level;
    }

    private void RegisterButtonListeners()
    {
        startButton.onClick.AddListener(StartNextLevel);
        nextLevelButton.onClick.AddListener(StartNextLevel);
        retryButton.onClick.AddListener(RestartLevel);
    }

    private void UpdateKicksText()
    {
        kicksText.text = "Kicks: " + GameManager.Kicks + "/" + GameManager.Par;
    }

    public void StartNextLevel()
    {
        endPanel.SetActive(false);
        startPanel.SetActive(false);
        GameManager.Instance.NextLevel();
    }

    public void ShowStartPanel()
    {
        GameManager.Kicks = 0;
        startPanel.SetActive(true);
        lvlText.text = "Level " + (GameManager.Level + 1);
    }

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
        endParText.text = "Kicks: " + GameManager.Kicks+ " / " + " Par: " + GameManager.Par;
        if(GameManager.TotalKicks > GameManager.TotalPar)
        {
            totalText.text = "Total: " + GameManager.TotalKicks + " Over Par" ;
        }
        else
        {
            totalText.text = "Total: " + GameManager.TotalKicks + " Under Par";
        }
        
    }

    public void RestartLevel()
    {
        GameManager.Instance.RetryLevel();
        endPanel.SetActive(false);
    }
}
