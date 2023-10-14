using System;
using System.Collections;
using System.Collections.Generic;
using BossBattle;
using Cinemachine;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }
    [HideInInspector] public static readonly string KEY_SELECTED_CHARACTER_INDEX = "SelectedCharacterIndex";

    //Interaction
    [SerializeField] public List<Interaction> interactionList;
    public GameObject player;

    //Rendering
    [Header("Rendering")]
    [SerializeField] public Camera worldUiCamera;

    //Physics
    [Header("Physics")]
    [SerializeField] public LayerMask groundLayer;

    [Header("Inventory")]
    public int keys;
    public bool hasBossKey;
    
    //Boss
    [Header("Boss")] 
    public GameObject boss;
    public GameObject bossBattleParts;
    public BossBattleHandler bossBattleHandler;
    public GameObject bossDeathSequence;

    [Header("Music")]
    public AudioSource gameplayMusic;
    public AudioSource bossMusic;
    public AudioSource ambienceMusic;

    [Header("UI")] public GameplayUI gameplayUI;

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

    private void Start()
    {
        bossBattleHandler = new BossBattleHandler();
        
        // Play gameplay music
        var gameplayTargetVolume = gameplayMusic.volume;
        gameplayMusic.volume = 0;
        StartCoroutine(FadeAudioSource.StartFade(gameplayMusic, gameplayTargetVolume, 1f));
        gameplayMusic.Play();
        
        // Play ambience music
        var ambienceTargetVolume = ambienceMusic.volume;
        ambienceMusic.volume = 0;
        StartCoroutine(FadeAudioSource.StartFade(ambienceMusic, ambienceTargetVolume,1f));
        ambienceMusic.Play();
    }

    void Update()
    {
        bossBattleHandler.Update();
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
