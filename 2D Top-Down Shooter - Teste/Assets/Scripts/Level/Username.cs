using UnityEngine;

public class Username : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo.playerName = "";
    }

    public void SetPlayerUsername(string newName) => playerInfo.playerName = newName;
}