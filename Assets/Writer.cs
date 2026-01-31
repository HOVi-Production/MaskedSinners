using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Writer : MonoBehaviour
{
    [SerializeField] float writingSpeed;
    [SerializeField] TMPro.TMP_Text textField;

    public UnityEvent OnWritingDone = new();
 
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Write(string text)
    {
        StopAllCoroutines();
        StartCoroutine(WritingCoroutine(text));
    }

    private IEnumerator WritingCoroutine(string text)
    {
        textField.text = "";
        
        foreach(var letter in text)
        {
            textField.text += letter;

            audioSource.pitch = Mathf.Lerp(0.9f, 1.1f, UnityEngine.Random.Range(0f, 1f));
            audioSource.PlayOneShot(audioSource.clip);

            if(letter !=  ' ')
            {
                yield return new WaitForSeconds(writingSpeed);
            }
        }
        
        OnWritingDone.Invoke();
    }
}
