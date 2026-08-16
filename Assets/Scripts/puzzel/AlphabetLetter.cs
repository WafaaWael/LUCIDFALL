using UnityEngine;

public class AlphabetLetter : MonoBehaviour
{
    [SerializeField] private char letter;
    [SerializeField] private AlphabetPuzzle puzzle;
    [SerializeField] private GameObject letterObject;

    public void Collect()
    {
        puzzle.CollectLetter(letter);

        gameObject.SetActive(false);
        letterObject.SetActive(true);
    }
}