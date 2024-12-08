using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ScCharges;

public class ScCharges : MonoBehaviour {
    [Header("~~~~~~Heal stats~~~~~~")]
    public int playerCharges;
    public int maxCharges;
    public enum ChargesType { single, dual, triple, quadruple, sextuple }
    public ChargesType chargeType;

    public Image[] charges;
    public Sprite chargesEmpty1;
    public Sprite chargesEmpty2;
    public Sprite chargesEmpty3;

    [Header("~~~~~~Single Charges~~~~~~")]
    public Sprite Charges1;
    public Sprite Charges2;
    public Sprite Charges3;

    [Header("~~~~~~Multiple Charges~~~~~~")]
    public Sprite normalCharges;
    public Sprite fullCharges;

    private void FixedUpdate() {
        playerCharges = ScShoot.Instance.bulletLeft;
        maxCharges = ScShoot.Instance.magazineSize;
        switch (ScShoot.Instance.chargesUse) {
            case 1:
                chargeType = ChargesType.single;
                break;
            case 2:
                chargeType = ChargesType.dual;
                break;
            case 3:
                chargeType= ChargesType.triple;
                break;
            case 4:
                chargeType = ChargesType.quadruple;
                break;
            case 6:
                chargeType = ChargesType.sextuple;
                break;
        }
    }

    private void Update(){
        for (int u = 0; u < charges.Length; u++) {
            if(u < playerCharges) {
                if(u == 0) {
                    if(chargeType == ChargesType.single) { charges[u].sprite = Charges1; }
                    else { charges[u].sprite = Charges3;}
                }
                else if (u == playerCharges - 1 && u == maxCharges - 1) { charges[u].sprite = Charges2; }
                else {
                    switch (chargeType){
                        case ChargesType.single:
                            charges[u].sprite = normalCharges;
                            break;
                            
                        case ChargesType.dual:
                            if (u % 2 == 0) { charges[u].sprite = fullCharges;   } 
                            else {charges[u].sprite = normalCharges; }
                            break;

                        case ChargesType.triple:
                            if (u % 3 == 0) { charges[u - 1].sprite = normalCharges; }
                            else { charges[u - 1].sprite = fullCharges; }
                            if (u == maxCharges - 2) { charges[u].sprite = fullCharges; }
                            break;

                        case ChargesType.quadruple:
                            if (u % 4 == 0) { charges[u - 1].sprite = normalCharges; }
                            else { charges[u - 1].sprite = fullCharges; }
                            if (u == maxCharges - 2) { charges[u].sprite = fullCharges; }
                            break;

                        case ChargesType.sextuple:
                            if (u % 6 == 0) { charges[u - 1].sprite = normalCharges; }
                            else { charges[u - 1].sprite = fullCharges; }
                            if (u == maxCharges - 2) { charges[u].sprite = fullCharges; }
                            break;
                    }
                }
            } else {
                if(u == 0) {
                    charges[u].sprite = chargesEmpty1;
                }
                else if (u == maxCharges - 1) { charges[u].sprite = chargesEmpty3; }
                else { charges[u].sprite = chargesEmpty2; }
            }

            if (u < maxCharges) {
                charges[u].enabled = true;
            }  else {
                charges[u].enabled = false;
            }
        }
        if(playerCharges > maxCharges) {
            playerCharges = maxCharges;
        }
    }
}


