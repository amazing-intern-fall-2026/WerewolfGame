using Unity.Netcode;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    public void StartHost()
    {
        Debug.Log("=== BUTTON HOST ===");
        Debug.Log("IsListening BEFORE: " + NetworkManager.Singleton.IsListening);

        if (NetworkManager.Singleton.IsListening)
        {
            Debug.LogWarning("NetworkManager đã Listening rồi!");
            return;
        }

        bool result = NetworkManager.Singleton.StartHost();

        Debug.Log("StartHost result: " + result);
    }

    public void StartClient()
    {
        Debug.Log("=== BUTTON CLIENT ===");
        Debug.Log("IsListening BEFORE: " + NetworkManager.Singleton.IsListening);

        if (NetworkManager.Singleton.IsListening)
        {
            Debug.LogWarning("NetworkManager đã Listening TRƯỚC KHI BẤM JOIN!");
            return;
        }

        bool result = NetworkManager.Singleton.StartClient();

        Debug.Log("StartClient result: " + result);
    }
}