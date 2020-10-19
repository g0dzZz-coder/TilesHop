using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText = null;
    [SerializeField] TMP_Text scoreEndText = null;
    [SerializeField] CanvasGroup endGamePanel = null;
    [SerializeField] SceneChanger sceneChanger = null;
    [SerializeField] float durationAnim = 0.33f;

    private GameManager gameMgr;

    public void Init(GameManager gameMgr)
    {
        this.gameMgr = gameMgr;

        ShowElement(scoreText.GetComponent<CanvasGroup>());
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void LoadScene(int sceneIndex)
    {
        sceneChanger.FadeToLevel(sceneIndex);
    }

    public void ShowEndGamePanel()
    {
        HideElement(scoreText.GetComponent<CanvasGroup>(), DisableScoreText);

        scoreEndText.text = scoreText.text;
        ShowElement(endGamePanel);
    }

    public void HideEndGamePanel()
    {
        HideElement(endGamePanel, DisableEndGamePanel);
    }

    private void ShowElement(CanvasGroup element)
    {
        element.alpha = 0.0f;
        element.blocksRaycasts = true;
        element.gameObject.SetActive(true);
        element.DOFade(1.0f, durationAnim);
    }

    private void HideElement(CanvasGroup element, TweenCallback callback)
    {
        element.blocksRaycasts = false;
        element.DOFade(0.0f, durationAnim).OnComplete(callback);
    }

    private void DisableScoreText() => scoreText.gameObject.SetActive(false);
    private void DisableEndGamePanel() => endGamePanel.gameObject.SetActive(false);
}
