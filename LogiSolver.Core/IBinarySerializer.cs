namespace LogiSolver.Core
{
    public interface IBinarySerializer<T>
    {
        public byte[] Serialize(T item);
        public T Deserialize(byte[] bytes);
    }
}