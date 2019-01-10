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
    public class MuesBLL<T>
    {
        #region GetTo
        /// <summary>
        /// Verilen id'ye ait satırı getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetTo(object id)
        {
            try
            {
                T instance = (T)Activator.CreateInstance(typeof(T));
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);

                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [" + MuesMethods.GetPluralTableName(c) + "] WHERE [" + MuesMethods.GetPluralTableName(c) + "].Id =" + id + " AND Status = 0  ORDER BY CreatedDate DESC", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    List<string> aList = new List<string>();
                    foreach (var item in instance.GetType().GetProperties())
                    {
                        if (item.GetAccessors()[0].IsVirtual == false)
                        {
                            aList.Add(item.Name);
                            var a = dt.Rows[0][item.Name].ToString();
                            if (dt.Rows[0][item.Name].ToString() != "")
                            {
                                item.SetValue(instance, dt.Rows[0][item.Name]);
                            }

                        }
                    }
                }
                return instance;
            }
            catch (Exception ex)
            {
                T instance = (T)Activator.CreateInstance(typeof(T));
                return instance;
            }
        }

        /// <summary>
        /// Verilen Koşula'a ait satırın aktif olanını getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Get(Expression<Func<T, bool>> expression)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            try
            {
                string cmd = MuesMethods.GetWhereClauseJoin<T>(expression);

                string c = typeof(T).Name;
                SqlConnection conn = new SqlConnection(DataAccess.Connection);

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [" + MuesMethods.GetPluralTableName(c) + "] WHERE " + cmd + " AND Status = 0  ORDER BY CreatedDate DESC", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();
                foreach (var item in instance.GetType().GetProperties())
                {
                    if (item.GetAccessors()[0].IsVirtual == false)
                    {
                        aList.Add(item.Name);
                        var a = dt.Rows[0][item.Name].ToString();
                        if (dt.Rows[0][item.Name].ToString() != "")
                        {
                            item.SetValue(instance, dt.Rows[0][item.Name]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return instance;
        }

        /// <summary>
        /// Verilen Koşula'a ait satırı getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetRow(Expression<Func<T, bool>> expression)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            try
            {
                string cmd = MuesMethods.GetWhereClauseJoin<T>(expression);

                string c = typeof(T).Name;
                SqlConnection conn = new SqlConnection(DataAccess.Connection);

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [" + MuesMethods.GetPluralTableName(c) + "] WHERE " + cmd + " ORDER BY CreateDate DESC", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> aList = new List<string>();
                foreach (var item in instance.GetType().GetProperties())
                {
                    if (item.GetAccessors()[0].IsVirtual == false)
                    {
                        aList.Add(item.Name);
                        var a = dt.Rows[0][item.Name].ToString();
                        if (dt.Rows[0][item.Name].ToString() != "")
                        {
                            item.SetValue(instance, dt.Rows[0][item.Name]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return instance;
        }
        #endregion

        #region GetMany
        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. 
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id)
        {
            try
            {
                List<T> tlist = new List<T>();
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetMany", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
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
                }

                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. Sıralama yapar.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, string orderColumn, bool isDescending)
        {
            try
            {
                List<T> tlist = new List<T>();
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyOrderBy", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
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
                }

                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. Sıralama yapar.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2)
        {
            try
            {
                List<T> tlist = new List<T>();
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyOrderBy2", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
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
                }

                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. Sıralama yapar.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <param name="orderColumn3">Sıralama yapılacak 3. kolon adı</param>
        /// <param name="isDescending3">Sıralama yapılacak 3. kolona tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2, string orderColumn3, bool isDescending3)
        {
            try
            {
                List<T> tlist = new List<T>();
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyOrderBy3", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn3", orderColumn3);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending3", isDescending3 ? "desc" : "asc");
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
                }

                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. 
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, int skip, int take)
        {
            try
            {
                List<T> tlist = new List<T>();
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyPaging", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. 
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, int skip, int take, string orderColumn, bool isDescending)
        {
            try
            {
                List<T> tlist = new List<T>();

                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyPagingOrderBy", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. 
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, int skip, int take, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2)
        {
            try
            {
                List<T> tlist = new List<T>();

                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyPagingOrderBy2", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. 
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, int skip, int take, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2, string orderColumn3, bool isDescending3)
        {
            try
            {
                List<T> tlist = new List<T>();

                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyPagingOrderBy3", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn3", orderColumn3);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending3", isDescending3 ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. Bir koşul sağlayarak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWhere", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);

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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. Bir koşul sağlayarak ve sıralama yaparak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression, string orderColumn, bool isDescending)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWhereOrderBy", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. Bir koşul sağlayarak ve sıralama yaparak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWhereOrderBy2", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır. Bir koşul sağlayarak ve sıralama yaparak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <param name="orderColumn3">Sıralama yapılacak 3. kolon adı</param>
        /// <param name="isDescending3">Sıralama yapılacak 3. kolona tipi true değer tersten sıralar</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2, string orderColumn3, bool isDescending3)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWhereOrderBy3", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn3", orderColumn3);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending3", isDescending3 ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır.  Bir koşul sağlayarak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression, int skip, int take)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWherePaging", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }

        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır.  Bir koşul sağlayarak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression, int skip, int take, string orderColumn, bool isDescending)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWherePagingOrderBy", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır.  Bir koşul sağlayarak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression, int skip, int take, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWherePagingOrderBy2", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Model class'ında bire çok ilişki için kullanılır.  Bir koşul sağlayarak getirilir.
        /// </summary>
        /// <param name="column">İlişki kurulan kolon adi</param>
        /// <param name="id">İlişki kurulan id</param>
        /// <param name="skip">Atlanacak satır sayısı</param>
        /// <param name="take">Getirilecek satır sayısı</param>
        /// <param name="orderColumn">Sıralama yapılacak kolon adı</param>
        /// <param name="isDescending">Sıralama tipi true değer tersten sıralar</param>
        /// <param name="orderColumn2">Sıralama yapılacak diğer kolon adı</param>
        /// <param name="isDescending2">Sıralama yapılacak diğer kolona tipi true değer tersten sıralar</param>
        /// <param name="orderColumn3">Sıralama yapılacak 3. kolon adı</param>
        /// <param name="isDescending3">Sıralama yapılacak 3. kolona tipi true değer tersten sıralar</param>
        /// <param name="expression">T için gerekli koşul.</param>
        /// <returns></returns>
        public static List<T> GetMany(string column, object id, Expression<Func<T, bool>> expression, int skip, int take, string orderColumn, bool isDescending, string orderColumn2, bool isDescending2, string orderColumn3, bool isDescending3)
        {
            try
            {
                List<T> tlist = new List<T>();
                string cmd = MuesMethods.GetWhereClause<T>(expression);
                if (id != "00000000-0000-0000-0000-000000000000" && id != "0" && id != null)
                {
                    string c = typeof(T).Name;
                    SqlConnection conn = new SqlConnection(DataAccess.Connection);
                    SqlDataAdapter adapter = new SqlDataAdapter("SP_GetManyWherePagingOrderBy3", conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@TableName", MuesMethods.GetPluralTableName(c));
                    adapter.SelectCommand.Parameters.AddWithValue("@column", column);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", id.ToString());
                    adapter.SelectCommand.Parameters.AddWithValue("@expression", cmd);
                    adapter.SelectCommand.Parameters.AddWithValue("@skip", skip);
                    adapter.SelectCommand.Parameters.AddWithValue("@take", take);
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn", orderColumn);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending", isDescending ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn2", orderColumn2);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending2", isDescending2 ? "desc" : "asc");
                    adapter.SelectCommand.Parameters.AddWithValue("@orderColumn3", orderColumn3);
                    adapter.SelectCommand.Parameters.AddWithValue("@isDescending3", isDescending3 ? "desc" : "asc");
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
                }
                return tlist;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
        #endregion

        #region MuesGetTable
        public static List<T> MuesGetTable()
        {
            try
            {
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string command = "SELECT * FROM " + MuesMethods.GetPluralTableName(c);
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);
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

        public static List<T> MuesGetTable(Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";

                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string command = "SELECT * FROM " + MuesMethods.GetPluralTableName(c) + " WHERE " + cmd;
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);
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
        public static List<T> MuesGetAll()
        {
            try
            {
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string command = "SELECT * FROM " + MuesMethods.GetPluralTableName(c) + " WHERE Status = 0";
                SqlConnection conn = new SqlConnection("Server=.;Database=DatabaseGuideDB;uid=sa;pwd=123");
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);
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

        public static List<T> MuesGetAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;

                string command = "SELECT * FROM " + MuesMethods.GetPluralTableName(c) + " WHERE Status = 0 AND " + cmd;
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);
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

        #region Add
        public static int Add(T item)
        {
            try
            {
                string c = typeof(T).Name;
                List<string> propertyNames = new List<string>();
                IList<PropertyInfo> props = typeof(T).GetProperties();
                string cmd = "insert into " + MuesMethods.GetPluralTableName(c) + " (";
                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetAccessors()[0].IsVirtual == false)
                    {
                        cmd += prop.Name + ",";
                    }
                }
                int len = cmd.Length;
                cmd = cmd.Substring(0, len - 1) + ") values (";

                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetAccessors()[0].IsVirtual == false)
                    {
                        cmd += "@" + prop.Name + ",";
                        propertyNames.Add(prop.Name);
                    }

                }
                len = cmd.Length;
                cmd = cmd.Substring(0, len - 1) + ")";


                T instance = item;

                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand command = new SqlCommand(cmd, conn);
                if (conn.State == ConnectionState.Closed) conn.Open();

                List<string> aList = new List<string>();
                foreach (var pitem in instance.GetType().GetProperties())
                {
                    if (pitem.GetAccessors()[0].IsVirtual == false)
                    {
                        aList.Add("@" + pitem.Name + "," + (pitem.GetValue(instance) ?? DBNull.Value));
                        command.Parameters.AddWithValue("@" + pitem.Name, pitem.GetValue(instance) ?? DBNull.Value);
                    }
                }
                int rowcount = command.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open) conn.Close();
                return rowcount;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int MuesAdd(T item)
        {
            try
            {
                string c = typeof(T).Name;
                List<string> propertyNames = new List<string>();
                IList<PropertyInfo> props = typeof(T).GetProperties();
                string cmd = "insert into " + MuesMethods.GetPluralTableName(c) + " (";
                foreach (PropertyInfo prop in props)
                {
                    if (c != "UserDetail")
                    {
                        if (prop.Name != "Id")
                        {
                            if (prop.GetAccessors()[0].IsVirtual == false)
                            {
                                cmd += prop.Name + ",";
                            }
                        }
                    }
                    else
                    {
                        if (prop.GetAccessors()[0].IsVirtual == false)
                        {
                            cmd += prop.Name + ",";
                        }
                    }
                }
                int len = cmd.Length;
                cmd = cmd.Substring(0, len - 1) + ") values (";

                foreach (PropertyInfo prop in props)
                {
                    if (c != "UserDetail")
                    {
                        if (prop.Name != "Id")
                        {
                            if (prop.GetAccessors()[0].IsVirtual == false)
                            {
                                cmd += "@" + prop.Name + ",";
                                propertyNames.Add(prop.Name);
                            }
                        }
                    }
                    else
                    {
                        if (prop.GetAccessors()[0].IsVirtual == false)
                        {
                            cmd += "@" + prop.Name + ",";
                            propertyNames.Add(prop.Name);
                        }
                    }

                }
                len = cmd.Length;
                cmd = cmd.Substring(0, len - 1) + ")";


                MuesDefaultValues mdv = new MuesDefaultValues();
                foreach (var mdvitem in mdv.GetType().GetProperties())
                {
                    if (propertyNames.Contains(mdvitem.Name))
                    {
                        item.GetType().GetProperty(mdvitem.Name).SetValue(item, mdvitem.GetValue(mdv));
                    }
                }

                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand command = new SqlCommand(cmd, conn);
                if (conn.State == ConnectionState.Closed) conn.Open();

                List<string> aList = new List<string>();
                foreach (var pitem in item.GetType().GetProperties())
                {
                    if (pitem.GetAccessors()[0].IsVirtual == false)
                    {
                        aList.Add("@" + pitem.Name + "," + (pitem.GetValue(item) ?? DBNull.Value));
                        command.Parameters.AddWithValue("@" + pitem.Name, pitem.GetValue(item) ?? DBNull.Value);
                    }
                }
                int rowcount = command.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open) conn.Close();
                return rowcount;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region Update
        public static int Update(T item)
        {
            try
            {
                string c = typeof(T).Name;
                List<string> propertyNames = new List<string>();
                IList<PropertyInfo> props = typeof(T).GetProperties();
                string cmd = "update " + MuesMethods.GetPluralTableName(c) + " set ";
                foreach (PropertyInfo prop in props)
                {
                    if (c != "UserDetail")
                    {
                        if (prop.Name != "Id")
                        {
                            if (prop.GetAccessors()[0].IsVirtual == false)
                            {
                                cmd += prop.Name + "=@" + prop.Name + ",";
                            }
                        }
                    }
                    else
                    {
                        if (prop.GetAccessors()[0].IsVirtual == false)
                        {
                            cmd += prop.Name + "=@" + prop.Name + ",";
                        }
                    }
                }
                int len = cmd.Length;
                cmd = cmd.Substring(0, len - 1);
                T instance = item;
                cmd = cmd + " Where Id = '" + instance.GetType().GetProperties()[0].GetValue(instance) + "'";

                MuesDefaultValues mdv = new MuesDefaultValues();
                foreach (var mdvitem in mdv.GetType().GetProperties())
                {
                    if (mdvitem.Name == "Status" && instance.GetType().GetProperty(mdvitem.Name).GetValue(instance) != "0")
                    {
                        instance.GetType().GetProperty(mdvitem.Name).SetValue(instance, instance.GetType().GetProperty(mdvitem.Name).GetValue(instance));
                    }
                    else
                    {
                        instance.GetType().GetProperty(mdvitem.Name).SetValue(instance, mdvitem.GetValue(mdv));
                    }
                      
                }

                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand command = new SqlCommand(cmd, conn);
                if (conn.State == ConnectionState.Closed) conn.Open();

                List<string> aList = new List<string>();
                foreach (var pitem in instance.GetType().GetProperties())
                {
                    if (c != "UserDetail")
                    {
                        if (pitem.Name != "Id")
                        {
                            if (pitem.GetAccessors()[0].IsVirtual == false)
                            {
                                command.Parameters.AddWithValue("@" + pitem.Name, pitem.GetValue(instance) ?? DBNull.Value);
                            }
                        }
                    }
                    else
                    {
                        if (pitem.GetAccessors()[0].IsVirtual == false)
                        {
                            command.Parameters.AddWithValue("@" + pitem.Name, pitem.GetValue(instance) ?? DBNull.Value);
                        }
                    }
                }
                int rowcount = command.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open) conn.Close();
                return rowcount;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region Remove
        public static int Remove(object id)
        {
            try
            {
                string c = typeof(T).Name;
                List<string> propertyNames = new List<string>();
                IList<PropertyInfo> props = typeof(T).GetProperties();
                string cmd = "Delete From " + MuesMethods.GetPluralTableName(c) + " Where Id = '" + id + "'";

                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand command = new SqlCommand(cmd, conn);
                if (conn.State == ConnectionState.Closed) conn.Open();

                int rowcount = command.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open) conn.Close();
                return rowcount;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int RemoveSoft(object id)
        {
            try
            {
                string c = typeof(T).Name;
                T item = GetTo(id);
                item.GetType().GetProperty("Status").SetValue(item, Convert.ToInt16(2));
                int rowcount = Update(item);

                return rowcount;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        public static IMuesPaging MuesOrderBy(Expression<Func<T, object>> expression)
        {
            string cmd = MuesMethods.GetWhereClauseOrderBy<T, object>(expression);
            MuesOrderByType t = new MuesOrderByType();
            t.Value = "";
            t.Expression = "";
            t.PagingValue = "";
            t.OrderByValue = "ORDER BY " + cmd + " ASC ";
            t.SelectValue = "";
            return t;
        }

        public static IMuesPaging MuesOrderByDescending(Expression<Func<T, object>> expression)
        {
            string cmd = MuesMethods.GetWhereClauseOrderBy<T, object>(expression);
            MuesOrderByType t = new MuesOrderByType();
            t.Value = "";
            t.Expression = "";
            t.PagingValue = "";
            t.OrderByValue = "ORDER BY " + cmd + " DESC ";
            t.SelectValue = "";
            return t;
        }

        #region MuesJoin
        public static MuesJoinTypeBase<T> MuesJoin<TTarget>()
        {
            string a = "Join " + MuesMethods.GetPluralTableName(typeof(T).Name) + " On " + MuesMethods.GetPluralTableName(typeof(T).Name) + "." + typeof(TTarget).Name + "Id = " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + ".Id";

            MuesJoinTypeBase<T> m = new MuesJoinTypeBase<T>();
            m.Value = a;
            m.Expression = "";
            m.PagingValue = "";
            m.OrderByValue = "";
            m.SelectValue = "";
            return m;
        }

        public static MuesJoinTypeBase<T> MuesJoin<TTarget>(Expression<Func<T, object>> baseId, Expression<Func<TTarget, object>> targetId, Expression<Func<T, bool>> expression, Expression<Func<TTarget, bool>> expression2)
        {
            string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
            string cmd2 = expression2 != null ? MuesMethods.GetWhereClauseJoin<TTarget>(expression2) : "";
            string command = cmd != "" && cmd2 != "" ? cmd + " AND " + cmd2 : (cmd != "" ? cmd : cmd2);

            string baseid = MuesMethods.GetWhereClauseOrderBy<T, object>(baseId);
            baseid = baseid.Replace("(", "").Replace(")", "");
            string targetid = MuesMethods.GetWhereClauseOrderBy<TTarget, object>(targetId);
            targetid = targetid.Replace("(", "").Replace(")", "");
            string a = "Join " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + " On " + baseid + " = " + targetid;

            MuesJoinTypeBase<T> m = new MuesJoinTypeBase<T>();
            m.Value = a;
            m.Expression = command;
            m.PagingValue = "";
            m.OrderByValue = "";
            m.SelectValue = "";
            return m;
        }

        public static MuesJoinTypeBase<T> MuesJoin<TTarget>(Expression<Func<T, bool>> expression, Expression<Func<TTarget, bool>> expression2)
        {

            string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
            string cmd2 = expression2 != null ? MuesMethods.GetWhereClauseJoin<TTarget>(expression2) : "";
            string command = cmd != "" && cmd2 != "" ? cmd + " AND " + cmd2 : (cmd != "" ? cmd : cmd2);
            string a = "Join " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + " On " + MuesMethods.GetPluralTableName(typeof(T).Name) + "." + typeof(TTarget).Name + "Id = " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + ".Id";

            MuesJoinTypeBase<T> m = new MuesJoinTypeBase<T>();
            m.Value = a;
            m.Expression = command;
            m.PagingValue = "";
            m.OrderByValue = "";
            m.SelectValue = "";
            return m;
        }

        public static MuesJoinTypeBase<T> MuesLeftJoin<TTarget>(Expression<Func<T, object>> baseId, Expression<Func<TTarget, object>> targetId, Expression<Func<T, bool>> expression, Expression<Func<TTarget, bool>> expression2)
        {
            string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
            string cmd2 = expression2 != null ? MuesMethods.GetWhereClauseJoin<TTarget>(expression2) : "";
            string command = cmd != "" && cmd2 != "" ? cmd + " AND " + cmd2 : (cmd != "" ? cmd : cmd2);

            string baseid = MuesMethods.GetWhereClauseOrderBy<T, object>(baseId);
            baseid = baseid.Replace("(", "").Replace(")", "");
            string targetid = MuesMethods.GetWhereClauseOrderBy<TTarget, object>(targetId);
            targetid = targetid.Replace("(", "").Replace(")", "");
            string a = " Left Join " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + " On " + baseid + " = " + targetid;

            MuesJoinTypeBase<T> m = new MuesJoinTypeBase<T>();
            m.Value = a;
            m.Expression = command;
            m.PagingValue = "";
            m.OrderByValue = "";
            m.SelectValue = "";
            return m;
        }

        public static MuesJoinTypeBase<T> MuesLeftJoin<TTarget>(Expression<Func<T, bool>> expression, Expression<Func<TTarget, bool>> expression2)
        {

            string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
            string cmd2 = expression2 != null ? MuesMethods.GetWhereClauseJoin<TTarget>(expression2) : "";
            string command = cmd != "" && cmd2 != "" ? cmd + " AND " + cmd2 : (cmd != "" ? cmd : cmd2);
            string a = " Left Join " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + " On " + MuesMethods.GetPluralTableName(typeof(T).Name) + "." + typeof(TTarget).Name + "Id = " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + ".Id";

            MuesJoinTypeBase<T> m = new MuesJoinTypeBase<T>();
            m.Value = a;
            m.Expression = command;
            m.PagingValue = "";
            m.OrderByValue = "";
            m.SelectValue = "";
            return m;
        }

        public static MuesJoinTypeBase<T> MuesRightJoin<TTarget>(Expression<Func<T, object>> baseId, Expression<Func<TTarget, object>> targetId, Expression<Func<T, bool>> expression, Expression<Func<TTarget, bool>> expression2)
        {
            string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
            string cmd2 = expression2 != null ? MuesMethods.GetWhereClauseJoin<TTarget>(expression2) : "";
            string command = cmd != "" && cmd2 != "" ? cmd + " AND " + cmd2 : (cmd != "" ? cmd : cmd2);

            string baseid = MuesMethods.GetWhereClauseOrderBy<T, object>(baseId);
            baseid = baseid.Replace("(", "").Replace(")", "");
            string targetid = MuesMethods.GetWhereClauseOrderBy<TTarget, object>(targetId);
            targetid = targetid.Replace("(", "").Replace(")", "");
            string a = " Right Join " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + " On " + baseid + " = " + targetid;

            MuesJoinTypeBase<T> m = new MuesJoinTypeBase<T>();
            m.Value = a;
            m.Expression = command;
            m.PagingValue = "";
            m.OrderByValue = "";
            m.SelectValue = "";
            return m;
        }

        public static MuesJoinTypeBase<T> MuesRightJoin<TTarget>(Expression<Func<T, bool>> expression, Expression<Func<TTarget, bool>> expression2)
        {

            string cmd = expression != null ? MuesMethods.GetWhereClauseJoin<T>(expression) : "";
            string cmd2 = expression2 != null ? MuesMethods.GetWhereClauseJoin<TTarget>(expression2) : "";
            string command = cmd != "" && cmd2 != "" ? cmd + " AND " + cmd2 : (cmd != "" ? cmd : cmd2);
            string a = " Right Join " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + " On " + MuesMethods.GetPluralTableName(typeof(T).Name) + "." + typeof(TTarget).Name + "Id = " + MuesMethods.GetPluralTableName(typeof(TTarget).Name) + ".Id";

            MuesJoinTypeBase<T> m = new MuesJoinTypeBase<T>();
            m.Value = a;
            m.Expression = command;
            m.PagingValue = "";
            m.OrderByValue = "";
            m.SelectValue = "";
            return m;
        } 
        #endregion

        public static long MuesCount()
        {
            try
            {
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM " + MuesMethods.GetPluralTableName(c), conn);
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

        public static long MuesCount(Expression<Func<T, bool>> expression)
        {
            try
            {
                string cmd = expression != null ? MuesMethods.GetWhereClause<T>(expression) : "";
                List<T> tlist = new List<T>();
                string c = typeof(T).Name;
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + MuesMethods.GetPluralTableName(c) + " WHERE " + cmd, conn);
                if (conn.State == ConnectionState.Closed) conn.Open();
                long rowcount = Convert.ToInt64(command.ExecuteScalar());
                if (conn.State == ConnectionState.Open) conn.Close();

                return rowcount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static IMuesPaging MuesSelect(Expression<Func<T, object>> expression)
        {
            string selectCommand = MuesMethods.GetWhereClauseSelect<T, object>(expression);

            MuesOrderByType t = new MuesOrderByType();
            t.Value = "";
            t.Expression = "";
            t.PagingValue = "";
            t.OrderByValue = "";
            t.SelectValue = selectCommand;
            return t;
        }

        public static object MuesIdentity()
        {
            try
            {
                string tname = typeof(T).Name;
                string query = "SELECT IDENT_CURRENT('" + MuesMethods.GetPluralTableName(tname) + "')";
                SqlConnection conn = new SqlConnection(DataAccess.Connection);
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                object currentid = dt.Rows[0][0];
                return currentid;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
