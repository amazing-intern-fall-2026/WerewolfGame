using System.Collections;
using UnityEngine;

//public class PhaseManger : MonoBehaviour
//{
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    public static PhaseManger Instance;
//    private void Awake()
//    {
//        Instance = this;
//    }
//    public void StartRoleReveal()
//    {
//        StartCoroutine(RoleRevealRoutine());
//    }
//    IEnumerator RoleRevealRoutine()
//    {
//        GameManager.Instance.SetPhase(GamePhase.RoleReveal);
//        yield return new WaitForSeconds(5);
//        StartDay();
//    }
//    private void StartDay()
//    {
//        StartCoroutine(DayRountine());
//    }
//    IEnumerator DayRountine()
//    {
//        GameManager.Instance.SetPhase(GamePhase.DayStart);
//        Debug.Log("DAY"+GameManager.Instance.currentDay);
//        yield return new WaitForSeconds(2);
//        StartEvent();
//    }
//    void StartEvent()
//    {
//        GameManager.Instance.SetPhase(GamePhase.Event);
//        EventManagert.Instance.TryStartEvent();
//        StartDiscussion();
//    }
//    private void StartDiscussion()
//    {
//        StartCoroutine(DisscusionRoutine());
//    }
//     IEnumerator DisscusionRoutine()
//        {
//            GameManager.Instance.SetPhase(GamePhase.Discussion);
//            yield return new WaitForSeconds(60);
//        StartVoting();
//        }
//    private void StartVoting()
//    {
//        GameManager.Instance.SetPhase(GamePhase.Voting);
//        Debug.Log("Voting Start ");
//    }

//}

// tesst
public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartRoleReveal()
    {
        StartCoroutine(RoleRevealRoutine());
    }

    private IEnumerator RoleRevealRoutine()
    {
        GameManager.Instance.SetPhase(GamePhase.RoleReveal);

        Debug.Log("ROLE REVEAL");

        yield return new WaitForSeconds(5);

        StartDay();
    }

    private void StartDay()
    {
        StartCoroutine(DayRoutine());
    }

    private IEnumerator DayRoutine()
    {
        GameManager.Instance.SetPhase(GamePhase.DayStart);

        Debug.Log(
            "DAY " + GameManager.Instance.currentDay
        );

        yield return new WaitForSeconds(2);

        StartDiscussion();
    }

    private void StartDiscussion()
    {
        StartCoroutine(DiscussionRoutine());
    }

    private IEnumerator DiscussionRoutine()
    {
        GameManager.Instance.SetPhase(GamePhase.Discussion);

        Debug.Log("DISCUSSION START");

        yield return new WaitForSeconds(60);

        StartVoting();
    }

    private void StartVoting()
    {
        GameManager.Instance.SetPhase(GamePhase.Voting);

        Debug.Log("VOTING START");
    }
}
