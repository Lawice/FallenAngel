using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScBomb : MonoBehaviour {
    [Header("~~~~~~Security Bomb stats~~~~~~")]
    public int nbBomb;
    public int maxBomb;

    [Header("~~~~~~BOMB~~~~~~")]
    public Image[] bombs;
    public Sprite fullBomb1;
    public Sprite fullBomb2;
    public Sprite fullBomb3;
    public Sprite emptyBomb1;
    public Sprite emptyBomb2;
    public Sprite emptyBomb3;

    private void Update() {
        for (int u = 0;u < bombs.Length; u++) {
            if(u < nbBomb) {
                if(u == 0) {
                    bombs[u].sprite = fullBomb1;
                }
                else if (u == nbBomb - 1 && u == maxBomb - 1) { bombs[u].sprite = fullBomb3; }
                else { bombs[u].sprite = fullBomb2; }
            } else {
                if(u == 0) {
                    bombs[u].sprite = emptyBomb1;
                }
                else if (u == maxBomb - 1) { bombs[u].sprite = emptyBomb3; }
                else { bombs[u].sprite = emptyBomb2; }
            }
        }
    }

    private void FixedUpdate(){
        nbBomb = ScShoot.Instance.nbBomb;
        maxBomb = ScShoot.Instance.maxBomb;
    }
}
