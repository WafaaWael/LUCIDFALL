using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AlphabetPuzzle : MonoBehaviour
{
    [Header("Puzzle")]
    [SerializeField] private string targetWord = "KEYBOARD";

    [Header("UI")]
    [SerializeField] private TMP_Text[] letterSlots;
    [SerializeField] private TMP_Text completedWordText;

    [Header("Events")]
    [SerializeField] private UnityEvent onLetterCollected;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    private int currentIndex = 0;

    private void Start()
    {
        // Clear the slots
        for (int i = 0; i < letterSlots.Length; i++)
        {
            letterSlots[i].text = "_";
        }

        completedWordText.gameObject.SetActive(false);
    }

    public void CollectLetter(char letter)
    {
        if (currentIndex >= targetWord.Length)
            return;

        letter = char.ToUpper(letter);

        // Add letter to the next available slot
        letterSlots[currentIndex].text = letter.ToString();

        currentIndex++;

        onLetterCollected?.Invoke();

        // Check if word is complete
        if (currentIndex >= targetWord.Length)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        completedWordText.text = targetWord;
        completedWordText.gameObject.SetActive(true);

        Debug.Log($"Word completed: {targetWord}");

        onPuzzleCompleted?.Invoke();
    }
}