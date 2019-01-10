using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MuesF.BLL;

namespace MuesF.Base
{
    public class MuesMethods
    {
        public static string GetPluralTableName(string name)
        {
            if (name.ToLower() != "news")
            {
                switch (name.Substring(name.Length - 1, 1))
                {

                    case "s":
                        return name + "es";
                    case "y":
                        return name.Substring(0, name.Length - 1) + "ies";
                    case "h":
                        return name + "es";
                    default:
                        return name + "s";
                }
            }
            else
            {
                return name;
            }
        }

        public static string GetWhereClause<T>(Expression<Func<T, bool>> expression)
        {
            WalkVisitor w = new WalkVisitor();
            Expression expr = w.Visit(expression.Body);

            return GetValueAsString(expr);
        }

        public static string GetValueAsString(Expression expression)
        {
            var value = "";
            var equalty = "";
            var left = GetLeftNode(expression);
            var right = GetRightNode(expression);

            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    equalty = "=";
                    break;
                case ExpressionType.And:
                    equalty = "AND";
                    break;
                case ExpressionType.AndAlso:
                    equalty = "AND";
                    break;
                case ExpressionType.Call:
                    equalty = "LIKE";
                    break;
                case ExpressionType.GreaterThan:
                    equalty = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    equalty = ">=";
                    break;
                case ExpressionType.LessThan:
                    equalty = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    equalty = "<=";
                    break;
                case ExpressionType.NotEqual:
                    equalty = "<>";
                    break;
                case ExpressionType.Or:
                    equalty = "OR";
                    break;
                case ExpressionType.OrAssign:
                    equalty = "OR";
                    break;
                case ExpressionType.OrElse:
                    equalty = "OR";
                    break;
            }

            if (left is MemberExpression)
            {
                var leftMem = left as MemberExpression;
                if (expression.NodeType == ExpressionType.Call)
                {
                    value = string.Format("({0} {1} '%{2}%')", leftMem.Member.Name, equalty, "{0}");
                }
                else
                {
                    object rightv = null;
                    if (right.NodeType != ExpressionType.Convert)
                    {
                        dynamic exp = right;
                        rightv = exp.Value;
                    }
                    if (right.NodeType != ExpressionType.Convert && rightv == null || rightv == "null")
                    {
                        value = string.Format("({0} {1})", leftMem.Member.Name, "{0}");
                    }
                    else
                    {
                        value = string.Format("({0}{1}'{2}')", leftMem.Member.Name, equalty, "{0}");
                    }

                }
            }
            //else if (left.NodeType == ExpressionType.Convert)
            //{
            //    dynamic exp = left;
            //    var val = exp.Operand.Member.Name;
            //    value = string.Format(value, val);
            //}
            if (right is ConstantExpression)
            {
                var rightConst = right as ConstantExpression;
                if (rightConst.Value == null || rightConst.Value.ToString() == "null")
                {
                    if (equalty == "=")
                    {
                        value = string.Format(value, "IS NULL");
                    }
                    else
                    {
                        value = string.Format(value, "IS NOT NULL");
                    }

                }
                else
                {
                    value = string.Format(value, rightConst.Value);
                }

            }
            else if (right is MemberExpression)
            {
                var rightMem = right as MemberExpression;
                var rightConst = rightMem.Expression as ConstantExpression;
                WalkVisitor w = new WalkVisitor();
                var val = w.Visit(rightMem.Expression);

                var member = rightMem.Member.DeclaringType;
                var type = rightMem.Member.MemberType;
                value = string.Format(value, val);
            }
            else if (right.NodeType == ExpressionType.Convert)
            {
                dynamic exp = right;
                var val = exp.Operand.Value;
                value = string.Format(value, val);
            }

            if (value == "")
            {
                var leftVal = GetValueAsString(left);
                var rigthVal = GetValueAsString(right);
                value = string.Format("({0} {1} {2})", leftVal, equalty, rigthVal);
            }

            return value;
        }

        private static Expression GetLeftNode(Expression expression)
        {
            dynamic exp = expression;
            if (expression.NodeType == ExpressionType.Call)
            {
                return ((Expression)exp.Object);
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                return ((Expression)exp.Operand);
            }
            else
            {
                if (exp.Left.NodeType == ExpressionType.Convert)
                {
                    return ((Expression)exp.Left.Operand);
                }
                else
                {
                    return ((Expression)exp.Left);
                }

            }
        }

