using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] Waypoints;
    private int CurrentWaypointIndex = 0;

    private SpriteRenderer sprite;

    [SerializeField] private float Speed = 2f;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(Waypoints[CurrentWaypointIndex].transform.position, transform.position) < .1f)
        {
            CurrentWaypointIndex++;
            if (CurrentWaypointIndex >= Waypoints.Length)
            {
                CurrentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, Waypoints[CurrentWaypointIndex].transform.position, Time.deltaTime * Speed);
        
        if (transform.position.x > Waypoints[CurrentWaypointIndex].transform.position.x)
        {
            sprite.flipX = false;
        }
        else if (transform.position.x < Waypoints[CurrentWaypointIndex].transform.position.x)
        {
            sprite.flipX = true;
        }
    }
}
