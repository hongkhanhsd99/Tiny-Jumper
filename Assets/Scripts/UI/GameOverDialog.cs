using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverDialog : Dialog
{
    public Text bestScoreText;
    bool replayBtnClicked;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestScoreText)
        {
            bestScoreText.text = Prefs.BestScore.ToString();
        }
    }

    public void Replay()
    {
        replayBtnClicked = true;
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToHome()
    {
        GameGUIManager.Ins.SHowGameGUI(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (replayBtnClicked)
        {
            GameGUIManager.Ins.SHowGameGUI(true);

            GameManager.Ins.PlayGame();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
