using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [HideInInspector] public float score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void SetScore(float sc)
    {
        score += sc;
    }
}
