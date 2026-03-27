using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameOverPanel;
    
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI alertText;

    private Coroutine alertCoroutine;
    public Button pauseButton;
    public Button resumeButton;
    public Button resetButton;
    public Button HomeButton;
    public Button G_resetButton;
    public Button G_HomeButton;
    void Start()
    {
        if (ScenesControler.Instance != null)
        {
            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(ScenesControler.Instance.TogglePause);
            }
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(ScenesControler.Instance.TogglePause);
            }
            if (resetButton != null)
            {
                resetButton.onClick.AddListener(ScenesControler.Instance.RestartCurrentScene);
            }
            if (HomeButton != null)
            {
                HomeButton.onClick.AddListener(() => ScenesControler.Instance.LoadScene("Home"));
            }
            if (G_resetButton != null)
            {
                G_resetButton.onClick.AddListener(ScenesControler.Instance.RestartCurrentScene);
            }
            if (G_HomeButton != null)
            {
                G_HomeButton.onClick.AddListener(() => ScenesControler.Instance.LoadScene("Home"));
            }
        }
    }
    private void Awake()
    {
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        if (alertText != null) alertText.gameObject.SetActive(false);
    }

    public void UpdateTime(float timeInSeconds)
    {
        if (timeText != null)
        {
            int minutes = (int)timeInSeconds / 60;
            int seconds = (int)timeInSeconds % 60;
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ShowAlert(string message, float duration = 3f)
    {
        if (alertText != null)
        {
            if (alertCoroutine != null) StopCoroutine(alertCoroutine);
            alertCoroutine = StartCoroutine(AlertRoutine(message, duration));
        }
    }

    private IEnumerator AlertRoutine(string message, float duration)
    {
        alertText.text = message;
        alertText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        alertText.gameObject.SetActive(false);
    }

    public void ShowPausePanel()
    {
        PausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        PausePanel.SetActive(false);
    }
    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }
}
