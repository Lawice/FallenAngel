using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScHeal : MonoBehaviour {
    [Header("~~~~~~Heal stats~~~~~~")]
    public int playerHP;
    public int maxPlayerHP;

    [Header("~~~~~~Hearts~~~~~~")]
    public Image[] hearts;
    public Sprite fullHeart1;
    public Sprite fullHeart2;
    public Sprite fullHeart3;
    public Sprite emptyHeart1;
    public Sprite emptyHeart2;
    public Sprite emptyHeart3;

    public static ScHeal Instance;
    private void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    private void Update(){
        for (int u = 0; u < hearts.Length; u++) {
            if(u < playerHP) {
                if(u == 0) {
                    hearts[u].sprite = fullHeart1;
                }
                else if (u == playerHP-1 && u == maxPlayerHP - 1) { hearts[u].sprite = fullHeart3; }
                else { hearts[u].sprite = fullHeart2; }
            } else {
                if(u == 0) {
                    hearts[u].sprite = emptyHeart1;
                }
                else if (u == maxPlayerHP-1) { hearts[u].sprite = emptyHeart3; }
                else { hearts[u].sprite = emptyHeart2; }
            }

            if (u < maxPlayerHP) {
                hearts[u].enabled = true;
            }  else {
                hearts[u].enabled = false;
            }
        }

        if(playerHP > maxPlayerHP) { 
            playerHP = maxPlayerHP;
        }
    }

    public void Heal() { 
        if (playerHP + 1 > maxPlayerHP) {
            maxPlayerHP ++;
        }
        else {
            playerHP ++;
        }
    }
}
