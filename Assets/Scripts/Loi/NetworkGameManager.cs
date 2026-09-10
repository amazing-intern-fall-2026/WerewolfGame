using Unity.Netcode;
using UnityEngine;

public class NetworkGameManager : NetworkBehaviour
{
    public static NetworkGameManager Instance;

    [Header("Current Game State")]
    public NetworkVariable<GameState> CurrentState =
        new NetworkVariable<GameState>(
            GameState.Lobby,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server
        );

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        CurrentState.OnValueChanged += OnGameStateChanged;

        Debug.Log(
            "NetworkGameManager Spawned | Current State = "
            + CurrentState.Value
        );
    }

    public override void OnNetworkDespawn()
    {
        CurrentState.OnValueChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(
        GameState oldState,
        GameState newState)
    {
        Debug.Log(
            "GAME STATE: "
            + oldState
            + " → "
            + newState
        );
    }

    // Chỉ Server được quyền đổi GameState
    public void ChangeGameState(GameState newState)
    {
        if (!IsServer)
        {
            Debug.LogWarning(
                "Chỉ Server mới được phép đổi GameState!"
            );

            return;
        }

        Debug.Log(
            "SERVER: "
            + CurrentState.Value
            + " → "
            + newState
        );

        CurrentState.Value = newState;
    }
}