        private static Expression GetRightNode(Expression expression)
        {
            dynamic exp = expression;
            if (expression.NodeType == ExpressionType.Call)
            {
                return ((Expression)exp.Arguments[0]);
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                return expression;
            }
            else
            {
                return ((Expression)exp.Right);
            }
        }

        #region GetWhereClouseJOIN
        static string tableName = "";
        public static string GetWhereClauseJoin<T>(Expression<Func<T, bool>> expression)
        {
            WalkVisitor w = new WalkVisitor();
            Expression expr = w.Visit(expression.Body);
            tableName = typeof(T).Name;
            return GetValueAsStringJoin(expr);

        }

        public static string GetValueAsStringJoin(Expression expression)
        {
            var value = "";
            var equalty = "";
            var left = GetLeftNodeJoin(expression);
            var right = GetRightNodeJoin(expression);

            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    equalty = "=";
                    break;
                case ExpressionType.And:
                    equalty = "AND";
                    break;
                case ExpressionType.AndAlso:
                    equalty = "AND";
                    break;
                case ExpressionType.Call:
                    equalty = "LIKE";
                    break;
                case ExpressionType.GreaterThan:
                    equalty = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    equalty = ">=";
                    break;
                case ExpressionType.LessThan:
                    equalty = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    equalty = "<=";
                    break;
                case ExpressionType.NotEqual:
                    equalty = "<>";
                    break;
                case ExpressionType.Or:
                    equalty = "OR";
                    break;
                case ExpressionType.OrAssign:
                    equalty = "OR";
                    break;
                case ExpressionType.OrElse:
                    equalty = "OR";
                    break;

            }

            if (left is MemberExpression)
            {
                var leftMem = left as MemberExpression;
                if (expression.NodeType == ExpressionType.Call)
                {
                    value = string.Format("({0} {1} '%{2}%')", GetPluralTableName(tableName) + "." + leftMem.Member.Name, equalty, "{0}");
                }
                else
                {
                    object rightv = null;
                    if (right.NodeType != ExpressionType.Convert)
                    {
                        dynamic exp = right;
                        rightv = exp.Value;
                    }
                    if (right.NodeType != ExpressionType.Convert && rightv == null || rightv == "null")
                    {
                        value = string.Format("({0} {1})", GetPluralTableName(tableName) + "." + leftMem.Member.Name, "{0}");
                    }
                    else
                    {


                        value = string.Format("({0}{1}'{2}')", GetPluralTableName(tableName) + "." + leftMem.Member.Name, equalty, "{0}");
                    }

                }
            }
            else
            {
                if (left.NodeType == ExpressionType.Convert)
                {
                    dynamic exp = left;
                    object aaa = exp.Operand;
                    var leftMem = aaa as MemberExpression;
                    if (right.NodeType == ExpressionType.Convert)
                    {
                        value = string.Format("({0}{1}'{2}')", GetPluralTableName(tableName) + "." + leftMem.Member.Name, equalty, "{0}");
                    }
                    else
                    {
                        var rightConst = right as ConstantExpression;
                        if (rightConst.Value == null || rightConst.Value.ToString() == "null")
                        {
                            value = string.Format("({0} {1})", GetPluralTableName(tableName) + "." + leftMem.Member.Name, "{0}");
                        }
                        else
                        {
                            value = string.Format("({0}{1}'{2}')", GetPluralTableName(tableName) + "." + leftMem.Member.Name, equalty, "{0}");
                        }
                    }
               

                }
            }

            if (right is ConstantExpression)
            {
                var rightConst = right as ConstantExpression;
                if (rightConst.Value == null || rightConst.Value.ToString() == "null")
                {
                    if (equalty == "=")
                    {
                        value = string.Format(value, "IS NULL");
                    }
                    else
                    {
                        value = string.Format(value, "IS NOT NULL");
                    }

                }
                else
                {
                    if (rightConst.Type.FullName.Contains("DateTime"))
                    {
                        object datetime = rightConst.Value;
                        object date = Convert.ToDateTime(datetime).ToString("MM.dd.yyyy HH:mm:ss");
                        value = string.Format(value, date);
                    }
                    else
                    {
                        value = string.Format(value, rightConst.Value);
                    }

                }

            }
            else if (right is MemberExpression)
            {
                var rightMem = right as MemberExpression;
                var rightConst = rightMem.Expression as ConstantExpression;
                WalkVisitor w = new WalkVisitor();
                var val = w.Visit(rightMem.Expression);

                var member = rightMem.Member.DeclaringType;
                var type = rightMem.Member.MemberType;
                value = string.Format(value, val);
            }
            else if (right.NodeType == ExpressionType.Convert)
            {
                dynamic exp = right;
                var val = exp.Operand.Value;
                value = string.Format(value, val);
            }

