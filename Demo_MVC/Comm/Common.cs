using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;

/// <summary>
/// XIN 的摘要说明
/// </summary>
public class Common
{
    public Common()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }

    public static SqlConnection createConn()
    {
        return new SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["cConnString"]);
 
    }

    #region 打开新窗口
    /// <summary>
    /// 打开新窗口
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="url">转向页</param>
    public void openPage(System.Web.UI.Page page,string url)
    {
        try
        {
            page.Response.Redirect(url);
        }
        catch (Exception E)
        {
        }
    }
    #endregion

    #region 判断textbox是否为空,　返回true时为空, 返回false时不为空;,0判断空值,!=0则不判断空值只判断长度
    /// <summary>
    /// 判断textbox是否为空,　返回true时为空, 返回false时不为空;,0判断空值,!=0则不判断空值只判断长度
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="nullType">是否判断为空,0判断是否为空,!=0则不判断空值</param>
    /// <param name="dataLen">长度:0时不判断长度</param>
    /// <returns></returns>
    public bool txtNull(TextBox tb,int nullType ,int dataLen)
    {
        try
        {
            string[] txt_name = tb.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (nullType == 0)
            {
                if (tb.Text.Trim() == "")
                {
                    tb.BackColor = System.Drawing.Color.Red;
                    HttpContext.Current.Response.Write("<script>alert('[" + txt_name[1].ToString() + "]不能为空！');</script>");
                    return true;
                }
                else
                {
                    if (dataLen != 0)
                    {
                        if (tb.Text.Length > dataLen)
                        {
                            tb.BackColor = System.Drawing.Color.Pink;
                            HttpContext.Current.Response.Write("<script>alert('[" + txt_name[1].ToString() + "]长度过长,请输入的数据长度小于" + dataLen.ToString().Trim() + "！');</script>");
                            return true;
                        }
                        else
                        {
                            tb.BackColor = System.Drawing.Color.Empty;
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (dataLen != 0)
                {
                    if (tb.Text.Length > dataLen)
                    {
                        tb.BackColor = System.Drawing.Color.Pink;
                        HttpContext.Current.Response.Write("<script>alert('[" + txt_name[1].ToString() + "]长度过长,请输入的数据长度小于" + dataLen.ToString().Trim() + "！');</script>");
                        return true;
                    }
                    else
                    {
                        tb.BackColor = System.Drawing.Color.Empty;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception E)
        {
            return true;
        }
    }
    #endregion

    #region this 判断textbox是否为空,　返回true时为空, 返回false时不为空;,0判断空值,!=0则不判断空值只判断长度
    /// <summary>
    /// 判断textbox是否为空,　返回true时为空, 返回false时不为空;,0判断空值,!=0则不判断空值只判断长度
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="nullType">是否判断为空,0判断是否为空,!=0则不判断空值</param>
    /// <param name="dataLen">长度:0时不判断长度</param>
    /// <returns></returns>
    public bool txtNull_Page( Page page, TextBox tb, int nullType, int dataLen)
    {
        try
        {
            string[] txt_name = tb.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (nullType == 0)
            {
                if (tb.Text.Trim() == "")
                {
                    tb.BackColor = System.Drawing.Color.Red;
                    page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + txt_name[1].ToString() + "] 不能为空!');</script>");
                    return true;
                }
                else
                {
                    if (dataLen != 0)
                    {
                        if (tb.Text.Length > dataLen)
                        {
                            tb.BackColor = System.Drawing.Color.Pink;
                            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + txt_name[1].ToString() + "]长度过长,请输入的数据长度小于" + dataLen.ToString().Trim() + "!');</script>");
                            return true;
                        }
                        else
                        {
                            tb.BackColor = System.Drawing.Color.Empty;
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (dataLen != 0)
                {
                    if (tb.Text.Length > dataLen)
                    {
                        tb.BackColor = System.Drawing.Color.Pink;
                        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + txt_name[1].ToString() + "]长度过长,请输入的数据长度小于" + dataLen.ToString().Trim() + "!');</script>");
                        return true;
                    }
                    else
                    {
                        tb.BackColor = System.Drawing.Color.Empty;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception E)
        {
            return true;
        }
    }
    #endregion

    #region 判断TextBox是否为整型,返回false则通过验证,返回true则不通过验证
    /// <summary>
    /// 判断是否为整型,返回false则通过验证,返回true则不通过验证
    /// </summary>
    /// <param name="tb">控件名称</param>
    /// <param name="dataType">数据类型,1整型, 2浮点,</param>
    /// <returns>返回false则通过验证,返回true则不通过验证</returns>
    public bool txtDataType(TextBox tb,int dataType)
    {      
        //Regex re = new Regex(@"^[0-9]+$");
        string[] txt_name = tb.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        bool returnbool = false;
        if (dataType == 1)
        {            
            Regex re = new Regex(@"^-?\d+$");
            if (!re.IsMatch(tb.Text))
            {
                tb.BackColor = System.Drawing.Color.PowderBlue;
                HttpContext.Current.Response.Write("<script>alert('[" + txt_name[1].ToString() + "]数据类型不正确,只接受整型数据！');</script>");
                returnbool = true;
            }
            else
            {
                tb.BackColor = System.Drawing.Color.Empty;
                returnbool = false;
            }
        }
        if (dataType == 2)
        {
            Regex re = new Regex(@"^([0-9]+|(\d+(\.\d+)+))$");
            if (!re.IsMatch(tb.Text))
            {
                tb.BackColor = System.Drawing.Color.PowderBlue;
                HttpContext.Current.Response.Write("<script>alert('[" + txt_name[1].ToString() + "]数据类型不正确,浮点型！');</script>");
                returnbool = true;
            }
            else
            {
                tb.BackColor = System.Drawing.Color.Empty;
                returnbool = false;
            }
        }
        return returnbool;
    }
    #endregion

    #region 判断是否为整型,返回false则通过验证,返回true则不通过验证
    /// <summary>
    /// 判断是否为整型,返回false则通过验证,返回true则不通过验证
    /// </summary>
    /// <param name="tb">控件名称</param>
    /// <param name="dataType">数据类型,1整型, 2浮点,</param>
    /// <returns>返回false则通过验证,返回true则不通过验证</returns>
    public bool txtDataType(Page page, TextBox tb, int dataType)
    {
        //Regex re = new Regex(@"^[0-9]+$");
        string[] txt_name = tb.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        bool returnbool = false;
        if (dataType == 1)
        {
            Regex re = new Regex(@"^-?\d+$");
            if (!re.IsMatch(tb.Text))
            {
                tb.BackColor = System.Drawing.Color.PowderBlue;
                //HttpContext.Current.Response.Write("<script>alert('[" + txt_name[1].ToString() + "]数据类型不正确,只接受整型数据！');</script>");
                page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + txt_name[1].ToString() + "]数据类型不正确,只接受整型数据!');</script>");
                returnbool = true;
            }
            else
            {
                tb.BackColor = System.Drawing.Color.Empty;
                returnbool = false;
            }
        }
        if (dataType == 2)
        {
            Regex re = new Regex(@"^([0-9]+|(\d+(\.\d+)+))$");
            if (!re.IsMatch(tb.Text))
            {
                tb.BackColor = System.Drawing.Color.PowderBlue;
                //HttpContext.Current.Response.Write("<script>alert('[" + txt_name[1].ToString() + "]数据类型不正确,浮点型！');</script>");
                page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + txt_name[1].ToString() + "]数据类型不正确,浮点型!');</script>");
                returnbool = true;
            }
            else
            {
                tb.BackColor = System.Drawing.Color.Empty;
                returnbool = false;
            }
        }
        return returnbool;
    }
    #endregion

    #region 显示消息提示对话框
    /// <summary>
    /// 显示消息提示对话框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <returns></returns>
    public void showMessage(System.Web.UI.Page page, string msg)
    {
        try
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('" + msg.ToString().Trim() + "');</script>");
            //page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('" + msg.ToString().Trim() + "');</script>");
            //page.Response.Write("<script>alert('" + msg.ToString().Trim() + "');</script>");
        }
        catch (Exception E)
        {
            
        }
    }
    #endregion

    #region 显示消息提示对话框,并转向新页
    /// <summary>
    /// 显示消息提示对话框,并转向新页
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">转向页</param>
    /// <returns></returns>
    public void showMessageTrunPage(System.Web.UI.Page page, string msg,string url)
    {
        try
        {            
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('" + msg.ToString().Trim() + "');window.location.href ='" + url + "'</script>");
        }
        catch (Exception E)
        {
        }
    }
    #endregion

    #region 读取数据返回datatable,入参查询语句sql
    /// <summary>
    /// 读取数据返回datatable,入参查询语句sql
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <returns></returns>
    public DataTable ReadDataTable(string sql)
    {
        DataTable dt = new DataTable();
        SqlConnection Conn = createConn();
        SqlCommand cmd = new SqlCommand(sql, Conn);
        try
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //Conn.Close();
            if (ds != null && ds.Tables.Count > 0)
            {
                //return ds.Tables[0];
                dt = ds.Tables[0];
            }             
        }
        catch (Exception E)
        {

        }
        finally
        {
            Conn.Close();
            cmd.Dispose();
        }
        return dt;
    }
    #endregion

    #region  读取数据返回datatable,入参查询语句sql,数据源为自定义
    /// <summary>
    /// 读取数据返回datatable,入参查询语句sql,数据源为自定义
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <returns></returns>
    public DataTable ReadDataTable(string sql, SqlConnection Conn)
    {      
        SqlCommand cmd = new SqlCommand(sql, Conn);
        try
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Conn.Close();
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        catch (Exception E)
        {
            return null;
        }
    }
    #endregion


    #region 执行DataTable中的查询返回新的DataTable
    /// <summary>
    /// 执行DataTable中的查询返回新的DataTable
    /// </summary>
    /// <param name="dt">源数据DataTable</param>
    /// <param name="condition">查询条件,系统模块='转发区域日志_卫生情况处理'</param>
    /// <returns></returns>
    public DataTable ReadDataTable_getSelect(DataTable dt, string condition)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }
    #endregion

    #region 执行DataTable中的distinct查询返回新的DataTable
    /// <summary>
    /// DataTable 返回 distinct keyFields
    /// </summary>
    /// <param name="SourceTable"></param>
    /// <param name="keyFields"></param>
    /// <param name="orderType">排序 "Asc" , "desc" ,"" </param>
    /// <returns>datatable</returns>
    public DataTable ReadDataTable_SelectDistinct(DataTable SourceTable, string keyFields, string orderType)
    {
        DataTable dtRet = SourceTable.DefaultView.ToTable(true, new string[] { keyFields });
        if (orderType != "")
        {
            dtRet.DefaultView.Sort = keyFields + " " + orderType;
        }
        dtRet = dtRet.DefaultView.ToTable();
        return dtRet;
    }
    #endregion

    #region 读取数据返回datatable,入参查询语句sql,  动态参数SqlParameter
    /// <summary>
    /// 读取数据返回datatable,入参查询语句sql,  动态参数SqlParameter
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="sqlPara">SqlParameter</param>
    /// <returns></returns>
    public DataTable ReadDataTable(string sql, SqlParameter[] sqlPara)
    {
        DataTable dt = new DataTable();
        SqlConnection Conn = createConn();
        SqlCommand cmd = new SqlCommand(sql, Conn);
        if (sqlPara != null)
            cmd.Parameters.AddRange(sqlPara);

        try
        {
            Conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
        }
        catch (Exception E)
        {

        }
        finally
        {
            Conn.Close();
            cmd.Parameters.Clear();
            cmd.Dispose();
        }
        return dt;
    }
    #endregion


    #region 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// <summary>
    /// 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// </summary>
    /// <param name="drp_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'</param>
    public void drlBind(DropDownList drp_list, string ItemText, string ItemValue, string sql,int type)
    {
        drp_list.Items.Clear();
        if (type == 1)
        {
        }
        if (type == 2)
        {
            drp_list.Items.Add(new ListItem("", ""));
        }
        if (type == 3)
        {
            drp_list.Items.Add(new ListItem("---请选择---", "---请选择---"));
        }
       
        DataTable dt = this.ReadDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drp_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
    }
    #endregion

    #region 绑定DropDownList,入参datatable,类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// <summary>
    /// 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// </summary>
    /// <param name="drp_list">控件名称</param>
    /// <param name="DataTable">DataTable</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'</param>
    public void drlBind(DropDownList drp_list, DataTable dt, string ItemText, string ItemValue,  int type)
    {
        drp_list.Items.Clear();
        if (type == 1)
        {
        }
        if (type == 2)
        {
            drp_list.Items.Add(new ListItem("", ""));
        }
        if (type == 3)
        {
            drp_list.Items.Add(new ListItem("---请选择---", "---请选择---"));
        }

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drp_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
    }
    #endregion

    #region 绑定DropDownList,入参datatable,类型：1实绑定, 2第一项为空, 3第一项为'---请选择---', 4第一项自定义
    /// <summary>
    /// 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---', 4第一项自定义
    /// </summary>
    /// <param name="drp_list">控件名称</param>
    /// <param name="DataTable">DataTable</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：1实绑定, 2第一项为空, 3第一项为'---请选择---',4第一项自定义</param>
    /// <param name="sql">查询语句</param>
    public void drlBind(DropDownList drp_list, DataTable dt, string ItemText, string ItemValue, int type, string toopTip)
    {
        drp_list.Items.Clear();
        if (type == 1)
        {
        }
        if (type == 2)
        {
            drp_list.Items.Add(new ListItem("", ""));
        }
        if (type == 3)
        {
            drp_list.Items.Add(new ListItem("---请选择---", "---请选择---"));
        }
        if (type == 4)
        {
            drp_list.Items.Add(new ListItem(toopTip, toopTip));
        }
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drp_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
    }
    #endregion

    #region 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---', 4自定义选项
    /// <summary>
    /// 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// </summary>
    /// <param name="drp_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'</param>
    /// <param name="toopTip">提示</param>
    public void drlBind(DropDownList drp_list, string ItemText, string ItemValue, string sql, int type, string toopTip)
    {
        drp_list.Items.Clear();
        if (type == 1)
        {
        }
        if (type == 2)
        {
            drp_list.Items.Add(new ListItem("", ""));
        }
        if (type == 3)
        {
            drp_list.Items.Add(new ListItem("---请选择---", "---请选择---"));
        }
        if (type == 4)
        {
            drp_list.Items.Add(new ListItem(toopTip, toopTip));
        }

        DataTable dt = this.ReadDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drp_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
    }
    #endregion

    #region 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---', 4自定义选项
    /// <summary>
    /// 绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// </summary>
    /// <param name="drp_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'</param>
    /// <param name="toopTip">提示</param>
    /// <param name="SqlParameter">参数</param>
    public void drlBind(DropDownList drp_list, string ItemText, string ItemValue, string sql, int type, string toopTip, SqlParameter[] sqlPara)
    {
        drp_list.Items.Clear();
        if (type == 1)
        {
        }
        if (type == 2)
        {
            drp_list.Items.Add(new ListItem("", ""));
        }
        if (type == 3)
        {
            drp_list.Items.Add(new ListItem("---请选择---", "---请选择---"));
        }
        if (type == 4)
        {
            drp_list.Items.Add(new ListItem(toopTip, toopTip));
        }

        DataTable dt = this.ReadDataTable(sql, sqlPara);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drp_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
    }
    #endregion

    #region 判断DropDownList是否选择,返回false通过验证, 返回true不通过验证;类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// <summary>
    /// 判断DropDownList是否选择,返回false通过验证, 返回true不通过验证;类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// </summary>
    /// <param name="drp_list">控件名称</param>
    /// <param name="type">类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'</param>
    /// <returns></returns>
    public bool drlNull(DropDownList drp_list,int type)
    {
        bool returntype = false;
        try
        {           
            string[] drl_name = drp_list.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (type == 1)
            {
                returntype = false;
            }
            if (type == 2)
            {
                if (drp_list.SelectedValue.Trim() == "")
                {
                    drp_list.ForeColor = System.Drawing.Color.Red;
                    HttpContext.Current.Response.Write("<script>alert('[" + drl_name[1].ToString() + "] 不能为空请先选择内容!');</script>");
                    returntype = true;
                }
                else
                {
                    drp_list.ForeColor = System.Drawing.Color.Empty;
                    returntype = false;
                }
            }
            if (type == 3)
            {
                if (drp_list.SelectedValue.Trim() == "---请选择---")
                {
                    drp_list.ForeColor = System.Drawing.Color.Red;
                    HttpContext.Current.Response.Write("<script>alert('[" + drl_name[1].ToString() + "] 请先选择内容!');</script>");
                    returntype = true;
                }
                else
                {
                    drp_list.ForeColor = System.Drawing.Color.Empty;
                    returntype = false;
                }
            }
        }
        catch (Exception E)
        {
            returntype = true;
        }
        finally
        {           
        }
        return returntype;
    }
    #endregion

    #region 判断DropDownList是否选择,返回false通过验证, 返回true不通过验证;类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// <summary>
    /// 判断DropDownList是否选择,返回false通过验证, 返回true不通过验证;类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'
    /// </summary>
    /// <param name="page">页面</param>
    /// <param name="drp_list">控件名称</param>
    /// <param name="type">类型：1实绑定, 2第一项为空, 3第一项为'---请选择---'</param>
    /// <returns></returns>
    public bool drlNull_Page( Page page, DropDownList drp_list, int type)
    {
        bool returntype = false;
        try
        {
            string[] drl_name = drp_list.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (type == 1)
            {
                returntype = false;
            }
            if (type == 2)
            {
                if (drp_list.SelectedValue.Trim() == "")
                {
                    drp_list.ForeColor = System.Drawing.Color.Red;                  
                    page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + drl_name[1].ToString() + "] 不能为空请先选择内容!');</script>");
                    
                    returntype = true;
                }
                else
                {
                    drp_list.ForeColor = System.Drawing.Color.Empty;
                    returntype = false;
                }
            }
            if (type == 3)
            {
                if (drp_list.SelectedValue.Trim() == "---请选择---")
                {
                    drp_list.ForeColor = System.Drawing.Color.Red;                   
                    page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + drl_name[1].ToString() + "] 不能为空请先选择内容!');</script>");
                    returntype = true;
                }
                else
                {
                    drp_list.ForeColor = System.Drawing.Color.Empty;
                    returntype = false;
                }
            }
        }
        catch (Exception E)
        {
            returntype = true;
        }
        finally
        {
        }
        return returntype;
    }
    #endregion

    #region 绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项
    /// <summary>
    /// 绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项
    /// </summary>
    /// <param name="rad_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项</param>
    public void radBind(RadioButtonList  rad_list, string ItemText, string ItemValue, string sql, int type)
    {
        rad_list.Items.Clear();       

        DataTable dt = this.ReadDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
        if (type == 1)
        {
            rad_list.Items[0].Selected = true;
        }
    }
    #endregion

    #region 绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项
    /// <summary>
    /// 绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项
    /// </summary>
    /// <param name="rad_list">控件名称</param>
    /// <param name="DataTable">数据表datatable</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
     /// <param name="type">类型：绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项</param>
    public void radBind(RadioButtonList rad_list,DataTable dt, string ItemText, string ItemValue, int type)
    {
        rad_list.Items.Clear();        
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
        if (type == 1)
        {
            rad_list.Items[0].Selected = true;
        }
    }
    #endregion

    #region 绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项
    /// <summary>
    /// 绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项
    /// </summary>
    /// <param name="rad_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="DataTable">查询语句</param>
    /// <param name="type">类型：绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项</param>
    public void radBindFromDatatable(RadioButtonList rad_list, string ItemText, string ItemValue, DataTable dt, int type)
    {
        rad_list.Items.Clear();
       
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
        if (type == 1)
        {
            rad_list.Items[0].Selected = true;
        }
    }
    #endregion

    #region 绑定CheckBoxList类型：0没有默认选中值, 1默认选中值第一项
    /// <summary>
    /// 绑定CheckBoxList类型：0没有默认选中值, 1默认选中值第一项
    /// </summary>
    /// <param name="chb_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项</param>
    public void chbBind(CheckBoxList chb_list, string ItemText, string ItemValue, string sql, int type)
    {
        chb_list.Items.Clear();

        DataTable dt = this.ReadDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chb_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
        if (type == 1)
        {
            chb_list.Items[0].Selected = true;
        }
    }
    #endregion

    #region 绑定CheckBoxList类型：0没有默认选中值, 1默认选中值第一项
    /// <summary>
    /// 绑定CheckBoxList类型：0没有默认选中值, 1默认选中值第一项
    /// </summary>
    /// <param name="chb_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="sql">查询语句</param>
    /// <param name="type">类型：绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项</param>
    public void chbBind(CheckBoxList chb_list,DataTable dt, string ItemText, string ItemValue, int type)
    {
        chb_list.Items.Clear();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chb_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
        if (type == 1)
        {
            chb_list.Items[0].Selected = true;
        }
    }
    #endregion

    #region 绑定CheckBoxList类型：0没有默认选中值, 1默认选中值第一项
    /// <summary>
    /// 绑定CheckBoxList类型：0没有默认选中值, 1默认选中值第一项
    /// </summary>
    /// <param name="chb_list">控件名称</param>
    /// <param name="ItemText">显示值</param>
    /// <param name="ItemValue">主键</param>
    /// <param name="DataTable">查询语句</param>
    /// <param name="type">类型：绑定RadioButtonList类型：0没有默认选中值, 1默认选中值第一项</param>
    public void chbBindFromDatatable(CheckBoxList chb_list, string ItemText, string ItemValue, DataTable dt, int type)
    {
        chb_list.Items.Clear();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chb_list.Items.Add(new ListItem(dt.Rows[i][ItemText].ToString().Trim(), dt.Rows[i][ItemValue].ToString().Trim()));
            }
        }
        if (type == 1)
        {
            chb_list.Items[0].Selected = true;
        }
    }
    #endregion

    #region 判断RadioButtonList是否选择,返回false通过验证, 返回true不通过验证
    /// <summary>
    /// 判断RadioButtonList是否选择,返回false通过验证, 返回true不通过验证
    /// </summary>
    /// <param name="drp_list">控件名称</param>  
    /// <returns></returns>
    public bool radNull(RadioButtonList rad_list)
    {
        bool returntype = false;
        try
        {
            string[] ral_name = rad_list.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (rad_list.SelectedValue.Trim() == "")
            {
                rad_list.ForeColor = System.Drawing.Color.Red;
                HttpContext.Current.Response.Write("<script>alert('[" + ral_name[1].ToString() + "] 不能为空请先选择内容!');</script>");
                returntype = true;
            }
            else
            {
                rad_list.ForeColor = System.Drawing.Color.Empty;
                returntype = false;
            }                  
        }
        catch (Exception E)
        {
            returntype = true;
        }
        finally
        {
        }
        return returntype;
    }
    #endregion

    #region 判断RadioButtonList是否选择,返回false通过验证, 返回true不通过验证
    /// <summary>
    /// 判断RadioButtonList是否选择,返回false通过验证, 返回true不通过验证
    /// </summary>
    /// <param name="page">this</param>  
    /// <param name="drp_list">控件名称</param>  
    /// <returns></returns>
    public bool radNull(Page page, RadioButtonList rad_list)
    {
        bool returntype = false;
        string[] ral_name = rad_list.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        try
        {           
            if (rad_list.SelectedValue.Trim() == "")
            {
                rad_list.ForeColor = System.Drawing.Color.Red;
                page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + ral_name[1].ToString() + "] 不能为空请先选择内容!');</script>");
                returntype = true;
            }
            else
            {
                rad_list.ForeColor = System.Drawing.Color.Empty;
                returntype = false;
            }
        }
        catch (Exception E)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' language='javascript' defer='defer'>alert('[" + ral_name[1].ToString() + "] 不能为空请先选择内容!');</script>");
            returntype = true;
        }
        finally
        {
        }
        return returntype;
    }
    #endregion

    #region 执行sql是否成功:成功返回true,失败返回false
    /// <summary>
    /// 执行sql是否成功:成功返回true,失败返回false
    /// </summary>
    /// <param name="sqlstr"></param>
    /// <returns></returns>
    public bool ExecSql(string sqlstr)
    {
        SqlConnection Conn = createConn();
        SqlCommand Cmd = new SqlCommand(sqlstr, Conn);
        bool returnBool = false;
        try
        {
            Conn.Open();
            if (Cmd.ExecuteNonQuery()>0)
            {
                returnBool = true;
            }
            else
            {
                returnBool = false;
            }
        }
        catch (System.Exception ex)
        {
            returnBool = false;
        }
        finally
        {
            Conn.Close();
            Cmd.Dispose();
        }
        return returnBool;
    }
    #endregion

    #region 执行sql是否成功:入参查询语句sql,  动态参数SqlParameter；成功返回true,失败返回false
    /// <summary>
    /// 执行sql是否成功:成功返回true,失败返回false
    /// </summary>
    /// <param name="sqlstr"></param>
    /// <param name="sqlPara">SqlParameter</param>
    /// <returns></returns>    
    public bool ExecSql(string sql, SqlParameter[] sqlPara)
    {
        bool returnBool = false;
        SqlConnection Conn = createConn();
        Conn.Open();
        SqlTransaction transaction = Conn.BeginTransaction(); //回滚定义
        SqlCommand cmd = new SqlCommand(sql, Conn);        
        if (sqlPara != null)
            cmd.Parameters.AddRange(sqlPara);
        cmd.Transaction = transaction;//添加
        try
        {
            cmd.ExecuteNonQuery();
            transaction.Commit();//提交
            returnBool = true;
        }
        catch(Exception e)
        {
            transaction.Rollback();//回滚
            returnBool = false;
        }
        finally
        {
            Conn.Close();
            cmd.Dispose();
        }
        return returnBool;
    }
    #endregion

    #region 执行sql是否成功:入参查询语句sql,  动态参数SqlParameter；成功返回true,失败返回false
    /// <summary>
    /// 执行sql是否成功:成功返回true,失败返回false
    /// </summary>
    /// <param name="sqlstr"></param>
    /// <param name="sqlPara">SqlParameter</param>
    /// <returns></returns>    
    public bool ExecSql(string sql, SqlParameter[] sqlPara, int timeOut)
    {
        bool returnBool = false;
        SqlConnection Conn = createConn();
        Conn.Open();
        SqlTransaction transaction = Conn.BeginTransaction(); //回滚定义
        SqlCommand cmd = new SqlCommand(sql, Conn);
        cmd.CommandTimeout = timeOut;
        if (sqlPara != null)
            cmd.Parameters.AddRange(sqlPara);
        cmd.Transaction = transaction;//添加
        try
        {
            cmd.ExecuteNonQuery();
            transaction.Commit();//提交
            returnBool = true;
        }
        catch
        {
            transaction.Rollback();//回滚
            returnBool = false;
        }
        finally
        {
            Conn.Close();
            cmd.Dispose();
        }
        return returnBool;
    }
    #endregion

    #region 定义导出Excel,Word的函数,sql
    /// <summary>
    /// 定义导出Excel,Word的函数,
    /// </summary>
    /// <param name="page">当前页面 this</param>
    /// <param name="FileType">导出类型:Excel:application/ms-excel, Word:application/ms-word</param>
    /// <param name="FileName">文件名称</param>
    /// <param name="GV">对象Gridview</param>
    public void ToExcelOrWordcSql(System.Web.UI.Page page, string FileType, string FileName, string sql)
    {
        GridView GV = new GridView();
        DataTable dt = ReadDataTable(sql);
        GV.DataSource = dt;
        GV.DataBind();
        page.Response.Charset = "GB2312";
        page.Response.ContentEncoding = System.Text.Encoding.UTF8;
        page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        page.Response.ContentType = FileType;
        page.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        GV.RenderControl(hw);
        page.Response.Write(tw.ToString());
        page.Response.End();
    }
    #endregion

    #region 定义导出Excel,Word的函数,GridView
    /// <summary>
    /// 定义导出Excel,Word的函数,
    /// </summary>
    /// <param name="page">当前页面 this</param>
    /// <param name="FileType">导出类型:Excel:application/ms-excel, Word:application/ms-word</param>
    /// <param name="FileName">文件名称</param>
    /// <param name="GV">对象Gridview</param>
    public void ToExcelOrWord(System.Web.UI.Page page, string FileType, string FileName, GridView GV)
    {
        page.Response.Charset = "GB2312";
        page.Response.ContentEncoding = System.Text.Encoding.UTF8;
        page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        page.Response.ContentType = FileType;
        page.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        GV.RenderControl(hw);
        page.Response.Write(tw.ToString());
        page.Response.End();
    }
    #endregion

    #region 定义导出Excel,Word的函数,DataList
    /// <summary>
    /// 定义导出Excel,Word的函数,
    /// </summary>
    /// <param name="page">当前页面 this</param>
    /// <param name="FileType">导出类型:Excel:application/ms-excel, Word:application/ms-word</param>
    /// <param name="FileName">文件名称</param>
    /// <param name="GV">对象Gridview</param>
    public void DataListToExcelOrWord(System.Web.UI.Page page, string FileType, string FileName, DataList DL)
    { 
        page.Response.Charset = "GB2312";
        page.Response.ContentEncoding = System.Text.Encoding.UTF8;
        page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        page.Response.ContentType = FileType;
        page.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        DL.RenderControl(hw);
        page.Response.Write(tw.ToString());
        page.Response.End();
    }
    #endregion

    #region 定义导出Excel,Word的函数,Control
    /// <summary>
    /// 定义导出Excel,Word的函数,
    /// </summary>
    /// <param name="page">当前页面 this</param>
    /// <param name="FileType">导出类型:ToExcel,ToWord; Excel:application/ms-excel, Word:application/ms-word</param>
    /// <param name="FileName">文件名称</param>
    /// <param name="Area">对象区域</param>
    public void ToExcelOrWordTarget(System.Web.UI.Page page, string FileType, string FileName, Control Area)
    {
        string ToExcelOrWord = "";
        if (FileType == "ToExcel")
        {
            ToExcelOrWord = "application/ms-excel";
        }
        else
        {
            ToExcelOrWord = "application/ms-word";
        }
        page.Response.Charset = "GB2312";
        page.Response.ContentEncoding = System.Text.Encoding.UTF8;
        page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        page.Response.ContentType = ToExcelOrWord;
        page.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        Area.RenderControl(hw);
        page.Response.Write(tw.ToString());
        page.Response.End();
    }
    #endregion

    #region 执行返回当前日期
    /// <summary>
    /// 执行返回当前日期
    /// </summary>   
    /// <returns></returns>
    public string ReturnDateTimeNow()
    {
        string DateTimeNow = "";
        try
        {
            DataTable dt =ReadDataTable("select convert(varchar(11),getdate(),120) as 日期");
            DateTimeNow = dt.Rows[0]["日期"].ToString().Trim();             
        }
        catch (System.Exception ex)
        {
            
        }
        finally
        {
          　
        }
        return DateTimeNow;
    }
    #endregion

    #region 执行返回当前日期的星期几
    /// <summary>
    /// 执行返回当前日期的星期几
   /// </summary>
   /// <param name="dateTimeNow">日期</param>
   /// <returns></returns>
    public string ReturnDateTimeWeek(string dateTimeNow)
    {
        string Temp = "";
        try
        {
            switch (Convert.ToDateTime(dateTimeNow).DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    Temp = "星期天";
                    break;
                case DayOfWeek.Monday:
                    Temp = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    Temp = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    Temp = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    Temp = "星期四";
                    break;
                case DayOfWeek.Friday:
                    Temp = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    Temp = "星期六";
                    break;
            }
        }
        catch (System.Exception ex)
        {

        }
        finally
        {

        }
        return Temp;
    }
    #endregion

    #region 合并GridView中某列相同信息的行（单元格,模板列）
    /// <summary>
    /// 合并GridView中某列相同信息的行（单元格,模板列）
    /// </summary>
    /// <param name="GridView1">GridView</param>
    /// <param name="cellNum">第几列</param>
    /// <param name="objectType">类型,cel,lbl,lin,hyl</param>
    /// <param name="btnName">linkbutton 的名称</param>
    public void GridView_Cell_Merge(GridView GridView1, int cellNum, string objectType, string btnName)
    {
        int i = 0, rowSpanNum = 1;
        while (i < GridView1.Rows.Count - 1)
        {
            GridViewRow gvr = GridView1.Rows[i];

            for (++i; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gvrNext = GridView1.Rows[i];

                string name1 = "", name2 = "";

                if (objectType == "cel")
                {
                    name1 = gvr.Cells[cellNum].Text;
                    name2 = gvrNext.Cells[cellNum].Text;
                }
                if (objectType == "lbl")
                {
                    name1 = ((Label)gvr.Cells[cellNum].FindControl(btnName)).Text;
                    name2 = ((Label)gvrNext.Cells[cellNum].FindControl(btnName)).Text;
                }
                if (objectType == "lin")
                {
                    name1 = ((LinkButton)gvr.Cells[cellNum].FindControl(btnName)).Text;
                    name2 = ((LinkButton)gvrNext.Cells[cellNum].FindControl(btnName)).Text;
                }
                if (objectType == "hyl")
                {
                    name1 = ((HyperLink)gvr.Cells[cellNum].FindControl(btnName)).Text;
                    name2 = ((HyperLink)gvrNext.Cells[cellNum].FindControl(btnName)).Text;
                }

                if (name1 == name2)
                {
                    gvrNext.Cells[cellNum].Visible = false;//不然会把其他的挤走，造成行突出
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    rowSpanNum = 1;
                    break;
                }

                if (i == GridView1.Rows.Count - 1)
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }
    #endregion

    #region 合并单元格 合并某一列所有行
    /// <summary>
    /// 合并GridView中某列相同信息的行（单元格）
    /// </summary>
    /// <param name="GridView1"></param>
    /// <param name="cellNum"></param>
    public void GridView_Cell_Merge(GridView gridView, int cols)
    {
        if (gridView.Rows.Count < 1 || cols > gridView.Rows[0].Cells.Count - 1)
        {
            return;
        }
        TableCell oldTc = gridView.Rows[0].Cells[cols];
        for (int i = 1; i < gridView.Rows.Count; i++)
        {
            TableCell tc = gridView.Rows[i].Cells[cols];
            if (oldTc.Text == tc.Text)
            {
                tc.Visible = false;
                if (oldTc.RowSpan == 0)
                {
                    oldTc.RowSpan = 1;
                }
                oldTc.RowSpan++;
                oldTc.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                oldTc = tc;
            }
        }
    }
    #endregion

    #region 合并单元格 合并某一列中的某些行
    /// <summary>
    /// 合并单元格 合并某一列中的某些行
    /// </summary>
    /// <param name="GridView1">GridView ID</param>
    /// <param name="cellNum">列</param>
    /// <param name="sRow">开始行</param>
    /// <param name="eRow">结束列</param>
    public void GridView_Cell_Merge(GridView gridView, int cols, int sRow, int eRow)
    {
        if (gridView.Rows.Count < 1 || cols > gridView.Columns.Count - 1)
        {
            return;
        }
        TableCell oldTc = gridView.Rows[sRow].Cells[cols];
        for (int i = 1; i < eRow - sRow; i++)
        {
            TableCell tc = gridView.Rows[sRow + i].Cells[cols];
            tc.Visible = false;
            if (oldTc.RowSpan == 0)
            {
                oldTc.RowSpan = 1;
            }
            oldTc.RowSpan++;
            oldTc.VerticalAlign = VerticalAlign.Middle;
        }
    }
    #endregion

    #region GridView中的所有相同的数据列全部合并
    ///　 <summary>　
    ///　 GridView中的所有相同的数据列全部合并
    ///　 </summary>　
    ///　 <param　 name="GridView1">GridView对象</param>　
    public void GridView_Row_Merge(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Text == previousRow.Cells[i].Text)
                {
                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                           previousRow.Cells[i].RowSpan + 1;
                    previousRow.Cells[i].Visible = false;
                }
            }
        }
    }
    #endregion

    #region GridView 所指定Row中相同的数据列合并
    ///　 <summary>　
    ///　 GridView 所指定Row中相同的数据列合并
    ///　 </summary>　
    ///　 <param　 name="GridView1">GridView对象</param>　
    ///　 <param　 name="cellNum">要指定的Row</param>
    public void GridView_Row_Merge(GridView gridView, int rows)
    {
        TableCell oldTc = gridView.Rows[rows].Cells[0];
        for (int i = 1; i < gridView.Rows[rows].Cells.Count; i++)
        {
            TableCell tc = gridView.Rows[rows].Cells[i];　 //Cells[0]就是你要合并的列
            if (oldTc.Text == tc.Text)
            {
                tc.Visible = false;
                if (oldTc.ColumnSpan == 0)
                {
                    oldTc.ColumnSpan = 1;
                }
                oldTc.ColumnSpan++;
                oldTc.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                oldTc = tc;
            }
        }
    }
    #endregion

    #region 合并单元格 合并一行中的多列
    /// <summary>
    /// 合并单元格 合并一行中的多列
    /// </summary>
    /// <param name="GridView1">GridView ID</param>
    /// <param name="rows">行</param>
    /// <param name="sCol">开始列</param>
    /// <param name="eCol">结束列</param>
    public void GridView_Row_Merge(GridView gridView, int rows, int sCol, int eCol)
    {
        TableCell oldTc = gridView.Rows[rows].Cells[sCol];
        for (int i = 1; i < eCol - sCol; i++)
        {
            TableCell tc = gridView.Rows[rows].Cells[i + sCol];　 //Cells[0]就是你要合并的列
            tc.Visible = false;
            if (oldTc.ColumnSpan == 0)
            {
                oldTc.ColumnSpan = 1;
            }
            oldTc.ColumnSpan++;
            oldTc.VerticalAlign = VerticalAlign.Middle;
        }
    }

    #endregion

    #region 合并单元格 合并GridViewHead中的指定列
    /// <summary>
    /// 合并单元格 合并GridViewHead中的指定列
    /// </summary>
    /// <param name="GridView1">GridView ID</param>
    /// <param name="sCol">开始列</param>
    /// <param name="eCol">结束列</param>
    public void GridView_Head_Merge(GridView gridView, int sCol, int eCol)
    {
        TableCell oldTc = gridView.HeaderRow.Cells[sCol];
        for (int i = 1; i < eCol - sCol; i++)
        {
            TableCell tc = gridView.HeaderRow.Cells[i + sCol];　 //Cells[0]就是你要合并的列
            tc.Visible = false;
            if (oldTc.ColumnSpan == 0)
            {
                oldTc.ColumnSpan = 1;
            }
            oldTc.ColumnSpan++;
            oldTc.VerticalAlign = VerticalAlign.Middle;
        }
    }   
    #endregion

    #region 根据教工卡号查找教工信息,根据入参类型返回
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tea">教工卡号</param>
    /// <param name="type">返回类型:1返回姓名"叶XX" ;2返回部门名称"信息中心"; 3返回姓名部门"叶XX、信息中心"; 4返回卡号姓名部门"80181、叶XX、信息中心"</param>
    /// <returns></returns>
    public string load_Init_FindTea(string tea,int type)
    {
        try
        {
            string teaName = "";
            DataTable dt = new DataTable();
            List<SqlParameter> para = new List<SqlParameter>();
            string str = "select * from XIN_V_公共_教工信息 where 卡号=@卡号 ";
            para.Add(new SqlParameter("@卡号", tea));
            SqlParameter[] Par = para.ToArray();
            dt = this.ReadDataTable(str, Par);
            if (dt.Rows.Count > 0)
            {
                if (type == 1)
                {
                    teaName = dt.Rows[0]["姓名"].ToString().Trim();
                }
                if (type == 2)
                {
                    teaName = dt.Rows[0]["部门"].ToString().Trim();
                }
                if (type == 3)
                {
                    teaName = dt.Rows[0]["姓名"].ToString().Trim()+"、"+ dt.Rows[0]["部门"].ToString().Trim();
                }
                if (type == 4)
                {
                    teaName = tea + "、" + dt.Rows[0]["姓名"].ToString().Trim() + "、" + dt.Rows[0]["部门"].ToString().Trim();
                }   
            }
            return teaName;
        }
        catch (Exception E)
        {
            return "0";
        }
    }
    #endregion

    #region 根据查询条件返回指定列值只能是一行
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlStr">查询名称</param>
    /// <param name="viewTxt">指定字段</param>
    /// <returns></returns>
    public string load_Init_FindSqlTxt(string sqlStr, string viewTxt)
    {
        try
        {
            string returnStr = "";
            DataTable dt = new DataTable();
            dt = ReadDataTable(sqlStr);
            if (dt.Rows.Count > 0)
            {
                returnStr = dt.Rows[0][viewTxt].ToString().Trim();
            }
            return returnStr;
        }
        catch (Exception E)
        {
            return "0";
        }
    }
    #endregion

    #region 将集合IList类转换成DataTable
    /// <summary>    
    /// 将集合类转换成DataTable    
    /// </summary>    
    /// <param name="list">集合</param>    
    /// <returns></returns>    
    public DataTable listToDataTable(IList list)
    {
        DataTable result = new DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();

            foreach (PropertyInfo pi in propertys)
            {
                result.Columns.Add(pi.Name, pi.PropertyType);
            }
            for (int i = 0; i < list.Count; i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    object obj = pi.GetValue(list[i], null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }
    #endregion

    #region 生成树
    /// <summary>
    /// 
    /// </summary>
    /// <param name="page">对象页this</param>
    /// <param name="vTreeView">树控件名</param>
    /// <param name="sqlStr">sqlStr查询条件,注意排序,上级编号,排序</param>
    /// <param name="viewTxt">显示字段</param>
    /// <param name="viewTip">提示字段</param>
    /// <param name="viewValue">主键字段</param>
    /// <param name="url">链接字段</param>
    /// <param name="target">链接目标</param>
    public void createTree(Page page, TreeView vTreeView, string sqlStr,string orderBy, string baseTxt, string viewTxt, string viewTip, string viewValue, string url, string target)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = this.ReadDataTable(sqlStr);
            if (dt.Rows.Count > 0)
            {
                createTreeBase(page,vTreeView, dt,orderBy, baseTxt, viewTxt, viewTip, viewValue, url, target);
            }
            else
            {
                this.showMessage(page, "生成树失败!");
            }
        }
        catch (System.Exception ex)
        {
            this.showMessage(page, ex.Message.ToString());
        }
    }
    #endregion

    #region
    /// 
    /// 生成树,添加顶级节点
    /// 
    /// 用户TreeView控件
    /// 数据表结果集
    public void createTreeBase(Page page, TreeView vTreeView, DataTable vDataTable,string orderBy, string baseTxt, string viewTxt, string viewTip, string viewValue, string url, string target)
    {
        try
        {
            string condition = orderBy + "='" + baseTxt + "'";
            DataTable dt = ReadDataTable_getSelect(vDataTable, condition);
            if (dt.Rows.Count < 0)                                 //如果没有儿子节点则直接返回
                return;
            TreeNode rootTreeNode = null;   
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rootTreeNode = new TreeNode(dt.Rows[i][viewTxt].ToString());//参数为显示的信息            
                vTreeView.Nodes.Add(rootTreeNode);
                rootTreeNode.ToolTip =dt.Rows[i][viewTip].ToString();

                rootTreeNode.NavigateUrl = dt.Rows[i][url].ToString();
                rootTreeNode.Target = target;
                string layNo = dt.Rows[i][viewValue].ToString();       //层次代码
                rootTreeNode.Value = layNo;//和winform的区别
                CreateTreeNode(vDataTable, rootTreeNode, orderBy, viewTxt, viewTip, viewValue, url, target);
            }
            //DataView vDataView = vDataTable.DefaultView;        //获取默认视图            
            //vDataView.Sort = orderBy;
            //DataRowView[] arrDRV = vDataView.FindRows(new object[] { baseTxt });       //树根节点的父亲节点必须是0
            //if (arrDRV.Length == 0)                             //如果没有儿子节点则直接返回
            //    return;
            //TreeNode rootTreeNode = null;            
            //foreach (DataRowView vDRV in arrDRV)
            //{
            //    rootTreeNode = new TreeNode(vDRV.Row[viewTxt].ToString());//参数为显示的信息            
            //    vTreeView.Nodes.Add(rootTreeNode);
            //    rootTreeNode.ToolTip = vDRV.Row[viewTip].ToString();
               
            //    rootTreeNode.NavigateUrl = vDRV.Row[url].ToString();
            //    rootTreeNode.Target = target;
            //    string layNo = vDRV.Row[viewValue].ToString();       //层次代码
            //    rootTreeNode.Value = layNo;//和winform的区别
            //    CreateTreeNode(vDataTable, rootTreeNode, orderBy, viewTxt, viewTip, viewValue, url, target);
            //}

             
        }
        catch (System.Exception ex)
        {
            showMessage(page, ex.Message.ToString());
        }
    }
    #endregion

    #region
    /// <summary>
    /// 利用递归生成树
    /// </summary>
    /// 
    /// 利用递归生成树
    /// 
    /// 获取默认视图
    /// TreeView控件节点引用
    //private static void CreateTreeNode(DataView vDataView, TreeNode parentNode,string viewTxt,string viewTip, string viewValue, string url, string target)
    private void CreateTreeNode(DataTable vDataTable, TreeNode parentNode, string orderBy, string viewTxt, string viewTip, string viewValue, string url, string target)
    {
        string condition = orderBy + "='" + parentNode.Value + "'";
        DataTable dt = ReadDataTable_getSelect(vDataTable, condition);      
        if (dt.Rows.Count< 0)                                 //如果没有儿子节点则直接返回
            return;
        TreeNode tmpTreeNode = null;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            tmpTreeNode = new TreeNode(dt.Rows[i][viewTxt].ToString());//参数为显示的信息
            parentNode.ChildNodes.Add(tmpTreeNode);//添加节点
            tmpTreeNode.Value = dt.Rows[i][viewValue].ToString(); ;//和winform的区别
            tmpTreeNode.ToolTip = dt.Rows[i][viewTip].ToString();            
            tmpTreeNode.NavigateUrl = dt.Rows[i][url].ToString();
            tmpTreeNode.Target = target;

            CreateTreeNode(vDataTable, tmpTreeNode, orderBy, viewTxt, viewTip, viewValue, url, target); 
        }
         
        //DataRowView[] arrDRV = vDataView.FindRows(parentNode.Value);//找出同一父节点的所有子节点 
        //if (arrDRV.Length == 0)                                 //如果没有儿子节点则直接返回
        //    return;
        //TreeNode tmpTreeNode = null;
        //foreach (DataRowView vDRV in arrDRV)
        //{
        //    tmpTreeNode = new TreeNode(vDRV.Row[viewTxt].ToString());//参数为显示的信息
        //    parentNode.ChildNodes.Add(tmpTreeNode);//添加节点
        //    parentNode.ToolTip = vDRV.Row[viewTip].ToString();
        //    parentNode.NavigateUrl = vDRV.Row[url].ToString();
        //    parentNode.Target = target;
        //    string layNo = vDRV.Row[viewValue].ToString();       //层次代码
        //    tmpTreeNode.Value = layNo;//和winform的区别
        //    CreateTreeNode(vDataView, tmpTreeNode, viewTxt, viewTip, viewValue, url, target);                    //进入递归
        //}
    }
    #endregion



    #region 附件上传,成功返回:"上传附件成功!"
    /// <summary>
    /// 附件上传
    /// </summary>
    /// <param name="objFile">控件名称</param>
    /// <param name="severPathFileName">附件路径+附件名称</param>
    /// <param name="fileSize">附件限制大小</param>
    /// <param name="type">格式限制类型:0不限制格式; 1只接爱指定格式; 2限制指定格式</param>
    /// 
    /// <returns></returns>
    public string file_upLoadFile(FileUpload objFile, string severPathFileName, int fileSize,int type , string[] allowed )
    {
        string returnFilName = "上传附件成功!";
        try
        {
            if (objFile.HasFile)
            {
                //判断文件   
                if (objFile.PostedFile.ContentLength < fileSize * 1024 * 1024)
                {
                    if (type == 0)
                    {
                        try
                        {
                            //使用SaveAs方法保存文件
                            objFile.SaveAs(severPathFileName);
                        }
                        catch (Exception ex)
                        {
                            returnFilName = "文件上传失败，失败原因：" + ex.Message + "!";
                        }       
                    }

                    if (type == 1)
                    {
                        Boolean TypeBool = false;
                        string myFileName = objFile.FileName.Substring(objFile.FileName.LastIndexOf("."));
                        for (int i = 0; i < allowed.Length; i++)
                        {
                            if (allowed[i] == myFileName)
                            {
                                TypeBool = true;
                            }
                        }
                        if (TypeBool == true)
                        {
                            try
                            {
                                //使用SaveAs方法保存文件
                                objFile.SaveAs(severPathFileName);
                            }
                            catch (Exception ex)
                            {
                                returnFilName = "文件上传失败，失败原因：" + ex.Message + "!";
                            }
                        }
                        else
                        {
                            returnFilName = "文件上传失败，失败原因：不支持该格式!";
                        }
                    }

                    if (type == 2)
                    {                        
                        Boolean TypeBool = false;
                        string myFileName = objFile.FileName.Substring(objFile.FileName.LastIndexOf("."));

                        for (int i = 0; i < allowed.Length; i++)
                        {
                            if (allowed[i] == myFileName)
                            {
                                TypeBool = true;
                            }
                        }
                        if (TypeBool == true)
                        {
                            returnFilName = "文件上传失败，失败原因：不支持[" + myFileName + "]格式!";
                        }
                        else
                        {
                            try
                            {
                                //使用SaveAs方法保存文件
                                objFile.SaveAs(severPathFileName);
                            }
                            catch (Exception ex)
                            {
                                returnFilName = "文件上传失败，失败原因：" + ex.Message + "!";
                            }
                        }
                    }                                
                }
                else
                {
                    returnFilName = "文件上传失败，上传文件不能大于：" + fileSize.ToString().Trim() + "M!";       
                }
            }
            else
            {
                returnFilName = "文件上传失败，请先选择要上传的附件!";                 　
            }
        }
        catch (Exception Ex)
        {
            returnFilName = "文件上传失败，失败原因：" + Ex.Message + "!";       
        }
        return returnFilName;
    }
    #endregion

    #region 附件下载
    /// <summary>
    /// 附件下载
    /// </summary>
    /// <param name="page">页面对象</param>
    /// <param name="path">附件路径</param>
    /// <param name="fileName">文件名称</param>   
    public void file_download(Page page, string path, string fileName)
    {       
        try
        {
            ////获取文件路径
           
            ////初始化 FileInfo 类的实例，它作为文件路径的包装
            //FileInfo fi = new FileInfo(path);

            ////将文件保存到本机上 page.Server.UrlEncode(fileName)
            //page.Response.Clear();
            //page.Response.AddHeader("Content-Disposition", "attachment; filename=" + page.Server.UrlEncode(fileName));
            //page.Response.AddHeader("Content-Length", fi.Length.ToString());
            //page.Response.ContentType = "application/octet-stream";
            //page.Response.Filter.Close();
            //page.Response.WriteFile(fi.FullName);
            //page.Response.End();



            string guidname = path;
            string reallyname = fileName;
            String FullFileName = page.Server.MapPath(guidname);
            FileInfo info = new FileInfo(FullFileName);
            page.Response.Clear();
            page.Response.ClearHeaders();
            page.Response.Buffer = false;
            page.Response.ContentType = "application/octet-stream";
            page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(reallyname, System.Text.Encoding.UTF8).Replace("+", "%20"));
            page.Response.AppendHeader("Content-Length", info.Length.ToString());
            page.Response.WriteFile(FullFileName);
            page.Response.Flush();
            page.Response.End();
           
        }
        catch (Exception Ex)
        {
            this.showMessage(page, "文件下载失败,失败原因:" + Ex.Message.ToString());           　
        }
         
    }

    /// <summary>
    /// 为字符串中的非英文字符编码
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToHexString(string s)
    {
        char[] chars = s.ToCharArray();
        StringBuilder builder = new StringBuilder();
        for (int index = 0; index < chars.Length; index++)
        {
            bool needToEncode = NeedToEncode(chars[index]);
            if (needToEncode)
            {
                string encodedString = ToHexString(chars[index]);
                builder.Append(encodedString);
            }
            else
            {
                builder.Append(chars[index]);
            }
        }

        return builder.ToString();
    }

    /// <summary>
    ///指定 一个字符是否应该被编码
    /// </summary>
    /// <param name="chr"></param>
    /// <returns></returns>
    private static bool NeedToEncode(char chr)
    {
        string reservedChars = "$-_.+!*'(),@=&";

        if (chr > 127)
            return true;
        if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
            return false;

        return true;
    }

    /// <summary>
    /// 为非英文字符串编码
    /// </summary>
    /// <param name="chr"></param>
    /// <returns></returns>
    private static string ToHexString(char chr)
    {
        UTF8Encoding utf8 = new UTF8Encoding();
        byte[] encodedBytes = utf8.GetBytes(chr.ToString());
        StringBuilder builder = new StringBuilder();
        for (int index = 0; index < encodedBytes.Length; index++)
        {
            builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
        }
        return builder.ToString();
    }



    #endregion


    #region 绑定listbox    
    /// <summary>
    /// 绑定listbox
    /// </summary>
    /// <param name="lib">控件名称</param>
    /// <param name="lib_values">绑定values值</param>
    /// <param name="lib_text">绑定text值</param>
    /// <param name="sqlStr">Sql查询语句</param>
    public void listboxSet(ListBox lib, string lib_values, string lib_text, string sqlStr)
    {
        try
        {
            lib.Items.Clear();
            DataTable dt = ReadDataTable(sqlStr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count ; i++)
                {
                    lib.Items.Add(new ListItem(dt.Rows[i][lib_text].ToString().Trim(),dt.Rows[i][lib_values].ToString().Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            
        }

    }

    #endregion



    #region 判断TextBox是否空值,空值时返回True并边线变色,否则返False;
    /// <summary>
    /// 判断TextBox是否空值,空值时返回True并边线变色,否则返False;
    /// </summary>
    /// <param name="txt">TextBox</param>
    /// <returns>空值时返回True并边线变色,否则返False</returns>
    public bool booltxtnullChangeBorderColor(TextBox txt)
    {
        string colorOne = "#ff0000";
        bool returnbool = false;
        if (txt.Text.Trim() == "")
        {
            txt.BorderColor = System.Drawing.ColorTranslator.FromHtml(colorOne); returnbool = true;
        }
        else
            txt.BorderColor = System.Drawing.Color.Empty;
        return returnbool;
    }
    #endregion

    #region 判断TextBox是否为数个,1整型2浮点,返回false则通过验证,返回true则不通过验证
    /// <summary>
    /// 判断是否为整型,返回false则通过验证,返回true则不通过验证
    /// </summary>
    /// <param name="tb">控件名称</param>
    /// <param name="dataType">数据类型,1整型, 2浮点,</param>
    /// <returns>返回false则通过验证,返回true则不通过验证</returns>
    public bool boolTxtNumber(TextBox tb, int dataType)
    {
        //Regex re = new Regex(@"^[0-9]+$");
        string[] txt_name = tb.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        bool returnbool = false;
        if (dataType == 1)
        {
            Regex re = new Regex(@"^-?\d+$");
            if (!re.IsMatch(tb.Text))
            {
                tb.BorderColor = System.Drawing.Color.Red;
                returnbool = true;
            }
            else
            {
                tb.BorderColor = System.Drawing.Color.Empty;
                returnbool = false;
            }
        }
        if (dataType == 2)
        {
            Regex re = new Regex(@"^([0-9]+|(\d+(\.\d+)+))$");
            if (!re.IsMatch(tb.Text))
            {
                tb.BorderColor = System.Drawing.Color.Red;
                returnbool = true;
            }
            else
            {
                tb.BorderColor = System.Drawing.Color.Empty;
                returnbool = false;
            }
        }
        return returnbool;
    }
    #endregion

    #region 判断DropDownList是否空值,空值时返回True并边线变色,否则返False;绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---',4第一项自定义
    /// <summary> 
    /// 判断DropDownList是否空值,空值时返回True并边线变色,否则返False;绑定DropDownList类型：1实绑定, 2第一项为空, 3第一项为'---请选择---',4第一项自定义
    /// </summary>
    /// <param name="drl">DropDownList</param>
    /// <param name="bindType">1实绑定, 2第一项为空, 3第一项为'---请选择---',4第一项自定义</param>
    /// <param name="topTip">第一项自定义</param>
    /// <returns>空值时返回True并边线变色,否则返False</returns>
    public bool booldrlnullChangeBorderColor(DropDownList drl,int bindType,string topTip)
    {
        string colorOne = "#ff0000";
        bool returnbool = false;
        if (bindType == 2)
        {
            if (drl.Text.Trim() == "")
            {
                drl.BorderColor = System.Drawing.ColorTranslator.FromHtml(colorOne); returnbool = true;
            }
            else
                drl.BorderColor = System.Drawing.Color.Empty;
        }
        if (bindType == 3)
        {
            if (drl.Text.Trim() == "---请选择---")
            {
                drl.BorderColor = System.Drawing.ColorTranslator.FromHtml(colorOne); returnbool = true;
            }
            else
                drl.BorderColor = System.Drawing.Color.Empty;
        }
        if (bindType == 4)
        {
            if (drl.Text.Trim() == topTip)
            {
                drl.BorderColor = System.Drawing.ColorTranslator.FromHtml(colorOne); returnbool = true;
            }
            else
                drl.BorderColor = System.Drawing.Color.Empty;
        }
        return returnbool;
    }
    #endregion

    #region 判断RadioButtonList是否空值,空值时返回True并边线变色,否则返False;
    /// <summary>
    /// 判断TextBox是否空值,空值时返回True并边线变色,否则返False;
    /// </summary>
    /// <param name="rad">RadioButtonList</param>
    /// <returns>空值时返回True并边线变色,否则返False</returns>
    public bool boolradnullChangeBorderColor(RadioButtonList rad)
    {
        string colorOne = "#ff0000";
        bool returnbool = false;        

        if (rad.SelectedValue.Trim() == "")
        {
            rad.ForeColor = System.Drawing.ColorTranslator.FromHtml(colorOne); returnbool = true;
        }
        else
            rad.ForeColor = System.Drawing.Color.Empty;
        return returnbool;
    }
    #endregion


    #region 截取等宽中英文字符串
    /// <summary>
    /// 截取等宽中英文字符串
    /// </summary>
    /// <param name="str">要截取的字符串</param>
    /// <param name="length">要截取的中文字符长度</param>
    /// <param name="appendStr">截取后后追加的字符串</param>
    /// <returns>截取后的字符串</returns>
    public string CutStr(string str, int length, string appendStr)
    {
        if (str == null) return string.Empty;

        int len = length * 2;
        //aequilateLength为中英文等宽长度,cutLength为要截取的字符串长度
        int aequilateLength = 0, cutLength = 0;
        Encoding encoding = Encoding.GetEncoding("gb2312");

        string cutStr = str.ToString();
        int strLength = cutStr.Length;
        byte[] bytes;
        for (int i = 0; i < strLength; i++)
        {
            bytes = encoding.GetBytes(cutStr.Substring(i, 1));
            if (bytes.Length == 2)//不是英文
                aequilateLength += 2;
            else
                aequilateLength++;

            if (aequilateLength <= len) cutLength += 1;

            if (aequilateLength > len)
                return cutStr.Substring(0, cutLength) + appendStr;
        }
        return cutStr;
    }
    #endregion

    #region 截取等宽中英文字符串
    /// <summary>
    /// Label数据格式化
    /// </summary>
    /// <param name="lbl"></param>
    public void loadLblFormat(Label lbl)
    {
        if (lbl.Text.Trim() != "")
        {
            lbl.Text = string.Format("{0:N2}", Convert.ToDecimal(lbl.Text));
            if (lbl.Text.Trim().Length > 3)
            {
                if (lbl.Text.Trim().Substring(lbl.Text.Trim().Length - 3, 3) == ".00")
                {
                    lbl.Text = lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 3);
                }
            }
        }
    }
    #endregion

    #region 读取数据返回DataSet,入参查询语句sql,  动态参数SqlParameter
    /// <summary>
    /// 读取数据返回DataSet,入参查询语句sql,  动态参数SqlParameter
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="sqlPara">SqlParameter</param>
    /// <returns></returns>
    public DataSet getDataSet(string sqlstr, SqlParameter[] sqlPara)
    {//调用存取过程查询
        SqlConnection Conn = createConn();
        SqlCommand cmd = new SqlCommand(sqlstr, Conn);
        if (sqlPara != null)
            cmd.Parameters.AddRange(sqlPara);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        try
        {
            Conn.Open();
            sda.Fill(ds);
        }
        catch
        {
            Conn.Close();
            cmd.Dispose();
        }
        finally
        {
            Conn.Close();
            cmd.Dispose();
        }
        return ds;
    }
    #endregion


    #region 判断上传文件格式
    /// <summary>
    /// 判断上传文件格式
    /// </summary>
    /// <param name="filName">文件后序</param>
    /// <param name="boolType">类型：1不限制格式，2只接受指定格，3不接受指定格式</param>
    /// <param name="fileForm"></param>
    /// <returns></returns>
    public bool boolFileFrom(string filName, int boolType, string fileForm)
    {
        Boolean returnBool = true;
        string[] allowed = fileForm.Split(new Char[] { '、' });
        if (boolType == 1)
        {
        }
        if (boolType == 2)
        {
            for (int i = 0; i < allowed.Length; i++)
            {
                if ("." + allowed[i].ToString().Trim() == filName)
                {
                    returnBool = false;
                }
            }
        }
        if (boolType == 3)
        {
            for (int i = 0; i < allowed.Length; i++)
            {
                if ("." + allowed[i].ToString().Trim() == filName)
                {
                    returnBool = false;
                }                
            }
            if (returnBool == false)
            {
                returnBool = true;
            }
        }
        return returnBool;
    }
    #endregion


    
    #region 身份证号码验证

    /// <summary> 
    /// 验证身份证号码 
    /// </summary> 
    /// <param name="Id">身份证号码</param> 
    /// <returns>验证成功为True，否则为False</returns> 
    public bool CheckIDCard(string Id)
    {
        if (Id.Length == 18)
        {
            bool check = CheckIDCard18(Id);
            return check;
        }
        else if (Id.Length == 15)
        {
            bool check = CheckIDCard15(Id);
            return check;
        }
        else
        {
            return false;
        }
    }

    /// <summary> 
    /// 验证15位身份证号 
    /// </summary> 
    /// <param name="Id">身份证号</param> 
    /// <returns>验证成功为True，否则为False</returns> 
    private bool CheckIDCard18(string Id)
    {
        long n = 0;
        if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
        {
            return false;//数字验证 
        }
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (address.IndexOf(Id.Remove(2)) == -1)
        {
            return false;//省份验证 
        }
        string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        DateTime time = new DateTime();
        if (DateTime.TryParse(birth, out time) == false)
        {
            return false;//生日验证 
        }
        string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
        string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
        char[] Ai = Id.Remove(17).ToCharArray();
        int sum = 0;
        for (int i = 0; i < 17; i++)
        {
            sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
        }
        int y = -1;
        Math.DivRem(sum, 11, out y);
        if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
        {
            return false;//校验码验证 
        }
        return true;//符合GB11643-1999标准 
    }

    /// <summary> 
    /// 验证18位身份证号 
    /// </summary> 
    /// <param name="Id">身份证号</param> 
    /// <returns>验证成功为True，否则为False</returns> 
    private bool CheckIDCard15(string Id)
    {
        long n = 0;
        if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
        {
            return false;//数字验证 
        }
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (address.IndexOf(Id.Remove(2)) == -1)
        {
            return false;//省份验证 
        }
        string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
        DateTime time = new DateTime();
        if (DateTime.TryParse(birth, out time) == false)
        {
            return false;//生日验证 
        }
        return true;//符合15位身份证标准 
    }
    #endregion 


    #region  比较两个DataTable数据（结构相同）

    /// <summary>
    /// 比较两个DataTable数据（结构相同）
    /// </summary>
    /// <param name="dt1">来自数据库的DataTable</param>
    /// <param name="dt2">来自文件的DataTable</param>
    /// <param name="keyField">关键字段名</param>
    /// <param name="dtRetAdd">新增数据（dt2中的数据）</param>
    /// <param name="dtRetDif1">不同的数据（数据库中的数据）</param>
    /// <param name="dtRetDif2">不同的数据（图2中的数据）</param>
    /// <param name="dtRetDel">删除的数据（dt2中的数据）</param>
    public void CompareDt(DataTable dt1, DataTable dt2, string keyField,
        out DataTable dtRetAdd, out DataTable dtRetDif1, out DataTable dtRetDif2,
        out DataTable dtRetDel)
    {
        //为三个表拷贝表结构
        dtRetDel = dt1.Clone();
        dtRetAdd = dtRetDel.Clone();
        dtRetDif1 = dtRetDel.Clone();
        dtRetDif2 = dtRetDel.Clone();

        int colCount = dt1.Columns.Count;

        DataView dv1 = dt1.DefaultView;
        DataView dv2 = dt2.DefaultView;

        //先以第一个表为参照，看第二个表是修改了还是删除了
        foreach (DataRowView dr1 in dv1)
        {
            dv2.RowFilter = keyField + " = '" + dr1[keyField].ToString() + "'";
            if (dv2.Count > 0)
            {
                if (!CompareUpdate(dr1, dv2[0]))//比较是否有不同的
                {
                    dtRetDif1.Rows.Add(dr1.Row.ItemArray);//修改前
                    dtRetDif2.Rows.Add(dv2[0].Row.ItemArray);//修改后
                    //dtRetDif2.Rows[dtRetDif2.Rows.Count - 1]["FID"] = dr1.Row["FID"];//将ID赋给来自文件的表，因为它的ID全部==0
                    continue;
                }
            }
            else
            {
                //已经被删除的
                dtRetDel.Rows.Add(dr1.Row.ItemArray);
            }
        }

        //以第一个表为参照，看记录是否是新增的
        dv2.RowFilter = "";//清空条件
        foreach (DataRowView dr2 in dv2)
        {
            dv1.RowFilter = keyField + " = '" + dr2[keyField].ToString() + "'";
            if (dv1.Count == 0)
            {
                //新增的
                dtRetAdd.Rows.Add(dr2.Row.ItemArray);
            }
        }
    }

    //比较是否有不同的
    private static bool CompareUpdate(DataRowView dr1, DataRowView dr2)
    {
        //行里只要有一项不一样，整个行就不一样,无需比较其它
        object val1;
        object val2;
        for (int i = 1; i < dr1.Row.ItemArray.Length; i++)
        {
            val1 = dr1[i];
            val2 = dr2[i];
            if (!val1.Equals(val2))
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region  MD5
    public string getMd5(string key)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(new UTF8Encoding().GetBytes(key));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        return sb.ToString();
    }
    #endregion

    #region  创建Dictionary
    public Dictionary<string, object> getSuccessDic()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("code", "000");
        dic.Add("msg", "请求成功");
        return dic;
    }

    public Dictionary<string, object> getAddSuccessDic()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("code", "000");
        dic.Add("msg", "请求成功");
        return dic;
    }
    public Dictionary<string, object> getAddFailDic()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("code", "001");
        dic.Add("msg", "新增失败");
        return dic;
    }
    public Dictionary<string, object> getTimeOutDic()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("code", "003");
        dic.Add("msg", "登录超时,请重新登录");
        return dic;
    }
    public Dictionary<string, object> getNoDataDic()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("code", "001");
        dic.Add("msg", "查询成功，但请求页码过大，没有返回值");
        return dic;
    }
    public Dictionary<string, object> geLoginDic()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("code", "000");
        dic.Add("msg", "登录成功");
        dic.Add("status", "true");
        return dic;
    }
    #endregion

    #region  查询总数量
    public string getCount(string sql, SqlParameter[] sqlPar) {
        string csql = " select count(1) count from( " + sql + ") t";
        return ReadDataTable(csql, sqlPar).Rows[0]["count"].ToString();
    }
    #endregion

    #region 批量插入
    public bool BatchInsertion(DataTable dt, string tableName)
    {
        bool returnBool = false;
        SqlConnection Conn = createConn();
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();
        SqlBulkCopy bulkCopy = new SqlBulkCopy(Conn, SqlBulkCopyOptions.CheckConstraints, tran);
        bulkCopy.DestinationTableName = tableName;
        bulkCopy.BatchSize = dt.Rows.Count;
        try
        {
            bulkCopy.WriteToServer(dt);
            tran.Commit();
            returnBool = true;
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.Write(e);
            tran.Rollback();
            returnBool = false;
        }
        finally
        {
            bulkCopy.Close();
            Conn.Close();
        }
        return returnBool;
    }
    #endregion


}
