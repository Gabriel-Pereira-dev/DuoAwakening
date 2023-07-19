using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject widgetPrefab;
    [SerializeField] Vector3 widgetOffset;
    public float radius = 10f;

    private GameObject widget;
    [HideInInspector] public InteractionWidget interactionWidgetComponent;
    private bool isAvailable = true;
    private bool isActive;

    public event EventHandler<InteractionEventArgs> OnInteraction;
    private void OnEnable()
    {
        GameManager.Instance.interactionList.Add(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.interactionList.Remove(this);
    }

    void Awake()
    {
        // Create Widget
        widget = Instantiate(widgetPrefab, transform.position + widgetOffset, widgetPrefab.transform.rotation);
        widget.transform.SetParent(gameObject.transform, true);
        interactionWidgetComponent = widget.GetComponent<InteractionWidget>();

    }

    void Start()
    {
        //Set widget camera
        var worldUiCamera = GameManager.Instance.worldUiCamera;
        var canvas = widget.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = worldUiCamera;
        }
        if (interactionWidgetComponent != null)
        {
            interactionWidgetComponent.worldUiCamera = worldUiCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;

        var interactionWidget = widget.GetComponent<InteractionWidget>();
        if (isActive)
        {
            interactionWidget.Show();
        }
        else
        {
            interactionWidget.Hide();
        }
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void SetAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
    }

    public void Interact()
    {
        OnInteraction?.Invoke(this, new InteractionEventArgs());
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position, radius);

    // }
}
