using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public GameObject HeartBurst;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameState != GameState.Game)
        {
            return;
        }

        HandleHeartCollision();
    }

    void HandleHeartCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Finish")
            {
                GameManager.SetEnd();
                return;
            }

            if (hit.collider.gameObject.tag == "PowerUp")
            {
                var powerUp = hit.collider.gameObject.GetComponent<PowerUp>();
                powerUp.Consume();
                return;
            }

            var heart = hit.collider.gameObject.GetComponent<Heart>();
            var heartValue = heart.Consume();
            if (heartValue.Style == HeartStyle.Heart)
            {
                var heartBurst = Instantiate(HeartBurst, new Vector3(transform.position.x, transform.position.y, -4), HeartBurst.transform.rotation);
                ScoreManager.AddScore(heartValue);
            } else if (heartValue.Style == HeartStyle.Rosas)
            {
                GameManager.Instance.TotalRosas++;
            } else if (heartValue.Style == HeartStyle.Energy)
            {
                Leni.Instance.AddEnergy();
            }
            Destroy(hit.collider.gameObject);
        }
    }
}
