using UnityEngine;

public class NetworkGameStateTest : MonoBehaviour
{
    private void Update()
    {
        if (NetworkGameManager.Instance == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.Lobby
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.RoleReveal
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.Night
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.Morning
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.Discussion
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.Voting
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.Resolve
            );
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            NetworkGameManager.Instance.ChangeGameState(
                GameState.GameOver
            );
        }
    }
}