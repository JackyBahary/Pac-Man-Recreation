using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public Animator animator;
    public AudioClip movementClip;
    private AudioSource source;
    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = movementClip;
        source.PlayOneShot(movementClip);
        StartCoroutine(playMovementAudio());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -7.5f && transform.position.y >= 13.5f)
        {
            animator.SetBool("UpReady", false);
            animator.SetBool("RightReady", true);
            transform.position += (new Vector3(1, 0, 0) * Time.deltaTime);
        }
        else if (transform.position.x >= -7.5f && transform.position.y >= 9.5f)
        {
            animator.SetBool("RightReady", false);
            animator.SetBool("DownReady", true);
            transform.position += (new Vector3(0, -1, 0) * Time.deltaTime);
        }
        else if (transform.position.x >= -12.5f && transform.position.y <= 13.5)
        {
            animator.SetBool("DownReady", false);
            animator.SetBool("LeftReady", true);
            transform.position += (new Vector3(-1, 0, 0) * Time.deltaTime);
        }
        else
        {
            animator.SetBool("LeftReady", false);
            animator.SetBool("UpReady", true);
            transform.position += (new Vector3(0, 1, 0) * Time.deltaTime);
        }
    }

    IEnumerator playMovementAudio()
    {
        while (moving)
        {
            yield return new WaitForSeconds(1.0f);
            source.PlayOneShot(movementClip);
        }
    }
}
