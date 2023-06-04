using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaterSplashScript : MonoBehaviour
{
    public GameObject waterSplashPrefab;
    private static readonly List<string> SPLASH_TAGS = new List<string> { "Bomb", "Player" };

    void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;

        bool haveToSplashWater = SPLASH_TAGS.Any(st => hitObject.CompareTag(st));
        if (haveToSplashWater)
        {
            Instantiate(waterSplashPrefab, hitObject.transform.position, Quaternion.identity);
        }
    }
}
