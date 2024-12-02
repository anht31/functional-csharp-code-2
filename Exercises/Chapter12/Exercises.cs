using NUnit.Framework;
using System;

using LaYumba.Functional;
using LaYumba.Functional.Data.LinkedList;
using static LaYumba.Functional.Data.LinkedList.LinkedList;

using LaYumba.Functional.Data.BinaryTree;
using static LaYumba.Functional.Data.BinaryTree.Tree;
using static System.Console;
using Generic = System.Collections.Generic;
using System.Collections;
using System.Reflection.Emit;
using System.Linq;
using System.Collections.Immutable;
//using LaYumba.Functional.Data.Bst;
//using System.Collections.Generic;

namespace Exercises.Chapter12;

static class Exercises
{
    //public static void Run()
    //{
    //    var cd = List(3, 4);
    //    var bc = List(2, cd);
    //    var ab = List(1, bc);

    //    WriteLine(ab);
    //    WriteLine();
    //    WriteLine(ab.DropWhile(x => x <= 3));

    //    ReadLine();
    //}

    // LISTS
    // List<T> = Empty | Cons(T, List<T>)

    // Implement functions to work with the singly linked List defined in this chapter:
    // Tip: start by writing the function signature in arrow-notation

    // InsertAt inserts an item at the given index
    // (List<T>, T, int) -> List<T>
    public static List<T> InsertAt<T>(this List<T> @this, T value, int index = 0)
    {
        WriteLine($"{index} -> {@this}");

        return @this.Match
            (
                () => index == 0
                    ? List(value) // Cons { Head = value, Tail = Empty { } }
                    : throw new IndexOutOfRangeException(),
                (head, tail) => index == 0
                    ? List(value, @this) // Cons {value, tail}
                    : List(head, tail.InsertAt(value, --index)) // head + Cons {value, tail}
            );
    }

    public static List<T> InsertAt2<T>(this List<T> @this, T value, int index = 0)
        => index == 0
            ? List(value, @this) // Cons {value, remaining}
            : @this.Match(
                () => throw new IndexOutOfRangeException(),
                (head, tail) => List(head, tail.InsertAt2(value, --index))
            );

    // RemoveAt removes the item at the given index
    // (List<T>, int) -> List<T>
    public static List<T> RemoveAt<T>(this List<T> @this, int index = 0) 
        => index == 0
            ? @this.Match( // Remove
                () => List<T>(), // Case RemoveAt last item (item Empty)
                (head, tail) => tail
            )
            : @this.Match(
                () => throw new IndexOutOfRangeException(),
                (head, tail) => List(head, tail.RemoveAt(--index))
            );

    // TakeWhile takes a predicate, and traverses the list yielding all items until it find one that fails the predicate
    // (List<T>, Func<T, bool>) -> List<T>
    public static List<T> TakeWhile<T>(this List<T> @this, Func<T, bool> predicate) 
        => @this.Match(
            () => List<T>(), // End of LinkList, return Empty -> @this == Empty
            (head, tail) => predicate(head)
                ? List(head, tail.TakeWhile(predicate))
                : List<T>() // Break by predicate, return Empty
        );

    // DropWhile works similarly, but excludes all items at the front of the list
    //(List<T>, Func<T, bool>) -> List<T>
    public static List<T> DropWhile<T>(this List<T> @this, Func<T, bool> predicate)
    {
        return @this.Match(
            () => List<T>(),
            (head, tail) => predicate(head)
                ? tail.DropWhile(predicate) // continue drop Head
                : @this // In the end, take last remaining
        );
    }

    // complexity:
    // InsertAt: 
    // RemoveAt: 
    // TakeWhile: 
    // DropWhile: 

    // number of new objects required: 
    // InsertAt: 
    // RemoveAt: 
    // TakeWhile: 
    // DropWhile: 

    // TakeWhile and DropWhile are useful when working with a list that is sorted 
    // and you’d like to get all items greater/smaller than some value; write implementations 
    // that take an IEnumerable rather than a List





    // TREES
    // Tree<T> = Leaf(T)

    //public static void Run()
    //{
    //    var tree = 
    //        Branch(
    //            Branch(
    //                Branch(
    //                    Leaf(1),
    //                    Leaf(2)
    //                )
    //                , 
    //                Leaf(3)
    //            ),
    //            Leaf(4)
    //        );

    //    Func<int, Tree<int>> f = (int i) => Leaf(i * 2);
        
