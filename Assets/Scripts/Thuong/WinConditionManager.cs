using Unity.VisualScripting;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    public static WinConditionManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public bool CheckWin()
    {
        int monster = 0;
        int villager = 0;

        foreach (PlayerData player in PlayerManger.Instance.players)
        {
            if (!player.isAlive)
                continue;
            if (player.faction == FactionType.Monster)
            {
                monster++;
            }
            else if (player.faction == FactionType.Villager)
            {
                villager++;
            }
        }
        if (monster == 0)
        {
            Debug.Log(" Villager Win ");

            GameManager.Instance.EndGame();

            return true;
        }
        if (monster >= villager)
        {
            Debug.Log(" Monster Win ");

            GameManager.Instance.EndGame();

            return true;
        }
        return false;
    }
}
