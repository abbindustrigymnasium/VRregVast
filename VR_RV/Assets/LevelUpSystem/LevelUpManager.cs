using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    private enum Level{
      Easy,
      Medium,
      Hard
    }

    [SerializeField]private Level current_level;

    // Placeholder
    public bool highlight_items;
    public bool subtitles;
    public bool item_names;
    public bool strict_time;
    public bool doctor_help;
    public bool strict_punishment;

#region Singleton
    public static LevelUpManager instance;

    void Awake(){
      if(instance == null){
        instance = this;
        DontDestroyOnLoad(this);
      }
      else {
        Destroy(this.gameObject);
      }
    }

#endregion

    void OnEnable()
    {
      Set_Level_Restrictions(current_level);
    }

    // Shit code i know, only used as placeholder for now
    private void Set_Level_Restrictions(Level current_user_level){
      switch(current_user_level){
        case Level.Easy:
          highlight_items = true;
          subtitles = true;
          item_names = true;
          strict_time = false;
          doctor_help = true;
          strict_punishment = false;
          break;
        case Level.Medium:
          highlight_items = false;
          subtitles = false;
          item_names = false;
          doctor_help = true;
          strict_time = false;
          strict_punishment = false;
          break;
        case Level.Hard:
          highlight_items = false;
          subtitles = false;
          item_names = false;
          doctor_help = false;
          strict_time = true;
          strict_punishment = true;
          break;
      }
    }
}
