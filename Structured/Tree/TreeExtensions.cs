using System;
using System.Collections.Generic;
using System.Linq;

namespace Structured
{
    public static class TreeExtensions
    {
        public static void TraverseTree<T>(this Leaf<T> leaf, Action<Leaf<T>> action) where T : class
        {
            leaf.Children.ToList().ForEach(x =>
            {
                action(x);
                TraverseTree(x, action);
            });
        }

        public static IEnumerable<Leaf<T>> FindLeaves<T>(this Leaf<T> leaf, Func<Leaf<T>, bool> whereExpression) where T : class
        {
            var flattenedLeaves = new List<Leaf<T>>();
            leaf.TraverseTree(x =>
            {
                flattenedLeaves.Add(x);
            });

            return flattenedLeaves.Where(whereExpression);
        }

        public static IEnumerable<Leaf<T>> FlattenTree<T>(this Tree<T> tree) where T : class
        {
            var leaves = new List<Leaf<T>> { tree.Root };
            tree.Root.TraverseTree(x =>
            {
                leaves.Add(x);
            });

            return leaves;
        }

        public static Leaf<T> FindRoot<T>(this Leaf<T> leaf) where T : class
        {
            if (leaf.Parent == null)
            {
                return leaf;
            }

            return FindRoot(leaf.Parent);
        }
    }
}
