using System.Collections;
using UnityEngine;

public class PhaseManger : MonoBehaviour
{
    public static PhaseManger Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartRoleReveal()
    {
        StartCoroutine(RoleRevealRoutine());
    }

    private IEnumerator RoleRevealRoutine()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPhase(GamePhase.RoleReveal);
        }

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
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPhase(GamePhase.DayStart);
            Debug.Log("DAY " + GameManager.Instance.currentDay);
        }

        yield return new WaitForSeconds(2);
        StartDiscussion();
    }

    private void StartDiscussion()
    {
        StartCoroutine(DiscussionRoutine());
    }

    private IEnumerator DiscussionRoutine()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPhase(GamePhase.Discussion);
        }

        Debug.Log("DISCUSSION START");
        yield return new WaitForSeconds(60);

        StartVoting();
    }

    private void StartVoting()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPhase(GamePhase.Voting);
        }

        Debug.Log("VOTING START");
    }
}