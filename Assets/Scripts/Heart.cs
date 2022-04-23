using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeartType
{
    Adder,
    Multiplier
}

public enum HeartStyle
{
    Heart,
    Rosas,
    Energy
}

public enum Movement
{
    Static,
    Bouncing
}

public class Heart : MonoBehaviour
{
    public AudioClip Sound;
    public string SoundName;
    public HeartStyle Style = HeartStyle.Heart;
    public HeartType Type = HeartType.Adder;
    public Movement Movement = Movement.Static;
    public float SpeedX = 0.1f;
    public float SpeedY = 0.1f;

    public int Value = 1;

    void Start()
    {
        SpeedX = UnityEngine.Random.Range(0, 2) == 0 ? -0.05f : 0.1f;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.Frozen)
        {
            return;
        }

        HandleEdge();
        HandleBounce();
    }

    private void HandleBounce()
    {
        if (Movement == Movement.Bouncing)
        {
            transform.position = new Vector3(transform.position.x + SpeedX, transform.position.y + SpeedY, transform.position.z);

            var y = transform.position.y;
            if (y > -1 || y < -4)
            {
                SpeedY *= -1;
            }
        }
    }

    private void HandleEdge()
    {
        transform.position -= new Vector3(GameManager.Instance.Speed, 0, 0);

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    public Heart Consume()
    {
        if (Style == HeartStyle.Heart)
        {
            GameManager.Instance.HeartStreakCounter++;
            GameManager.Instance.HeartStreakTimer = GameManager.Instance.HeartStreakDuration;

            var pitchAdjustment = Mathf.Clamp(1 + (GameManager.Instance.HeartStreakCounter * 0.2f), 1, 3);

            AudioManager.Play(SoundName, Sound, pitchAdjustment, false);
            Destroy(gameObject);
        }
        else
        {
            AudioManager.Play(SoundName, Sound, 1, false, 0.8f);
            Destroy(gameObject);
        }
        return this;
    }
}
