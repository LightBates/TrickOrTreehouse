using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour {

    private float lastShotTime;
    private TurretData turretData;

    public List<GameObject> enemiesInRange;

	// Use this for initialization
	void Start () {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        turretData = gameObject.GetComponentInChildren<TurretData>();

    }

    void OnEnemyDestroy (GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    void Shoot(Collider2D target)
    {
        GameObject bulletPreFab = turretData.CurrentLevel.bullet;

        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPreFab.transform.position.z;
        targetPosition.z = bulletPreFab.transform.position.z;

        GameObject newBullet = (GameObject)Instantiate(bulletPreFab);
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;
        
    }
	
	// Update is called once per frame
	void Update () {
        GameObject target = null;

        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<Enemy_Movement>().distanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > turretData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI + 90,
                new Vector3(0, 0, 1));
            
        }
	}
}
