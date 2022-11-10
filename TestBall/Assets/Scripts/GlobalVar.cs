using UnityEngine;

public class GlobalVar : MonoBehaviour
{
   public static int LevelDifficulty;
   public static int GameRoundCount;

   private void Awake()
   {
        LevelDifficulty = 1;
        GameRoundCount = PlayerPrefs.GetInt("GameRound", 0);
   }
}
