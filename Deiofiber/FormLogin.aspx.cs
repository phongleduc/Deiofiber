using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Deiofiber.Common;
using System.Web.Security;

namespace Deiofiber
{
    public partial class FormLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    if (LoadUser(Request.Cookies["UserName"].Value, Request.Cookies["Password"].Value))
                    {
                        Response.Redirect("FormWarning.aspx", false);
                    }
                    else
                    {
                        HttpCookie uCookie = new HttpCookie("UserName");
                        uCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(uCookie);

                        HttpCookie pCookie = new HttpCookie("Password");
                        pCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(pCookie);

                        Response.Redirect("FormLogin.aspx", false);
                    }
                }
            }
            CommonList.AutoExtendContract();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoadUser(txtUsername.Text.Trim(), CommonList.EncryptPassword(txtPassword.Text.Trim())))
                {
                    HttpCookie uCookie = new HttpCookie("UserName");
                    HttpCookie pCookie = new HttpCookie("Password");

                    if (chkRememberMe.Checked)
                    {
                        uCookie.Expires = DateTime.MaxValue;
                        pCookie.Expires = DateTime.MaxValue;

                        uCookie.Value = txtUsername.Text.Trim();
                        pCookie.Value = CommonList.EncryptPassword(txtPassword.Text.Trim());
                    }
                    else
                    {
                        uCookie.Expires = DateTime.Now.AddDays(-1);
                        pCookie.Expires = DateTime.Now.AddDays(-1);
                    }

                    Response.Cookies.Add(uCookie);
                    Response.Cookies.Add(pCookie);

                    WriteLog(CommonList.ACTION_LOGIN, false);
                    Response.Redirect("FormWarning.aspx", false);
                }
                else
                {
                    lblMessage.Text = "Đăng nhập không thành công.";
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, true);
                throw ex;
            }
        }

        private bool LoadUser(string user, string password)
        {
            List<Account> lst = new List<Account>();
            using (var db = new DeiofiberEntities())
            {
                var acc = from s in db.Accounts
                          where s.ACC == user && s.PASSWORD == password
                          select s;

                lst = acc.ToList();
            }

            if (lst.Count > 0)
            {
                int storeid = lst[0].STORE_ID;
                using (var db = new DeiofiberEntities())
                {
                    if (storeid != 0)
                    {
                        var item = db.Stores.FirstOrDefault(s => s.ID == storeid);

                        if (!item.ACTIVE)
                            return false;
                    }
                } // Xu ly khi tai khoan cua hang bi khoa thi khong dang nhap duoc


                Session["username"] = lst[0].ACC;
                Session["name"] = lst[0].NAME;
                Session["permission"] = lst[0].PERMISSION_ID;
                Session["city_id"] = lst[0].CITY_ID;
                Session["store_id"] = lst[0].STORE_ID;

                using (var db = new DeiofiberEntities())
                {
                    var st = from s in db.Stores
                             where s.ID == storeid
                             select s;

                    List<Store> lstStore = st.ToList();
                    if (lstStore.Count > 0)
                    {
                        Session["store_name"] = lstStore[0].NAME;
                    }
                    else
                    {
                        Session["store_name"] = string.Empty;
                    }
                }
            }

            return lst.Count > 0;
        }

        private void WriteLog(string action, bool isCrashed)
        {
            using (var db = new DeiofiberEntities())
            {
                Log lg = new Log();
                lg.ACCOUNT = Session["username"].ToString();
                string strStore = string.Empty;
                string strStoreName = Session["store_name"].ToString();
                if (strStoreName != string.Empty)
                {
                    strStore = string.Format("cửa hàng {0} ", strStoreName);
                }
                lg.STORE = strStore;
                lg.LOG_ACTION = action;
                lg.LOG_DATE = DateTime.Now;
                lg.IS_CRASH = isCrashed;
                lg.LOG_MSG = string.Format("Tài khoản {0} {1}thực hiện {2} vào lúc {3}", lg.ACCOUNT, strStore, lg.LOG_ACTION, lg.LOG_DATE);
                lg.SEARCH_TEXT = lg.LOG_MSG;
                lg.STORE_ID = Convert.ToInt32(Session["store_id"]);

                db.Logs.Add(lg);
                db.SaveChanges();
            }
        }
    }
}