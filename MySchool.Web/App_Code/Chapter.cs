using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyExtension;
/// <summary>
/// Summary description for Person
/// </summary>


public class ChapterEntity
{

    public ChapterEntity()
    {
        this.BookPartData = new global::BookPartEntity();
    }

    public int Id { get; set; }
    public string Name_Ar { get; set; }
    public string Name_En { get; set; }

    public BookPartEntity BookPartData { get; set; }

    public string Cont { get; set; }

    public string RowId { get; set; }


    public DateTime DateMade { get; set; }
    public int MadeById { get; set; }

    internal static GetDataByDelegate<ChapterEntity>.GetDataDelegate<int> delGetData = null;


    public bool GetData()
    {

        bool x = false;
        ChapterEntity data = delGetData(this.Id);

        if (!data.IsNull_Ex())
        {
            x = true;
            this.Id = data.Id;
            this.DateMade = data.DateMade;

            this.MadeById = data.MadeById;
            this.Name_Ar = data.Name_Ar;
            this.Name_En = data.Name_En;
            this.BookPartData.Id = data.BookPartData.Id;
            this.Cont = data.Cont;
            this.RowId = data.RowId;

        }
        return x;
    }


}
public class ChapterBal
{

    I_DbHelper dbHelper = null;

    public ChapterBal()
    {

        this.dbHelper = new SqlDbHelper();

        ChapterEntity.delGetData = this.GetData;
    }

    //GetData
    private string GetSql()
    {
        string sql = " SELECT Id , Name_Ar , Name_En , PartId, Cont , RowId , MadeById , DateMade FROM Chapters ";
        return sql;
    }
    private void SetData(DataRow Row, ChapterEntity data)
    {
        data.Id = Row["Id"].ToString().ToIntVal();
        data.Name_Ar = Row["Name_Ar"].ToString();
        data.Name_En = Row["Name_En"].ToString();
        data.BookPartData.Id = Row["PartId"].ToString().ToIntVal();
        data.Cont = Row["Cont"].ToString();
        data.RowId = Row["RowId"].ToString();
        data.DateMade = Row["DateMade"].ToString().ToDateVal();

        data.MadeById = Row["MadeById"].ToString().ToIntVal();

    }

    public int GetNewId()
    {
        DataTable table;
        string sql = " Select Max(Id) + 1 AS NewId From Chapters ";
        table = dbHelper.retriveData(sql);

        int newId = 0;
        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            if (row["NewId"] == null || row["NewId"].ToString().IsStrNullOrEmpty())
            {
                newId = 1;
            }
            else
            {
                newId = row["NewId"].ToString().ToIntVal();
            }
        }
        else
        {
            newId = 1;
        }

