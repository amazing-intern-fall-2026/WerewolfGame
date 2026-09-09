using TMPro;
using Unity.Netcode;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerListText;

    private NetworkList<ulong> playerIds;

    private void Awake()
    {
        playerIds = new NetworkList<ulong>();
    }

    public override void OnNetworkSpawn()
    {
        playerIds.OnListChanged += OnPlayerListChanged;

        if (IsServer)
        {
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;

            // Thêm Host
            AddPlayer(NetworkManager.LocalClientId);

            // Kiểm tra những Client đã kết nối
            foreach (ulong clientId in NetworkManager.ConnectedClientsIds)
            {
                AddPlayer(clientId);
            }
        }

        UpdatePlayerList();
    }

    public override void OnNetworkDespawn()
    {
        playerIds.OnListChanged -= OnPlayerListChanged;

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (!IsServer)
            return;

        AddPlayer(clientId);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (!IsServer)
            return;

        RemovePlayer(clientId);
    }

    private void AddPlayer(ulong clientId)
    {
        if (!playerIds.Contains(clientId))
        {
            playerIds.Add(clientId);

            Debug.Log(
                "Lobby: Player " +
                clientId +
                " đã vào Lobby"
            );
        }
    }

    private void RemovePlayer(ulong clientId)
    {
        if (playerIds.Contains(clientId))
        {
            playerIds.Remove(clientId);

            Debug.Log(
                "Lobby: Player " +
                clientId +
                " đã rời Lobby"
            );
        }
    }

    private void OnPlayerListChanged(
        NetworkListEvent<ulong> changeEvent)
    {
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        if (playerListText == null)
            return;

        string text = "Players:\n\n";

        for (int i = 0; i < playerIds.Count; i++)
        {
            text +=
                "Player " +
                (i + 1) +
                "\n";
        }

        playerListText.text = text;
    }
}