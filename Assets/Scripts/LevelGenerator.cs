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
    public TilemapRenderer[] tilemaps;
    public GameObject[] sprites;
    int val = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Create Top Left Sprite
        Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with default rotation at Top-Left Coordinate
        startX++; //Increment X coordinate

        //Disable Level 01 Manual Level Layout
        foreach (TilemapRenderer set in tilemaps)
        {
            set.enabled = false;
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
                    continue;
                }

                if (val == 1 || val == 3) //If the sprite is Outer Wall or Inner Wall
                {
                    if (((l - 1) >= 0) && (levelMap[k, l - 1] == 1 || levelMap[k, l - 1] == 2 || levelMap[k, l - 1] == 3 || levelMap[k, l - 1] == 4)) //Check if left of the current index is within range and has any sprites(Outer Corner or Outer Wall or Inner Corner or Inner Wall)
                    {
                        Debug.Log("no");
                        Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.identity); //Create Sprite with proper Rotation
                    }
                    else if (((k - 1) >= 0) && (levelMap[k - 1, l] == 1 || levelMap[k - 1, l] == 2 || levelMap[k - 1, l] == 3 || levelMap[k - 1, l] == 4)) //Check if Top of the current index is within range and has any sprites(Outer Corner or Outer Wall or Inner Corner or Inner Wall)
                    {
                        Instantiate(sprites[val], new Vector3(startX, startY), Quaternion.Euler(0, 0, 90)); //Create Sprite with proper Rotation
                    }
                }
                startX++; //Increment X coordinate to go to the next space in the row
            }
            startX = -13.5f; //After a whole row is completed, restart at left most column
            startY--; //Decrement the Y coordinate to go to the next row
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
