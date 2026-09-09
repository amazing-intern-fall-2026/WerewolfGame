using System.Collections.Generic;
using UnityEngine;

public class VoteManger: MonoBehaviour
{
    public static VoteManger Instance;

    Dictionary<int,int>vote = new Dictionary<int,int>();

    private void Awake()
    {
        Instance = this;
    }
    public void StartVote()
    { 
        vote.Clear();
    }
    public void Vote(int voterID,int targetID)
    { 
        PlayerData voter= PlayerManger.Instance.GetplayerByID(voterID);
        if (!voter.isAlive)
        {
            return;
        }
        if (!vote.ContainsKey(targetID))
        {
            vote[targetID] = 0;
        }
        vote[targetID] += voter.votPower;
    }
}
