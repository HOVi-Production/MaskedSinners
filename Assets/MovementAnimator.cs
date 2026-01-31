using DG.Tweening;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{

    Sequence anim;

    Transform container;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        container = transform.GetChild(0);
        audioSource = GetComponent<AudioSource>();

        anim = DOTween.Sequence();
        anim.Append(container.DORotate(Vector3.forward * 5, 0.05f).OnComplete(() => audioSource.PlayOneShot(audioSource.clip)));
        anim.AppendInterval(0.2f);
        anim.Append(container.DORotate(Vector3.forward * -5, 0.05f).OnComplete(() => audioSource.PlayOneShot(audioSource.clip)));
        anim.AppendInterval(0.2f);
        anim.SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animate(int dir)
    {
        container.localScale = new Vector3(dir * Mathf.Abs(container.localScale.x), 1, 1);
        if(!anim.IsPlaying())
            anim.Play();
    }

    public void Stop()
    {
        if(anim.IsPlaying())
            anim.Pause();
        container.DORotate(Vector3.zero, 0.01f);
    }
}
