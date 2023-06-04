using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectionScript : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacterIndex = 0;
    public TextMeshProUGUI text;

    private bool playingCharacterAnimation;

    void Start()
    {
        text.text = characters[selectedCharacterIndex].name;
    }

    void Update()
    {
        bool pressedEnter = Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter);
        if (pressedEnter && !playingCharacterAnimation)
        {
            StartGame();
        }
    }
    public void NextCharacter()
    {
        if (!playingCharacterAnimation)
        {
            characters[selectedCharacterIndex].SetActive(false);
            selectedCharacterIndex = (selectedCharacterIndex + 1) % characters.Length;
            characters[selectedCharacterIndex].SetActive(true);
            text.text = characters[selectedCharacterIndex].name;
        }
    }

    public void PreviousCharacter()
    {
        if (!playingCharacterAnimation)
        {
            characters[selectedCharacterIndex].SetActive(false);
            selectedCharacterIndex--;
            if (selectedCharacterIndex < 0)
            {
                selectedCharacterIndex += characters.Length;
            }
            characters[selectedCharacterIndex].SetActive(true);
            text.text = characters[selectedCharacterIndex].name;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt(GameManager.KEY_SELECTED_CHARACTER_INDEX, selectedCharacterIndex);
        characters[selectedCharacterIndex].GetComponent<Animator>().SetTrigger("tCharacterSelected");
        playingCharacterAnimation = true;
        Invoke("LoadScene", 2f);
    }

    void LoadScene()
    {
        playingCharacterAnimation = false;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
