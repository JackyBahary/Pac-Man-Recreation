using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip introClip;
    public AudioClip normalClip;
    public AudioClip scaredClip;
    private AudioSource source;
    public static bool scaredReady = false;
    public static bool walkReady = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(playIntroClip());
    }

    // Update is called once per frame
    void Update()
    {
        if (scaredReady)
        {
            StopCoroutine(playNormalClip());
            StartCoroutine(playScaredClip());
            scaredReady = false;
        }
        if (walkReady)
        {
            StopCoroutine(playScaredClip());
            StartCoroutine(playNormalClip());
            walkReady = false;
        }
    }

    IEnumerator playIntroClip()
    {
        source.clip = introClip;
        source.Play();
        yield return new WaitForSeconds(introClip.length);
        StartCoroutine(playNormalClip());
    }

    IEnumerator playNormalClip()
    {
        source.clip = normalClip;
        source.Play();
        source.loop = true;
        yield return null;
    }

    IEnumerator playScaredClip()
    {
        source.clip = scaredClip;
        source.Play();
        source.loop = true;
        yield return null;
    }
}
