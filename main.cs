using System;
using System.Collections.Generic;

namespace SpeedTest
{
    internal class main
    {
        struct Table
        {
            public string Answer;
            public char AnswerKey;
            public string[] Options;
        }
        static string GetRandomStr(int length)
        {
            char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'N', 'M', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            Random rd = new();
            string str = "";
            for(int i = 0;i<length;i++)
            {
                var _str = chars[rd.Next(0,chars.Length - 1)];
                if (rd.Next(0,100) < 50)
                    str += _str;
                else
                    str += _str.ToString().ToLower();
            }
            return str;
        }
        static List<Table> GetRandomTable(int size,int count)
        {
            char[] AnswerKeys = { '1', '2', '3', '4' };
            Random rd = new Random();
            List<Table> tables = new();
            for (int a = 0; a < count ; a++)
            {
                List<string> _options = new();
                for (int i = 0; i < size; i++)
                {
                    var str = GetRandomStr(2);
                    if (_options.Contains(str))
                    {
                        i--;
                        continue;
                    }
                    _options.Add(str);
                }
                var options = _options.ToArray();
                var answer = options[rd.Next(0, size)];
                Table table = new();
                table.Answer = answer;
                table.Options = options;
                table.AnswerKey = AnswerKeys[_options.IndexOf(answer)];
                tables.Add(table);
            }
            return tables;
        }
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("#################################################################");
            Console.WriteLine("     Author: LeZi");
            Console.WriteLine("     ");
            Console.WriteLine("     介绍:");
            Console.WriteLine("     本软件可用于测试反应速度及准确度");
            Console.WriteLine("     可以自定义题数，默认为100道");
            Console.WriteLine("     每题共有四道选项，只有一个选项是正确的，请用最短的时间选出");
            Console.WriteLine("#################################################################");
            Console.WriteLine("请输入题量(Default:100):");
            int count = 100;
            int finshed = 0;
            List<Table> eTables = new();
            try
            {
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                    count = int.Parse(input);
            }
            catch
            {
                Console.WriteLine("你要不要看看你在输入什么?");
                Console.ReadKey();
                Main(args);
            }
            var tables = GetRandomTable(4, count);
            var startTime = DateTime.Now.Ticks;
            tables.ForEach(table =>
            {
                Console.Clear();
                Console.WriteLine($"[{tables.IndexOf(table)+1}]. {table.Answer}\n");
                Console.Write($"1.{table.Options[0]} 2.{table.Options[1]} 3.{table.Options[2]} 4.{table.Options[3]}\n");
                if (Console.ReadKey().KeyChar == table.AnswerKey)
                    finshed++;
                else
                    eTables.Add(table);
            });
            var endTime = DateTime.Now.Ticks;
            Console.Clear();
            Console.WriteLine("#################################################################");
            Console.WriteLine("                          测试完毕");
            Console.WriteLine($"     题目数量:{count}");
            Console.WriteLine($"     正确题数:{finshed}");
            Console.WriteLine($"     错误题数:{eTables.Count}");
            Console.WriteLine($"     正确率:{(finshed /(double)count) * 100}%");
            Console.WriteLine($"     耗时:{(new TimeSpan(endTime - startTime)).TotalSeconds}s");
            Console.WriteLine("#################################################################");
            Console.WriteLine("错题:");
            eTables.ForEach(table =>
            {
                Console.WriteLine($"[{tables.IndexOf(table) + 1}]. {table.Answer}\n");
                Console.Write($"A.{table.Options[0]} B.{table.Options[1]} C.{table.Options[2]} D.{table.Options[3]}\n");
            });
            Console.ReadKey();
        }
    }
}
