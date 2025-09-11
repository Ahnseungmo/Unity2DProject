using UnityEngine;

public class Player : Character
{
    public PlayerInventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        GameData.Instance.LoadPlayerState(this);
    }

    private void OnDestroy()
    {
        if (GameData.Instance != null)
            GameData.Instance.SavePlayerState(this);
    }
}