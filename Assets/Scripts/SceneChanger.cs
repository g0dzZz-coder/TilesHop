using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    private int _sceneToLoad = 0;

    public void FadeToLevel(int newScene)
    {
        _sceneToLoad = newScene;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }
}
