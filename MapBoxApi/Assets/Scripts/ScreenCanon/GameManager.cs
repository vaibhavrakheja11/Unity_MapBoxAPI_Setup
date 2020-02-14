using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GameManager : MonoBehaviour
{
    private bool gameStarted = false;
    private bool gameRunning = false;
    private int score = 0;

    private bool GameStarted { get { return gameStarted; } }
    private bool GameRunning {  get { return gameRunning; } }
    public static GameManager instance;
    public int GetScore { get { return score; } }
    public static GameManager Instance
    {
        get { return instance; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Start()
    {
        UnityARSessionNativeInterface.ARAnchorAddedEvent += AnchorAdded;
        UnityARSessionNativeInterface.ARAnchorRemovedEvent += RemoveAnchor;
    }

    public void EnemyHit()
    {
        score++;
    }

    public void BuildingHit()
    {
        score = Mathf.Max(0, score - 5);
    }

    private void AnchorAdded(ARPlaneAnchor anchor)
    {
        gameStarted = true;
        gameRunning = true;
    }

    private void RemoveAnchor(ARPlaneAnchor anchor)
    {
        
        gameRunning = false;
    }
}
