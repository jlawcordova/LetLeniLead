using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip Sound;

    public void Consume()
    {
        AudioManager.Play("PowerUp", Sound, 1, false, 1f);
        Destroy(gameObject);

        GameManager.Instance.Freeze();

        // TODO: Add powerup effect.
    }
}
