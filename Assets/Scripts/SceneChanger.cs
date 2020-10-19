using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;

    private int sceneToLoad = 0;

    public void FadeToLevel(int newScene)
    {
        sceneToLoad = newScene;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
