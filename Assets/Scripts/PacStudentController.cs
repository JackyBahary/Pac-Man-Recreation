using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public Tweener tweener;
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
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
            //Vector3 targetPos = transform.position;
            //targetPos.x = targetPos.x + 5;
            //tweener.AddTween(transform, transform.position, targetPos, 10);
            //Debug.Log("Going Again.");

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
                    currentInput = lastInput;
                    Vector3 targetPos = transform.position;
                    targetPos.y = targetPos.y + 1;
                    pacY += 1;
                    gridRow -= 1;
                    tweener.AddTween(transform, transform.position, targetPos, 1);
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the top is not walkable
                        {
                            animator.speed = 0;
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the left is not walkable
                        {
                            animator.speed = 0;
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the bottom is not walkable
                        {
                            animator.speed = 0;
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the right is not walkable
                        {
                            animator.speed = 0;
                        }
                    }
                }
            }

            if (gridColumn - 1 >= 0) //If left is not out of bounds
            {
                if (lastInput.Equals("a")) //If last input is "a"
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
                        currentInput = lastInput;
                        Vector3 targetPos = transform.position;
                        targetPos.x = targetPos.x - 1;
                        pacX -= 1;
                        gridColumn -= 1;
                        tweener.AddTween(transform, transform.position, targetPos, 1);
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the top is not walkable
                            {
                                animator.speed = 0;
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the left is not walkable
                            {
                                animator.speed = 0;
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the bottom is not walkable
                            {
                                animator.speed = 0;
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the right is not walkable
                            {
                                animator.speed = 0;
                            }
                        }
                    }
                }
            }
            else //If left is out of bounds
            {
                animator.speed = 0;
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
                    currentInput = lastInput;
                    Vector3 targetPos = transform.position;
                    targetPos.y = targetPos.y - 1;
                    pacY -= 1;
                    gridRow += 1;
                    tweener.AddTween(transform, transform.position, targetPos, 1);
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the top is not walkable
                        {
                            animator.speed = 0;
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the left is not walkable
                        {
                            animator.speed = 0;
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the bottom is not walkable
                        {
                            animator.speed = 0;
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
                            tweener.AddTween(transform, transform.position, targetPos, 1);
                        }
                        else //If the right is not walkable
                        {
                            animator.speed = 0;
                        }
                    }
                }
            }

            if (gridColumn + 1 <= (levelMap.GetLength(1) - 1)) //If the right is not out of bounds
            {
                if (lastInput.Equals("d")) //If last input is "d"
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
                        currentInput = lastInput;
                        Vector3 targetPos = transform.position;
                        targetPos.x = targetPos.x + 1;
                        pacX += 1;
                        gridColumn += 1;
                        tweener.AddTween(transform, transform.position, targetPos, 1);
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the top is not walkable
                            {
                                animator.speed = 0;
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the left is not walkable
                            {
                                animator.speed = 0;
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the bottom is not walkable
                            {
                                animator.speed = 0;
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
                                tweener.AddTween(transform, transform.position, targetPos, 1);
                            }
                            else //If the right is not walkable
                            {
                                animator.speed = 0;
                            }
                        }
                    }
                }
            }
            else //If right is out of bounds
            {
                animator.speed = 0;
            }
        }
    }
}
