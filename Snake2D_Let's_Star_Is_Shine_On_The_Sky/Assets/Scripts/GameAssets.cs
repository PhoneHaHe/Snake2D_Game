using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

    public static GameAssets gameAsset;

    private void Awake() {
        gameAsset = this;
    }

    public Sprite snakeHeadSprite;
    public Sprite snakeBodySprite;

    public Sprite appleSprite;
    public Sprite grapeSprite;
    public Sprite durianSprite;
    public Sprite RockSprite;

}
