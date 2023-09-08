using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private MapManagement mapManager;

    private void Awake()
    {
        if (GameManager.Instance != null) Debug.LogError("On 1 GameManager Allow");

        GameManager.Instance = this;
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        mapManager.Oninit();
    }
}
