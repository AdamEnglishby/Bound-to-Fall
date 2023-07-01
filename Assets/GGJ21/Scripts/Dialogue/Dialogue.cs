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
        _currentTextBox.Show(FindObjectOfType<Dialogue>().text);

        InputHandler.OnPrimaryButtonPressed += AdvanceText;
    }

    private static void AdvanceText(InputValue value)
    {
        if (!value.isPressed) return;
        
        if(FindObjectOfType<Dialogue>().text.isShowingText)
        {
            _currentTextBox.FinishInstantly(FindObjectOfType<Dialogue>().text);
            return;
        }
        
        if (_currentTextBox.HasNext)
        {
            //AkSoundEngine.PostEvent("Play_OpeningConversation", gameObject);
            _currentTextBox.Next(FindObjectOfType<Dialogue>().text);
            
        }
        else
        {
            _currentTextBox.Hide(FindObjectOfType<Dialogue>().text);
            InputHandler.OnPrimaryButtonPressed -= AdvanceText;
            //AkSoundEngine.PostEvent("Reset_OpeningConversation", gameObject);
        }
    }
    
    private IEnumerator TextBoxDelay()
    {
        yield return new WaitForSeconds(3);
        AkSoundEngine.PostEvent("Play_OpeningConversation", gameObject);
        Dialogue.Show(openingConversation);
    }

}