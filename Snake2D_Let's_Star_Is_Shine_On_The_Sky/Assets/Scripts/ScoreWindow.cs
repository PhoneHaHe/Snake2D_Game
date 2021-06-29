using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    public Text appleText;
    public Text speedText;

    [SerializeField] private Snake snake;

    private void Awake() {
        appleText = transform.Find("FoodText").GetComponent<Text>();
        speedText = transform.Find("SpeedText").GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        appleText.text = snake.GetBodySize().ToString();
        speedText.text = snake.GetSpeedRate().ToString();
    }
}
