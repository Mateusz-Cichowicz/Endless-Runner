using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;


public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MenuPanel;
    [SerializeField]
    private GameObject m_SettingsPanel;
    [SerializeField]
    private GameObject m_RecordsPanel;
    [SerializeField]
    private GameObject m_GameOverPanel;
    [SerializeField]
    private TMP_Text m_EndGameText;
    [SerializeField]
    private TMP_Text m_StartGameText;

    private GameObject m_ActiveMenu;
    private GameManager m_GameManager;

    [SerializeField]
    private List<TMP_Text> UiRecords = new List<TMP_Text>();
    private void Start()
    {
        m_ActiveMenu = m_MenuPanel;
        m_GameManager = FindAnyObjectByType<GameManager>();
        m_GameManager.OnGameFinished += EndGame;
        DisplayRecords();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && m_ActiveMenu == null) 
        {
            Time.timeScale = 0;
            m_StartGameText.text = "Continue";
            m_MenuPanel.SetActive(true);
            m_ActiveMenu = m_MenuPanel;
        }

    }

    private void EndGame(int points) 
    {
        m_ActiveMenu = m_GameOverPanel;
        ChangeMenu(m_GameOverPanel);
        m_EndGameText.text = "You earned " + points.ToString() + " points";
    }
    public void StartGame() 
    {
        Time.timeScale = 1;
        m_StartGameText.text = "Start Game";
        m_MenuPanel.SetActive(false);
        m_ActiveMenu = null;
    }

    public void ChangeMenu(GameObject menu) 
    {
        m_ActiveMenu.SetActive(false);
        m_ActiveMenu = menu;
        m_ActiveMenu.SetActive(true);
    }
    public void ExitGame() 
    {
    #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
        Application.Quit();
#elif (UNITY_WEBGL)
        Application.OpenURL("https://mateusz-cichowicz.github.io");
#endif
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void DisplayRecords()
    {
        for (int i = 0; i < 10; i++)
        {
            UiRecords[i].text = m_GameManager.Records[i].ToString();
        }
    }
}
