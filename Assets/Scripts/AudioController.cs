using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip introClip;
    public AudioClip normalClip;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(playNormalClip());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playNormalClip()
    {
        source.clip = introClip;
        source.Play();
        yield return new WaitForSeconds(introClip.length);
        source.clip = normalClip;
        source.Play();
        source.loop = true;
    }
}
