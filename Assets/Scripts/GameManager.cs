using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instence;

    public List<Transform> sptrans = new List<Transform>();
    public List<GameObject> enemyprelist = new List<GameObject>();

    public int boci = 5;

    public GameObject Boss;

    public Text NumText;
    public int NUm;

    public GameObject Winpanel;
    public GameObject overpanel;

    private void Awake()
    {
        instence = this;

        InvokeRepeating("SpawnEnemy",1,20);
    }


    public void SpawnEnemy()
    {
        if (boci<1)
        {
            Boss.SetActive(true);
            return;
        }

        boci--;
        for (int i = 0; i < 10; i++)
        {

            int spindex = Random.Range(0, sptrans.Count);
            int preindex = Random.Range(0, enemyprelist.Count);

            Instantiate(enemyprelist[preindex], sptrans[spindex].position,Quaternion.identity);
        }

    }

    public void AddNum()
    {
        NUm++;
        NumText.text = "score:"+NUm.ToString();

    }

}
