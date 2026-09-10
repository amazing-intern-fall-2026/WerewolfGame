using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private bool isLeaving = false;

    private void Awake()
    {
        // Chỉ giữ lại một SceneLoader
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // SceneLoader không bị hủy khi chuyển Scene
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(RegisterDisconnectCallback());
    }

    private IEnumerator RegisterDisconnectCallback()
    {
        // Chờ NetworkManager tồn tại
        yield return null;

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

            Debug.Log("SceneLoader đã đăng ký Disconnect Callback.");
        }
    }

    private void OnDisable()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    // =========================================================
    // CLIENT / HOST CHỦ ĐỘNG LEAVE
    // =========================================================

    public void LeaveLobby()
    {
        if (isLeaving)
            return;

        isLeaving = true;

        if (NetworkManager.Singleton == null)
        {
            LoadMainMenu();
            return;
        }

        // HOST
        if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("HOST đang Leave Lobby.");

            NetworkManager.Singleton.Shutdown();

            StartCoroutine(LoadMainMenuAfterShutdown());
        }
        // CLIENT
        else if (NetworkManager.Singleton.IsClient)
        {
            Debug.Log("CLIENT đang Leave Lobby.");

            NetworkManager.Singleton.Shutdown();

            StartCoroutine(LoadMainMenuAfterShutdown());
        }
        else
        {
            LoadMainMenu();
        }
    }

    // =========================================================
    // CLIENT BỊ HOST DISCONNECT
    // =========================================================

    private void OnClientDisconnected(ulong clientId)
    {
        if (NetworkManager.Singleton == null)
            return;

        // Không xử lý lại nếu người chơi đã chủ động Leave
        if (isLeaving)
            return;

        // Kiểm tra có phải chính Client này bị disconnect không
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("CLIENT bị disconnect khỏi Host.");

            isLeaving = true;

            StartCoroutine(LoadMainMenuAfterDisconnect());
        }
    }

    // =========================================================
    // LOAD MAIN MENU SAU KHI SHUTDOWN
    // =========================================================

    private IEnumerator LoadMainMenuAfterShutdown()
    {
        // Chờ NetworkManager xử lý Shutdown
        yield return null;

        yield return new WaitForEndOfFrame();

        LoadMainMenu();
    }

    // =========================================================
    // LOAD MAIN MENU SAU KHI BỊ HOST DISCONNECT
    // =========================================================

    private IEnumerator LoadMainMenuAfterDisconnect()
    {
        Debug.Log("Đang chuyển Client về MainMenu...");

        yield return null;

        yield return new WaitForEndOfFrame();

        LoadMainMenu();
    }

    // =========================================================
    // LOAD MAIN MENU
    // =========================================================

    private void LoadMainMenu()
    {
        Debug.Log("→ LOAD MAIN MENU");

        SceneManager.LoadScene("MainMenu");
    }
}