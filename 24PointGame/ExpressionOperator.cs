using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24PointGame
{
    public class ExpressionOperator
    {
        /// <summary>
        /// Gets the number left.
        /// </summary>
        /// <value>
        /// The number left.
        /// </value>
        public double Num_left
        {
            get
            {
                switch (_expression)
                {
                    case Expressions.NDivision:
                    case Expressions.NSubtraction: return _b;
                    default: return _a;
                }
            }
        }

        /// <summary>
        /// Gets the number right.
        /// </summary>
        /// <value>
        /// The number right.
        /// </value>
        public double Num_right
        {
            get
            {
                switch (_expression)
                {
                    case Expressions.NDivision:
                    case Expressions.NSubtraction: return _a;
                    default: return _b;
                }
            }
        }

        /// <summary>
        /// Gets the number b.
        /// </summary>
        /// <value>
        /// The number b.
        /// </value>
        public double Num_b => _b;

        /// <summary>
        /// Gets the number a.
        /// </summary>
        /// <value>
        /// The number a.
        /// </value>
        public double Num_a => _a;
        private readonly double _a;
        private readonly double _b;
        private readonly Expressions _expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionOperator"/> class.
        /// </summary>
        /// <param name="num_a">The number a.</param>
        /// <param name="num_b">The number b.</param>
        /// <param name="expression">The expression.</param>
        public ExpressionOperator(double num_a, double num_b, Expressions expression)
        {
            _a = num_a;
            _b = num_b;
            _expression = expression;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns></returns>
        public double GetResult()
        {
            switch (_expression)
            {
                case Expressions.Addition: return _a + _b;
                case Expressions.Subtraction: return _a - _b;
                case Expressions.NSubtraction: return _b - _a;
                case Expressions.Multiplication: return _a * _b;
                case Expressions.Division: return _b == 0 ? Single.NaN : _a / _b;
                case Expressions.NDivision: return _a == 0 ? Single.NaN : _b / _a;
                default: return Single.NaN;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{GetExpressionString()}={GetResult()}";
        }

        /// <summary>
        /// Gets the expression string.
        /// </summary>
        /// <returns></returns>
        public string GetExpressionString() => GetExpressionString(_a, _b);

        /// <summary>
        /// Gets the expression string.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public string GetExpressionString(object left, object right)
        {
            left = left is string ? $"({left})" : left;
            right = right is string ? $"({right})" : right;
            switch (_expression)
            {
                case Expressions.Addition: return $"{left}+{right}";
                case Expressions.Subtraction: return $"{left}-{right}";
                case Expressions.NSubtraction: return $"{right}-{left}";
                case Expressions.Multiplication: return $"{left}*{right}";
                case Expressions.Division: return $"{left}/{right}";
                case Expressions.NDivision: return $"{right}/{left}";
                default: return "";
            }

        }
    }
}
