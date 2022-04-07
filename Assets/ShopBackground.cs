using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBackground : MonoBehaviour
{
    public GameObject ShopBackgroundObject;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Instantiate(ShopBackgroundObject, new Vector3(-9f + (i * 4.5f), -9f + (j * 4.5f), 0f), Quaternion.identity);
            }
        }
    }
}
