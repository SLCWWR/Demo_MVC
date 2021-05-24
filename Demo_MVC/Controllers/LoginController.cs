using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace XSXK.Controllers
{

    public class LoginController : Controller
    {
        Common comm = new Common();
        // POST api/<controller>
        [HttpPost]
        public string getlogin(string longin_username, string longin_password)
        {
            string sql = " select 卡号,姓名,部门ID from DB_VB_教工信息 where 卡号 =@卡号  and 密码=@密码 ";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@卡号", longin_username));
            parameters.Add(new SqlParameter("@密码", longin_password));
            SqlParameter[] sqlPar = parameters.ToArray();
            DataTable dt = comm.ReadDataTable(sql, sqlPar);
            if (dt.Rows.Count > 0)
            {
                sql = " select count(1) count from XSKK_B_权限管理 where 卡号 =@卡号 ";
                if (comm.ReadDataTable(sql, sqlPar).Rows[0]["count"].ToString()== "0") {
                    dic.Add("code", "fail");
                    dic.Add("message", "您没有权限,请联系管理员");
                    return JsonConvert.SerializeObject(dic);
                }
                Session["code"] = dt.Rows[0]["卡号"].ToString();
                Session["userName"] = dt.Rows[0]["姓名"].ToString();
                Session["departmentId"] = dt.Rows[0]["部门ID"].ToString();
                dic.Add("code", "success");
                dic.Add("message", "index.html");
                dic.Add("userName", dt.Rows[0]["姓名"].ToString());
                return JsonConvert.SerializeObject(dic);    
            }
            else
            {
                dic.Add("code", "fail");
                dic.Add("message", "账号或密码错误");
                return JsonConvert.SerializeObject(dic);
            }
        }
        public string lout()
        {
            Session.Clear();
            return "login.html";
        }
    }
}