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
    public Sprite[] gemSprite;
    public GameObject destroyParticle;
    public bool isDestroy = false;

	// Use this for initialization
	void Start () {
        isMove = false;
        puzzleNum = Random.Range(0, 7);
        gameObject.name = "square" + num++.ToString();
        gameObject.GetComponent<SpriteRenderer>().sprite = gemSprite[puzzleNum];

        GameObject.Find("GameManager").GetComponent<GameManager>().CheckMatch();
    }

    private void OnDestroy()
    {
        if(isDestroy) Instantiate(destroyParticle).transform.position = transform.position;
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
            if (!gameManager.CheckSquaerIsMove())
            {
                gameManager.CheckMatch();
                gameManager.CreateSquare();
            }
        }

    }
}
