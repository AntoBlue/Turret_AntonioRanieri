using System;
using UnityEngine;
using Random = System.Random;

public class MoveBullet : MonoBehaviour
{
    private float _speed = 1;

    public void Configure(float speed)
    {
        _speed = speed;
        
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * _speed), Space.Self);
    }
}
