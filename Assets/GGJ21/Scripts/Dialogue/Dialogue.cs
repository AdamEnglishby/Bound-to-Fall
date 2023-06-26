using System.Collections;
using Febucci.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{

    private static TextBox _currentTextBox;

    public TextBox openingConversation;
    public TypewriterByCharacter text;

    private void Start()
    {
        StartCoroutine(TextBoxDelay());
    }

    public static void Show(TextBox box)
    {
        if (_currentTextBox && _currentTextBox.isShown)
        {
            _currentTextBox.Hide(FindObjectOfType<Dialogue>().text);
        }
        _currentTextBox = box;
        FindObjectOfType<Dialogue>().text.gameObject.SetActive(true);
        if (!_currentTextBox.isShown)
        {
            _currentTextBox.Show(FindObjectOfType<Dialogue>().text);
        }

        InputHandler.OnPrimaryButtonPressed += AdvanceText;
    }

    private static void AdvanceText(InputValue value)
    {
        if (!value.isPressed) return;
        
        if (_currentTextBox.HasNext)
        {
            _currentTextBox.Next(FindObjectOfType<Dialogue>().text);
        }
        else
        {
            _currentTextBox.Hide(FindObjectOfType<Dialogue>().text);
            InputHandler.OnPrimaryButtonPressed -= AdvanceText;
        }
    }
    
    private IEnumerator TextBoxDelay()
    {
        yield return new WaitForSeconds(3);
        Dialogue.Show(openingConversation);
    }

}