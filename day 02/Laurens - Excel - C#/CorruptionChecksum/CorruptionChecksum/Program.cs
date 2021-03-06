﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorruptionChecksum {
    class Program {
        static void Main(string[] args)
        {
            Day2.PrintExercise();
        }
    }

    public class Day2
    {
        public static void PrintExercise()
        {
            var example = "5\t9\t2\t8\r\n9\t4\t7\t3\r\n3\t8\t6\t5";
            Console.Out.WriteLine(new Day2().Checksum_Pt2(example)); // should be 9

            var input = "515\t912\t619\t2043\t96\t93\t2242\t1385\t2110\t860\t2255\t621\t1480\t118\t1230\t99\r\n161\t6142\t142\t1742\t237\t6969\t211\t4314\t5410\t4413\t3216\t6330\t261\t3929\t5552\t109\r\n1956\t4470\t3577\t619\t105\t3996\t128\t1666\t720\t4052\t108\t132\t2652\t306\t1892\t1869\r\n2163\t99\t2257\t895\t112\t1771\t1366\t1631\t2064\t2146\t103\t865\t123\t1907\t2362\t876\r\n1955\t3260\t1539\t764\t185\t5493\t5365\t5483\t4973\t175\t207\t1538\t4824\t205\t1784\t2503\r\n181\t3328\t2274\t3798\t1289\t2772\t4037\t851\t1722\t3792\t175\t603\t725\t158\t2937\t174\r\n405\t247\t2083\t956\t725\t258\t2044\t206\t2054\t561\t2223\t2003\t2500\t355\t306\t2248\r\n837\t937\t225\t1115\t446\t451\t160\t1219\t56\t61\t62\t922\t58\t1228\t1217\t1302\r\n1371\t1062\t2267\t111\t135\t2113\t1503\t2130\t1995\t2191\t129\t2494\t2220\t739\t138\t1907\r\n3892\t148\t2944\t371\t135\t1525\t3201\t3506\t3930\t3207\t115\t3700\t2791\t597\t3314\t132\r\n259\t162\t186\t281\t210\t180\t184\t93\t135\t208\t88\t178\t96\t25\t103\t161\r\n1080\t247\t1036\t936\t108\t971\t908\t1035\t123\t974\t103\t1064\t129\t1189\t1089\t938\r\n148\t1874\t122\t702\t922\t2271\t123\t111\t454\t1872\t2142\t2378\t126\t813\t1865\t1506\r\n842\t267\t230\t1665\t2274\t236\t262\t1714\t3281\t4804\t4404\t3833\t661\t4248\t3893\t1105\r\n1112\t1260\t809\t72\t1104\t156\t104\t1253\t793\t462\t608\t84\t99\t1174\t449\t929\r\n707\t668\t1778\t1687\t2073\t1892\t62\t1139\t908\t78\t1885\t800\t945\t712\t57\t65";
            Console.Out.WriteLine(new Day2().Checksum_Pt2(input));
        }

        private List<List<int>> ParseInput(string input)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            string[] rows = input.Split(stringSeparators, StringSplitOptions.None);
            
            var output = rows.Select(row => row.Split('\t').Select(int.Parse).ToList()).ToList();
            
            return output;
        }

        public int Checksum_Pt2(string input)
        {
            var sheet = ParseInput(input);
            bool foundRow = false;
            int sum = 0;

            foreach (var row in sheet)
            {
                for (var first = 0; !foundRow && first < row.Count; first++)
                {
                    for (var second = first + 1; !foundRow && second < row.Count; second++)
                    {
                        if (row[first] % row[second] == 0)
                        {
                            sum += row[first] / row[second];
                            foundRow = true;
                        }
                        else if (row[second] % row[first] == 0)
                        {
                            sum += row[second] / row[first];
                            foundRow = true;
                        }
                    }
                }
                foundRow = false;
            }

            return sum;
        }
    }
}
