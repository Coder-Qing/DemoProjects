using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace 代码练习控制台_20210323
{
    class Program
    {
        private static Dictionary<string, int> keyValuePairs;
        static void Main(string[] args)
        {
            string test = "Hello World Hello";
            keyValuePairs =  CountWords(test);
        }

        /// <summary>
        /// 获取传入字符串中的单词是否出现的次数
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static Dictionary<string,int> CountWords(string test)
        {
            Dictionary<string, int> frequencies;
            frequencies = new Dictionary<string, int>();

            string[] words = Regex.Split(test,@"\W+");
            foreach (string word in words)
            {
                if (frequencies.ContainsKey(word))
                {
                    frequencies[word]++;
                }
                else
                {
                    frequencies[word] = 1;
                }
            }
            return frequencies;
        }

    }
}
