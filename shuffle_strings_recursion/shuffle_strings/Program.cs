
using System;
using System.Collections.Generic;
using System.Linq;


namespace shuffle_strings
{
    class Program
    {
        const string str = "Нафига козе баян";
        static void Main(string[] args)
        {
            string[] strArr = str.Split(' ');
            var strList = new List<string>(strArr);

            var permutations = allcombinations(strList, new List<string>());
            foreach (var lst in permutations)
                output(lst);

            Console.ReadKey();
        }

        private static IEnumerable<List<string>> allcombinations(List<string> strList, List<string> awithout)
        {
            if (strList.Count == 1)
            {
                var result = new List<List<string>>();
                result.Add(new List<string>());
                result[0].Add(strList[0]);
                return result;
            }
            else
            {
                var result = new List<List<string>>();

                foreach (var first in strList)
                {
                    awithout.Add(first);
                    var others = new List<string>(strList.Except(awithout));

                    var combinations = allcombinations(others, awithout);
                    awithout.Remove(first);

                    foreach (var lst in combinations)
                    {
                        lst.Insert(0, first);
                        result.Add(lst);
                    }
                }
                return result;
            }
        }

        private static void output(IEnumerable<string> arg)
        {
            foreach (var str in arg)
                Console.Write(str + " ");
            Console.WriteLine();
        }
    }
}
