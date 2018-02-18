using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyExtension;

/// <summary>
/// Summary description for Users
/// </summary>

public class UserEntity
{
    public UserEntity()
    {
       
    }

    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
   

    internal static GetDataByDelegate<UserEntity>.GetDataDelegate<int> delGetData = null;


    public bool GetData()
    {

        bool x = false;
        UserEntity data = delGetData(this.UserId);

        if (!data.IsNull_Ex())
        {
            x = true;
            this.UserId = data.UserId;
            
            this.UserName = data.UserName;
            this.Email = data.Email;

        }
        return x;
    }


}
public class UserBal
{
    I_DbHelper dbHelper = null;

    public UserBal()
    {
        this.dbHelper = new SqlDbHelper();
        UserEntity.delGetData = this.GetData;
    }

    //---------Get Data
    private string getSql()
    {
        string sql = "";
        sql += " SELECT UserId ,UserName,Email ";
        sql += " FROM Users ";

        return sql;

    }

    private void SetData(DataRow Row, UserEntity data)
    {
        data.UserId = Row["UserId"].ToString().ToIntVal();
        data.UserName = Row["UserName"].ToString();
        data.Email = Row["Email"].ToString();
        
    }

    public UserEntity GetData(int userId)
    {
        UserEntity data = null;

        DataTable table;
        string sql = this.getSql();
        sql += " WHERE UserId = " + userId + " ";

        table = this.dbHelper.retriveData(sql);


        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];

            data = new UserEntity();
            this.SetData(row, data);

        }

        return data;
    }

   
    
    

    public UserEntity GetData(string userName)
    {
        UserEntity data = null;

        DataTable table;
        string sql = this.getSql();
        sql += " WHERE UserName = '" + userName + "' ";

        table = this.dbHelper.retriveData(sql);


        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];

            data = new UserEntity();
            this.SetData(row, data);

        }

        return data;
    }

    public List<UserEntity> GetList(string currentUserName)
    {
        List<UserEntity> lst = null;

        DataTable table;
        string sql = this.getSql();
        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<UserEntity>();

            foreach (DataRow row in table.Rows)
            {
                UserEntity data = new UserEntity();
                this.SetData(row, data);

                lst.Add(data);
            }

            if (currentUserName.ToLower() != "control")
            {
                lst = (from u in lst where u.UserName.ToLower() != "control" select u).ToList();
            }


        }
        return lst;
    }

    //----------Get User Profile Url
    public string GetUserProfileUrl(int userId)
    {
        string s = null;

        DataTable table;
        string sql = " SELECT ProfileUrl FROM Users ";
        sql += " WHERE UserId = " + userId + " ";

        table = this.dbHelper.retriveData(sql);


        if (table != null && table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];

            s = row["ProfileUrl"].ToString();

        }

        return s;
    }




    //Update
    public string UpdateSql(int id, UserEntity data)
    {
        string sql = "UPDATE Users SET   ";
        sql += " UserName = '" + data.UserName + "' ";
       
        sql += ",Email = '" + data.Email + "'";

        sql += " WHERE UserId = " + id + " ";

        return sql;
    }

    public int Update(int id, UserEntity data)
    {
        string sql = this.UpdateSql(id, data);

        int x = this.dbHelper.whriteData(sql);
        return x;
    }
    public int UpdateUserProfileUrl(int userId, string profileUrl)
    {
        string sql = " UPDATE Users SET   ";
        sql += " ProfileUrl = '" + profileUrl + "'";
        sql += " WHERE UserId = " + userId + " ";

        int x = this.dbHelper.whriteData(sql);
        return x;
    }

}



