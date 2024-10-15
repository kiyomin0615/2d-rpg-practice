using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    GameObject mainCamera;

    [SerializeField] float parallaxEffect; // 0 - 1

    float xPosition;
    float length;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distToMove = mainCamera.transform.position.x * parallaxEffect;
        float diff = mainCamera.transform.position.x - distToMove;

        if (diff > xPosition + length)
        {
            xPosition += length;
        }
        else if (diff < xPosition - length)
        {
            xPosition -= length;
        }

        transform.position = new Vector3(xPosition + distToMove, transform.position.y);
    }
}
