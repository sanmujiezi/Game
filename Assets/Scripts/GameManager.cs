using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UIManager.Instance.GetPanel<GamePlayView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
