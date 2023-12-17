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
    public const long modConst = 1000000007; // max long 9223372036854775807

    /*
     * Complete the 'hackerrankCity' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY A as parameter.
     */
    public static long hackerrankCity(List<int> A)
    {
        City currCity = new City
        {
            N = 1
        };

        foreach (long L in A)
        {
            City nextCity = new City();
            nextCity.N = (4 * currCity.N + 2) % modConst;
            nextCity.D = (2 * currCity.D + 3 * L) % modConst;
            nextCity.C = (4 * currCity.C + 3 * L + 2 * currCity.D + 8 * currCity.N * L + 3 * currCity.N * currCity.D) % modConst;
            nextCity.T = (4 * currCity.T + L + 12 * currCity.N * L + 8 * currCity.C + 12 * (currCity.N * currCity.C % modConst) + 16 * (currCity.N * currCity.N % modConst) * L) % modConst;

            currCity = nextCity;
        }

        return currCity.T;
    }

    public struct City
    {
        public long N;
        public long D;
        public long C;
        public long T;
    }
}


class Solution
{
    public static void Main(string[] args)
    {
        string[] rawInput = File.ReadLines("in.txt").ToArray();
        List<int> A = rawInput[1].Split(' ').Select(x => int.Parse(x)).ToList();

        long result = Result.hackerrankCity(A);

        Console.WriteLine(result);
    }
}