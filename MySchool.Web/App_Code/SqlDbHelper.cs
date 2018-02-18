using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public interface I_DbHelper
{
    string GetConString();
    DataTable retriveData(string _SqlSelect);
    DataTable retriveData(string _SqlSelect, List<System.Data.IDataParameter> ParameterList);
    int whriteData(string _sqlWhrite);
    int whriteData(string _sqlWhrite, List<System.Data.IDataParameter> ParameterList);
    int whritTransData(string[] _sqlWhriteTrans);
    string GetNewId();
    string GetServerDate();
}




/// <summary>
/// Summary description for SqlDbHelper
/// </summary>
public class SqlDbHelper : I_DbHelper
{


    public static string CONNECTION_STRING = "";

    #region I_DbHelper Members


    private DataTable retriveData(string _SqlSelect)
    {
        DataTable dt = null;
        using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {

                cmd.CommandTimeout = 0;
                cmd.CommandText = _SqlSelect;

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        da.SelectCommand = cmd;

                        dt = new DataTable("Table1");
                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        dt = ds.Tables[0];

                    }
                }
                catch
                {
                    throw;
                }


            }
        }

        return dt;

    }

    string I_DbHelper.GetConString()
    {
        return CONNECTION_STRING;
    }

    DataTable I_DbHelper.retriveData(string _SqlSelect)
    {
        return this.retriveData(_SqlSelect);
    }

    DataTable I_DbHelper.retriveData(string _SqlSelect, List<IDataParameter> ParameterList)
    {
        DataTable dt = null;


        using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {

                cmd.CommandTimeout = 0;
                cmd.CommandText = _SqlSelect;

                if (ParameterList != null)
                {
                    for (int i = 0; i < ParameterList.Count; ++i)
                    {
                        cmd.Parameters.Add(ParameterList[i]);
                    }

                }



                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        da.SelectCommand = cmd;

                        dt = new DataTable("Table1");
                        DataSet ds = new DataSet();

                        da.Fill(ds);
                        dt = ds.Tables[0];

                    }
                }
                catch
                {
                    throw;
                }



            }
        }

        return dt;
    }

    int I_DbHelper.whriteData(string _sqlWhrite)
    {
        int RowCount = 0;
        using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {

                cmd.CommandText = _sqlWhrite;

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    RowCount = cmd.ExecuteNonQuery();


                }
                catch
                {
                    throw;
                }
            }
        }

        return RowCount;
    }

    int I_DbHelper.whriteData(string _sqlWhrite, List<System.Data.IDataParameter> ParameterList)
    {
        int RowCount = 0;
        using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {

                cmd.CommandText = _sqlWhrite;

                if (ParameterList != null)
                {
                    for (int i = 0; i < ParameterList.Count; ++i)
                    {
                        cmd.Parameters.Add(ParameterList[i]);
                    }

                }

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    RowCount = cmd.ExecuteNonQuery();

                }
                catch
                {
                    throw;
                }
            }
        }

        return RowCount;
    }

    int I_DbHelper.whritTransData(string[] _sqlWhriteTrans)
    {
        int RowCount = 0;
        using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {

                SqlTransaction transaction = null;

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    cmd.Transaction = transaction;

                    if (_sqlWhriteTrans != null)
                    {
                        for (int i = 0; i < _sqlWhriteTrans.Length; i++)
                        {
                            cmd.CommandText = _sqlWhriteTrans[i];
                            RowCount += cmd.ExecuteNonQuery();
                        }

                    }

                    transaction.Commit();



                }
                catch
                {

                    try
                    {
                        transaction.Rollback();
                        RowCount = 0;
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }

                }
            }






        }

        return RowCount;
    }

    string I_DbHelper.GetNewId()
    {
        string SqlNewId = null;

        DataTable table;
        string sql = "SELECT NEWID() AS NEWID";
        table = retriveData(sql);

        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            SqlNewId = row["NEWID"].ToString();
        }

        return SqlNewId;
    }

    string I_DbHelper.GetServerDate()
    {
        string SqlServerDate = null;

        DataTable table;
        string sql = "SELECT GetDate() AS CurrentDate";
        table = retriveData(sql);

        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            SqlServerDate = row["CurrentDate"].ToString();
        }

        return SqlServerDate;
    }

    #endregion





}