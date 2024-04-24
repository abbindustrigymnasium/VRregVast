using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
  private const int max_player_score = 100;
  public int current_player_score { get; private set; }

  public event Action<int> OnScoreUpdate;
  public ObjectDropped objectDropped;

  public static ScoreManager instance;


  [SerializeField] private int time;
  [SerializeField] private int penalty_period;
  [SerializeField] private int time_score_penalty;


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
  void Start()
  {
    current_player_score = max_player_score;

    Check_Time_Penalty();
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
  }

  private void Update_Score(int score_to_remove)
  {
    current_player_score -= score_to_remove;
    Debug.Log("Score: " + current_player_score);
  }


  private void Check_Time_Penalty()
  {
    time -= 600;
    if (time >= 0)
    {
      int penalty_multiplyer = (int)Mathf.Floor(time / penalty_period);
      int time_score_to_remove = time_score_penalty * penalty_multiplyer;

      Update_Score(time_score_to_remove);
    }
  }
}