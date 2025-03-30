using UnityEngine;
using UnityEngine.Events;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public Transform correctPos;

    public int score;
    public int combo;
    public int maxCombo;

    public GameInfo gameInfo;


    public UnityEvent gameEndEvents;

    private void Update()
    {
        maxCombo = Mathf.Max(combo, maxCombo);
    }

    public void GameEnd()
    {
        gameEndEvents?.Invoke();
    }
}
