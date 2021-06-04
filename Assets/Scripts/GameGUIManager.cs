using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject GameGUI;
    public GameObject HomeGUI;
    public Text scoreCountingText;
    public Image powerBarSlider;

    public Dialog archivementDialog;
    public Dialog gameOverDialog;
    public Dialog helpDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void SHowGameGUI(bool isShow)
    {
        if (GameGUI)
        {
            GameGUI.SetActive(isShow);
        }
        if (HomeGUI)
        {
           HomeGUI.SetActive(!isShow);
        }
    }

    public void UpdateScoreCounting(int score)
    {
        if (scoreCountingText)
        {
            scoreCountingText.text = score.ToString();
        }
    }
    public void UpdatePowerBar(float curVal, float totalVal)
    {
        if (powerBarSlider)
        {
            powerBarSlider.fillAmount=curVal / totalVal;
        }
    }
    public void ShowArchivementDialog()
    {
        if (archivementDialog)
        {
            archivementDialog.Show(true);
        }
    }
    public void ShowGameOverDialog()
    {
        if (gameOverDialog)
        {
          gameOverDialog.Show(true);
        }
    }
    public void ShowHelpDialog()
    {
        if (helpDialog)
        {
           helpDialog.Show(true);
        }
    }
}
