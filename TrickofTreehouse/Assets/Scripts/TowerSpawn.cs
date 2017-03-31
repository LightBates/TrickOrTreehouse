using UnityEngine;
using System.Collections;

public class TowerSpawn : MonoBehaviour {

    public GameObject Tower1;
    public GameObject Tower2;
    public GameObject TowerSelection;
    public GameObject whatToDoWithTower;
    public GameObject UpgradeTower;
    public GameObject DeleteTower;
    public bool T1 = false;
    public bool T2 = false;

    public void SpawnTower1() {
        Tower1.SetActive(true);
        T1 = true;
    }
    public void SpawnTower2() {
        Tower2.SetActive(true);
        T2 = true;
    }
    public void WhichTower()
    {
        TowerSelection.SetActive(true);
    }
 //   public void levelUpTower()
 //   {

 //   }
    public void Eliminate()
    {

    }
}
