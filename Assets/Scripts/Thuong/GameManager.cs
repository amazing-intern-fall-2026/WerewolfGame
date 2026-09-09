using UnityEngine;



//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public GamePhase currentPhase;

//    public int currentDay = 1;

//    public bool gameEnded;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Start()
//    {
//        StartGame();
//    }

//    public void StartGame()
//    {
//        gameEnded = false;
//        currentDay = 1;

//        PlayerManger.Instance.CreateTestPlayer(8);

//        RoleManger.Instance.AssignRole();

//        PhaseManger.Instance.StartRoleReveal();
//    }

//    public void SetPhase(GamePhase phase)
//    {
//        currentPhase = phase;

//        Debug.Log("PHASE: " + currentPhase);
//    }

//    public void EndGame()
//    {
//        gameEnded = true;

//        SetPhase(GamePhase.GameOver);
//    }
//}


// --------------------test---------------------------

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GamePhase currentPhase;

    public int currentDay = 1;

    public bool gameEnded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameEnded = false;
        currentDay = 1;

        PlayerManger.Instance.CreateTestPlayer(8);

        RoleManger.Instance.AssignRole();

        PhaseManager.Instance.StartRoleReveal();
    }

    public void SetPhase(GamePhase phase)
    {
        currentPhase = phase;

        Debug.Log("PHASE: " + currentPhase);
    }

    public void EndGame()
    {
        gameEnded = true;

        SetPhase(GamePhase.GameOver);
    }
}