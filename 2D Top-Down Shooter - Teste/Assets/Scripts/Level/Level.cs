using UnityEngine;

public class Level : Singleton<Level>
{
    [SerializeField] Transform playableArea;

    Vector2 LevelRadius { get => playableArea.localScale / 2; }

    protected override void Awake()
    {
        base.Awake();

        if (playableArea == null)
            Debug.LogError("Please assign a playable area.");
    }

    public Vector2 GetRandomPointInMap()
    {
        float x = Random.Range(-LevelRadius.x, LevelRadius.x);
        float y = Random.Range(-LevelRadius.y, LevelRadius.y);

        return new Vector2(x, y);
    }

    public bool CheckIfPointIsInsideMap(Vector2 point)
    {
        return (point.x > -LevelRadius.x && point.x < LevelRadius.x &&
                point.y > -LevelRadius.y && point.y < LevelRadius.y);
    }
}