using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    private static GameHandler instrance;
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;

    private void Awake() {
        instrance = this;
    }
    private void Start() {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20,20);
        snake.SetUpLevelGrid(levelGrid);
        levelGrid.SetUpSnakeRef(snake);
    }

}
