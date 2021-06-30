using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    private static GameHandler instrance;
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;

    [SerializeField] private Transform _gameMode;
    [SerializeField] private Transform _SelectView;

    private void Awake() {
        instrance = this;
    }
    private void Start() {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20,20);
        snake.SetUpLevelGrid(levelGrid);
        levelGrid.SetUpSnakeRef(snake);
    }


    public void SelectGameMode(int _number_GameMode){

        if(_gameMode != null){
            _gameMode.transform.GetChild(_number_GameMode).gameObject.SetActive(true);
            levelGrid.SetUpRockList(_gameMode.transform.GetChild(_number_GameMode));
            _SelectView.gameObject.SetActive(false);
            snake.snakeStart();
        }
        
    }

}
