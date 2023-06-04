using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }
    [HideInInspector] public static readonly string KEY_SELECTED_CHARACTER_INDEX = "SelectedCharacterIndex";




    [Header("Physics")]
    [SerializeField] public LayerMask groundLayer;

    // [Header("Character")]
    // public int selectedCharacterIndex;
    // public GameObject[] characters;
    // public string characterName;
    // public new CinemachineVirtualCamera camera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


        // Character
        // selectedCharacterIndex = PlayerPrefs.GetInt(KEY_SELECTED_CHARACTER_INDEX);
        // LoadCharacter();
    }

    void Update()
    {

    }


    // private void LoadCharacter()
    // {
    //     GameObject prefab = characters[selectedCharacterIndex];
    //     prefab.SetActive(true);
    //     camera.m_Follow = prefab.transform;
    //     camera.m_LookAt = prefab.transform;

    //     characterName = prefab.name;

    // }

}
