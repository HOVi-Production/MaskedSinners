using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cutsceneController : MonoBehaviour
{

    [SerializeField][TextArea()] string cutsceneText;

    [ SerializeField] Writer writer;

    [SerializeField] GameObject button;

    [SerializeField] int sceneIndexOfNextScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        writer.Write(cutsceneText);
        writer.OnWritingDone.AddListener(() =>
        {
            button.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        SceneManager.LoadScene(sceneIndexOfNextScene);
    }
}
