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

    [Header("~~~~~~Invincibilty~~~~~~")]
    public float invincibilityDura = 2f;
    public bool isInvincible = false;
    private Renderer _playerRenderer;

    private void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    private void Start() {
        _playerRenderer = GetComponent<Renderer>();
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
        Debug.Log(isInvincible); 
        if (playerHP <= 0) { Destroy(this.gameObject); }
    }

    public void Heal() { 
        if (playerHP + 1 > maxPlayerHP) {
            maxPlayerHP ++;
        }
        else {
            playerHP ++;
        }
    }


    public void TakeDamage(int _damage) {
        if (!isInvincible) {
            if(playerHP-_damage > 0) { 
                playerHP -= _damage;
                StartInvincibility();
            }
            else { 
                playerHP = 0;
            }
        }
    }

    void StartInvincibility(){
        if (!isInvincible) {
            invincibilityDura = 2f;
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility() {
        isInvincible = true;

        while (invincibilityDura > 0){
            _playerRenderer.enabled = !_playerRenderer.enabled;
            yield return new WaitForSeconds(0.2f); 
            invincibilityDura -= 0.2f;
        }
        isInvincible = false;
        _playerRenderer.enabled = true;
    }
}
