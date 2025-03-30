using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboValueText;

    private void Update()
    {
        comboText.text = $"{GamePlayManager.instance.combo} Combo";
        scoreText.text = $"{GamePlayManager.instance.score:N0}";
        string text = "";
        if (GamePlayManager.instance.combo >= 0)
            text = "x1";
        if (GamePlayManager.instance.combo >= 20)
            text = "x2";
        if (GamePlayManager.instance.combo >= 40)
            text = "x3";
        if (GamePlayManager.instance.combo >= 80)
            text = "x4";

        comboValueText.text = text;
    }
}
