using UnityEngine;

public abstract class PlayerState
{
    public Player Player { get; }
    public Rigidbody2D Rigidbody { get; }

    public PlayerState(Player player)
    {
        Player = player;
        Rigidbody = player.GetComponent<Rigidbody2D>();
    }
}