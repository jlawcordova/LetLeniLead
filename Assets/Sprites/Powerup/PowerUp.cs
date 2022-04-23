using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip Sound;
    public GameObject PowerUpCutScene;
    public GameObject IpanaloCutscene;

    public void Consume()
    {
        AudioManager.Play("PowerUp", Sound, 1, false, 1f);
        Destroy(gameObject);

        GameManager.Instance.Freeze();
        Instantiate(PowerUpCutScene, Canvas.Instance.transform);


        // TODO: Add powerup effect.
        Instantiate(IpanaloCutscene, Canvas.Instance.transform);
    }
}
