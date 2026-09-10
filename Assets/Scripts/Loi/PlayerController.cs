using Unity.Netcode;
using UnityEngine;

public enum PlayerState
{
    Alive,
    Dead,
    Sleeping,
    Spectating
}

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    // =========================
    // PLAYER STATE
    // =========================

    public NetworkVariable<PlayerState> State =
        new NetworkVariable<PlayerState>(
            PlayerState.Alive,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server
        );

    // =========================
    // UNITY
    // =========================

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        State.OnValueChanged += OnStateChanged;

        Debug.Log(
            "Player " +
            OwnerClientId +
            " Spawned | State = " +
            State.Value
        );
    }

    private void OnDestroy()
    {
        State.OnValueChanged -= OnStateChanged;
    }

    // =========================
    // UPDATE
    // =========================

    private void Update()
    {
        if (!IsOwner)
            return;

        // TEST:
        // Nhấn K để yêu cầu Server đổi trạng thái
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleStateServerRpc();
        }

        // Chỉ Alive mới được di chuyển
        if (State.Value != PlayerState.Alive)
            return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }

    // =========================
    // MOVEMENT
    // =========================

    private void FixedUpdate()
    {
        if (!IsOwner)
            return;

        if (State.Value != PlayerState.Alive)
            return;

        rb.MovePosition(
            rb.position +
            movement * moveSpeed * Time.fixedDeltaTime
        );
    }

    // =========================
    // CLIENT → SERVER
    // =========================

    [ServerRpc]
    private void ToggleStateServerRpc()
    {
        Debug.Log(
            "SERVER: Player " +
            OwnerClientId +
            " yêu cầu đổi State"
        );

        if (State.Value == PlayerState.Alive)
        {
            State.Value = PlayerState.Dead;
        }
        else
        {
            State.Value = PlayerState.Alive;
        }
    }

    // =========================
    // STATE CHANGED
    // =========================

    private void OnStateChanged(
        PlayerState oldState,
        PlayerState newState)
    {
        Debug.Log(
            "Player " +
            OwnerClientId +
            " | State: " +
            oldState +
            " → " +
            newState
        );
    }
}