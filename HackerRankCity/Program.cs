using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{
    public const long modConst = 1000000007;

    /*
     * Complete the 'hackerrankCity' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY A as parameter.
     */

    public static long hackerrankCity(List<int> A)
    {
        CondensedTree city = new CondensedTree(0, 0, 1);
        long c2c = 0;
        foreach (int d in A)
        {

            CondensedTree quadrant = CombineTree(city, new CondensedTree(d, d, 2), true, d);
            CondensedTree half = CombineTree(quadrant, quadrant, false);
            CondensedTree extendedHalf = CombineTree(half, new CondensedTree(d, d, 2), true, d);

            long cornerDescendantDist =
                (city.descendantDist +
                c2c + d +
                c2c + 2 * d +
                ((c2c + 2 * d) * city.numNodes) % modConst + city.descendantDist +
                (((c2c + 3 * d) * city.numNodes) % modConst + city.descendantDist) * 2) % modConst;

            //Console.WriteLine($"corner: {cornerDescendantDist}");


            city = CombineTree(half, extendedHalf, false);
            city.descendantDist = cornerDescendantDist;
            c2c = (c2c * 2 + 3 * d) % modConst;
            //Console.WriteLine($"total dist = {city.totalDist}");
        }


        return city.totalDist;

    }

    public static CondensedTree CombineTree(CondensedTree t1, CondensedTree t2, bool extend, long d = 0)
    {
        long totalDist = (t1.descendantDist * t2.numNodes + t2.descendantDist * t1.numNodes + (t1.totalDist - t1.descendantDist) + (t2.totalDist - t2.descendantDist)) % modConst;
        long descendantDist = (extend ? (t1.descendantDist + t1.numNodes * d) : (t1.descendantDist + t2.descendantDist)) % modConst;
        long numNodes = (t1.numNodes + t2.numNodes - 1) % modConst;

        return new CondensedTree(totalDist, descendantDist, numNodes);
    }
}

struct CondensedTree
{
    public long totalDist;
    public long descendantDist;
    public long numNodes;

    public CondensedTree(long a, long b, long c)
    {
        totalDist = a;
        descendantDist = b;
        numNodes = c;
    }

}




class Solution
{
    public static void Main(string[] args)
    {
        //int ACount = Convert.ToInt32(Console.ReadLine().Trim());
        List<int> A = new List<int> { 1, 1 };

        //List<int> A = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList();

        long result = Result.hackerrankCity(A);

        Console.WriteLine(result);
    }
}