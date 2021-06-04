using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchivementDialog : Dialog
{
    public Text bestScoreText;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (bestScoreText)
        {
            bestScoreText.text = Prefs.BestScore.ToString();
        }
    }

}
