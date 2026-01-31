using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenucontroller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(CardSystem.Instance.handSize > 0)
            {
                return;   
            }

            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeInHierarchy);
        }
    }

    public void ExittoMainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
