using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkUI : MonoBehaviour
{
    public void StartHost()
    {
        Debug.Log("HOST BUTTON");

        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("Không tìm thấy NetworkManager!");
            return;
        }

        if (NetworkManager.Singleton.IsListening)
        {
            Debug.LogWarning("NetworkManager đã Listening rồi!");
            return;
        }

        bool result = NetworkManager.Singleton.StartHost();

        Debug.Log("StartHost result: " + result);

        if (result)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(
                "Lobby",
                LoadSceneMode.Single
            );
        }
    }

    public void StartClient()
    {
        Debug.Log("CLIENT BUTTON");

        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("Không tìm thấy NetworkManager!");
            return;
        }

        if (NetworkManager.Singleton.IsListening)
        {
            Debug.LogWarning("NetworkManager đã Listening rồi!");
            return;
        }

        bool result = NetworkManager.Singleton.StartClient();

        Debug.Log("StartClient result: " + result);

        if (!result)
        {
            Debug.LogError("StartClient thất bại!");
        }
    }
}