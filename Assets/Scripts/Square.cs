using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {
    
    [SerializeField] float speed = 0.2f; // 이동속도
    bool isMove; // 현재 움직이는 중인가?
    Vector2 targetPos; // 이동 목표 위치
    static int num = 0;
    public bool isCheck = false;

    public int puzzleNum;
    public bool IsMove { get { return isMove; } }

	// Use this for initialization
	void Start () {
        isMove = false;
        puzzleNum = Random.Range(0, 7);
        gameObject.name = "square" + num++.ToString();

        switch (puzzleNum)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 160, 0);
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
                break;
            case 5:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 160, 0);
                break;
            case 6:
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 255);
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        if(isMove)
        {
            Move(targetPos);
        }
    }

    public void Move(Vector2 _targetPos)
    {
        isMove = true;
        targetPos = _targetPos;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed);

        if (targetPos == (Vector2)transform.position)
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            isMove = false;
            if(!gameManager.CheckSquaerIsMove()) gameManager.CreateSquare();
        }

    }
}
