using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class Sceenfader : MonoBehaviour
{
    [SerializeField] UnityEvent2 _onEventStared;
    [SerializeField] UnityEvent2 _onEventFinished;
    [SerializeField] private Canvas blinkPanel;
    [SerializeField] private float blinkDuration = 0.08f;
    private void Start()
    {
        Blink();
    }

    public void Blink()
    {
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        // Close eyes
        blinkPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(blinkDuration);

        // Open eyes
        blinkPanel.gameObject.SetActive(false);
    }
    
}
