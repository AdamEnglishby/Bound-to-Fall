using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GGJ21/Text Box")]
public class TextBox : ScriptableObject
{

    [HideInInspector] public bool isShown;
    public string[] lines, wwiseEvents;
    private int _currentLine;

    public void Show(TypewriterByCharacter text)
    {
        text.gameObject.SetActive(true);
        isShown = true;
        _currentLine = 0;
        if (wwiseEvents.GetValue(_currentLine) != null)
        {
            AkSoundEngine.PostEvent(wwiseEvents[_currentLine], text.gameObject);
        }
        text.GetComponent<TMP_Text>().text = lines[_currentLine] + " <wave a=0.25>▼</wave>";
    }

    public bool HasNext => _currentLine < lines.Length - 1;

    public void FinishInstantly(TypewriterByCharacter text)
    {
        text.SkipTypewriter();
    }
    
    public void Next(TypewriterByCharacter text)
    {
        if (HasNext)
        {
            _currentLine++;
            if (wwiseEvents.GetValue(_currentLine) != null)
            {
                AkSoundEngine.PostEvent(wwiseEvents[_currentLine], text.gameObject);
            }
            text.GetComponent<TMP_Text>().text = lines[_currentLine] + " <wave a=0.25>▼</wave>";
        }
    }

    public void Hide(TypewriterByCharacter text)
    {
        isShown = false;
        text.GetComponent<TMP_Text>().text = "";
        text.SkipTypewriter();
    }

}