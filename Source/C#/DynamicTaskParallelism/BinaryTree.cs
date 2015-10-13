namespace DynamicTaskParallelism
{
    public class BinaryTree<T>
    {
        public BinaryTree(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public BinaryTree<T> Left { get; set; }

        public BinaryTree<T> Right { get; set; }
    }
}