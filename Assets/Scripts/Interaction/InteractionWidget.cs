using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private CanvasGroup canvasGroup;
    public Camera worldUiCamera;

    private string inputString = "E";
    private string actionString = "";
    [SerializeField] private bool isVisible = false;

    void Awake()
    {
        worldUiCamera = GameManager.Instance.worldUiCamera;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Reset Text
        inputText.text = inputString;
        actionText.text = actionString;

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        // Visibility
        var targetAlpha = isVisible ? 1 : 0;
        var currentAlpha = canvasGroup.alpha;
        var newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, 0.1f);
        canvasGroup.alpha = newAlpha;

        // Face camera
        transform.rotation = worldUiCamera.transform.rotation;
    }

    public void setInputText(string text)
    {
        inputString = text;
        inputText.text = inputString;
    }

    public void setActionText(string text)
    {
        actionString = text;
        actionText.text = actionString;
    }

    public void Show()
    {
        isVisible = true;
    }

    public void Hide()
    {
        isVisible = false;
    }
}
