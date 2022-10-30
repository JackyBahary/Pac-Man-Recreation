using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PacStudentController : MonoBehaviour
{
    public Tweener tweener;
    public TextMeshProUGUI score;
    public TextMeshProUGUI ghostTimer;
    public TextMeshProUGUI gameCountTimer;
    public TextMeshProUGUI gameOver;
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
    private float deadPacTimer = 1;
    private float deadGhostTimer1 = 5;
    private float deadGhostTimer2 = 5;
    private float deadGhostTimer3 = 5;
    private float deadGhostTimer4 = 5;
    public float loadingTimer = 4;
    private float gameTimer = 0;
    private float gameOverTimer = 3;
    public Image[] lives = new Image[3];
    public TextMeshProUGUI[] countdown = new TextMeshProUGUI[4];
    private string minute = "";
    private string second = "";
    private string millisecond = "";
    public static bool gameTimerActive = true;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        em = particleSystem.emission;
    }

    // Update is called once per frame
    void Update()
    {
        //Loading Timer
        loadingTimer -= Time.deltaTime;
        if (loadingTimer > 0)
        {
            em.enabled = false;
            currentInput = "";
            lastInput = "";
            animator.enabled = false;
            ghost_animator1.enabled = false;
            ghost_animator2.enabled = false;
            ghost_animator3.enabled = false;
            ghost_animator4.enabled = false;
        }
        if (loadingTimer < 3)
        {
            countdown[0].enabled = false;
            countdown[1].enabled = true;
        }
        if (loadingTimer < 2)
        {
            countdown[1].enabled = false;
            countdown[2].enabled = true;
        }
        if (loadingTimer < 1)
        {
            countdown[2].enabled = false;
            countdown[3].enabled = true;
        }
        if (loadingTimer < 0)
        {
            countdown[3].enabled = false;
            animator.enabled = true;
            ghost_animator1.enabled = true;
            ghost_animator2.enabled = true;
            ghost_animator3.enabled = true;
            ghost_animator4.enabled = true;

            //Game Over Code
            if (gameOver.enabled == true)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                tweener.enabled = false;
                em.enabled = false;
                currentInput = "";
                lastInput = "";
                animator.enabled = false;
                ghost_animator1.enabled = false;
                ghost_animator2.enabled = false;
                ghost_animator3.enabled = false;
                ghost_animator4.enabled = false;
                PacStudentController.gameTimerActive = false;
                gameOverTimer -= Time.deltaTime;
                if (gameOverTimer < 0)
                {
                    SceneManager.LoadScene(0, LoadSceneMode.Single);
                }

                //Saving Score and Game Timer Code

                //Creating Keys
                const string saveScore = "userScore";
                const string saveTimeMinute = "userTimeMinute";
                const string saveTimeSecond = "userTimeSecond";
                const string saveTimeMillisecond = "userTimeMillisecond";

                //Loading user's scores and times
                string[] scores = score.text.Split(':');
                int userScore = int.Parse(scores[1]);
                int userMinute = int.Parse(minute);
                int userSecond = int.Parse(second);
                int userMillisecond = int.Parse(millisecond);
                Debug.Log("User Score: " + userScore);
                Debug.Log("User Minute: " + userMinute);
                Debug.Log("User Second: " + userSecond);
                Debug.Log("User Millisecond: " + userMillisecond);

                //Loading saved High Score and Times
                int userSavedScore = PlayerPrefs.GetInt(saveScore);
                int userSavedMinute = PlayerPrefs.GetInt(saveTimeMinute);
                int userSavedSecond = PlayerPrefs.GetInt(saveTimeSecond);
                int userSavedMillisecond = PlayerPrefs.GetInt(saveTimeMillisecond);
                Debug.Log("Saved Score: " + userSavedScore);
                Debug.Log("Saved Minute: " + userSavedMinute);
                Debug.Log("Saved Second: " + userSavedSecond);
                Debug.Log("Saved Millisecond: " + userSavedMillisecond);

                //Logic
                if (userSavedScore < userScore) //If saved score is lower than current score
                {
                    PlayerPrefs.SetInt(saveScore, userScore);
                    PlayerPrefs.SetInt(saveTimeMinute, userMinute);
                    PlayerPrefs.SetInt(saveTimeSecond, userSecond);
                    PlayerPrefs.SetInt(saveTimeMillisecond, userMillisecond);
                    PlayerPrefs.Save();
                }
                else if (userSavedScore == userScore) //If saved score is equal to current score
                {
                    if (userSavedMinute > userMinute) //If saved minute is greater than current minute
                    {
                        PlayerPrefs.SetInt(saveScore, userScore);
                        PlayerPrefs.SetInt(saveTimeMinute, userMinute);
                        PlayerPrefs.SetInt(saveTimeSecond, userSecond);
                        PlayerPrefs.SetInt(saveTimeMillisecond, userMillisecond);
                        PlayerPrefs.Save();
                    }
                    else if (userSavedMinute == userMinute) //If saved minute is equal to current minute
                    {
                        if (userSavedSecond > userSecond) //If saved second is greater than current second
                        {
                            PlayerPrefs.SetInt(saveScore, userScore);
                            PlayerPrefs.SetInt(saveTimeMinute, userMinute);
                            PlayerPrefs.SetInt(saveTimeSecond, userSecond);
                            PlayerPrefs.SetInt(saveTimeMillisecond, userMillisecond);
                            PlayerPrefs.Save();
                        }
                        else if (userSavedSecond == userSecond) //If saved second is equal to current second
                        {
                            if (userSavedMillisecond > userMillisecond) //If saved millisecond is greater than current millisecond
                            {
                                PlayerPrefs.SetInt(saveScore, userScore);
                                PlayerPrefs.SetInt(saveTimeMinute, userMinute);
                                PlayerPrefs.SetInt(saveTimeSecond, userSecond);
                                PlayerPrefs.SetInt(saveTimeMillisecond, userMillisecond);
                                PlayerPrefs.Save();
                            }
                        }
                    }
                }
            }

            //Game Timer Code
            if (gameTimerActive) {
                gameTimer += Time.deltaTime;
                string timer = gameTimer.ToString();
                string[] times = timer.Split('.');
                if (int.Parse(times[0]) < 60)
                {
                    string finalGameTimer = "00:" + int.Parse(times[0]).ToString("00") + ":" + (int.Parse(times[1]) % 100).ToString("00");
                    minute = "00";
                    second = int.Parse(times[0]).ToString("00");
                    millisecond = (int.Parse(times[1]) % 100).ToString("00");
                    gameCountTimer.text = "Timer: " + finalGameTimer;
                }
                if (int.Parse(times[0]) >= 60)
                {
                    string[] minutes = (float.Parse(times[0]) / 60.0).ToString("0.000000").Split('.');
                    string finalGameTimer = int.Parse(minutes[0]).ToString("00") + ":" + (float.Parse("0." + minutes[1]) * 60).ToString("00") + ":" + (int.Parse(times[1]) % 100).ToString("00");
                    minute = int.Parse(minutes[0]).ToString("00");
                    second = (float.Parse("0." + minutes[1]) * 60).ToString("00");
                    millisecond = (int.Parse(times[1]) % 100).ToString("00");
                    gameCountTimer.text = "Timer: " + finalGameTimer;
                }
                Debug.Log("Minute:" + minute);
                Debug.Log("Second: " + second);
                Debug.Log("Millisecond: " + millisecond);
            }
            

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
                    AudioController.scaredReady = false;
                    AudioController.deadReady = false;
                    AudioController.walkReady = true;
                }
            }

            //Animation for Dead PacStudent
            if (animator.GetBool("LeftDead") == true || animator.GetBool("RightDead") == true || animator.GetBool("DownDead") == true || animator.GetBool("UpDead") == true)
            {
                deadPacTimer -= Time.deltaTime;
                if (deadPacTimer < 0)
                {
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
                    this.GetComponent<BoxCollider>().enabled = true;
                    deadPacTimer = 1;
                }
            }

            //Animation for Dead Ghost 1
            if (ghost_animator1.GetBool("deadReady") == true)
            {
                deadGhostTimer1 -= Time.deltaTime;
                if (deadGhostTimer1 < 0)
                {
                    ghost_animator1.SetBool("deadReady", false);
                    ghost_animator1.SetBool("scaredReady", false);
                    ghost_animator1.SetBool("recoveringReady", false);
                    ghost_animator1.SetBool("walkingReady", true);
                    deadGhostTimer1 = 5;
                }
            }

            //Animation for Dead Ghost 2
            if (ghost_animator2.GetBool("deadReady") == true)
            {
                deadGhostTimer2 -= Time.deltaTime;
                if (deadGhostTimer2 < 0)
                {
                    ghost_animator2.SetBool("deadReady", false);
                    ghost_animator2.SetBool("scaredReady", false);
                    ghost_animator2.SetBool("recoveringReady", false);
                    ghost_animator2.SetBool("walkingReady", true);
                    deadGhostTimer2 = 5;
                }
            }

            //Animation for Dead Ghost 3
            if (ghost_animator3.GetBool("deadReady") == true)
            {
                deadGhostTimer3 -= Time.deltaTime;
                if (deadGhostTimer3 < 0)
                {
                    ghost_animator3.SetBool("deadReady", false);
                    ghost_animator3.SetBool("scaredReady", false);
                    ghost_animator3.SetBool("recoveringReady", false);
                    ghost_animator3.SetBool("walkingReady", true);
                    deadGhostTimer3 = 5;
                }
            }

            //Animation for Dead Ghost 4
            if (ghost_animator4.GetBool("deadReady") == true)
            {
                deadGhostTimer4 -= Time.deltaTime;
                if (deadGhostTimer4 < 0)
                {
                    ghost_animator4.SetBool("deadReady", false);
                    ghost_animator4.SetBool("scaredReady", false);
                    ghost_animator4.SetBool("recoveringReady", false);
                    ghost_animator4.SetBool("walkingReady", true);
                    deadGhostTimer4 = 5;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.name.Contains("OutsideWall") || other.gameObject.name.Contains("InnerWall") || other.gameObject.name.Contains("InnerCorner"))
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
                this.GetComponent<BoxCollider>().enabled = false;
                lastInput = "";
                currentInput = "";
                source.PlayOneShot(pacDeadClip, 3.0f);
                pacStudentDead.Play();
                if (animator.GetBool("LeftReady") == true)
                {
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
                    animator.SetBool("LeftDead", false);
                    animator.SetBool("RightDead", false);
                    animator.SetBool("DownDead", false);
                    animator.SetBool("UpDead", true);
                    animator.SetBool("LeftReady", false);
                    animator.SetBool("RightReady", false);
                    animator.SetBool("UpReady", false);
                    animator.SetBool("DownReady", false);
                }
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
                        gameOver.enabled = true;
                        lost = false;
                    }
                    else
                    {
                        lost = false;
                    }
                }
            }
            else if (other.gameObject.name.Contains("GhostNormal1") && ((ghost_animator1.GetBool("scaredReady") == true) || (ghost_animator1.GetBool("recoveringReady") == true)))
            {
                AudioController.deadReady = true;
                ghost_animator1.SetBool("deadReady", true);
                string[] scoreTexts = score.text.Split(':');
                int scorePoint = int.Parse(scoreTexts[1]) + 300;
                score.text = "Score: " + scorePoint.ToString();
            }
            else if (other.gameObject.name.Contains("GhostNormal2") && ((ghost_animator2.GetBool("scaredReady") == true) || (ghost_animator2.GetBool("recoveringReady") == true)))
            {
                AudioController.deadReady = true;
                ghost_animator2.SetBool("deadReady", true);
                string[] scoreTexts = score.text.Split(':');
                int scorePoint = int.Parse(scoreTexts[1]) + 300;
                score.text = "Score: " + scorePoint.ToString();
            }
            else if (other.gameObject.name.Contains("GhostNormal3") && ((ghost_animator3.GetBool("scaredReady") == true) || (ghost_animator3.GetBool("recoveringReady") == true)))
            {
                AudioController.deadReady = true;
                ghost_animator3.SetBool("deadReady", true);
                string[] scoreTexts = score.text.Split(':');
                int scorePoint = int.Parse(scoreTexts[1]) + 300;
                score.text = "Score: " + scorePoint.ToString();
            }
            else if (other.gameObject.name.Contains("GhostNormal4") && ((ghost_animator4.GetBool("scaredReady") == true) || (ghost_animator4.GetBool("recoveringReady") == true)))
            {
                AudioController.deadReady = true;
                ghost_animator4.SetBool("deadReady", true);
                string[] scoreTexts = score.text.Split(':');
                int scorePoint = int.Parse(scoreTexts[1]) + 300;
                score.text = "Score: " + scorePoint.ToString();
            }
        }
    }
}
