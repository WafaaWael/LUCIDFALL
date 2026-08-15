using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Error404Anim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform content;
    [SerializeField] private TextMeshProUGUI textPrefab;

    [Header("Animation")]
    [SerializeField] private int textCount = 8;
    [SerializeField] private float intervalDuration = 0.8f;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float totalDuration = 10f;

    [Header("Events")]
    [SerializeField] private UnityEvent2 _onAnimationStarted;
    [SerializeField] private UnityEvent2 _onAnimationEnded;

    private readonly List<RectTransform> texts = new List<RectTransform>();

    private bool _moveTexts;
    private float animationTimer;

    private float spacing;

    private void Start()
    {
        CreateTexts();

        StartAnimation();
    }

    private void Update()
    {
        if (!_moveTexts)
            return;

        MoveTexts();

        animationTimer += Time.deltaTime;

        if (animationTimer >= totalDuration)
        {
            EndAnimation();
        }
    }

    private void CreateTexts()
    {
        spacing = intervalDuration * moveSpeed;

        for (int i = 0; i < textCount; i++)
        {
            TextMeshProUGUI newText = Instantiate(textPrefab, content);

            newText.text = "ERROR 404";

            RectTransform rect = newText.rectTransform;

            rect.anchoredPosition = new Vector2(
                0f,
                i * spacing
            );

            texts.Add(rect);
        }
    }

    private void MoveTexts()
    {
        foreach (RectTransform text in texts)
        {
            text.anchoredPosition +=
                Vector2.down * moveSpeed * Time.deltaTime;

            if (text.anchoredPosition.y <
                -content.rect.height / 2f - spacing)
            {
                MoveToTop(text);
            }
        }
    }

    private void MoveToTop(RectTransform text)
    {
        float highestY = float.MinValue;

        foreach (RectTransform other in texts)
        {
            if (other == text)
                continue;

            if (other.anchoredPosition.y > highestY)
            {
                highestY = other.anchoredPosition.y;
            }
        }

        text.anchoredPosition = new Vector2(
            0f,
            highestY + spacing
        );
    }

    public void StartAnimation()
    {
        animationTimer = 0f;
        _moveTexts = true;

        _onAnimationStarted?.Invoke();
    }

    private void EndAnimation()
    {
        _moveTexts = false;

        _onAnimationEnded?.Invoke();
    }

    public void SetMoveText(bool move)
    {
        _moveTexts = move;
    }
}