using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public int maxHealth;
    // [HideInInspector] 
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
}
