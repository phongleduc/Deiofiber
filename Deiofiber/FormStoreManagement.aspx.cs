using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deiofiber.Common;

namespace Deiofiber
{
    public partial class FormShopManagement : Page
    {
        int pageSize = 20;

        //raise button click events on content page for the buttons on master page
        protected void Page_Init(object sender, EventArgs e)
        {
            DropDownList drpStore = this.Master.FindControl("ddlStore") as DropDownList;
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
                LoadData(string.Empty, 0, 0);
                //CheckPermission();
            }
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpStore = sender as DropDownList;
            LoadData(txtSearch.Text.Trim(), Helper.parseInt(drpStore.SelectedValue), Convert.ToInt32(ddlPager.SelectedValue) - 1);
        }

        private void CheckPermission()
        {
            string username = Convert.ToString(Session["username"]);
            string name = Convert.ToString(Session["name"]);
            int permission = Convert.ToInt32(Session["permission"]);

            if (string.IsNullOrEmpty(username))
            {
                Response.Redirect("FormLogin.aspx");
            }

            if (permission == 1)
            {
                // permission_id = 1 --> ADMIN
                // permission_id = 0 --> NORMAL

                foreach (RepeaterItem itm in rptStore.Items)
                {
                    HiddenField hdf = (HiddenField)(itm.FindControl("hdfStore_id"));
                    HyperLink hpl = (HyperLink)(itm.FindControl("hplStoreDetail"));
                    if (hdf.Value.ToString() == Convert.ToString(Session["store_id"]))
                    {
                        hpl.Visible = true;
                    }
                    else
                    { hpl.Visible = false; }
                }
            }
            else
            {
                btnNew.Enabled = false;
            }
        }

        private void LoadData(string strSearch, int storeId, int page)
        {
            // LOAD PAGER
            int totalRecord = 0;
            using (var db = new DeiofiberEntities())
            {
                if (storeId != 0)
                {
                    var count = (from c in db.Stores
                                 where c.SEARCH_TEXT.Contains(strSearch) && c.ID == storeId
                                 select c).Count();
                    totalRecord = Convert.ToInt32(count);
                }
                else
                {
                    var count = (from c in db.Stores
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
            if (pageList.Count > 0)
            {
                ddlPager.SelectedIndex = page;
            }

            // LOAD DATA WITH PAGING
            List<STORE_FULL_VW> dataList;
            int skip = page * pageSize;
            using (var db = new DeiofiberEntities())
            {
                if (storeId != 0)
                {
                    var st = from s in db.STORE_FULL_VW
                             where s.SEARCH_TEXT.Contains(strSearch) && s.ID == storeId
                             orderby s.ID
                             select s;

                    dataList = st.Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    var st = from s in db.STORE_FULL_VW
                             where s.SEARCH_TEXT.Contains(strSearch)
                             orderby s.ID
                             select s;

                    dataList = st.Skip(skip).Take(pageSize).ToList();
                }
            }

            rptStore.DataSource = dataList; 
            rptStore.DataBind();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormStoreDetail.aspx");
        }

        protected void rptStore_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("FormStoreDetail.aspx");
        }

        protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim(), 0, Convert.ToInt32(ddlPager.SelectedValue) - 1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim(), 0, 0);
        }
    }
}