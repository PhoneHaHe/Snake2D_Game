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
    private GameObject rockGameObjcet;

    private List<Rock> rockList;
    private Rock rock;
    private Snake snake;
    private int width;
    private int height;

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        this.rockList = new List<Rock>();
    }

    public void SetUpSnakeRef(Snake snake)
    {
        this.snake = snake;

        SpawnAppleFood();
        SpawnGrapeFood();
        SpawnDurianFood();
        SpawnRock();
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

    private void SpawnGrapeFood()
    {
        do
        {
            grapefoodGridPosition = new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1));
        } while (snake.GetFullSnakeGridPostion().IndexOf(grapefoodGridPosition) != -1);

        grapefoodGameObjcet = new GameObject("GrapeFood", typeof(SpriteRenderer));
        grapefoodGameObjcet.GetComponent<SpriteRenderer>().sprite = GameAssets.gameAsset.grapeSprite;
        grapefoodGameObjcet.transform.position = new Vector3(grapefoodGridPosition.x, grapefoodGridPosition.y);
    }

    private void SpawnDurianFood()
    {
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

    public bool TrySnakeEatGrapeFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == grapefoodGridPosition)
        {
            Object.Destroy(grapefoodGameObjcet);

            SpawnGrapeFood();
            return true;
        }

        return false;
    }

    public bool TrySnakeEatDurianFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == durianfoodGridPosition)
        {
            Object.Destroy(durianfoodGameObjcet);

            SpawnDurianFood();
            return true;
        }

        return false;
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {

        if (gridPosition.x < 1)
        {
            gridPosition.x = width - 1;
        }

        if (gridPosition.x > width - 1)
        {
            gridPosition.x = 1;
        }

        if (gridPosition.y < 1)
        {
            gridPosition.y = height - 1;
        }

        if (gridPosition.y > height - 1)
        {
            gridPosition.y = 1;
        }
        return gridPosition;

    }

    private class Rock
    {
        private Vector2Int rockGridPosition;


        public Vector2Int GetRockGridPosition()
        {
            return rockGridPosition;
        }

        public void SetRockGridPosition(Vector2Int gridPosition)
        {
            rockGridPosition = gridPosition;
        }
    }

    private void SpawnRock()
    {
        rock = new Rock();
        do
        {
            rock.SetRockGridPosition(new Vector2Int(Random.Range(1, width - 1), Random.Range(1, height - 1)));
        } while (snake.GetFullSnakeGridPostion().IndexOf(rock.GetRockGridPosition()) != -1);

        rockGameObjcet = new GameObject("Rock", typeof(SpriteRenderer));
        rockGameObjcet.GetComponent<SpriteRenderer>().sprite = GameAssets.gameAsset.RockSprite;
        rockGameObjcet.transform.position = new Vector3(rock.GetRockGridPosition().x, rock.GetRockGridPosition().y);

        rockList.Add(rock);
    }

    public bool TrySnakeMoveAround(Vector2Int gridPosition)
    {
        foreach (Rock rock in rockList)
        {
            Vector2Int rockGridPosition = rock.GetRockGridPosition();
            if (gridPosition == rockGridPosition)
            {
                return true;
            }
        }

        return false;
    }
}
