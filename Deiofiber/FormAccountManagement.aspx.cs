using Deiofiber.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deiofiber
{
    public partial class FormAccountManagement : System.Web.UI.Page
    {
        int pageSize = 20;
        private DropDownList drpStore;

        //raise button click events on content page for the buttons on master page
        protected void Page_Init(object sender, EventArgs e)
        {
            drpStore = this.Master.FindControl("ddlStore") as DropDownList;
            drpStore.SelectedIndexChanged += new EventHandler(ddlStore_SelectedIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["store_id"] == null)
            {
                Response.Redirect("FormLogin.aspx");
            }
            if (!IsPostBack)
            {
                //CheckPermission();

                LoadData(0, string.Empty, 0);
            }
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpStore = sender as DropDownList;
            LoadData(Helper.parseInt(drpStore.SelectedValue), string.Empty, 0);
        }

        private void LoadData(int storeId, string strSearch, int page)
        {
            // LOAD PAGER
            int totalRecord = 0;
            using (var db = new DeiofiberEntities())
            {
                if (storeId != 0)
                {
                    var count = (from c in db.Accounts
                                 where c.SEARCH_TEXT.Contains(strSearch) && c.STORE_ID == storeId
                                 select c).Count();
                    totalRecord = Convert.ToInt32(count);
                }
                else
                {
                    var count = (from c in db.Accounts
                                 where c.SEARCH_TEXT.Contains(strSearch)
                                 select c).Count();
                    totalRecord = Convert.ToInt32(count);
                }
            }

            int totalPage = totalRecord % pageSize == 0 ? totalRecord / pageSize : totalRecord / pageSize + 1;
            List<int> pageList = new List<int>();
            for (int i = 1; i <= totalPage; i++)
            {
                pageList.Add(i);
            }

            ddlPager.DataSource = pageList;
            ddlPager.DataBind();
            if(pageList.Count > 0)
            {
                ddlPager.SelectedIndex = page;
            }

            // LOAD DATA WITH PAGING
            List<Account> dataList;
            int skip = page * pageSize;
            using (var db = new DeiofiberEntities())
            {
                var st = from s in db.Accounts
                         where s.SEARCH_TEXT.Contains(strSearch)
                         orderby s.ID
                         select s;

                dataList = st.Skip(skip).Take(pageSize).ToList();
            }

            rptAccount.DataSource = dataList;
            rptAccount.DataBind();
        }

        public string GetStoreName(int storeId)
        {
            using (var db = new DeiofiberEntities())
            {
                Store st = db.Stores.FirstOrDefault(c => c.ID == storeId);
                if (st != null)
                {
                    return st.NAME;
                }
                return "Tất cả";
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormAccountUpdate.aspx");
        }

        protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(Helper.parseInt(drpStore.SelectedValue), txtSearch.Text.Trim(), Convert.ToInt32(ddlPager.SelectedValue) - 1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(Helper.parseInt(drpStore.SelectedValue), txtSearch.Text.Trim(), 0);
        }
    }
}