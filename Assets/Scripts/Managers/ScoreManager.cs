using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int lines;
    public int level = 1;
    public int linesPerLevel = 5;
    const int minLines = 1;
    const int maxLines = 4;
    public Text linesText;
    public Text levelText;
    public Text scoreText;
    public bool didLevelUp = false;

    public void ScoreLine(int n)
    {
        didLevelUp = false;
        n = Mathf.Clamp(n, minLines, maxLines);
        switch (n)
        {
            case 1:
                score += 40 * level;
                break;
            case 2:
                score += 100 * level;
                break;
            case 3:
                score += 300 * level;
                break;
            case 4:
                score += 1200 * level;
                break;
        }
        lines -= n;
        if (lines <= 0)
        {
            LevelUp();
        }
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        if (scoreText)
        {
            scoreText.text = PadZero(score, 5);
        }
        if (linesText)
        {
            linesText.text = lines.ToString();
        }
        if (levelText)
        {
            levelText.text = level.ToString();
        }
    }

    private void Start()
    {
        Reset();
    }
    public void Reset()
    {
        level = 1;
        lines = linesPerLevel * level;
        UpdateUIText();
    }
    string PadZero(int n, int padDigits)
    {
        string str = n.ToString();
        while (str.Length < padDigits)
        {
            str = "0" + str;
        }
        return str;
    }
    public void LevelUp()
    {
        level++;
        lines = linesPerLevel * level;
        didLevelUp = true;
    }
}
