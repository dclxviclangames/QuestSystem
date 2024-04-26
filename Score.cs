using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public Text scoreText;

   // public bool isBonuse = false;

    private void Update()
    {
        scoreText.text = ((int)(player.position.z)).ToString();
    }
}
