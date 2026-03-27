using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIControl : MonoBehaviour
{
    [SerializeField] private GameObject HelpPanel;
    public Button StartButton;
    public Button QuitButton;
    void Start()
    {
        if (ScenesControler.Instance != null)
        {
            if (StartButton != null)
            {
                StartButton.onClick.AddListener(() => ScenesControler.Instance.LoadScene("game"));
            }
            if (QuitButton != null)
            {
                QuitButton.onClick.AddListener(ScenesControler.Instance.QuitGame);
            }
        }
    }
    private void Awake()
    {
        HelpPanel.SetActive(false);
    }

    public void ShowHelpPanel()
    {
        HelpPanel.SetActive(true);
    }

    public void HideHelpPanel()
    {
        HelpPanel.SetActive(false);
    }
}
