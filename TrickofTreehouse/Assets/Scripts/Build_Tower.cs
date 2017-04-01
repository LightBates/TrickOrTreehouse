using UnityEngine;
using System.Collections;

public class Build_Tower : MonoBehaviour {

    public GameObject towerPrefab;
    private GameObject tower;
    private Open_Build_Menu parent;
    private int price;
    public Color buttonColor;

    // Use this for initialization
    void Start () {
        parent = transform.parent.gameObject.GetComponent<Open_Build_Menu>();
        price = towerPrefab.GetComponent<TurretData>().baseCost;
    }


	
	// Update is called once per frame
	void Update () {
	    if (!canAffordTower())
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 116, 228);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = buttonColor;

        }
	}

    private bool canPlaceTower()
    {
        return tower == null;
    }

    private bool canAffordTower()
    {
        return (price <= parent.GameManager.GetComponent<GameManagerBehavior>().Candy);
    }

    void OnMouseUp()
    {
        // If we can place a tower
        if (canPlaceTower() & canAffordTower())
        {
            // create a tower
            tower = (GameObject)Instantiate(towerPrefab, parent.transform.position, Quaternion.identity);

            parent.gameObject.GetComponent<Open_Build_Menu>().GameManager.GetComponent<GameManagerBehavior>().Candy -= price;
            gameObject.SetActive(false);


            parent.GetComponent<Open_Build_Menu>().tower = tower;
        }
    }
}
