namespace Resources
{
    public interface IMemoizeFunctions<in TKey, out TValue>
    {
        TValue GetOrInvoke(TKey key);
    }
}