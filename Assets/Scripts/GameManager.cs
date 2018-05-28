﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject squarePrefab; // 사각형 프리팹
    public GameObject temp;

    bool created = false; // 이번 턴에 사각형을 만들었는가?
    Vector3 zeroIndexPos; // squareArray[0,0]의 위치 값
    GameObject[,] squareArray; // 퍼즐 맵
    GameObject[,] tempIndex;
    Text[,] textIndex;
    Transform canvas;
    Vector2[] removeIndex;

    int stack = 1;
    int currentPuzzleNum;


    // Use this for initialization
    void Start () {
        zeroIndexPos = squarePrefab.transform.position;
        squareArray = new GameObject[8, 8];
        textIndex = new Text[8, 8];
        tempIndex = new GameObject[8, 8];
        for(int i = 0; i < 8; i++)
        {
            for(int y = 0; y < 8; y++)
            {
                tempIndex[i, y] = Instantiate(temp, new Vector3(-7 + (0.5f * y), 4 - (0.5f * i), 0), Quaternion.identity);
            }
        }

        CreateSquare();
        removeIndex = new Vector2[64];
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CheckSquaerIsMove()) return;



            for (int y = 0; y < 8; y++)
            {
                for (int x = 1; x < 8; x++)
                {
                    if (squareArray[y, x] == null) { continue; }
                    int topX = 0;

                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (squareArray[y, i] == null) topX = i;
                    }

                    if (squareArray[y, topX] == null && squareArray[y, x] != null)
                    {
                        squareArray[y, topX] = squareArray[y, x];
                        squareArray[y, x] = null;
                        squareArray[y, topX].GetComponent<Square>().Move(new Vector2(zeroIndexPos.x + (0.6f * topX), zeroIndexPos.y - (0.6f * y)));
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CheckSquaerIsMove()) return;


            for (int y = 0; y < 8; y++)
            {
                for (int x = 6; x >= 0; x--)
                {
                    if (squareArray[y, x] == null) { continue; }
                    int topX = 7;

                    for (int i = x + 1; i < 8; i++)
                    {
                        if (squareArray[y, i] == null) topX = i;
                    }

                    if (squareArray[y, topX] == null && squareArray[y, x] != null)
                    {
                        squareArray[y, topX] = squareArray[y, x];
                        squareArray[y, x] = null;
                        squareArray[y, topX].GetComponent<Square>().Move(new Vector2(zeroIndexPos.x + (0.6f * topX), zeroIndexPos.y - (0.6f * y)));
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CheckSquaerIsMove()) return;

            for (int y = 1; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (squareArray[y, x] == null) { continue; }
                    int topY = 0;

                    for (int i = y - 1; i >= 0; i--)
                    {
                        if (squareArray[i, x] == null) topY = i;
                    }

                    if (squareArray[topY, x] == null && squareArray[y, x] != null)
                    {
                        squareArray[topY, x] = squareArray[y, x];
                        squareArray[y, x] = null;
                        squareArray[topY, x].GetComponent<Square>().Move(new Vector2(zeroIndexPos.x + (0.6f * x), zeroIndexPos.y - (0.6f * topY)));
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CheckSquaerIsMove()) return;

            for (int y = 6; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (squareArray[y, x] == null) { continue; }
                    int topY = 7;

                    for (int i = y + 1; i < 8; i++)
                    {
                        if (squareArray[i, x] == null)
                            topY = i;
                    }

                    if (squareArray[topY, x] == null && squareArray[y, x] != null)
                    {
                        squareArray[topY, x] = squareArray[y, x];
                        squareArray[y, x] = null;
                        squareArray[topY, x].GetComponent<Square>().Move(new Vector2(zeroIndexPos.x + (0.6f * x), zeroIndexPos.y - (0.6f * topY)));
                    }
                }
            }

        }
    }

    public void CreateSquare()
    {
        if (IsFull()) { return; }
        if (created) { return; }
        created = true;

        int x = 0;
        int y = 0;
        while (true)
        {
            x = Random.Range(0, 8);
            y = Random.Range(0, 8);
            if (squareArray[y, x] == null) { break; }
        }

        squareArray[y, x] = Instantiate(squarePrefab, new Vector3(zeroIndexPos.x + (0.6f * x), zeroIndexPos.y - (0.6f * y), zeroIndexPos.z), Quaternion.identity);
    }

    public bool IsFull()
    {
        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                if (squareArray[y, x] == null) return false;
            }
        }

        return true;
    }

    public bool CheckSquaerIsMove()
    {
        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                if (squareArray[y, x] == null) { continue; }
                if (squareArray[y, x].GetComponent<Square>().IsMove) { return true; }
            }
        }

        created = false;
        return false;
    }

    public void CheckBlock()
    {
        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                if (squareArray[y, x] == null) tempIndex[y, x].GetComponent<SpriteRenderer>().color = new Color(0,0,0);
                else tempIndex[y, x].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            }
        }
    }
    
    public void CheckMatch()
    {
        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                if (squareArray[y,x] == null) continue;
                if (squareArray[y, x].GetComponent<Square>().isCheck) continue;

                stack = 0;
                currentPuzzleNum = squareArray[y, x].GetComponent<Square>().puzzleNum;

                CheckSquare(x, y);

                Debug.Log(x.ToString() + " " + y.ToString() + " " + stack.ToString() + " " + currentPuzzleNum);

                if (stack >= 3)
                {
                    for (int i = 0; i < stack; i++)
                    {
                        Destroy(squareArray[(int)removeIndex[i].y, (int)removeIndex[i].x]);
                    }
                }
            }
        }

        for(int y = 0; y < 8; y++)
        {
            for(int x = 0; x < 8; x++)
            {
                if (squareArray[y, x] == null) continue;
                squareArray[y, x].GetComponent<Square>().isCheck = false;
            }
        }
    }

    void CheckSquare(int x, int y)
    {
        if (squareArray[y, x] == null) return;
        if(squareArray[y,x].GetComponent<Square>().puzzleNum == currentPuzzleNum && !squareArray[y, x].GetComponent<Square>().isCheck)
        {
            removeIndex[stack++] = new Vector2(x, y);
            squareArray[y, x].GetComponent<Square>().isCheck = true;

            if (0 <= x - 1) CheckSquare(x - 1, y);
            if (7 >= x + 1) CheckSquare(x + 1, y);
            if (0 <= y - 1) CheckSquare(x, y - 1);
            if (7 >= y + 1) CheckSquare(x, y + 1);
        }
    }
}
