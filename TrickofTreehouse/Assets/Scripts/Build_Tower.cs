using UnityEngine;
using System.Collections;

public class Build_Tower : MonoBehaviour {

    public GameObject towerPrefab;
    private GameObject tower;
    private Open_Build_Menu parent;

    // Use this for initialization
    void Start () {
        parent = transform.parent.gameObject.GetComponent<Open_Build_Menu>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool canPlaceTower()
    {
        return tower == null;
    }

    void OnMouseUp()
    {
        // If we can place a tower
        if (canPlaceTower())
        {
            // create a tower
            tower = (GameObject)Instantiate(towerPrefab, parent.transform.position, Quaternion.identity);

            gameObject.SetActive(false);

            

            // TODO: Deduct gold
            parent.GetComponent<Open_Build_Menu>().tower = tower;
        }
    }
}
