using UnityEngine;
using System.Collections;



public class Open_Build_Menu : MonoBehaviour {

    [HideInInspector]
    public GameObject tower = null;

    public GameObject GameManager;

    public int towerPrice;

    // Use this for initialization
    void Start () {

        foreach (Transform child in transform)
        {           
             child.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (canPlaceTower() == false)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
	}

    private bool canPlaceTower()
    {
        return tower == null;
    }

    private bool canAffordTower()
    {
        return (towerPrice <= GameManager.GetComponent<GameManagerBehavior>().Candy);
    }

    // When the mouse is clicked and released
    void OnMouseUp()
    {
        // If the slot doesn't already contain a tower
        if (canPlaceTower() & canAffordTower())
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            // Play audio
            //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneShot(audioSource.clip);

        }
    }
}
