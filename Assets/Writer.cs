using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Writer : MonoBehaviour
{
    [SerializeField] float writingSpeed;
    [SerializeField] TMPro.TMP_Text textField;

    public UnityEvent OnWritingDone = new();
 
    public void Write(string text)
    {
        StartCoroutine(WritingCoroutine(text));
    }

    private IEnumerator WritingCoroutine(string text)
    {
        textField.text = "";
        
        foreach(var letter in text)
        {
            textField.text += letter;

            if(letter !=  ' ')
            {
                yield return new WaitForSeconds(writingSpeed);
            }
        }
        
        OnWritingDone.Invoke();
    }
}