        return newId;
    }
    public ChapterEntity GetData(int id)
    {
        ChapterEntity data = null;

        DataTable table;
        string sql = this.GetSql();
        sql += " WHERE [Id] = " + id + " ";

        table = this.dbHelper.retriveData(sql);


        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];

            data = new ChapterEntity();
            this.SetData(row, data);

        }

        return data;
    }
    public ChapterEntity GetData(string rowId)
    {
        ChapterEntity data = null;

        DataTable table;
        string sql = this.GetSql();
        sql += " WHERE RowId = '" + rowId + "' ";

        table = this.dbHelper.retriveData(sql);


        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];

            data = new ChapterEntity();
            this.SetData(row, data);

        }

        return data;
    }

    public List<ChapterEntity> GetList()
    {
        List<ChapterEntity> lst = null;

        DataTable table;
        string sql = this.GetSql();
        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<ChapterEntity>();

            foreach (DataRow row in table.Rows)
            {
                ChapterEntity data = new ChapterEntity();
                this.SetData(row, data);

                lst.Add(data);
            }
        }
        return lst;
    }
    public List<ChapterEntity> GetList(int partId)
    {
        List<ChapterEntity> lst = null;

        DataTable table;
        string sql = this.GetSql();
        sql += " WHERE PartId = " + partId + " ";

        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<ChapterEntity>();

            foreach (DataRow row in table.Rows)
            {
                ChapterEntity data = new ChapterEntity();
                this.SetData(row, data);

                lst.Add(data);
            }
        }
        return lst;
    }

    //Update
    public string UpdateSql(int id, ChapterEntity data)
    {
        string sql = "UPDATE Chapters SET   ";
        sql += " Name_Ar = @Name_Ar ";
        sql += ",Name_En = @Name_En ";
        sql += ",PartId = " + data.BookPartData.Id + " ";
        sql += ",Cont = @Cont ";
        //---------------
        sql += ",DateMade = '" + this.dbHelper.GetServerDate().ToDateVal().FormatDate("MM/dd/yyyy HH:mm") + "'";

        sql += ",MadeById = " + data.MadeById + "";

        sql += " WHERE Id = " + id + " ";

        return sql;
    }

    public int Update(int id, ChapterEntity data)
    {
        string sql = this.UpdateSql(id, data);

        System.Data.SqlClient.SqlParameter NameArParam = new System.Data.SqlClient.SqlParameter();
        NameArParam.ParameterName = "Name_Ar";
        NameArParam.DbType = DbType.String;
        NameArParam.Direction = ParameterDirection.Input;
        NameArParam.Value = data.Name_Ar;

        System.Data.SqlClient.SqlParameter NameEnParam = new System.Data.SqlClient.SqlParameter();
        NameEnParam.ParameterName = "Name_En";
        NameEnParam.DbType = DbType.String;
        NameEnParam.Direction = ParameterDirection.Input;
        NameEnParam.Value = data.Name_En;


        System.Data.SqlClient.SqlParameter ContParam = new System.Data.SqlClient.SqlParameter();
        ContParam.ParameterName = "Cont";
        ContParam.DbType = DbType.String;
        ContParam.Direction = ParameterDirection.Input;
        ContParam.Value = data.Cont;

        int x = this.dbHelper.whriteData(sql, new List<IDataParameter>() { ContParam ,NameArParam ,NameEnParam });
        return x;
    }

    //Delete
    internal string DeleteSql(int id)
    {
        string sql = "DELETE FROM Chapters WHERE Id = " + id + " ";
        return sql;
    }

    public int Delete(int id)
    {
        string sql = this.DeleteSql(id);
        int x = this.dbHelper.whriteData(sql);
        return x;
    }
    public int Delete(List<int> idlst)
    {
        int x = 0;
        if (idlst != null)
        {
            string paramLst = "";

            for (int i = 0; i < idlst.Count; i++)
            {
                if (i != idlst.Count - 1)
                {
                    paramLst += idlst[i].ToString() + ",";
                }
                else
                {
                    paramLst += idlst[i].ToString();
                }

            }

            string sql = "DELETE FROM Chapters  ";

            sql += " WHERE Id in (" + paramLst + ") ";
            x = this.dbHelper.whriteData(sql);
        }
        return x;
    }
    //Add
    internal string AddSql(ChapterEntity data)
    {
        string colNames = "";
        string colValue = "";

        colNames += " Id ";
        colValue += " " + this.GetNewId() + " ";

        colNames += ",Name_Ar";
        colValue += ",@Name_Ar";

        colNames += ",Name_En";
        colValue += ",@Name_En";

        colNames += ",PartId";
        colValue += "," + data.BookPartData.Id + "";

        colNames += ",Cont";
        colValue += ",@Cont";

        colNames += ",RowId";
        colValue += ",'" + data.RowId + "'";

        colNames += ",DateMade";
        colValue += ",'" + this.dbHelper.GetServerDate().ToDateVal().FormatDate("MM/dd/yyyy HH:mm") + "'";

        colNames += ",MadeById";
        colValue += "," + data.MadeById + "";

        //-------------------------------
        string sql = " INSERT INTO Chapters ( " + colNames + " ) VALUES ( " + colValue + " ) ";
        return sql;
    }

    public int Add(ChapterEntity data)
    {
        string sql = this.AddSql(data);

        System.Data.SqlClient.SqlParameter NameArParam = new System.Data.SqlClient.SqlParameter();
        NameArParam.ParameterName = "Name_Ar";
        NameArParam.DbType = DbType.String;
        NameArParam.Direction = ParameterDirection.Input;
        NameArParam.Value = data.Name_Ar;

        System.Data.SqlClient.SqlParameter NameEnParam = new System.Data.SqlClient.SqlParameter();
        NameEnParam.ParameterName = "Name_En";
        NameEnParam.DbType = DbType.String;
        NameEnParam.Direction = ParameterDirection.Input;
        NameEnParam.Value = data.Name_En;

        System.Data.SqlClient.SqlParameter ContParam = new System.Data.SqlClient.SqlParameter();
        ContParam.ParameterName = "Cont";
        ContParam.DbType = DbType.String;
        ContParam.Direction = ParameterDirection.Input;
        ContParam.Value = data.Cont;

        int x = this.dbHelper.whriteData(sql, new List<IDataParameter>() { ContParam,NameArParam ,NameEnParam });
        return x;
    }


}


