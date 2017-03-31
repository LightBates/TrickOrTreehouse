using UnityEngine;
using System.Collections;

public class Enemy_Health : MonoBehaviour {

    private Enemy_Movement parent;
    private float originalScale;

    // Use this for initialization
    void Start () {
        originalScale = gameObject.transform.localScale.x;
        parent = transform.parent.gameObject.GetComponent<Enemy_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = parent.Health / parent.maxHealth * originalScale;
        gameObject.transform.localScale = tmpScale;
    }

}
