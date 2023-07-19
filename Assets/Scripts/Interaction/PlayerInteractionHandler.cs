using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private Interaction currentInteraction;

    private readonly float scanInterval = 0.5f;
    private float scanCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if ((scanCooldown -= Time.deltaTime) <= 0f)
        {
            scanCooldown = scanInterval;
            ScanObjects();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteraction != null)
            {
                currentInteraction.Interact();
                Debug.Log("Interagiu com algo");
                ScanObjects();
            }
        }
    }



    private void ScanObjects()
    {

        Interaction nearestInteraction = GetNearesInteraction(transform.position);
        if (nearestInteraction != currentInteraction)
        {
            currentInteraction?.SetActive(false);
            nearestInteraction?.SetActive(true);
            currentInteraction = nearestInteraction;
        }
    }

    public Interaction GetNearesInteraction(Vector3 position)
    {
        var interactionList = GameManager.Instance.interactionList;

        //Create cache
        float closestDistance = -1f;
        Interaction closestInteraction = null;

        // Iterate through objects
        foreach (var interaction in interactionList)
        {
            var distance = (interaction.transform.position - position).magnitude;
            var isCloseEnough = distance <= interaction.radius;
            var isAvailable = interaction.IsAvailable();
            bool isCacheInvalid = closestDistance < 0;
            if (isCloseEnough && isAvailable)
            {
                if (isCacheInvalid || distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteraction = interaction;
                }
            }

        }
        return closestInteraction;
    }

}
