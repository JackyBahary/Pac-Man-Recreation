using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PacStudentController : MonoBehaviour
{
    public Tweener tweener;
    public TextMeshProUGUI score;
    public TextMeshProUGUI ghostTimer;
    public new ParticleSystem particleSystem;
    public ParticleSystem wallCollidedParticles;
    public ParticleSystem pacStudentDead;
    private ParticleSystem.EmissionModule em;
    private string lastInput = "";
    private string currentInput = "";
    int[,] levelMap =
    {
    {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
    {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
    {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
    {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
    {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
    {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
    {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
    {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
    {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
    {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
    {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
    {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
    {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
    {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
    {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
    {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
    {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
    {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
    {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
    {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
    {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
    };
    private float pacX = -12.5f;
    private float pacY = 13.5f;
    private int gridRow = 1;
    private int gridColumn = 1;
    public Animator animator;
    public AudioClip movementClip;
    public AudioClip pelletEatenClip;
    public AudioClip wallBumpClip;
    public AudioClip pacDeadClip;
    private AudioSource source;
    public Animator ghost_animator1;
    public Animator ghost_animator2;
    public Animator ghost_animator3;
    public Animator ghost_animator4;
    private float scaredTimer = 10;
    private float deadTimer = 1;
    public Image[] lives = new Image[3];

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        em = particleSystem.emission;

    }

    // Update is called once per frame
    void Update()
    {                
        //Ghost Scared Timer
        if (ghostTimer.isActiveAndEnabled)
        {
            ghostTimer.text = "Scared Timer: " + (int)scaredTimer;
            scaredTimer -= Time.deltaTime;
            if (scaredTimer < 4)
            {
                ghost_animator1.SetBool("scaredReady", false);
                ghost_animator1.SetBool("recoveringReady", true);
                ghost_animator1.SetBool("walkingReady", false);
                ghost_animator2.SetBool("scaredReady", false);
                ghost_animator2.SetBool("recoveringReady", true);
                ghost_animator2.SetBool("walkingReady", false);
                ghost_animator3.SetBool("scaredReady", false);
                ghost_animator3.SetBool("recoveringReady", true);
                ghost_animator3.SetBool("walkingReady", false);
                ghost_animator4.SetBool("scaredReady", false);
                ghost_animator4.SetBool("recoveringReady", true);
                ghost_animator4.SetBool("walkingReady", false);
            }
            if (scaredTimer < 1)
            {
                ghost_animator1.SetBool("scaredReady", false);
                ghost_animator1.SetBool("recoveringReady", false);
                ghost_animator1.SetBool("walkingReady", true);
                ghost_animator2.SetBool("scaredReady", false);
                ghost_animator2.SetBool("recoveringReady", false);
                ghost_animator2.SetBool("walkingReady", true);
                ghost_animator3.SetBool("scaredReady", false);
                ghost_animator3.SetBool("recoveringReady", false);
                ghost_animator3.SetBool("walkingReady", true);
                ghost_animator4.SetBool("scaredReady", false);
                ghost_animator4.SetBool("recoveringReady", false);
                ghost_animator4.SetBool("walkingReady", true);
                ghostTimer.enabled = false;
                scaredTimer = 10;
                AudioController.walkReady = true;
            }
        }

        //Animation for Dead PacStudent
        if (animator.GetBool("LeftDead") == true || animator.GetBool("RightDead") == true || animator.GetBool("DownDead") == true || animator.GetBool("UpDead") == true)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer < 0)
            {
                this.GetComponent<BoxCollider>().enabled = true;
                animator.SetBool("LeftDead", false);
                animator.SetBool("RightDead", false);
                animator.SetBool("DownDead", false);
                animator.SetBool("UpDead", false);
                animator.SetBool("LeftReady", false);
                animator.SetBool("RightReady", false);
                animator.SetBool("UpReady", false);
                animator.SetBool("DownReady", false);
                Vector3 newPos = new Vector3(-12.5f, 13.5f, 0f);
                pacX = -12.5f;
                pacY = 13.5f;
                gridRow = 1;
                gridColumn = 1;
                tweener.AddTween(transform, newPos, newPos, 0f);
                deadTimer = 1;
            }
        }

        //Pausing of Particle Trail
        if (lastInput.Equals(""))
        {
            em.enabled = false;
        }
        else if (animator.speed > 0)
        {
            em.enabled = true;
        }
        else
        {
            em.enabled = false;
        }

        //Storing Inputs into "lastInput" variable
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = "w";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = "a";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = "s";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = "d";
        }

        //Lerping Code
        if (!tweener.TweenExists(transform)) //If PacStudent is not moving
        {
            if (lastInput.Equals("w")) //If last input is "w"
            {
                if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6 || levelMap[gridRow - 1, gridColumn] == 0) //If the top of the pacStudent is a normal pellet, bonus pellet or empty space
                {
                    source.PlayOneShot(movementClip);
                    if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6)
                    {
                        source.PlayOneShot(pelletEatenClip);
                    }
                    animator.speed = 1;
                    animator.SetBool("LeftReady", false);
                    animator.SetBool("RightReady", false);
                    animator.SetBool("UpReady", true);
                    animator.SetBool("DownReady", false);
                    animator.SetBool("LeftDead", false);
                    animator.SetBool("RightDead", false);
                    animator.SetBool("DownDead", false);
                    animator.SetBool("UpDead", false);
                    currentInput = lastInput;
                    Vector3 targetPos = transform.position;
                    targetPos.y = targetPos.y + 1;
                    pacY += 1;
                    gridRow -= 1;
                    tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                    Debug.Log("Grid Row: " + gridRow);
                    Debug.Log("Grid Column: " + gridColumn);
                    Debug.Log("pacX: " + pacX);
                    Debug.Log("pacY: " + pacY);
                }
                else //If the top of pacStudent is not walkable
                {
                    if (currentInput.Equals("w")) //If current input is "w"
                    {
                        if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6 || levelMap[gridRow - 1, gridColumn] == 0) //If the top of pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.y = targetPos.y + 1;
                            pacY += 1;
                            gridRow -= 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the top is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                    if (currentInput.Equals("a")) //If current input is "a"
                    {
                        if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6 || levelMap[gridRow, gridColumn - 1] == 0) //If the left of pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.x = targetPos.x - 1;
                            pacX -= 1;
                            gridColumn -= 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the left is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                    if (currentInput.Equals("s")) //If current input is "s"
                    {
                        if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6 || levelMap[gridRow + 1, gridColumn] == 0) //If the bottom of pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.y = targetPos.y - 1;
                            pacY -= 1;
                            gridRow += 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the bottom is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                    if (currentInput.Equals("d")) //If current input is "d"
                    {
                        if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6 || levelMap[gridRow, gridColumn + 1] == 0) //If the right of the pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.x = targetPos.x + 1;
                            pacX += 1;
                            gridColumn += 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the right is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                }
            }

            if (lastInput.Equals("a")) //If last input is "a"
            {
                if (gridColumn - 1 >= 0) //If left is not out of bounds
                {
                    if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6 || levelMap[gridRow, gridColumn - 1] == 0) //If the left of pacStudent is a normal pellet, bonus pellet or empty space
                    {
                        source.PlayOneShot(movementClip);
                        if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6)
                        {
                            source.PlayOneShot(pelletEatenClip);
                        }
                        animator.speed = 1;
                        animator.SetBool("LeftReady", true);
                        animator.SetBool("RightReady", false);
                        animator.SetBool("UpReady", false);
                        animator.SetBool("DownReady", false);
                        animator.SetBool("LeftDead", false);
                        animator.SetBool("RightDead", false);
                        animator.SetBool("DownDead", false);
                        animator.SetBool("UpDead", false);
                        currentInput = lastInput;
                        Vector3 targetPos = transform.position;
                        targetPos.x = targetPos.x - 1;
                        pacX -= 1;
                        gridColumn -= 1;
                        tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                        Debug.Log("Grid Row: " + gridRow);
                        Debug.Log("Grid Column: " + gridColumn);
                        Debug.Log("pacX: " + pacX);
                        Debug.Log("pacY: " + pacY);
                    }
                    else //If the left of pacStudent is not walkable
                    {
                        if (currentInput.Equals("w")) //If current input is "w"
                        {
                            if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6 || levelMap[gridRow - 1, gridColumn] == 0) //If the top of pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.y = targetPos.y + 1;
                                pacY += 1;
                                gridRow -= 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the top is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                        if (currentInput.Equals("a")) //If current input is "a"
                        {
                            if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6 || levelMap[gridRow, gridColumn - 1] == 0) //If the left of pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.x = targetPos.x - 1;
                                pacX -= 1;
                                gridColumn -= 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the left is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                        if (currentInput.Equals("s")) //If current input is "s"
                        {
                            if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6 || levelMap[gridRow + 1, gridColumn] == 0) //If the bottom of pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.y = targetPos.y - 1;
                                pacY -= 1;
                                gridRow += 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the bottom is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                        if (currentInput.Equals("d")) //If current input is "d"
                        {
                            if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6 || levelMap[gridRow, gridColumn + 1] == 0) //If the right of the pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.x = targetPos.x + 1;
                                pacX += 1;
                                gridColumn += 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the right is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                    }
                }
                else //If left is out of bounds
                {
                    animator.speed = 0;
                    source.PlayOneShot(movementClip);
                    Vector3 newPos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
                    Vector3 targetPos = newPos;
                    targetPos.x = targetPos.x - 1;
                    pacX = 12.5f;
                    gridColumn = 26;
                    tweener.AddTween(transform, newPos, targetPos, 0.5f);
                    Debug.Log("Grid Row: " + gridRow);
                    Debug.Log("Grid Column: " + gridColumn);
                    Debug.Log("pacX: " + pacX);
                    Debug.Log("pacY: " + pacY);
                }
            }

            if (lastInput.Equals("s")) //If last input is "s"
            {
                if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6 || levelMap[gridRow + 1, gridColumn] == 0) //If the bottom of pacStudent is a normal pellet, bonus pellet or empty space
                {
                    source.PlayOneShot(movementClip);
                    if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6)
                    {
                        source.PlayOneShot(pelletEatenClip);
                    }
                    animator.speed = 1;
                    animator.SetBool("LeftReady", false);
                    animator.SetBool("RightReady", false);
                    animator.SetBool("UpReady", false);
                    animator.SetBool("DownReady", true);
                    animator.SetBool("LeftDead", false);
                    animator.SetBool("RightDead", false);
                    animator.SetBool("DownDead", false);
                    animator.SetBool("UpDead", false);
                    currentInput = lastInput;
                    Vector3 targetPos = transform.position;
                    targetPos.y = targetPos.y - 1;
                    pacY -= 1;
                    gridRow += 1;
                    tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                    Debug.Log("Grid Row: " + gridRow);
                    Debug.Log("Grid Column: " + gridColumn);
                    Debug.Log("pacX: " + pacX);
                    Debug.Log("pacY: " + pacY);
                }
                else //If the bottom of pacStudent is not walkable
                {
                    if (currentInput.Equals("w")) //If current input is "w"
                    {
                        if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6 || levelMap[gridRow - 1, gridColumn] == 0) //If the top of pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.y = targetPos.y + 1;
                            pacY += 1;
                            gridRow -= 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the top is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                    if (currentInput.Equals("a")) //If current input is "a"
                    {
                        if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6 || levelMap[gridRow, gridColumn - 1] == 0) //If the left of pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.x = targetPos.x - 1;
                            pacX -= 1;
                            gridColumn -= 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the left is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                    if (currentInput.Equals("s")) //If current input is "s"
                    {
                        if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6 || levelMap[gridRow + 1, gridColumn] == 0) //If the bottom of pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.y = targetPos.y - 1;
                            pacY -= 1;
                            gridRow += 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the bottom is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                    if (currentInput.Equals("d")) //If current input is "d"
                    {
                        if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6 || levelMap[gridRow, gridColumn + 1] == 0) //If the right of the pacStudent is a normal pellet, bonus pellet or empty space
                        {
                            source.PlayOneShot(movementClip);
                            if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6)
                            {
                                source.PlayOneShot(pelletEatenClip);
                            }
                            Vector3 targetPos = transform.position;
                            targetPos.x = targetPos.x + 1;
                            pacX += 1;
                            gridColumn += 1;
                            tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                        else //If the right is not walkable
                        {
                            animator.speed = 0;
                            Debug.Log("Grid Row: " + gridRow);
                            Debug.Log("Grid Column: " + gridColumn);
                            Debug.Log("pacX: " + pacX);
                            Debug.Log("pacY: " + pacY);
                        }
                    }
                }
            }

            if (lastInput.Equals("d")) //If last input is "d"
            {
                if (gridColumn + 1 <= (levelMap.GetLength(1) - 1)) //If the right is not out of bounds
                {
                    if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6 || levelMap[gridRow, gridColumn + 1] == 0) //If the right of the pacStudent is a normal pellet, bonus pellet or empty space
                    {
                        source.PlayOneShot(movementClip);
                        if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6)
                        {
                            source.PlayOneShot(pelletEatenClip);
                        }
                        animator.speed = 1;
                        animator.SetBool("LeftReady", false);
                        animator.SetBool("RightReady", true);
                        animator.SetBool("UpReady", false);
                        animator.SetBool("DownReady", false);
                        animator.SetBool("LeftDead", false);
                        animator.SetBool("RightDead", false);
                        animator.SetBool("DownDead", false);
                        animator.SetBool("UpDead", false);
                        currentInput = lastInput;
                        Vector3 targetPos = transform.position;
                        targetPos.x = targetPos.x + 1;
                        pacX += 1;
                        gridColumn += 1;
                        tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                        Debug.Log("Grid Row: " + gridRow);
                        Debug.Log("Grid Column: " + gridColumn);
                        Debug.Log("pacX: " + pacX);
                        Debug.Log("pacY: " + pacY);
                    }
                    else //If the right of pacStudent is not walkable
                    {
                        if (currentInput.Equals("w")) //If current input is "w"
                        {
                            if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6 || levelMap[gridRow - 1, gridColumn] == 0) //If the top of pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow - 1, gridColumn] == 5 || levelMap[gridRow - 1, gridColumn] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.y = targetPos.y + 1;
                                pacY += 1;
                                gridRow -= 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the top is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                        if (currentInput.Equals("a")) //If current input is "a"
                        {
                            if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6 || levelMap[gridRow, gridColumn - 1] == 0) //If the left of pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow, gridColumn - 1] == 5 || levelMap[gridRow, gridColumn - 1] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.x = targetPos.x - 1;
                                pacX -= 1;
                                gridColumn -= 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the left is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                        if (currentInput.Equals("s")) //If current input is "s"
                        {
                            if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6 || levelMap[gridRow + 1, gridColumn] == 0) //If the bottom of pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow + 1, gridColumn] == 5 || levelMap[gridRow + 1, gridColumn] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.y = targetPos.y - 1;
                                pacY -= 1;
                                gridRow += 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the bottom is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                        if (currentInput.Equals("d")) //If current input is "d"
                        {
                            if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6 || levelMap[gridRow, gridColumn + 1] == 0) //If the right of the pacStudent is a normal pellet, bonus pellet or empty space
                            {
                                source.PlayOneShot(movementClip);
                                if (levelMap[gridRow, gridColumn + 1] == 5 || levelMap[gridRow, gridColumn + 1] == 6)
                                {
                                    source.PlayOneShot(pelletEatenClip);
                                }
                                Vector3 targetPos = transform.position;
                                targetPos.x = targetPos.x + 1;
                                pacX += 1;
                                gridColumn += 1;
                                tweener.AddTween(transform, transform.position, targetPos, 0.5f);
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                            else //If the right is not walkable
                            {
                                animator.speed = 0;
                                Debug.Log("Grid Row: " + gridRow);
                                Debug.Log("Grid Column: " + gridColumn);
                                Debug.Log("pacX: " + pacX);
                                Debug.Log("pacY: " + pacY);
                            }
                        }
                    }
                }
                else //If right is out of bounds
                {
                    animator.speed = 0;
                    source.PlayOneShot(movementClip);
                    Vector3 newPos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
                    Vector3 targetPos = newPos;
                    targetPos.x = targetPos.x + 1;
                    pacX = -12.5f;
                    gridColumn = 1;
                    tweener.AddTween(transform, newPos, targetPos, 0.5f);
                    Debug.Log("Grid Row: " + gridRow);
                    Debug.Log("Grid Column: " + gridColumn);
                    Debug.Log("pacX: " + pacX);
                    Debug.Log("pacY: " + pacY);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.name.Contains("OutsideWall") || other.gameObject.name.Contains("InnerWall"))
            {
                if (lastInput == currentInput)
                {
                    source.PlayOneShot(wallBumpClip, 3.0f);
                    wallCollidedParticles.Play();
                }
            }
            else if (other.gameObject.name.Contains("NormalPellet"))
            {
                Destroy(other.gameObject);
                string[] scoreTexts = score.text.Split(':');
                int scorePoint = int.Parse(scoreTexts[1]) + 10;
                score.text = "Score: " + scorePoint.ToString();
            }
            else if (other.gameObject.name.Contains("Cherry"))
            {
                Destroy(other.gameObject);
                string[] scoreTexts = score.text.Split(':');
                int scorePoint = int.Parse(scoreTexts[1]) + 100;
                score.text = "Score: " + scorePoint.ToString();
            }
            else if (other.gameObject.name.Contains("PowerPellet"))
            {
                Destroy(other.gameObject);
                ghost_animator1.SetBool("scaredReady", true);
                ghost_animator1.SetBool("recoveringReady", false);
                ghost_animator1.SetBool("walkingReady", false);
                ghost_animator2.SetBool("scaredReady", true);
                ghost_animator2.SetBool("recoveringReady", false);
                ghost_animator2.SetBool("walkingReady", false);
                ghost_animator3.SetBool("scaredReady", true);
                ghost_animator3.SetBool("recoveringReady", false);
                ghost_animator3.SetBool("walkingReady", false);
                ghost_animator4.SetBool("scaredReady", true);
                ghost_animator4.SetBool("recoveringReady", false);
                ghost_animator4.SetBool("walkingReady", false);
                AudioController.scaredReady = true;
                ghostTimer.enabled = true;
            }
            else if (other.gameObject.name.Contains("Ghost") && (ghost_animator1.GetBool("scaredReady") == false) && (ghost_animator1.GetBool("recoveringReady") == false))
            {
                source.PlayOneShot(pacDeadClip, 3.0f);
                pacStudentDead.Play();
                if (animator.GetBool("LeftReady") == true)
                {
                    this.GetComponent<BoxCollider>().enabled = false;
                    animator.SetBool("LeftDead", true);
                    animator.SetBool("RightDead", false);
                    animator.SetBool("DownDead", false);
                    animator.SetBool("UpDead", false);
                    animator.SetBool("LeftReady", false);
                    animator.SetBool("RightReady", false);
                    animator.SetBool("UpReady", false);
                    animator.SetBool("DownReady", false);
                }
                else if (animator.GetBool("RightReady") == true)
                {
                    this.GetComponent<BoxCollider>().enabled = false;
                    animator.SetBool("LeftDead", false);
                    animator.SetBool("RightDead", true);
                    animator.SetBool("DownDead", false);
                    animator.SetBool("UpDead", false);
                    animator.SetBool("LeftReady", false);
                    animator.SetBool("RightReady", false);
                    animator.SetBool("UpReady", false);
                    animator.SetBool("DownReady", false);
                }
                else if (animator.GetBool("DownReady") == true)
                {
                    this.GetComponent<BoxCollider>().enabled = false;
                    animator.SetBool("LeftDead", false);
                    animator.SetBool("RightDead", false);
                    animator.SetBool("DownDead", true);
                    animator.SetBool("UpDead", false);
                    animator.SetBool("LeftReady", false);
                    animator.SetBool("RightReady", false);
                    animator.SetBool("UpReady", false);
                    animator.SetBool("DownReady", false);
                }
                else if (animator.GetBool("UpReady") == true)
                {
                    this.GetComponent<BoxCollider>().enabled = false;
                    animator.SetBool("LeftDead", false);
                    animator.SetBool("RightDead", false);
                    animator.SetBool("DownDead", false);
                    animator.SetBool("UpDead", true);
                    animator.SetBool("LeftReady", false);
                    animator.SetBool("RightReady", false);
                    animator.SetBool("UpReady", false);
                    animator.SetBool("DownReady", false);
                }
                lastInput = "";
                currentInput = "";
                bool lost = true;
                while (lost)
                {
                    if (lives[2].enabled == true)
                    {
                        lives[2].enabled = false;
                        lost = false;
                    }
                    else if (lives[1].enabled == true)
                    {
                        lives[1].enabled = false;
                        lost = false;
                    }
                    else if (lives[0].enabled == true)
                    {
                        lives[0].enabled = false;
                        lost = false;
                    }
                    else
                    {
                        lost = false;
                    }
                }
            }
        }
    }
}
