using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyExtension;
/// <summary>
/// Summary description for UserRoles
/// </summary>

public class UserRolesEntity
{

    public UserRolesEntity()
    {
        this.UserData = new UserEntity();
        this.RoleData = new RoleEntity();
    }

    public UserEntity UserData { get; set; }
    public RoleEntity RoleData { get; set; }

}
public class UserRolesBal
{
    I_DbHelper dbHelper = null;

    public UserRolesBal()
    {
        this.dbHelper = new SqlDbHelper();
    }


    private string getSql()
    {
        string sql = "";
        sql += " SELECT UserId , RoleId ";
        sql += " FROM webpages_UsersInRoles ";

        return sql;

    }

    private string getSqlQry()
    {
        string sql = "";
        sql += " SELECT     webpages_UsersInRoles.UserId , Users.UserName , webpages_UsersInRoles.RoleId , webpages_Roles.RoleName ";
        sql += " FROM       webpages_Roles RIGHT OUTER JOIN ";
        sql += " webpages_UsersInRoles ON webpages_Roles.RoleId = webpages_UsersInRoles.RoleId LEFT OUTER JOIN ";
        sql += " Users ON webpages_UsersInRoles.UserId = Users.UserId ";
        return sql;

    }


    private void SetData(DataRow Row, UserRolesEntity data)
    {
        data.UserData.UserId = Row["UserId"].ToString().ToIntVal();
        data.RoleData.RoleId = Row["RoleId"].ToString().ToIntVal();
    }
    private void SetQryData(DataRow Row, UserRolesEntity data)
    {
        data.UserData.UserName = Row["UserName"].ToString();
        data.RoleData.RoleName = Row["RoleName"].ToString();
    }

    public List<UserRolesEntity> GetList()
    {
        List<UserRolesEntity> lst = null;

        DataTable table;
        string sql = this.getSql();
        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<UserRolesEntity>();

            foreach (DataRow row in table.Rows)
            {
                UserRolesEntity data = new UserRolesEntity();
                this.SetData(row, data);

                lst.Add(data);
            }
        }
        return lst;
    }
    public List<UserRolesEntity> GetQryList()
    {
        List<UserRolesEntity> lst = null;

        DataTable table;
        string sql = getSqlQry();

        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<UserRolesEntity>();

            foreach (DataRow row in table.Rows)
            {
                UserRolesEntity data = new UserRolesEntity();
                this.SetData(row, data);
                this.SetQryData(row, data);
                lst.Add(data);
            }
        }
        return lst;
    }

    public List<UserRolesEntity> GetUserRolesQryList(int userId)
    {
        List<UserRolesEntity> lst = null;

        DataTable table;
        string sql = getSqlQry();
        sql += " WHERE webpages_UsersInRoles.UserId = " + userId + " ";


        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<UserRolesEntity>();

            foreach (DataRow row in table.Rows)
            {
                UserRolesEntity data = new UserRolesEntity();
                this.SetData(row, data);
                this.SetQryData(row, data);
                lst.Add(data);
            }
        }
        return lst;
    }
    public List<UserRolesEntity> GetUserRolesQryList(string userName)
    {
        List<UserRolesEntity> lst = null;

        DataTable table;
        string sql = getSqlQry();
        sql += " WHERE Users.UserName = '" + userName + "' ";


        table = this.dbHelper.retriveData(sql);

        if (table != null && table.Rows.Count > 0)
        {

            lst = new List<UserRolesEntity>();

            foreach (DataRow row in table.Rows)
            {
                UserRolesEntity data = new UserRolesEntity();
                this.SetData(row, data);
                this.SetQryData(row, data);
                lst.Add(data);
            }
        }
        return lst;
    }

    public int Delete(int userId, List<int> idlst)
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

            string sql = " DELETE FROM webpages_UsersInRoles  ";

            sql += " WHERE UserId = " + userId + " AND RoleId in (" + paramLst + ") ";
            x = this.dbHelper.whriteData(sql);
        }
        return x;
    }
}