using BrunoMikoski.AnimationSequencer;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class AnimationSequenceManager : MonoBehaviour
{
    [SerializeField] private AnimationSequencerController _animSeq;
    [SerializeField] private UnityEvent2 _onSequenceComplete;

    [Header("Timers")]
    [SerializeField] private float _bigTimer = 10f;
    [SerializeField] private float _smallTimer = 2f;

    [SerializeField] private bool _isLoop;

    private Coroutine _loopCoroutine;

    private void Start()
    {
        if (_isLoop)
            StartSequence();
    }

    public void StartSequence()
    {
        if (_loopCoroutine != null)
            StopCoroutine(_loopCoroutine);

        _loopCoroutine = StartCoroutine(AnimationLoop());
    }

    private IEnumerator AnimationLoop()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _bigTimer)
        {
            // Call your function
            PlayAnimation();

            // Wait for the small timer
            yield return new WaitForSeconds(_smallTimer);

            elapsedTime += _smallTimer;
        }
        _onSequenceComplete?.Invoke();
        _loopCoroutine = null;
    }

    private void PlayAnimation()
    {
        _animSeq.Play();
    }
}