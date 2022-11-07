using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (transform.position.x >= -20)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    void Update()
    {
    }
}
