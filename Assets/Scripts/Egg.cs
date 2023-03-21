using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private bool _isBroken = false;
    public bool isBroken
    {
        get{return _isBroken;}
        set{
            _isBroken = value;
            if(value == true)
            {
                GetComponent<Animator>().SetTrigger("Destroy");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player) && !isBroken)
        {
            player.eggs.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.eggs.Remove(this);
        }
    }
}
