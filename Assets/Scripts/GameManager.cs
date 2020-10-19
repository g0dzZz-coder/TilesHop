using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Score { get; private set; }

    public bool IsGameStarted { get; private set; }

    public Ball Ball { get; private set; }
    public TileManager TileMgr { get; private set; }
    public UIManager UIMgr { get; private set; }

    private void Awake()
    {
        Ball = FindObjectOfType<Ball>();
        TileMgr = FindObjectOfType<TileManager>();
        UIMgr = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        UIMgr.Init(this);

        IsGameStarted = false;
        Score = 0;
    }

    private void Update()
    {
        if (IsGameStarted)
            return;

        if (Input.GetMouseButtonDown(0))
            StartGame();
    }

    public void StartGame()
    {
        IsGameStarted = true;

        Ball.Init(this);
        TileMgr.Init(this);

        Debug.Log("Game Started");
    }

    public void RestartGame()
    {
        UIMgr.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddPoints()
    {
        Score += 1;

        UIMgr.UpdateScore(Score);
    }

    public void EndGame()
    {
        IsGameStarted = false;

        UIMgr.ShowEndGamePanel();
    }
}
