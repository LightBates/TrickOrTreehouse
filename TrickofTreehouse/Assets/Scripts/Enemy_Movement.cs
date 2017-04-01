using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Movement : MonoBehaviour {

    public float maxHealth = 100;
    
    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;
    private GameManagerBehavior gameManager;
    private float currentHealth;
    public List<Sprite> spriteList;

    public float Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public float distanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }

    // Use this for initialization
    void Start () {
        lastWaypointSwitchTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update () {
        // Find start position and end position
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        // Find path length, total time for path, current time on path and move a little
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        // if we make it to the destination...
        if (gameObject.transform.position.Equals(endPosition))
        {
            // if we haven't made it to the last waypoint yet...
            if (currentWaypoint < waypoints.Length - 2)
            {
                // set the current waypoint to the next one
                currentWaypoint++;
                // Update our time
                lastWaypointSwitchTime = Time.time;

                // Forward Facing Sprite
                if (currentWaypoint == 0 | currentWaypoint == 2 | currentWaypoint == 4 | currentWaypoint == 6)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[0];
                }

                // Right Facing Sprite
                else if (currentWaypoint == 1 | currentWaypoint == 5 | currentWaypoint == 7)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[1];
                }

                // Left Facing Sprite
                else if (currentWaypoint == 3)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[2];
                }
            }
            else
            {
                
                gameManager.Health--;

                // We made it to the last waypoint, we are no longer needed
                Destroy(gameObject);

               
                
                
            }
        }
    }
}
