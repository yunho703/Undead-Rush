using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFloat : MonoBehaviour
{
    public float speed = 0.5f;
    public float distance = 0.2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPos + new Vector3(offset, 0f, 0f);
    }
}