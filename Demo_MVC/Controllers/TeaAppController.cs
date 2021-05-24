using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_MVC.Controllers
{
    public class TeaAppController : Controller
    {
        Common comm = new Common();
        // GET: TeaApp
        public ActionResult Index()
        {
            return View();
        }
        public string GetTeacherApp(string kw)
        {
            try
            {
                string sql = "SELECT ID_,LinkName,LinkAddress,LinkImges,Remark,Status FROM [dbo].[Tea_B_wxConfigure]" +
                    " where FatherNodeID=1 and LinkName like '%'+@kw+'%' ORDER BY OrderNum,creatdate";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@kw", kw));
                SqlParameter[] sqlPar = parameters.ToArray();
                DataTable dt = comm.ReadDataTable(sql, sqlPar);
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                return "err@" + ex.Message;
            }
        }
        public string AddApp( string name, string link, string iconImg, string orderNum, string remark, string status)
        {
            try
            {
                string account = Session["code"].ToString();
                string sql = "INSERT INTO Tea_B_wxConfigure (ID_, FatherNodeID,LinkName,LinkAddress,LinkImges,Remark,OrderNum,Status,CreatOpet,CreatDate) " +
                    "VALUES(@ID_,@FatherNodeID,@LinkName,@LinkAddress,@LinkImges,@Remark,@OrderNum,@Status,@CreatOpet,GETDATE());";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ID_", Guid.NewGuid().ToString("N")));
                parameters.Add(new SqlParameter("@LinkName", name));
                parameters.Add(new SqlParameter("@LinkAddress", link));
                parameters.Add(new SqlParameter("@FatherNodeID", 1));
                parameters.Add(new SqlParameter("@LinkImges", iconImg));
                parameters.Add(new SqlParameter("@Remark", remark));
                parameters.Add(new SqlParameter("@OrderNum", orderNum));
                parameters.Add(new SqlParameter("@Status", status));
                parameters.Add(new SqlParameter("@CreatOpet", account));
                SqlParameter[] sqlPar = parameters.ToArray();
                if (comm.ExecSql(sql, sqlPar))
                {
                    return "success";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                return "err@" + ex.Message;
            }

        }
        public string ModifyApp(string ID, string name, string link, string iconImg, string orderNum, string remark, string status)
        {
            try
            {
                string sql = "UPDATE Tea_B_wxConfigure set LinkName=@LinkName, LinkAddress=@LinkAddress, LinkImges=@LinkImges, Remark=@Remark, OrderNum=@OrderNum, Status=@Status where ID_=@ID_";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ID_", ID));
                parameters.Add(new SqlParameter("@LinkName", name));
                parameters.Add(new SqlParameter("@LinkAddress", link));
                parameters.Add(new SqlParameter("@LinkImges", iconImg));
                parameters.Add(new SqlParameter("@Remark", remark));
                parameters.Add(new SqlParameter("@OrderNum", orderNum));
                parameters.Add(new SqlParameter("@Status", status));
                SqlParameter[] sqlPar = parameters.ToArray();
                if (comm.ExecSql(sql, sqlPar))
                {
                    return "success";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                return "err@" + ex.Message;
            }

        }
        public string GetAppInfo(string ID)
        {
            try
            {
                string sql = "SELECT ID_, LinkName, LinkAddress, LinkImges, Remark, Status,OrderNum FROM [dbo].[Tea_B_wxConfigure] where ID_ = @ID ";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ID", ID));
                SqlParameter[] sqlPar = parameters.ToArray();
                DataTable dt = comm.ReadDataTable(sql, sqlPar);
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                return "err@" + ex.Message;
            }
        }
        public string DeleteApp(string ID)
        {
            try
            {
                string sql = "Delete FROM [dbo].[Tea_B_wxConfigure] where ID_=@ID";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ID", ID));
                SqlParameter[] sqlPar = parameters.ToArray();
                if (comm.ExecSql(sql, sqlPar))
                {
                    return "success";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                return "err@" + ex.Message;
            }

        }
    }
}