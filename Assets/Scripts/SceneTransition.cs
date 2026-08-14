using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(Transition(sceneIndex));
    }

    private IEnumerator Transition(string sceneName)
    {
        yield return FadeOut();

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return FadeIn();
    }

    private IEnumerator Transition(int sceneIndex)
    {
        yield return FadeOut();

        yield return SceneManager.LoadSceneAsync(sceneIndex);

        yield return FadeIn();
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            fadePanel.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }

        fadePanel.alpha = 1f;
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            fadePanel.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }

        fadePanel.alpha = 0f;
    }
}