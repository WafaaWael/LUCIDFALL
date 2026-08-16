using UnityEngine;

public class TableLetter : MonoBehaviour
{
    [SerializeField] private char letter;

    public char Letter => char.ToUpper(letter);
}