    //    WriteLine(tree);
    //    WriteLine(tree.Bind(f));

    //    ReadLine();
    //}


    // Is it possible to define `Bind` for the binary tree implementation shown in this
    // chapter? If so, implement `Bind`, else explain why it’s not possible (hint: start by writing
    // the signature; then sketch binary tree and how you could apply a tree-returning funciton to
    // each value in the tree).
    // Bind: (Tree<T>, Func<T, Tree<R>>) -> Tree<T>
    public static Tree<R> Bind<T, R>(this Tree<T> @this, Func<T, Tree<R>> f)
        => @this.Match(
            Leaf: f,
            Branch: (left, right) => Branch(left.Bind(f), right.Bind(f))
        );


    //public static void Run()
    //{
    //    var tree =
    //        Branch(
    //            Branch(
    //                Branch(
    //                    Leaf(1),
    //                    Leaf(2)
    //                )
    //                ,
    //                Leaf(3)
    //            ),
    //            Leaf(4)
    //        );

    //    Func<int, Tree<int>> f = (int i) => Leaf(i * 2);

    //    WriteLine(tree);
    //    WriteLine(tree.Bind(f));

    //    ReadLine();
    //}


    // Implement a LabelTree type, where each node has a label of type string and a list of subtrees; 
    // this could be used to model a typical navigation tree or a cateory tree in a website
    public record LabelTree(string Label, ImmutableList<LabelTree> SubTrees) : IEquatable<LabelTree>
    {
        public override string ToString() => $"[{Label}: {string.Join(",", SubTrees.Select(x => x.ToString()))}]";

        bool IEquatable<LabelTree>.Equals(LabelTree other)
            => other is not null && this.ToString() == other.ToString();
    }

    //public class LabelTree //(string Label, ImmutableList<LabelTree> SubTrees)
    //{
    //    public string Label { get; set; }
    //    public ImmutableList<LabelTree> SubTrees { get; set; }

    //    public LabelTree(string Label, ImmutableList<LabelTree> SubTrees)
    //    {
    //        this.Label = Label;
    //        this.SubTrees = SubTrees;
    //    }

    //    public override string ToString() => $"{Label}: {SubTrees}";

    //    //public override string ToString() => $"[{Label}: {string.Join(",", SubTrees.Select(x => x.ToString()))}]";

    //    public bool Equals(object other) => other is not null && this.ToString() == other.ToString();
    //}

    // Imagine you need to add localization to your navigation tree: you're given a `LabelTree` where
    // the value of each label is a key, and a dictionary that maps keys
    // to translations in one of the languages that your site must support
    // (hint: define `Map` for `LabelTree` and use it to obtain the localized navigation/category tree)
    public static LabelTree Map(this LabelTree tree, Func<string, string> f)
            => new LabelTree(
                f(tree.Label),
                tree.SubTrees.Map(x => x.Map(f)).ToImmutableList()
            );


    // Unit test the preceding implementation.
    public static void Run()
    {
        var tree = CreateLabelTree();
        WriteLine(tree);

        ReadLine();
    }

    [Test]
    public static void MapLabelTree_WhenGivenTreeInput_WillBeReturnTrueBindingOutput()
    {
        // Arrange
        var tree = CreateLabelTree();
        var expect = Tree(
            "root",
            ListTree(
                Tree("trang chu"),
                Tree("gioi thieu"),
                Tree("san pham", ListTree(
                    Tree("hang dien tu"),
                    Tree("quan ao")
                ))
            )
        );

        var dic = new Generic.Dictionary<string, string>
        {
            { "root", "root" },
            { "home", "trang chu" },
            { "about", "gioi thieu" },
            { "products", "san pham" },
            { "electronics", "hang dien tu" },
            { "clothing", "quan ao" },
        };

        // Act
        var result = tree.Map(x => dic[x]);

        // Assert
        Assert.AreEqual(result, expect);
    }


    // Setup
    public static LabelTree Tree(string label, ImmutableList<LabelTree> items = null)
        => new LabelTree(label, items ?? ImmutableList<LabelTree>.Empty);

    public static ImmutableList<LabelTree> ListTree(params LabelTree[] items)
        => ImmutableList.CreateRange(items);


    public static LabelTree CreateLabelTree()
        => Tree(
            "root",
            ListTree(
                Tree("home"),
                Tree("about"),
                Tree("products", ListTree(
                    Tree("electronics"),
                    Tree("clothing")
                ))
            )
        );


}