using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public BallAudio ballAudio;
    public ParticleSystem collisionParticle;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;    
    public float maxStartY = 4f;
    public float speedMultiplier = 1.1f;

    private float startX = 0f;

    private void Start()
    {
        GameManager.instance.onReset += ResetBall;
        GameManager.instance.gameUI.onStartGame += ResetBall;
    }

    private void ResetBall()
    {
        ResetBallPosition();
        InitialPush();
    }

    private void InitialPush()
    {
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;

        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;

        EmitParticle(32);
    }

    private void ResetBallPosition()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if(scoreZone)
        {
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
            GameManager.instance.screenshake.StartShake(0.33f, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = collision.collider.GetComponent<Paddle>();
        if(paddle)
        {
            ballAudio.PlayPaddleSound();
            rb2d.velocity *= speedMultiplier;
            EmitParticle(16);
            GameManager.instance.screenshake.StartShake(Mathf.Sqrt(rb2d.velocity.magnitude) * 0.02f, 0.075f);
        }

        Wall wall = collision.collider.GetComponent<Wall>();
        if (wall)
        {
            ballAudio.PlayWallSound();
            EmitParticle(8);
            GameManager.instance.screenshake.StartShake(0.033f, 0.033f);
        }
    }

    private void EmitParticle(int amount)
    {
        collisionParticle.Emit(amount);
    }
}
