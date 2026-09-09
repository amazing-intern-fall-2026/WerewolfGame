using Unity.VisualScripting;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    public static NightManager Instance;
    int montserTarget = -1;
    int protectedTarget=-1;

    private void Awake()
    {
        Instance = this;
    }
    public void StartNight()
    {
        montserTarget = -1;
        protectedTarget = -1;
    }
    public void SetMonsterTarget(int id)
    { 
        montserTarget = id;
    }
    public void SetProtectedTarget(int id)
    { 
        protectedTarget = id;
    }
    public void ResolveNight()
    {
        if (protectedTarget != -1)
        { 
            PlayerData target =PlayerManger.Instance.GetplayerByID(protectedTarget);
            target.status.isProtected = true;
        }
        if (montserTarget != -1)
        {
            DeathResolver.Instance.TryKillPlayer(montserTarget,DeathCause.Monster);
        }
    }
}
