using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public List<RankData> rankData = new();

    public List<GameObject> rankText = new();

    public GameObject rankingUI;

    public TextMeshProUGUI songName;

    public TextMeshProUGUI endText;

    private void Start()
    {
        GamePlayManager.instance.gameEndEvents.AddListener(() => Register());
    }

    private void Update()
    {
        songName.text = GamePlayManager.instance.gameInfo.songName;
    }

    public void OnClickSubMitBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Load()
    {
        rankData.Clear();
        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt($"{GamePlayManager.instance.gameInfo.songName}{i}Score", 0);
            int combo = PlayerPrefs.GetInt($"{GamePlayManager.instance.gameInfo.songName}{i}Combo", 0);

            rankData.Add(new RankData(score, combo));
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < 5; i++)
        {
            TextMeshProUGUI score = rankText[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI combo = rankText[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            score.text = $"{rankData[i].score:N0}";
            combo.text = $"{rankData[i].combo}cb";
        }
    }

    private void Save()
    {
        for (int i = 0; i < Mathf.Min(5, rankData.Count); i++)
        {
            PlayerPrefs.SetInt($"{GamePlayManager.instance.gameInfo.songName}{i}Score", rankData[i].score);
            PlayerPrefs.SetInt($"{GamePlayManager.instance.gameInfo.songName}{i}Combo", rankData[i].combo);
        }

        UpdateUI();
    }

    private void Sort()
    {
        rankData = rankData.OrderByDescending(a => a.score).ToList();
        rankData = rankData.GetRange(0, Mathf.Min(rankData.Count, 5));
    }

    public void Register()
    {
        Load();
        rankingUI.SetActive(true);
        rankData.Add(new RankData(GamePlayManager.instance.score, GamePlayManager.instance.maxCombo));
        Sort();
        Save();
        UpdateUI();

        StartCoroutine(SetTimer());
    }

    private IEnumerator SetTimer()
    {
        for (int i = 5; i >=0; i--)
        {
            endText.text = $"¸¶Ä¡±â...{i}";
            yield return new WaitForSeconds(1f);
        }

        OnClickSubMitBtn();
        yield break;
    }
}

[System.Serializable]
public class RankData
{
    public int score;
    public int combo;

    public RankData(int score, int combo)
    {
        this.score = score;
        this.combo = combo;
    }
}