            if (value == "")
            {
                var leftVal = GetValueAsStringJoin(left);
                var rigthVal = GetValueAsStringJoin(right);
                value = string.Format("({0} {1} {2})", leftVal, equalty, rigthVal);
            }

            return value;
        }

        private static Expression GetLeftNodeJoin(Expression expression)
        {
            dynamic exp = expression;
            if (expression.NodeType == ExpressionType.Call)
            {
                return ((Expression)exp.Object);
            }
            else
            {
                return ((Expression)exp.Left);
            }
        }

        private static Expression GetRightNodeJoin(Expression expression)
        {
            dynamic exp = expression;
            if (expression.NodeType == ExpressionType.Call)
            {
                return ((Expression)exp.Arguments[0]);
            }
            else
            {
                return ((Expression)exp.Right);
            }
        }
        #endregion

        #region GetWhereClouseOrderBy
        static string orderBytableName = "";
        public static string GetWhereClauseOrderBy<T, Tkey>(Expression<Func<T, object>> expression)
        {
            WalkVisitor w = new WalkVisitor();
            Expression expr = w.Visit(expression.Body);
            orderBytableName = typeof(T).Name;
            return GetValueAsStringOrderBy(expr);
        }

        public static string GetValueAsStringOrderBy(Expression expression)
        {
            var value = "";
            var left = GetLeftNodeOrderBy(expression);
            if (left is MemberExpression)
            {
                var leftMem = left as MemberExpression;
                value = string.Format("{0}", GetPluralTableName(orderBytableName) + "." + leftMem.Member.Name, "{0}");

            }

            if (value == "")
            {
                var leftVal = GetValueAsStringOrderBy(left);
                value = string.Format("{0}", GetPluralTableName(orderBytableName) + "." + leftVal);
            }

            return value;
        }

        private static Expression GetLeftNodeOrderBy(Expression expression)
        {
            dynamic exp = expression;
            if (expression.NodeType == ExpressionType.Convert)
            {
                return ((Expression)exp.Operand);
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                return exp;
            }
            else
            {
                return ((Expression)exp.Expression);
            }
        }


        #endregion

        #region GetWhereClauseSelect

        static string selectBytableName = "";
        public static string GetWhereClauseSelect<T, Tkey>(Expression<Func<T, object>> expression)
        {
            WalkVisitor w = new WalkVisitor();
            Expression expr = w.Visit(expression.Body);
            selectBytableName = typeof(T).Name;
            return GetValueAsStringSelect(expr);
        }

        public static string GetValueAsStringSelect(Expression expression)
        {
            var value = "";
            dynamic exp = expression;
            if (expression.Type.FullName.Contains("AnonymousType") && expression.NodeType == ExpressionType.New)
            {
                var arg = exp.Arguments;

                foreach (var item in arg)
                {
                    value += MuesMethods.GetPluralTableName(selectBytableName) + "." + item.Member.Name + ",";
                }
                value = value.Substring(0, value.Length - 1);
            }
            else
            {
                if (expression is MemberExpression)
                {
                    var leftMem = expression as MemberExpression;
                    value = string.Format("{0}", GetPluralTableName(selectBytableName) + "." + leftMem.Member.Name, "{0}");

                }

                if (value == "")
                {
                    var leftVal = GetValueAsStringOrderBy(expression);
                    value = string.Format("{0}", GetPluralTableName(selectBytableName) + "." + leftVal);
                }
            }

            return value;
        }

        #endregion

        public static Expression<Func<T, bool>> LambdaUnion<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            Expression a = System.Linq.Expressions.Expression.AndAlso(left.Body, right.Body);
            Expression<Func<T, bool>> expr = Expression.Lambda<Func<T, bool>>(a, Expression.Parameter(typeof(T)));

            return expr;
        }

        public static object[] ArrayAdd(object[] array, params object[] items)
        {
            foreach (var item in items)
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = item;
            }
            return array;
        }
    }
}
