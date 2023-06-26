using Febucci.UI;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ21/Text Box")]
public class TextBox : ScriptableObject
{

    [HideInInspector] public bool isShown;
    public string[] lines;
    private int _currentLine;

    public void Show(TypewriterByCharacter text)
    {
        text.gameObject.SetActive(true);
        isShown = true;
        _currentLine = 0;
        text.GetComponent<TMP_Text>().text = lines[_currentLine];
    }

    public bool HasNext => _currentLine < lines.Length - 1;
    
    public void Next(TypewriterByCharacter text)
    {
        if (HasNext)
        {
            _currentLine++;
            text.GetComponent<TMP_Text>().text = lines[_currentLine];
        }
    }

    public void Hide(TypewriterByCharacter text)
    {
        isShown = false;
        text.GetComponent<TMP_Text>().text = "";
    }

}