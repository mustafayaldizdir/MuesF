using AppDatabasesGuide.Data;
using MuesF.Base;
using MuesF.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MuesF.BLL
{
    public static class Extensions
    {
        #region MuesJoin
        public static IMuesJoin MuesJoin<TBase, TTarget>(this IMuesJoin data)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            string a = " Join " + MuesMethods.GetPluralTableName(typeof(TBase).Name) + " On " + MuesMethods.GetPluralTableName(typeof(TBase).Name) + "." + typeof(TTarget).Name + "Id = " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + ".Id";

            MuesJoinType m = new MuesJoinType();
            m.Value = command + a;
            m.Expression = exp;
            m.PagingValue = pagingValue;
            m.OrderByValue = orderByValue;
            m.SelectValue = selectValue;
            return m;
        }

        public static IMuesJoin MuesJoin<TBase, TTarget>(this IMuesJoin data, Expression<Func<TBase, bool>> expression, Expression<Func<TTarget, bool>> expression2)
        {
            string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<TBase>(expression) : "";
            string cmd2 = expression2 != null ? MuesMethods.GetWhereClauseJoin<TTarget>(expression2) : "";
            string fullCommand = cmd != "" && cmd2 != "" ? cmd + " AND " + cmd2 : (cmd != "" ? cmd : cmd2);

            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            string a = " Join " + MuesMethods.GetPluralTableName(typeof(TBase).Name) + " On " + MuesMethods.GetPluralTableName(typeof(TBase).Name) + "." + typeof(TTarget).Name + "Id = " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + ".Id";

            MuesJoinType m = new MuesJoinType();
            m.Value = command + a;
            m.Expression = exp;
            m.PagingValue = pagingValue;
            m.OrderByValue = orderByValue;
            m.SelectValue = selectValue;
            return m;
        } 
        #endregion

        #region MuesOrdered
        public static IMuesPaging MuesOrderBy<T>(this IMuesJoin data, Expression<Func<T, object>> expression)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            string cmd = MuesMethods.GetWhereClauseOrderBy<T, object>(expression);
            MuesOrderByType t = new MuesOrderByType();
            t.Value = command;
            t.Expression = exp;
            t.PagingValue = pagingValue;
            t.OrderByValue = orderByValue + " ORDER BY " + cmd + " ASC ";
            t.SelectValue = selectValue;
            return t;
        }

        public static IMuesPaging MuesOrderByDescending<T>(this IMuesJoin data, Expression<Func<T, object>> expression)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            string cmd = MuesMethods.GetWhereClauseOrderBy<T, object>(expression);
            MuesOrderByType t = new MuesOrderByType();
            t.Value = command;
            t.Expression = exp;
            t.PagingValue = pagingValue;
            t.OrderByValue = orderByValue + " ORDER BY " + cmd + " DESC ";
            t.SelectValue = selectValue;
            return t;
        }

        public static IMuesPaging MuesThenBy<T>(this IMuesPaging data, Expression<Func<T, object>> expression)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            string cmd = MuesMethods.GetWhereClauseOrderBy<T, object>(expression);
            MuesOrderByType t = new MuesOrderByType();
            t.Value = command;
            t.Expression = exp;
            t.PagingValue = pagingValue;
            t.OrderByValue = orderByValue + ", " + cmd + " ASC ";
            t.SelectValue = selectValue;
            return t;
        }

        public static IMuesPaging MuesThenByDescending<T>(this IMuesPaging data, Expression<Func<T, object>> expression)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            string cmd = MuesMethods.GetWhereClauseOrderBy<T, object>(expression);
            MuesOrderByType t = new MuesOrderByType();
            t.Value = command;
            t.Expression = exp;
            t.PagingValue = pagingValue;
            t.OrderByValue = orderByValue + ", " + cmd + " DESC ";
            t.SelectValue = selectValue;
            return t;
        } 
        #endregion

        #region MuesPaging
        public static IAnonymous MuesTake(this IMuesPaging data, int count)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            AnonymousType t = new AnonymousType();
            t.Value = command;
            t.Expression = exp;
            t.PagingValue = pagingValue + " OFFSET (0) ROWS FETCH NEXT (" + count + ") ROWS ONLY ";
            t.OrderByValue = orderByValue;
            t.SelectValue = selectValue;
            return t;
        }

        public static IAnonymous MuesSkip(this IMuesPaging data, int count)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            AnonymousType m = new AnonymousType();
            m.Value = command;
            m.Expression = exp;
            m.PagingValue = pagingValue + " OFFSET (" + count + ") ROWS FETCH NEXT (0) ROWS ONLY ";
            m.OrderByValue = orderByValue;
            m.SelectValue = selectValue;
            return m;
        }

        public static IAnonymous MuesSkipTake(this IMuesPaging data, int skipCount, int takeCount)
        {
            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;
            AnonymousType m = new AnonymousType();
            m.Value = command;
            m.Expression = exp;
            m.PagingValue = pagingValue + " OFFSET (" + skipCount + ") ROWS FETCH NEXT (" + takeCount + ") ROWS ONLY ";
            m.OrderByValue = orderByValue;
            m.SelectValue = selectValue;
            return m;
        } 
        #endregion

        #region MuesSelect
        public static IMuesPaging MuesSelect<T>(this IMuesPaging data, Expression<Func<T, object>> expression)
        {
            string selectCommand = MuesMethods.GetWhereClauseSelect<T, object>(expression);

            dynamic datadyn = data;
            string command = datadyn.Value;
            string exp = datadyn.Expression;
            string pagingValue = datadyn.PagingValue;
            string orderByValue = datadyn.OrderByValue;
            string selectValue = datadyn.SelectValue;

            MuesOrderByType t = new MuesOrderByType();
            t.Value = command;
            t.Expression = exp;
            t.PagingValue = pagingValue;
            t.OrderByValue = orderByValue;
            t.SelectValue = (selectValue != "" ? selectValue + "," : "") + selectCommand;
            return t;
        } 
        #endregion

        #region MuesGetTable
        public static List<T> MuesGetTable<T>(this IMuesJoin data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetTable<T>(this IMuesJoin data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetTable<T>(this IMuesOrderBy data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetTable<T>(this IMuesOrderBy data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetTable<T>(this IMuesPaging data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetTable<T>(this IMuesPaging data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetTable<T>(this IAnonymous data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetTable<T>(this IAnonymous data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        } 

        #endregion

        #region MuesGetAll
        public static List<T> MuesGetAll<T>(this IMuesJoin data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetAll<T>(this IMuesJoin data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetAll<T>(this IMuesOrderBy data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetAll<T>(this IMuesOrderBy data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetAll<T>(this IMuesPaging data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetAll<T>(this IMuesPaging data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetAll<T>(this IAnonymous data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static List<T> MuesGetAll<T>(this IAnonymous data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression != null && datadyn.Expression != "" ? " AND " + cmd : cmd;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                string selectValue = datadyn.SelectValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string query = "SELECT " + (selectValue != "" ? selectValue : "*") + " FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + " WHERE " + MuesMethods.GetPluralTableName(c) + ".Status = 0 " + (exp != "" ? " AND " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();

                foreach (DataRow row in dt.Rows)
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = row[item.Name].ToString();
                            if (row[item.Name].ToString() != "")
                            {
                                item.SetValue(instance, row[item.Name]);
                            }
                        }
                    }
                    tlist.Add(instance);
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
        #endregion

        #region MuesCount
        public static long MuesCount<T>(this IMuesJoin data)
        {
            try
            {
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string cumle = "SELECT COUNT(*) FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand cmd = new SqlCommand("SELECT * FROM " + MuesMethods.GetPluralTableName(c) + command + (exp != "" ? " WHERE " + exp : ""), conn);
                if (conn.State == ConnectionState.Closed) conn.Open();
                long rowcount = Convert.ToInt64(cmd.ExecuteScalar());
                if (conn.State == ConnectionState.Open) conn.Close();

                return rowcount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static long MuesCount<T>(this IMuesJoin data, Expression<Func<T, bool>> expression)
        {
            try
            {
                string exprs = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                dynamic datadyn = data;
                string command = datadyn.Value;
                string exp = datadyn.Expression + " AND " + exprs;
                string pagingValue = datadyn.PagingValue;
                string orderByValue = datadyn.OrderByValue;
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string cumle = "SELECT COUNT(*) FROM " + MuesMethods.GetPluralTableName(c) + (command != "" ? " " + command : "") + (exp != "" ? " WHERE " + exp : "") + (orderByValue != "" ? " " + orderByValue : "") + (pagingValue != "" ? " " + pagingValue : "");
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand cmd = new SqlCommand("SELECT * FROM " + MuesMethods.GetPluralTableName(c) + command + (exp != "" ? " WHERE " + exp : ""), conn);
                if (conn.State == ConnectionState.Closed) conn.Open();
                long rowcount = Convert.ToInt64(cmd.ExecuteScalar());
                if (conn.State == ConnectionState.Open) conn.Close();

                return rowcount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        } 
        #endregion
    }
}
