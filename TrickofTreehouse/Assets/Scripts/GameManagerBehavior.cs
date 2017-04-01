using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerBehavior : MonoBehaviour {

    public Text candyLabel;
    public Text waveLabel;


    private int candy;
    private int wave;


  
        public int Candy
    {
        get { return candy; }
        set
        {
            candy = value;
            candyLabel.GetComponent<Text>().text = "CANDY: " + candy;
        }
    }

        public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            waveLabel.GetComponent<Text>().text = "WAVE: " + (wave + 1);
        }
    }


    

    public bool gameOver = false;
    public Text healthLabel;
    public GameObject[] healthIndicator;

    private int health;
    public int Health
    // Health amount will be established in Start() 
    {
        get { return health; }
        set
        {
            health = value;
            //healthLabel.text = "Health: " + health;

            if (health <= 0 && !gameOver)
            // When player's health is equal to 0, and gameOver = false
            {
                gameOver = true;
            }

            for (int i = 0; i < healthIndicator.Length; i++)
            {
                if (i < Health)
                // If i is less than Health, healthIndicator is activated. If not, healthIndicator is deactivated.
                {
                    healthIndicator[i].SetActive(true);
                }
                else
                {
                    healthIndicator[i].SetActive(false);
                }
            }
        }
    }
    // Use this for initialization
    void Start () {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();

        Candy = 100;
        Wave = 0;
        Health = 1;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (gameOver)
        {
            SceneManager.LoadScene("StartingMenu", LoadSceneMode.Single);
        }
	
	}
}
