using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{

    public GameObject wavePrefab;

    void Start()
        {
            Invoke("DeleteWave",1.5f);
        }

    public void DeleteWave()
    {
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //var waveClone = (GameObject)Instantiate(wavePrefab);
        Destroy(this.gameObject);
    }

    public void CreateWave()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

}
