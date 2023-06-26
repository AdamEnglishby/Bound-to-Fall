using Febucci.UI;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ21/Text Box")]
public class TextBox : ScriptableObject
{

    [HideInInspector] public bool isShown, isInProgress;
    public string[] lines;
    private int _currentLine;

    public void Show(TypewriterByCharacter text)
    {
        text.gameObject.SetActive(true);
        isShown = true;
        _currentLine = 0;
        text.GetComponent<TMP_Text>().text = lines[_currentLine];
        isInProgress = true;
    }

    public bool HasNext => _currentLine < lines.Length - 1;

    public void FinishInstantly(TypewriterByCharacter text)
    {
        text.SkipTypewriter();
        isInProgress = false;
    }
    
    public void Next(TypewriterByCharacter text)
    {
        if (HasNext)
        {
            _currentLine++;
            text.GetComponent<TMP_Text>().text = lines[_currentLine];
            isInProgress = true;
        }
    }

    public void Hide(TypewriterByCharacter text)
    {
        isInProgress = false;
        isShown = false;
        text.GetComponent<TMP_Text>().text = "";
        text.SkipTypewriter();
    }

}