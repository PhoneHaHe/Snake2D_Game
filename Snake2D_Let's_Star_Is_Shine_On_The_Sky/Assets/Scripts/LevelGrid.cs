using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class LevelGrid : MonoBehaviour
{
    private Vector2Int applefoodGridPosition;
    private GameObject applefoodGameObjcet;
    private Vector2Int grapefoodGridPosition;
    private GameObject grapefoodGameObjcet;
    private Vector2Int durianfoodGridPosition;
    private GameObject durianfoodGameObjcet;
    private Snake snake;
    private int width;
    private int height;

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void SetUpSnakeRef(Snake snake)
    {
        this.snake = snake;

        SpawnAppleFood();
        SpawnGrapeFood();
        SpawnDurianFood();
    }

    private void SpawnAppleFood()
    {
        do
        {
            applefoodGridPosition = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        } while (snake.GetFullSnakeGridPostion().IndexOf(applefoodGridPosition) != -1);

        applefoodGameObjcet = new GameObject("AppleFood", typeof(SpriteRenderer));
        applefoodGameObjcet.GetComponent<SpriteRenderer>().sprite = GameAssets.gameAsset.appleSprite;
        applefoodGameObjcet.transform.position = new Vector3(applefoodGridPosition.x, applefoodGridPosition.y);
    }

    private void SpawnGrapeFood(){
        do
        {
            grapefoodGridPosition = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        } while (snake.GetFullSnakeGridPostion().IndexOf(grapefoodGridPosition) != -1);

        grapefoodGameObjcet = new GameObject("GrapeFood", typeof(SpriteRenderer));
        grapefoodGameObjcet.GetComponent<SpriteRenderer>().sprite = GameAssets.gameAsset.grapeSprite;
        grapefoodGameObjcet.transform.position = new Vector3(grapefoodGridPosition.x, grapefoodGridPosition.y);
    }

    private void SpawnDurianFood(){
        do
        {
            durianfoodGridPosition = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        } while (snake.GetFullSnakeGridPostion().IndexOf(grapefoodGridPosition) != -1);

        durianfoodGameObjcet = new GameObject("DurianFood", typeof(SpriteRenderer));
        durianfoodGameObjcet.GetComponent<SpriteRenderer>().sprite = GameAssets.gameAsset.durianSprite;
        durianfoodGameObjcet.transform.position = new Vector3(durianfoodGridPosition.x, durianfoodGridPosition.y);
    }

    public bool TrySnakeEatAppleFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == applefoodGridPosition)
        {
            Object.Destroy(applefoodGameObjcet);

            SpawnAppleFood();

            return true;
        }

        return false;
    }

    public bool TrySnakeEatGrapeFood(Vector2Int snakeGridPosition){
        if (snakeGridPosition == grapefoodGridPosition)
        {
            Object.Destroy(grapefoodGameObjcet);

            SpawnGrapeFood();
            return true;
        }
        
        return false;
    }

    public bool TrySnakeEatDurianFood(Vector2Int snakeGridPosition){
        if (snakeGridPosition == durianfoodGridPosition)
        {
            Object.Destroy(durianfoodGameObjcet);

            SpawnDurianFood();
            return true;
        }
        
        return false;
    }
}
