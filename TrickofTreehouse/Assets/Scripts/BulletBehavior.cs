using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();



        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;

    }
	
	// Update is called once per frame
	void Update () {
        float timeInterval = Time.time - startTime;

        if (target.transform.position != null)
        {
            if (target != null)
                targetPosition = target.transform.position;
        }
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                    audioSource.PlayOneShot(audioSource.clip);

                target.GetComponent<Enemy_Movement>().Health -= Mathf.Max(damage, 0);

                if (target.GetComponent<Enemy_Movement>().Health <= 0)
                {
                    Destroy(target);

                    gameManager.Candy += 10;
                }

            }
            Destroy(gameObject);
        }
        
	}
}
