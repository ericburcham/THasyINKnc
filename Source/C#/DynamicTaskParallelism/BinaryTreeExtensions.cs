using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicTaskParallelism
{
    public static class BinaryTreeExtensions
    {
        public static void ParallelWalk<T>(this BinaryTree<T> tree, Action<T> action)
        {
            if (tree == null)
            {
                return;
            }

            var doActionTask = Task.Factory.StartNew(() => action(tree.Data));
            var walkLeftTask = Task.Factory.StartNew(() => tree.Left.ParallelWalk(action));
            var walkRightTask = Task.Factory.StartNew(() => tree.Right.ParallelWalk(action));

            Task.WaitAll(doActionTask, walkLeftTask, walkRightTask);
        }

        public static void ParallelWalkWhileNotEmpty<T>(this BinaryTree<T> tree, Action<T> action)
        {
            if (tree == null)
            {
                return;
            }

            var initialValues = new[] { tree };
            Action<BinaryTree<T>, Action<BinaryTree<T>>> producterAction = (item, producer) =>
                {
                    if (item.Left != null)
                    {
                        producer(item.Left);
                    }

                    if (item.Right != null)
                    {
                        producer(item.Right);
                    }

                    action(item.Data);
                };
            ParallelWhileNotEmpty(initialValues, producterAction);
        }

        public static void ParallelWalkWithParentChildTasks<T>(this BinaryTree<T> tree, Action<T> action)
        {
            if (tree == null)
            {
                return;
            }

            var attachedToParent = TaskCreationOptions.AttachedToParent;

            Action doAction = () => action(tree.Data);
            var doActionTask = Task.Factory.StartNew(doAction, attachedToParent);

            Action walkLeftAction = () => ParallelWalkWithParentChildTasks(tree.Left, action);
            var walkLeftTask = Task.Factory.StartNew(walkLeftAction, attachedToParent);

            Action walkRightAction = () => ParallelWalkWithParentChildTasks(tree.Right, action);
            var walkRightTask = Task.Factory.StartNew(walkRightAction, attachedToParent);

            Task.WaitAll(doActionTask, walkLeftTask, walkRightTask);
        }

        public static void Walk<T>(this BinaryTree<T> tree, Action<T> action)
        {
            if (tree == null)
            {
                return;
            }

            action(tree.Data);
            tree.Left.Walk(action);
            tree.Right.Walk(action);
        }

        private static void ParallelWhileNotEmpty<T>(IEnumerable<T> initialValues, Action<T, Action<T>> producerAction)
        {
            var from = new ConcurrentQueue<T>(initialValues);

            while (!from.IsEmpty)
            {
                var to = new ConcurrentQueue<T>();
                Action<T> enqueue = to.Enqueue;
                Parallel.ForEach(from, v => producerAction(v, enqueue));
                from = to;
            }
        }
    }
}