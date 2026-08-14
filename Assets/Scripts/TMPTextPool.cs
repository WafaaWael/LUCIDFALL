using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TMPTextPool : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private Transform poolParent;
    [SerializeField] private int poolSize = 10;

    [Header("Sequence")]
    [SerializeField] private float totalDuration = 10f;
    [SerializeField] private float intervalDuration = 1f;

    private Queue<TextMeshProUGUI> availableTexts = new();
    private Queue<TextMeshProUGUI> activeTexts = new();

    private Coroutine sequenceCoroutine;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            TextMeshProUGUI text = Instantiate(textPrefab, poolParent);

            text.gameObject.SetActive(false);

            availableTexts.Enqueue(text);
        }
    }

    public void StartSequence()
    {
        if (sequenceCoroutine != null)
            StopCoroutine(sequenceCoroutine);

        sequenceCoroutine = StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        float elapsed = 0f;

        int targetActiveCount = poolSize / 2;

        // -------------------------
        // Fill half of the pool
        // -------------------------

        while (activeTexts.Count < targetActiveCount &&
               elapsed < totalDuration)
        {
            Get();

            yield return new WaitForSeconds(intervalDuration);

            elapsed += intervalDuration;
        }

        // -------------------------
        // Rolling release + Get
        // -------------------------

        while (elapsed < totalDuration)
        {
            ReleaseNext();

            Get();

            yield return new WaitForSeconds(intervalDuration);

            elapsed += intervalDuration;
        }

        // -------------------------
        // Cleanup
        // -------------------------

        while (activeTexts.Count > 0)
        {
            ReleaseNext();
        }

        sequenceCoroutine = null;
    }

    private void Get()
    {
        if (availableTexts.Count == 0)
            return;

        TextMeshProUGUI text = availableTexts.Dequeue();

        text.gameObject.SetActive(true);

        activeTexts.Enqueue(text);
    }

    private void ReleaseNext()
    {
        if (activeTexts.Count == 0)
            return;

        TextMeshProUGUI text = activeTexts.Dequeue();

        text.gameObject.SetActive(false);

        availableTexts.Enqueue(text);
    }
}