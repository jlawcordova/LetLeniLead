using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeartType
{
    Adder,
    Multiplier
}

public class Heart : MonoBehaviour
{
    public AudioClip Sound;
    public string SoundName;
    public HeartType Type = HeartType.Adder;

    public int Value = 1;

    void FixedUpdate()
    {
        HandleEdge();
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
        GameManager.Instance.HeartStreakCounter++;
        GameManager.Instance.HeartStreakTimer = GameManager.Instance.HeartStreakDuration;

        var pitchAdjustment = Mathf.Clamp(1 + (GameManager.Instance.HeartStreakCounter * 0.2f), 1, 3);

        AudioManager.Play(SoundName, Sound, pitchAdjustment, false);
        Destroy(gameObject);

        return this;
    }
}
