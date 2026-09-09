using UnityEngine;

public class DeathResolver:MonoBehaviour
{
    public static DeathResolver Instance;

    private void Awake()
    {
        Instance = this;
    }
    public bool TryKillPlayer(int targetID, DeathCause cause)
    { 
        PlayerData target =PlayerManger.Instance.GetplayerByID(targetID);

        if (target == null)
        { 
            return false;
        }
        if (!target.isAlive)
        {
            return false;
        }
        if (target.roleType == RoleType.Idiot && cause == DeathCause.Vote)
        {
            return false;
        }
        if (target.status.isProtected)
        { 
            target.status.isProtected = false;
            return false;
        }
        return true;
    }
}
