using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyExtension;
/// <summary>
/// Summary description for Person
/// </summary>


public class CatEntity
{

    public CatEntity()
    {
        this.DepartmentData = new global::DepartmentEntity();
    }

    public int Id { get; set; }
    public string Name_Ar { get; set; }
    public string Name_En { get; set; }

    public DepartmentEntity DepartmentData { get; set; }

    public string RowId { get; set; }

   
    public DateTime DateMade { get; set; }
    public int MadeById { get; set; }

    internal static GetDataByDelegate<CatEntity>.GetDataDelegate<int> delGetData = null;


    public bool GetData()
    {

        bool x = false;
        CatEntity data = delGetData(this.Id);

        if (!data.IsNull_Ex())
        {
            x = true;
            this.Id = data.Id;
            this.DateMade = data.DateMade;
          
            this.MadeById = data.MadeById; 
            this.Name_Ar = data.Name_Ar;
            this.Name_En = data.Name_En;
            this.DepartmentData.Id = data.DepartmentData.Id;   
            this.RowId = data.RowId;

        }
        return x;
    }


}
public class CatBal
{

    I_DbHelper dbHelper = null;

    public CatBal()
    {

        this.dbHelper = new SqlDbHelper();

        CatEntity.delGetData = this.GetData;
    }

    //GetData
    private string GetSql()
    {
        string sql = " SELECT Id , Name_Ar , Name_En , DepartmentId , RowId , MadeById , DateMade FROM Cat ";
        return sql;
    }
    private void SetData(DataRow Row, CatEntity data)
    {
        data.Id = Row["Id"].ToString().ToIntVal();
        data.Name_Ar = Row["Name_Ar"].ToString();
        data.Name_En = Row["Name_En"].ToString();
        data.DepartmentData.Id = Row["DepartmentId"].ToString().ToIntVal();
        data.RowId = Row["RowId"].ToString();
        data.DateMade = Row["DateMade"].ToString().ToDateVal();
       
        data.MadeById = Row["MadeById"].ToString().ToIntVal();

    }

    public int GetNewId()
    {
        DataTable table;
        string sql = " Select Max(Id) + 1 AS NewId From Cat ";
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
    public CatEntity GetData(int id)
    {
        CatEntity data = null;

        DataTable table;
        string sql = this.GetSql();
        sql += " WHERE [Id] = " + id + " ";

        table = this.dbHelper.retriveData(sql);


        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];

            data = new CatEntity();
            this.SetData(row, data);

        }

        return data;
    }
    public CatEntity GetData(string rowId)
    {
        CatEntity data = null;

        DataTable table;
        string sql = this.GetSql();
        sql += " WHERE RowId = '" + rowId + "' ";

        table = this.dbHelper.retriveData(sql);


        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];

            data = new CatEntity();
            this.SetData(row, data);

        }

        return data;
    }

    public List<CatEntity> GetList()
    {
        List<CatEntity> lst = null;

        DataTable table;
        string sql = this.GetSql();
        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<CatEntity>();

            foreach (DataRow row in table.Rows)
            {
                CatEntity data = new CatEntity();
                this.SetData(row, data);

                lst.Add(data);
            }
        }
        return lst;
    }
    public List<CatEntity> GetList(int departId)
    {
        List<CatEntity> lst = null;

        DataTable table;
        string sql = this.GetSql();
        sql += " WHERE  DepartmentId = " + departId + "";

        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<CatEntity>();

            foreach (DataRow row in table.Rows)
            {
                CatEntity data = new CatEntity();
                this.SetData(row, data);

                lst.Add(data);
            }
        }
        return lst;
    }


    //Update
    public string UpdateSql(int id, CatEntity data)
    {
        string sql = "UPDATE Cat SET   ";
        sql += " Name_Ar = '" + data.Name_Ar + "' ";
        sql += ",Name_En = '" + data.Name_En + "' ";
        sql += ",DepartmentId = " + data.DepartmentData.Id + " ";
        //---------------
        sql += ",DateMade = '" + this.dbHelper.GetServerDate().ToDateVal().FormatDate("MM/dd/yyyy HH:mm") + "'";
       
        sql += ",MadeById = " + data.MadeById + "";

        sql += " WHERE Id = " + id + " ";

        return sql;
    }

    public int Update(int id, CatEntity data)
    {
        string sql = this.UpdateSql(id, data);

        int x = this.dbHelper.whriteData(sql);
        return x;
    }

    //Delete
    internal string DeleteSql(int id)
    {
        string sql = "DELETE FROM Cat WHERE Id = " + id + " ";
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

            string sql = "DELETE FROM Cat  ";

            sql += " WHERE Id in (" + paramLst + ") ";
            x = this.dbHelper.whriteData(sql);
        }
        return x;
    }
    //Add
    internal string AddSql(CatEntity data)
    {
        string colNames = "";
        string colValue = "";

        colNames += " Id ";
        colValue += " " + this.GetNewId() + " ";

        colNames += ",Name_Ar";
        colValue += ",'" + data.Name_Ar + "'";

        colNames += ",Name_En";
        colValue += ",'" + data.Name_En + "'";

        colNames += ",DepartmentId";
        colValue += "," + data.DepartmentData.Id + "";

        colNames += ",RowId";
        colValue += ",'" + data.RowId + "'";

        colNames += ",DateMade";
        colValue += ",'" + this.dbHelper.GetServerDate().ToDateVal().FormatDate("MM/dd/yyyy HH:mm") + "'";
        
        colNames += ",MadeById";
        colValue += "," + data.MadeById + "";

        //-------------------------------
        string sql = " INSERT INTO Cat ( " + colNames + " ) VALUES ( " + colValue + " ) ";
        return sql;
    }

    public int Add(CatEntity data)
    {
        string sql = this.AddSql(data);
        int x = this.dbHelper.whriteData(sql);
        return x;
    }


}


