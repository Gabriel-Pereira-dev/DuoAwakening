using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    public float Delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginSelfDestruction());
    }

    private IEnumerator BeginSelfDestruction()
    {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }

}
