using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
    private const int max_player_score = 50;
    public int current_player_score {get; private set;}

    public event Action<int> OnScoreUpdate;

    public static ScoreManager instance;


    #region Singleton
    void Awake(){
      if(instance == null){
        instance = this;
        DontDestroyOnLoad(this);
      }
      else{
        Destroy(this.gameObject);
      }
    }
    #endregion

    void Start()
    {
        current_player_score = max_player_score;
    }

    private void OnEnable(){
      OnScoreUpdate += (int score_to_remove) =>{
        Update_Score(score_to_remove);
      };
    }

    private void OnDisable(){
      OnScoreUpdate -= (int score_to_remove) =>{
        Update_Score(score_to_remove);
      };
    }

    private void Update_Score(int score_to_remove){
        current_player_score -= score_to_remove;
    }
}
