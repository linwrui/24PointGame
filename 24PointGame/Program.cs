/*
 * 作者：林文锐
 * 时间：2017.02.05
 * 功能：游戏算24点
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24Game
{
    class Program
    {
        const int operateResult = 24;
        const double Threadhold = 0.0000001F;
        private static string[] numsRule = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
        private static Dictionary<int, string> resultExps = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            InitExps();
            GO();

        }

        /// <summary>
        /// 运行函数，重复运行
        /// </summary>
        private static void GO()
        {
            Console.WriteLine("Please input 4 nums:");
            string nums = "";
            string num;
            for (int i = 0; i < 4; i++)//读取输入的4个数组
            {
                if (i != 3)
                    nums += (num = Console.ReadLine()) + ",";
                else
                    nums += num = Console.ReadLine();
                if (!numsRule.Contains(num))
                {
                    Console.WriteLine("Error Input!");
                    GO();
                    return;
                }

            }
            string[] operateNums = nums.Split(',');
            string result = Operates(new Tuple<string, string, string, string>(operateNums[0], operateNums[1], operateNums[2], operateNums[3]));
            Console.WriteLine(result);
            Console.WriteLine();
            GO();
        }

        /// <summary>
        /// 初始化运算符和Index的关联
        /// </summary>
        private static void InitExps()
        {
            resultExps.Add(0, "+");
            resultExps.Add(1, "-");
            resultExps.Add(2, "-");
            resultExps.Add(3, "*");
            resultExps.Add(4, "/");
            resultExps.Add(5, "/");
        }

        /// <summary>
        /// 运算输入的数值并返回结果
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        static string Operates(Tuple<string, string, string, string> nums)
        {
            int a1 = Convert.ToInt32(nums.Item1);
            int a2 = Convert.ToInt32(nums.Item2);
            int a3 = Convert.ToInt32(nums.Item3);
            int a4 = Convert.ToInt32(nums.Item4);
            return sortNums(new int[] { a1, a2, a3, a4 });
        }

        /// <summary>
        /// 对数组a所有可能的排列进行组合运算并返回运算的结果
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static string sortNums(int[] a)
        {
            for (int ai = 0; ai < a.Count(); ai++)
            {
                int a1 = a[ai];
                for (int bi = 0; bi < a.Count(); bi++)
                {
                    if (bi != ai)
                    {
                        int a2 = a[bi];
                        for (int ci = 0; ci < a.Count(); ci++)
                        {
                            if (ci != ai && ci != bi)
                            {
                                int a3 = a[ci];
                                for (int di = 0; di < a.Count(); di++)
                                {
                                    if (di != ai && di != bi && di != ci)
                                    {
                                        int a4 = a[di];
                                        int aii = -1; int bii = -1; int abi = -1; //三个Index，用以还原两个数之间的运算符
                                        if (OperatesTwoTwo(a1, a2, a3, a4, out aii, out bii, out abi))//注意：这里如果有结果匹配需要判断是否需要倒换两个数的运算位置，比如a1-a2和a2-a1的结果是不一样的
                                        {
                                            string left = (aii == 2 || aii == 5) ? $"({a2}{resultExps[aii]}{a1})" : $"({a1}{resultExps[aii]}{a2})";
                                            string right = (bii == 2 || bii == 5) ? $"({a4}{resultExps[bii]}{a3})" : $"({a3}{resultExps[bii]}{a4})";
                                            return $"Success!{((abi == 2 || abi == 5) ? right : left)}{resultExps[abi]}{((abi == 2 || abi == 5) ? left : right)}=24";
                                        }
                                        else if (OperatesOneThree(a1, a2, a3, a4, out aii, out bii, out abi))
                                        {
                                            string left1 = (aii == 2 || aii == 5) ? $"({a2}{resultExps[aii]}{a1})" : $"({a1}{resultExps[aii]}{a2})";
                                            string left = (bii == 2 || bii == 5) ? $"({a3}{resultExps[bii]}{left1})" : $"({left1}{resultExps[bii]}{a3})";
                                            return $"Success!{((abi == 2 || abi == 5) ? a4.ToString() : left)}{resultExps[abi]}{((abi == 2 || abi == 5) ? left : a4.ToString())}=24";
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "Failed!";
        }

        /// <summary>
        /// (a1{Exp}a2{Exp}a3){Exp}a4
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="a4"></param>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="i3"></param>
        /// <returns></returns>
        private static bool OperatesOneThree(int a1, int a2, int a3, int a4, out int i1, out int i2, out int i3)
        {
            double[] a12 = Operates(new double[] { a1, a2 });//(a1{Exp}a2{Exp}a3){Exp}a4
            //double[] a34 = Operates(new double[] { a3, a4 });
            for (int ai = 0; ai < a12.Count(); ai++)
            {
                double a = a12[ai];
                double[] a123 = Operates(new double[] { a, a3 });
                for (int bi = 0; bi < a123.Count(); bi++)
                {
                    double b = a123[bi];
                    double[] ab = Operates(new double[] { b, a4 });
                    for (int abi = 0; abi < ab.Count(); abi++)
                    {
                        if (Math.Abs(ab[abi] - operateResult) < Threadhold)
                        {
                            i1 = ai;
                            i2 = bi;
                            i3 = abi;
                            return true;
                        }
                    }
                }
            }
            i1 = i2 = i3 = -1;
            return false;

        }

        /// <summary>
        /// (a1{Exp}a2){Exp}(a3{Exp}a4)
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="a4"></param>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="i3"></param>
        /// <returns></returns>
        private static bool OperatesTwoTwo(double a1, double a2, double a3, double a4, out int i1, out int i2, out int i3)
        {
            double[] a12 = Operates(new double[] { a1, a2 });
            double[] a34 = Operates(new double[] { a3, a4 });
            for (int ai = 0; ai < a12.Count(); ai++)
            {
                double a = a12[ai];
                for (int bi = 0; bi < a34.Count(); bi++)
                {
                    double b = a34[bi];
                    double[] ab = Operates(new double[] { a, b });
                    for (int abi = 0; abi < ab.Count(); abi++)
                    {
                        double h = Math.Abs(ab[abi] - operateResult);
                        if (h < Threadhold)
                        {
                            i1 = ai;
                            i2 = bi;
                            i3 = abi;
                            return true;
                        }
                    }
                }
            }
            i1 = i2 = i3 = -1;
            return false;
        }
        /// <summary>
        /// 运算两个数的四则运算并将结果作为一个数组返回
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static double[] Operates(double[] v)
        {
            double a1 = v[0];
            double a2 = v[1];
            return new double[] { a1 + a2, a1 - a2, a2 - a1, a1 * a2, a2 == 0 ? Single.NaN : (a1 / a2), a2 / a1 };//减法和除法运算时两个数位置相倒但未通知到前台
        }
    }
}
