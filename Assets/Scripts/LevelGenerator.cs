using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    int[,] levelMap = 
    {
    {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
    {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
    {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
    {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
    {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
    {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
    {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
    {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
    {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
    {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
    {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
    {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
    {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    float startX = -13.5f;
    float startY = 14.5f;
    int size;
    public TilemapRenderer[] tilemaps;
    public GameObject[] disabledSprites;
    public GameObject[] sprites;
    private GameObject[] levelClones;
    int val = 0;
    int clonesIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;

        //Find size of 2D Array
        size = levelMap.GetLength(0) * levelMap.GetLength(1);

        //Initiate Size of Clone GameObject Array
        levelClones = new GameObject[size];

        //Create Top Left Sprite
        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no rotation at Top-Left Coordinate
        clonesIndex++; //Increment cloneIndex
        startX++; //Increment X coordinate

        //Disable Level 01 Manual Level Layout
        foreach (TilemapRenderer set in tilemaps)
        {
            set.enabled = false;
        }
        foreach (GameObject set in disabledSprites)
        {
            set.SetActive(false);
        }

        //Creation of Procedural Level Generator
        for (int k = 0; k < levelMap.GetLength(0); k++) //Rows 
        {
            for (int l = 0; l < levelMap.GetLength(1); l++) //Columns
            {
                if (k == 0 && l == 0) //If Its the Top Left, skip this loop
                {
                    continue;
                }

                val = levelMap[k, l] - 1; //Get the value from levelMap according to row and column and fix its index for sprites array
                
                if (val == -1) //If Index is -1 (for '0' value or empty space in levelMap), skip this loop
                {
                    startX++;
                    levelClones[clonesIndex] = Instantiate(sprites[7], new Vector3(startX, startY), Quaternion.identity);
                    clonesIndex++;
                    continue;
                }

                if (val == 0) //If the sprite is Outer Corner 
                {
                    if ((k - 1 < 0) && (l - 1 < 0) || ((k + 1 < levelMap.GetLength(0)) && (l + 1 < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 1 || levelMap[k, l + 1] == 2) && (levelMap[k + 1, l] == 1 || levelMap[k + 1, l] == 2))) //Check if Top and Left of the current index is outside range or Right and Bottom has sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                        clonesIndex++;
                    }
                    else if (((l - 1) >= 0) && (k - 1 >= 0) && (levelMap[k, l - 1] == 1 || levelMap[k, l - 1] == 2) && (levelMap[k - 1, l] == 1 || levelMap[k - 1, l] == 2)) //Check if Top and Left of the current index is within range and has sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 180)); //Create Sprite with 180 at Z Rotation
                        clonesIndex++;
                    }
                    else if ((k - 1 >= 0) && (l + 1 < levelMap.GetLength(1)) && (levelMap[k - 1, l] == 1 || levelMap[k - 1, l] == 2) && (levelMap[k, l + 1] == 1 || levelMap[k, l + 1] == 2)) //Check if Top and Right of the current index is within range and has sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                    }
                    else if ((l - 1 >= 0) && (k + 1 < levelMap.GetLength(0)) && (levelMap[k, l - 1] == 1 || levelMap[k, l - 1] == 2) && (levelMap[k + 1, l] == 1 || levelMap[k + 1, l] == 2)) //Check if Left and Down of the current index is within range and has sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 270)); //Create Sprite with 270 at Z Rotation
                        clonesIndex++;
                    }
                    else if (((k - 1 >= 0) && (levelMap[k - 1, l] == 1 || levelMap[k - 1, l] == 2) && (l + 1 > levelMap.GetLength(1) - 1)) || ((l + 1 < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 1 || levelMap[k, l + 1] == 2) && (k - 1 < 0))) //If Right is out of bounds and Up is Outer Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                    }
                    else if (((k + 1 < levelMap.GetLength(0)) && (levelMap[k + 1, l] == 1 || levelMap[k + 1, l] == 2) && (l + 1 > levelMap.GetLength(1) - 1)) || ((l + 1 < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 1 || levelMap[k, l + 1] == 2) && (k + 1 > levelMap.GetLength(0) - 1))) //If Right is out of bounds and Down is Outer Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                        clonesIndex++;
                    }
                    else if (((k - 1 >= 0) && (levelMap[k - 1, l] == 1 || levelMap[k - 1, l] == 2) && (l - 1 < 0)) || ((l - 1 >= 0) && (levelMap[k, l - 1] == 1 || levelMap[k, l - 1] == 2) && (k - 1 < 0))) //If Left is out of bounds and Up is Outer Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 180)); //Create Sprite with 180 at Z Rotation
                        clonesIndex++;
                    }
                    else if (((k + 1 < levelMap.GetLength(0)) && (levelMap[k + 1, l] == 1 || levelMap[k + 1, l] == 2) && (l - 1 < 0)) || ((l - 1 >= 0) && (levelMap[k, l - 1] == 1 || levelMap[k, l - 1] == 2) && (k + 1 > levelMap.GetLength(0) - 1))) //If Left is out of bounds and Down is Outer Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 270)); //Create Sprite with 270 at Z Rotation
                        clonesIndex++;
                    }
                    else
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Default
                        clonesIndex++;
                    }
                }

                if (val == 1) //If the sprite is Outer Wall
                {
                    if (((l - 1) >= 0) && (levelMap[k, l - 1] == 1 || levelMap[k, l - 1] == 2)) //Check if left of the current index is within range and has any sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                        clonesIndex++;
                    }
                    else if (((k - 1) >= 0) && (levelMap[k - 1, l] == 1 || levelMap[k - 1, l] == 2)) //Check if top of the current index is within range and has any sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                    }
                    else if (((l + 1) < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 1 || levelMap[k, l + 1] == 2)) //Check if right of the current index is within range and has any sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                        clonesIndex++;
                    }
                    else if (((k + 1) < levelMap.GetLength(0)) && (levelMap[k + 1, l] == 1 || levelMap[k + 1, l] == 2)) //Check if down of the current index is within range and has any sprites(Outer Corner or Outer Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                    }
                    else
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Default
                        clonesIndex++;
                    }
                }

                if (val == 2) //If the sprite is Inner Corner 
                {
                    if ((k - 1 < 0) && (l - 1 < 0) || ((k + 1 < levelMap.GetLength(0)) && (l + 1 < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 3 || levelMap[k, l + 1] == 4) && (levelMap[k + 1, l] == 3 || levelMap[k + 1, l] == 4))) //Check if Top and Left of the current index is outside range or Right and Bottom has sprites(Inner Corner or Inner Wall)
                    {
                        
                        index = (14 * (k - 1)) + l;
                        if (levelClones[index] != null && levelClones[index].name.Equals("InnerWall(Clone)") && levelClones[index].transform.eulerAngles.z == 90 && levelMap[k, l + 1] == 4) //If Inner Wall, if  rotation of top = 90 and right is Inner wall
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with no Rotation
                            clonesIndex++;
                        }
                        else
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                            clonesIndex++;
                        }
                        
                    }
                    else if (((l - 1) >= 0) && (k - 1 >= 0) && (levelMap[k, l - 1] == 3 || levelMap[k, l - 1] == 4) && (levelMap[k - 1, l] == 3 || levelMap[k - 1, l] == 4)) //Check if Top and Left of the current index is within range and has sprites(Inner Corner or Inner Wall)
                    {
                        int index2 = (14 * k) + (l - 1);
                        if (levelClones[index] != null && levelClones[index].name.Equals("InnerWall(Clone)") && levelClones[index].transform.eulerAngles.z == 0 && levelMap[k + 1, l] == 4)
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 270)); //Create Sprite with no Rotation
                            clonesIndex++;
                        }
                        else
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 180)); //Create Sprite with 180 at Z Rotation
                            clonesIndex++;
                        }                        
                    }
                    else if ((k - 1 >= 0) && (l + 1 < levelMap.GetLength(1)) && (levelMap[k - 1, l] == 3 || levelMap[k - 1, l] == 4) && (levelMap[k, l + 1] == 3 || levelMap[k, l + 1] == 4)) //Check if Top and Right of the current index is within range and has sprites(Inner Corner or Inner Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                    }
                    else if ((l - 1 >= 0) && (k + 1 < levelMap.GetLength(0)) && (levelMap[k, l - 1] == 3 || levelMap[k, l - 1] == 4) && (levelMap[k + 1, l] == 3 || levelMap[k + 1, l] == 4)) //Check if Left and Down of the current index is within range and has sprites(Inner Corner or Inner Wall)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 270)); //Create Sprite with 270 at Z Rotation
                        clonesIndex++;
                    }
                    else if (((k - 1 >= 0) && (levelMap[k - 1, l] == 3 || levelMap[k - 1, l] == 4) && (l + 1 > levelMap.GetLength(1) - 1)) || ((l + 1 < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 3 || levelMap[k, l + 1] == 4) && (k - 1 < 0))) //If Right is out of bounds and Up is Inner Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                    }
                    else if (((k + 1 < levelMap.GetLength(0)) && (levelMap[k + 1, l] == 3 || levelMap[k + 1, l] == 4) && (l + 1 > levelMap.GetLength(1) - 1)) || ((l + 1 < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 3 || levelMap[k, l + 1] == 4) && (k + 1 > levelMap.GetLength(0) - 1))) //If Right is out of bounds and Down is Inner Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                        clonesIndex++;
                    }
                    else if (((k - 1 >= 0) && (levelMap[k - 1, l] == 3 || levelMap[k - 1, l] == 4) && (l - 1 < 0)) || ((l - 1 >= 0) && (levelMap[k, l - 1] == 3 || levelMap[k, l - 1] == 4) && (k - 1 < 0))) //If Left is out of bounds and Up is Inner Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 180)); //Create Sprite with 180 at Z Rotation
                        clonesIndex++;
                    }
                    else if (((k + 1 < levelMap.GetLength(0)) && (levelMap[k + 1, l] == 3 || levelMap[k + 1, l] == 4) && (l - 1 < 0)) || ((l - 1 >= 0) && (levelMap[k, l - 1] == 3 || levelMap[k, l - 1] == 4) && (k + 1 > levelMap.GetLength(0) - 1))) //If Left is out of bounds and Down is Inner Corner or Wall or Vice Versa
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 270)); //Create Sprite with 270 at Z Rotation
                        clonesIndex++;
                    }
                    else
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Default
                        clonesIndex++;
                    }
                }

                if (val == 3) //If the sprite is Inner Wall
                {
                    if (((l - 1) >= 0) && (levelMap[k, l - 1] == 3 || levelMap[k, l - 1] == 4 || levelMap[k, l - 1] == 7)) //Check if left of the current index is within range and has any sprites(Inner Corner or Inner Wall or TJunction)
                    {
                        //levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                        //clonesIndex++;
                        index = (14 * k) + (l - 1);
                        if (levelClones[index].name.Equals("InnerWall(Clone)") && levelClones[index].transform.eulerAngles.z == 0) //If Inner Wall, if rotation = 0
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                            clonesIndex++;
                        }
                        else if (levelClones[index].name.Equals("InnerCorner(Clone)") && (levelClones[index].transform.eulerAngles.z == 90 || levelClones[index].transform.eulerAngles.z == 0)) //If Inner Corner, if rotation = 90 or 0
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                            clonesIndex++;
                        }
                        else if (levelClones[index].name.Equals("TJunction(Clone)") && levelClones[index].transform.eulerAngles.z == 90 && levelClones[index].transform.eulerAngles.y == 0) //If TJunction, if rotation : y = 0 z = 90
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                            clonesIndex++;
                        }
                        else
                        {
                            levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with no Rotation
                            clonesIndex++;
                        }
                    }
                    else if (((k - 1) >= 0) && (levelMap[k - 1, l] == 3 || levelMap[k - 1, l] == 4 || levelMap[k - 1, l] == 7)) //Check if top of the current index is within range and has any sprites(Inner Corner or Inner Wall or TJunction)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                        //index = (14 * (k - 1)) + l;
                        //if (levelClones[index].name.Equals("InnerWall(Clone)") && levelClones[index].transform.eulerAngles.z == 90) //If Inner Wall, if rotation = 90
                        //{
                        //    levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        //    clonesIndex++;
                        //}
                        //else if (levelClones[index].name.Equals("InnerCorner(Clone)") && (levelClones[index].transform.eulerAngles.z == 0 || levelClones[index].transform.eulerAngles.z == 270)) //If Inner Corner, if rotation = 0 or 270
                        //{
                        //    levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        //    clonesIndex++;
                        //}
                        //else if (levelClones[index].name.Equals("TJunction(Clone)") && levelClones[index].transform.eulerAngles.z == 0) //If TJunction, if rotation = 0
                        //{
                        //    levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        //    clonesIndex++;
                        //}
                    }
                    else if (((l + 1) < levelMap.GetLength(1)) && (levelMap[k, l + 1] == 3 || levelMap[k, l + 1] == 4 || levelMap[k, l + 1] == 7)) //Check if right of the current index is within range and has any sprites(Inner Corner or Inner Wall or TJunction)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with no Rotation
                        clonesIndex++;
                    }
                    else if (((k + 1) < levelMap.GetLength(0)) && (levelMap[k + 1, l] == 3 || levelMap[k + 1, l] == 4 || levelMap[k + 1, l] == 7)) //Check if down of the current index is within range and has any sprites(Inner Corner or Inner Wall or TJunction)
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 at Z Rotation
                        clonesIndex++;
                    }
                    else
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Default
                        clonesIndex++;
                    }
                }

                if (val == 4 || val == 5) //If the sprite is Normal Pellet or Power Pellet
                {
                    levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with proper Rotation
                    clonesIndex++;
                }

                if (val == 6) //If the sprite is TJunction
                {
                    if ((l - 1 >= 0) && (k + 1 < levelMap.GetLength(0)) && levelMap[k, l - 1] == 2 && levelMap[k + 1, l] == 4) //Check if Left is Outer Wall and Bottom is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Default
                        clonesIndex++;
                    }
                    else if ((l + 1 < levelMap.GetLength(1)) && (k + 1 < levelMap.GetLength(0)) && levelMap[k, l + 1] == 2 && levelMap[k + 1, l] == 4) //Check if Right is Outer Wall and Bottom is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 180, 0)); //Create Sprite with 180 Y Rotation
                        clonesIndex++;
                    }
                    else if ((l - 1 >= 0) && (k - 1 >= 0) && levelMap[k, l - 1] == 2 && levelMap[k - 1, l] == 4) //Check if Left is Outer Wall and Up is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 180, 180)); //Create Sprite with 180 Y Rotation and 180 Z Rotation
                        clonesIndex++;
                    }
                    else if ((l + 1 < levelMap.GetLength(1)) && (k - 1 >= 0) && levelMap[k, l + 1] == 2 && levelMap[k - 1, l] == 4) //Check if Right is Outer Wall and Up is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 180)); //Create Sprite with 180 Z Rotation 
                        clonesIndex++;
                    }
                    else if ((k - 1 >= 0) && (l - 1 >= 0) && levelMap[k - 1, l] == 2 && levelMap[k, l - 1] == 4) //Check if Up is Outer Wall and Left is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 270)); //Create Sprite with 270 Z Rotation
                        clonesIndex++;
                    }
                    else if ((k - 1 >= 0) && (l + 1 < levelMap.GetLength(1)) && levelMap[k - 1, l] == 2 && levelMap[k, l + 1] == 4) //Check if Up is Outer Wall and Right is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 180, 270)); //Create Sprite with 180 Y Rotation and 270 Z Rotation
                        clonesIndex++;
                    }
                    else if ((k + 1 < levelMap.GetLength(0)) && (l - 1 >= 0) && levelMap[k + 1, l] == 2 && levelMap[k, l - 1] == 4) //Check if Down is Outer Wall and Left is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 180, 90)); //Create Sprite with 180 Y Rotation and 90 Z Rotation
                        clonesIndex++;
                    }
                    else if ((k + 1 < levelMap.GetLength(0)) && (l + 1 < levelMap.GetLength(1)) && levelMap[k + 1, l] == 2 && levelMap[k, l + 1] == 4) //Check if Down is Outer Wall and Right is Inner Wall
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with 90 Z Rotation
                        clonesIndex++;
                    }
                    else
                    {
                        levelClones[clonesIndex] = Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Default
                        clonesIndex++;
                    }
                }

                startX++; //Increment X coordinate to go to the next space in the row

            }

            startX = -13.5f; //After a whole row is completed, restart at left most column
            startY--; //Decrement the Y coordinate to go to the next row

        }


        for (int i = 0; i < levelClones.Length; i++)
        {
            if (levelClones[i] != null)
            {
                Instantiate(levelClones[i], new Vector3(-levelClones[i].transform.position.x, levelClones[i].transform.position.y, levelClones[i].transform.position.z), Quaternion.Euler(levelClones[i].transform.eulerAngles.x, levelClones[i].transform.eulerAngles.y + 180, levelClones[i].transform.eulerAngles.z));
            }
            else
            {
                i++;
            }
        }
        for (int i = 0; i < levelClones.Length - 1; i++)
        {
            if (levelClones[i] != null)
            {
                Instantiate(levelClones[i], new Vector3(levelClones[i].transform.position.x, -levelClones[i].transform.position.y + 1, levelClones[i].transform.position.z), Quaternion.Euler(levelClones[i].transform.eulerAngles.x + 180, levelClones[i].transform.eulerAngles.y, levelClones[i].transform.eulerAngles.z));
            }
            else
            {
                i++;
            }
        }
        for (int i = 0; i < levelClones.Length - 1; i++)
        {
            if (levelClones[i] != null)
            {
                Instantiate(levelClones[i], new Vector3(-levelClones[i].transform.position.x, -levelClones[i].transform.position.y + 1, levelClones[i].transform.position.z), Quaternion.Euler(levelClones[i].transform.eulerAngles.x + 180, levelClones[i].transform.eulerAngles.y + 180, levelClones[i].transform.eulerAngles.z));
            }
            else
            {
                i++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
