using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {
    
    float speed = 0.2f; // 이동속도
    bool isMove; // 현재 움직이는 중인가?
    Vector2 targetPos; // 이동 목표 위치

    public bool IsMove { get { return isMove; } }
    public int indexNum;

	// Use this for initialization
	void Start () {
        isMove = false;
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
            isMove = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().CreateSquare();
        }

    }
}
