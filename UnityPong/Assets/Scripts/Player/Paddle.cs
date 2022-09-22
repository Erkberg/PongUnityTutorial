using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public BoxCollider2D coll2d;
    public int id;
    public float moveSpeed = 2f;

    [Header("AI")]
    public float aiDeadzone = 1f;
    public float aiMoveSpeedMultiplierMin = 0.5f, aiMoveSpeedMultiplierMax = 1.5f;

    private Vector3 startPosition;
    private int direction = 0;
    private float moveSpeedMultiplier = 1f;

    private const string MovePlayer1InputName = "MovePlayer1";
    private const string MovePlayer2InputName = "MovePlayer2";

    private void Start()
    {
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        if(IsAi())
        {
            MoveAi();
        }
        else
        {
            float movement = GetInput();
            Move(movement);
        }        
    }

    private bool IsAi()
    {
        bool isPlayer1Ai = IsLeftPaddle() && GameManager.instance.IsPlayer1Ai();
        bool isPlayer2Ai = !IsLeftPaddle() && GameManager.instance.IsPlayer2Ai();
        return isPlayer1Ai || isPlayer2Ai;
    }

    private void MoveAi()
    {
        Vector2 ballPos = GameManager.instance.ball.transform.position;
        
        if (Mathf.Abs(ballPos.y - transform.position.y) > aiDeadzone)
        {
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }

        if (Random.value < 0.01f)
        {
            moveSpeedMultiplier = Random.Range(aiMoveSpeedMultiplierMin, aiMoveSpeedMultiplierMax);
        }

        Move(direction);
    }

    private float GetInput()
    {
        return IsLeftPaddle() ? Input.GetAxis(MovePlayer1InputName) : Input.GetAxis(MovePlayer2InputName);
    }

    private void Move(float movement)
    {
        Vector2 velo = rb2d.velocity;
        velo.y = moveSpeed * moveSpeedMultiplier * movement;
        rb2d.velocity = velo;
    }

    public float GetHeight()
    {
        return coll2d.size.y;
    }

    public bool IsLeftPaddle()
    {
        return id == 1;
    }
}
