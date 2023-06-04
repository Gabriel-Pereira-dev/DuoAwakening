using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyantScript : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    // public float bouyancyForce = 10f;
    public Vector2 bouyancyForceForceRange = new Vector2(10.0f, 10.0f);
    private Rigidbody thisRigidbody;
    private bool hasTouchedWater;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if underwater
        float diffY = transform.position.y;
        bool isUnderwater = diffY < 0;

        if (isUnderwater)
        {
            hasTouchedWater = true;
        }

        // Ignore if never touched water
        if (!hasTouchedWater)
        {
            return;
        }

        // Buyoancy Logic
        if (isUnderwater)
        {
            float bouyancyForce = Random.Range(bouyancyForceForceRange.x, bouyancyForceForceRange.y);
            Vector3 vector = Vector3.up * bouyancyForce * -diffY;
            thisRigidbody.AddForce(vector, ForceMode.Acceleration);
        }
        thisRigidbody.drag = isUnderwater ? underwaterDrag : airDrag;
        thisRigidbody.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}
