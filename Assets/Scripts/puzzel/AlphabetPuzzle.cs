using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlphabetPuzzle : MonoBehaviour
{
    [Header("Puzzle")]
    [SerializeField] private string targetWord = "KEYBOARD";

    [Header("Table Letters")]
    [SerializeField] private List<TableLetter> tableLetters;

    [Header("Events")]
    [SerializeField] private UnityEvent onLetterCollected;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    private readonly HashSet<char> collectedLetters = new();

    private void Start()
    {
        // Make sure all table letters are hidden at the beginning
        foreach (TableLetter tableLetter in tableLetters)
        {
            if (tableLetter != null)
            {
                tableLetter.gameObject.SetActive(false);
            }
        }
    }

    public bool CollectLetter(char letter)
    {
        letter = char.ToUpper(letter);

        // Make sure this letter is actually required
        if (!targetWord.ToUpper().Contains(letter.ToString()))
        {
            Debug.Log($"Letter {letter} is not part of the puzzle.");
            return false;
        }

        // Don't allow collecting the same letter twice
        if (collectedLetters.Contains(letter))
        {
            Debug.Log($"Letter {letter} has already been collected.");
            return false;
        }

        // Add the letter to the collected letters
        collectedLetters.Add(letter);

        Debug.Log($"Collected letter: {letter}");

        // Find the matching letter on the table
        TableLetter tableLetter = FindTableLetter(letter);

        if (tableLetter != null)
        {
            tableLetter.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"No table letter found for: {letter}");
        }

        onLetterCollected?.Invoke();

        CheckPuzzleCompleted();

        return true;
    }

    private TableLetter FindTableLetter(char letter)
    {
        foreach (TableLetter tableLetter in tableLetters)
        {
            if (tableLetter == null)
                continue;

            if (tableLetter.Letter == letter)
            {
                return tableLetter;
            }
        }

        return null;
    }

    private void CheckPuzzleCompleted()
    {
        // Check every letter required by the target word
        foreach (TableLetter letter in tableLetters)
        {
            if (!letter.gameObject.activeSelf) // Use activeSelf to check if the GameObject is active
            {
                return;
            }
        }

        CompletePuzzle();
    }

    private void CompletePuzzle()
    {
        Debug.Log($"Puzzle completed! Word: {targetWord}");

        onPuzzleCompleted?.Invoke();
    }
}