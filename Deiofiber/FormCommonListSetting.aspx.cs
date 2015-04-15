using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deiofiber
{
    public partial class FormCommonListSetting : System.Web.UI.Page
    {
        int pageSize =20;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["store_id"] == null)
            {
                Response.Redirect("FormLogin.aspx");
            }
            if (!IsPostBack)
            {
                //CheckPermission();

                LoadData(string.Empty, 0);
            }
        }

        private void LoadData(string strSearch, int page)
        {
            // LOAD PAGER
            int totalRecord = 0;
            using (var db = new DeiofiberEntities())
            {
                var count = (from c in db.RentTypes
                             //where c.SEARCH_TEXT.Contains(strSearch)
                             select c).Count();
                totalRecord = Convert.ToInt32(count);
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
            List<RentType> dataList;
            int skip = page * pageSize;
            using (var db = new DeiofiberEntities())
            {
                var st = from s in db.RentTypes
                         //where s.SEARCH_TEXT.Contains(strSearch)
                         orderby s.ID
                         select s;

                dataList = st.Skip(skip).Take(pageSize).ToList();
            }

            rptRentType.DataSource = dataList;
            rptRentType.DataBind();
        }

        protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(string.Empty, Convert.ToInt32(ddlPager.SelectedValue) - 1);
        }
    }
}