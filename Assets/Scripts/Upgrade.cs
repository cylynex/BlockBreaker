using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public class Upgrade : ScriptableObject {

    public string upgradeName;
    public string upgradeFlavorText;
    public Color upgradeColor;
    public float upgradeDuration = 0f;

    public int freePoints = 0;
    public int ballsToSpawn = 0;
    public float paddleScale = 0f;

    public Color upgradeAltColor;

}
