public interface IPooledObject
{
    PoolingSystem Pool { get; set; }
    int AmountToPool { get; }
}
