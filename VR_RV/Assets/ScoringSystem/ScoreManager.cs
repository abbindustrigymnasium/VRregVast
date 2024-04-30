using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
  private const int max_player_score = 100;
  public int current_player_score { get; private set; }

  public ObjectDropped objectDropped;

  public static ScoreManager instance;


  [SerializeField] private int time;
  [SerializeField] private int penalty_period;
  [SerializeField] private int time_score_penalty;
  [SerializeField] private int percentage_cleaned;

  // Only one instance of ScoreManager can exist in the scene
  #region Singleton
  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(this);
    }
    else
    {
      Destroy(this.gameObject);
    }
  }
  #endregion

  private void Start()
  {
    current_player_score = max_player_score;

    Set_Time_Penalty(time);
    Set_Desinfection_Penalty(percentage_cleaned);
  }


  private void OnEnable()
  {
    objectDropped.OnItemDropped += (int score_to_remove) =>
    {
      Update_Score(score_to_remove);
    };
  }

  private void OnDisable()
  {
    objectDropped.OnItemDropped -= (int score_to_remove) =>
    {
      Update_Score(score_to_remove);
    };

    if(current_player_score == 100f){
      Debug.Log("You Win!");
    }
    else{
      Debug.Log("You Lose!");
    }
  }

  private void Update_Score(int score_to_remove)
  {
    current_player_score -= score_to_remove;
    Debug.Log("Score: " + current_player_score);
  }


  private void Set_Time_Penalty(int time)
  {
    time -= 600;
    if (time >= 0)
    {
      int penalty_multiplyer = (int)Mathf.Floor(time / penalty_period);
      int time_score_to_remove = time_score_penalty * penalty_multiplyer;

      Update_Score(time_score_to_remove);
    }
  }

  private void Set_Desinfection_Penalty(int percent_clean){
    int score_to_remove = 100 - percent_clean;
    Update_Score(score_to_remove);
  }

  private void Set_Tool_Penalty(int number_of_tools_with_faults){
    Update_Score(number_of_tools_with_faults);
  }
}
