using UnityEngine;

public interface IGiveReward : IGivePoints
{
    int ChanceToDropLoot { get; }
    GameObject[] DroppableItems { get; } 
    bool ShouldDropLoot();
}