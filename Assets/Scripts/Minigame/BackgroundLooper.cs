using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    int _obstacleCount = 0;
    int _backgroundCount = 5;
    Vector3 _obstacleLastPosition = Vector3.zero;

    private void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        _obstacleLastPosition = obstacles[0].transform.position;
        _obstacleCount = obstacles.Length;

        for (int i = 0; i < _obstacleCount; i++)
        {
            _obstacleLastPosition = obstacles[i].SetRandomPlace(_obstacleLastPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered : " + collision.name);

        if (collision.CompareTag("Background"))
        {
            float backgroundWidth = ((BoxCollider2D)collision).size.x;
            Debug.Log("Triggered : " + backgroundWidth);

            Vector3 pos = collision.transform.position;

            pos.x += backgroundWidth * _backgroundCount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            _obstacleLastPosition = obstacle.SetRandomPlace(_obstacleLastPosition);

        }
    }
}
