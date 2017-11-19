using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24PointGame
{
    public class CardOperator
    {
        private readonly int[] _cards;
        const double Threadhold = 0.0000001F;

        public CardOperator(int[] cards)
        {
            _cards = cards;
        }

        /// <summary>
        /// 对数组a所有可能的排列进行组合运算并返回运算的结果
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public string Operate(double checkResult)
        {
            string result = "";
            List<string> results = new List<string>();
            for (int ai = 0; ai < _cards.Count(); ai++)
            {
                int a1 = _cards[ai];
                for (int bi = 0; bi < _cards.Count(); bi++)
                {
                    if (bi != ai)
                    {
                        int a2 = _cards[bi];
                        for (int ci = 0; ci < _cards.Count(); ci++)
                        {
                            if (ci != ai && ci != bi)
                            {
                                int a3 = _cards[ci];
                                for (int di = 0; di < _cards.Count(); di++)
                                {
                                    if (di != ai && di != bi && di != ci)
                                    {
                                        int a4 = _cards[di];
                                        if (OperateTwoTwo(new int[] { a1, a2, a3, a4 }, checkResult, out result))
                                        {
                                            if (!results.Contains(result))
                                            {
                                                Console.WriteLine(result);
                                                results.Add(result);
                                            }
                                            //return result;
                                        }
                                        if (OperateTreeOne(new int[] { a1, a2, a3, a4 }, checkResult, out result))
                                        {
                                            if (!results.Contains(result))
                                            {
                                                Console.WriteLine(result);
                                                results.Add(result);
                                            }
                                            // return result;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return $"计算完成，共有 {results.Count} 种运算方式。" ;
        }

        /// <summary>
        /// (a1{Exp}a2){Exp}(a3{Exp}a4)
        /// </summary>
        /// <returns></returns>
        private bool OperateTwoTwo(int[] cards, double checkResult, out string expression)
        {
            ExpressionOperator[] a12 = Operates(cards[0], cards[1]);
            ExpressionOperator[] a34 = Operates(cards[2], cards[3]);
            for (int ai = 0; ai < a12.Count(); ai++)
            {
                ExpressionOperator a = a12[ai];
                for (int bi = 0; bi < a34.Count(); bi++)
                {
                    ExpressionOperator b = a34[bi];
                    ExpressionOperator[] ab = Operates(a.GetResult(), b.GetResult());
                    for (int abi = 0; abi < ab.Count(); abi++)
                    {
                        double h = Math.Abs(ab[abi].GetResult() - checkResult);
                        if (h < Threadhold)
                        {
                            expression = ab[abi].GetExpressionString(a.GetExpressionString(), b.GetExpressionString())+$"={ab[abi].GetResult()}";
                            return true;
                        }
                    }
                }
            }
            expression = "";
            return false;
        }

        /// <summary>
        /// (a1{Exp}a2{Exp}a3){Exp}a4
        /// </summary>
        /// <returns></returns>
        private bool OperateTreeOne(int[] cards, double checkResult, out string expression)
        {
            ExpressionOperator[] a12 = Operates(cards[0], cards[1]);//(a1{Exp}a2{Exp}a3){Exp}a4
            //double[] a34 = Operates(new double[] { a3, a4 });
            for (int ai = 0; ai < a12.Count(); ai++)
            {
                ExpressionOperator a = a12[ai];
                ExpressionOperator[] a123 = Operates(a.GetResult(), cards[2]);
                for (int bi = 0; bi < a123.Count(); bi++)
                {
                    ExpressionOperator b = a123[bi];
                    ExpressionOperator[] ab = Operates(b.GetResult(), cards[3]);
                    for (int abi = 0; abi < ab.Count(); abi++)
                    {
                        if (Math.Abs(ab[abi].GetResult() - checkResult) < Threadhold)
                        {
                            expression = ab[abi].GetExpressionString(b.GetExpressionString(a.GetExpressionString(), b.Num_b), cards[3]) + $"={ab[abi].GetResult()}";
                            return true;
                        }
                    }
                }
            }
            expression = "";
            return false;
        }

        /// <summary>
        /// 运算两个数的四则运算并将结果作为一个数组返回
        /// </summary>
        /// <param name="num_a">The number a.</param>
        /// <param name="num_b">The number b.</param>
        /// <returns></returns>
        private ExpressionOperator[] Operates(double num_a, double num_b)
        {
            return new ExpressionOperator[]
            {
                new ExpressionOperator(num_a,num_b,Expressions.Addition),
                new ExpressionOperator(num_a,num_b,Expressions.Subtraction),
                new ExpressionOperator(num_a,num_b,Expressions.NSubtraction),
                new ExpressionOperator(num_a,num_b,Expressions.Multiplication),
                new ExpressionOperator(num_a,num_b,Expressions.Division),
                new ExpressionOperator(num_a,num_b,Expressions.NDivision),
            };
        }

    }
}
