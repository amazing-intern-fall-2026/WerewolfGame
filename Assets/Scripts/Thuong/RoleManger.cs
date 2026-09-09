using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoleManger : MonoBehaviour
{
    public static RoleManger Instance;

    public Dictionary<int, BaseRole> playerRoles =
        new Dictionary<int, BaseRole>();


    private void Awake()
    {
        Instance = this;
    }


    public void AssignRole()
    {
        List<PlayerData> list =
            new List<PlayerData>(PlayerManger.Instance.players);

        Shuffle(list);

        CreateRole(RoleType.DogSprit, list[0]);
        CreateRole(RoleType.Mayor, list[1]);
        CreateRole(RoleType.Seer, list[2]);
        CreateRole(RoleType.VillageGuardian, list[3]);
        CreateRole(RoleType.Idiot, list[4]);

        // Những player còn lại là Villager
        for (int i = 5; i < list.Count; i++)
        {
            CreateRole(RoleType.Villager, list[i]);
        }
    }


    private void CreateRole(RoleType type, PlayerData player)
    {
        BaseRole role = null;

        switch (type)
        {
            case RoleType.Villager:
                role = new VillagerRole(player);
                break;

            case RoleType.Mayor:
                role = new MayorRole(player);
                break;

            case RoleType.Seer:
                role = new SeerRole(player);
                break;

            case RoleType.VillageGuardian:
                role = new GuardianRole(player);
                break;

            case RoleType.DogSprit:
                role = new DogSpirit(player);
                break;

            case RoleType.Idiot:
                role = new IdiotRole(player);
                break;
        }


        if (role == null)
        {
            Debug.LogError("Không tạo được role: " + type);
            return;
        }


        // Gán dữ liệu role cho PlayerData
        player.roleType = type;
        player.faction = role.faction;

        // Lưu instance role theo Player ID
        playerRoles[player.playerID] = role;

        Debug.Log(
            player.playerName +
            " nhận role: " +
            type +
            " | Phe: " +
            role.faction
        );
    }


    private void Shuffle(List<PlayerData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);

            PlayerData temp = list[i];

            list[i] = list[random];
            list[random] = temp;
        }
    }
}