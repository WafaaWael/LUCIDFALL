using UnityEngine;

public class AlphabetLetter : MonoBehaviour
{
    [SerializeField] private char letter;
    [SerializeField] private AlphabetPuzzle puzzle;

    public void Collect()
    {
        puzzle.CollectLetter(letter);

        gameObject.SetActive(false);
    }
}