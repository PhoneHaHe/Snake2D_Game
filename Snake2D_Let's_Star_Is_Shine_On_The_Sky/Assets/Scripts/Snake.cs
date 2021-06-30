using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Snake : MonoBehaviour
{
    private enum Directions
    {
        Left, Right, Up, Down
    }

    private enum State
    {
        Alive, Dead
    }

    public bool isGameStart;
    private State SnakeState;
    private Vector2Int gridPosition;
    private Directions gridMoveDirection;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    public int speedRate = 1;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;


    public void SetUpLevelGrid(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }
    private void Awake()
    {
        gridPosition = new Vector2Int(1, 1);
        gridMoveTimerMax = .5f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Directions.Right;
        SnakeState = State.Alive;
        isGameStart = false;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodySize = 0;
        snakeBodyPartList = new List<SnakeBodyPart>();


    }

    // Update is called once per frame
    private void Update()
    {

        if (isGameStart)
        {
            switch (SnakeState)
            {
                case State.Alive:
                    HandleInput();
                    HandleGridMovement();
                    break;
                case State.Dead:
                    break;
            }
        }

    }

    private void HandleInput()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (gridMoveDirection)
            {
                default:
                case Directions.Left: gridMoveDirection = Directions.Down; break;
                case Directions.Right: gridMoveDirection = Directions.Up; break;
                case Directions.Up: gridMoveDirection = Directions.Left; break;
                case Directions.Down: gridMoveDirection = Directions.Right; break;
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            switch (gridMoveDirection)
            {
                default:
                case Directions.Left: gridMoveDirection = Directions.Up; break;
                case Directions.Right: gridMoveDirection = Directions.Down; break;
                case Directions.Up: gridMoveDirection = Directions.Right; break;
                case Directions.Down: gridMoveDirection = Directions.Left; break;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime * speedRate;
        if (gridMoveTimer >= gridMoveTimerMax)
        {

            gridMoveTimer -= gridMoveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];

            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Directions.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                case Directions.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Directions.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
                case Directions.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
            }

            gridPosition += gridMoveDirectionVector;

            bool snakeOutOfSpace = levelGrid.ValidateGridPosition(gridPosition);

            if (snakeOutOfSpace)
            {
                SnakeState = State.Dead;
            }

            bool snakeAteAppleFood = levelGrid.TrySnakeEatAppleFood(gridPosition);
            bool snakeAteGrapeFood = levelGrid.TrySnakeEatGrapeFood(gridPosition);
            bool snakeAteDurianFood = levelGrid.TrySnakeEatDurianFood(gridPosition);

            if (snakeAteAppleFood)
            {
                //snake body grown;
                snakeBodySize++;

                CreateSnakeBodyPart();
            }

            if (snakeAteGrapeFood)
            {
                //Speed Control Incress;
                speedRate++;
            }

            if (snakeAteDurianFood)
            {
                //Spedd Decress
                if (speedRate > 1)
                {
                    speedRate--;
                }
            }

            bool snakeHitedRock = levelGrid.TrySnakeMoveAround(gridPosition);

            if (snakeHitedRock)
            {
                SnakeState = State.Dead;
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyPart();

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int sankeBodyGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == sankeBodyGridPosition)
                {
                    SnakeState = State.Dead;
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleDirectionFromVector(gridMoveDirectionVector) - 90);


        }

    }

    private void CreateSnakeBodyPart()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    private void UpdateSnakeBodyPart()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleDirectionFromVector(Vector2Int other)
    {

        float n = Mathf.Atan2(other.y, other.x) * Mathf.Rad2Deg;

        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public int GetBodySize()
    {
        return snakeBodySize;
    }

    public int GetSpeedRate()
    {
        return speedRate;
    }

    public void snakeStart(){
        this.isGameStart = true;
    }

    public List<Vector2Int> GetFullSnakeGridPostion()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };

        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;
        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.gameAsset.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);
            float angle;
            switch (snakeMovePosition.GetDirections())
            {
                default:
                case Directions.Up: // Currently going up
                    switch (snakeMovePosition.GetPreviousSnakeMovePosition())
                    {
                        default: angle = 0; break;
                        case Directions.Left: angle = 0 + 45; break;// Previously was going Left
                        case Directions.Right: angle = 0 - 45; break;// Previously was going Right
                    }
                    break;
                case Directions.Down:
                    switch (snakeMovePosition.GetPreviousSnakeMovePosition())
                    {
                        default: angle = 180; break;
                        case Directions.Left: angle = 180 + 45; break;// Previously was going Left
                        case Directions.Right: angle = 180 - 45; break;// Previously was going Right
                    }
                    break;
                case Directions.Left:
                    switch (snakeMovePosition.GetPreviousSnakeMovePosition())
                    {
                        default: angle = -90; break;
                        case Directions.Down: angle = -45; break;// Previously was going Down
                        case Directions.Up: angle = 45; break;// Previously was going Up
                    }
                    break;
                case Directions.Right:
                    switch (snakeMovePosition.GetPreviousSnakeMovePosition())
                    {
                        default: angle = 90; break;
                        case Directions.Down: angle = +45; break;// Previously was going Down
                        case Directions.Up: angle = -45; break;// Previously was going Up
                    }
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }

    private class SnakeMovePosition
    {

        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Directions directions;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Directions directions)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.directions = directions;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Directions GetDirections()
        {
            return directions;
        }

        public Directions GetPreviousSnakeMovePosition()
        {

            if (previousSnakeMovePosition == null)
            {
                return Directions.Right;
            }
            else
            {
                return previousSnakeMovePosition.directions;
            }
        }
    }
}
