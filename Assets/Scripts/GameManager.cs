using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject squarePrefab; // 사각형 프리팹
    bool created = false; // 이번 턴에 사각형을 만들었는가?
    Vector3 zeroIndexPos; // squareArray[0,0]의 위치 값
    GameObject[,] squareArray; // 퍼즐 맵

	// Use this for initialization
	void Start () {
        zeroIndexPos = squarePrefab.transform.position;
        squareArray = new GameObject[8, 8];

        CreateSquare();
    }

    // Update is called once per frame
    void Update () {


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 1; x < 8; x++)
                {
                    if (squareArray[y, x] = null) { continue; }

                    for (int i = x - 1; x >= 0; i++)
                    {

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

        squareArray[y, x] = Instantiate(squarePrefab, new Vector3(zeroIndexPos.x + (0.6f * x), zeroIndexPos.y - (0.6f * y), zeroIndexPos.z), Quaternion.identity);    }

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
        for(int i = 0; i < 64; i ++)
        {
        }

        return false;
    }
}
