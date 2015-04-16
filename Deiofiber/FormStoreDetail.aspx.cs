using Deiofiber.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deiofiber
{
    public partial class FormStoreDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["store_id"] == null)
            {
                Response.Redirect("FormLogin.aspx");
            }
            if (!IsPostBack)
            {
                Common.CommonList.LoadCity(ddlCity);
                string id = Request.QueryString["ID"];
                if (!string.IsNullOrEmpty(id))
                {
                    LoadStoreById(id);
                }
            }
        }

        private void LoadStoreById(string id)
        {
            int storeid = Convert.ToInt32(id);
            List<Store> lst;
            using (var db = new DeiofiberEntities())
            {
                var st = from s in db.Stores
                         where s.ID == storeid
                         select s;

                lst = st.ToList<Store>();
            }

            Store store = lst[0];
            txtName.Text = store.NAME;
            txtAddress.Text = store.ADDRESS;
            ddlCity.SelectedValue = store.CITY_ID.ToString();
            txtPhone.Text = store.PHONE;
            txtStartCapital.Text = store.START_CAPITAL.ToString(); // Common.CommonList.FormatedAsCurrency(Convert.ToInt32(store.START_CAPITAL));
            txtCurrentCapital.Text = store.CURRENT_CAPITAL.ToString();
            txtTotalRevenueBefore.Text = store.REVENUE_BEFORE_APPLY.ToString();
            txtTotalCostBefore.Text = store.TOTAL_COST_BEFORE.ToString();
            txtTotalInvesmentBefore.Text = store.TOTAL_INVESMENT_BEFORE.ToString();
            rdbActive.Checked = store.ACTIVE;
            rdbDeActive.Checked = !rdbActive.Checked;
            txtNote.Text = store.NOTE;
        }

        protected string ValidateFields()
        {
            string id = Request.QueryString["ID"];
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                return "Bạn cần phải nhập tên cửa hàng";
            }
            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                return "Bạn cần phải nhập địa chỉ cửa hàng.";
            }
            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                return "Bạn cần phải nhập số điện thoại.";
            }
            if (string.IsNullOrEmpty(txtStartCapital.Text.Trim()) || Helper.parseInt(txtStartCapital.Text) == 0)
            {
                return "Bạn cần phải nhập số vốn ban đầu.";
            }
            return string.Empty;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string result = ValidateFields();
            if (!string.IsNullOrEmpty(result))
            {
                lblMessage.Text = result;
                return;
            }
            string id = Request.QueryString["ID"];
            if (string.IsNullOrEmpty(id))
            {
                // New store
                CreateNewStore();
            }
            else
            {
                // Edit
                UpdateStore(id);
            }

            Response.Redirect("FormStoreManagement.aspx");
        }

        private void CreateNewStore()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                Store st = new Store();
                st.NAME = txtName.Text.Trim();
                st.ADDRESS = txtAddress.Text.Trim();
                st.CITY_ID = Convert.ToInt32(ddlCity.SelectedValue);
                st.PHONE = txtPhone.Text.Trim();
                st.FAX = string.Empty;
                st.START_CAPITAL = Convert.ToDecimal(txtStartCapital.Text);
                st.ACTIVE = rdbActive.Checked;
                st.NOTE = txtNote.Text.Trim();
                st.SEARCH_TEXT = string.Format("{0} {1} {2}", st.NAME, st.ADDRESS, st.PHONE);
                st.CREATED_BY = Session["username"].ToString();
                st.CREATED_DATE = DateTime.Now;
                st.UPDATED_BY = Session["username"].ToString();
                st.UPDATED_DATE = DateTime.Now;
                using (var rb = new DeiofiberEntities())
                {
                    rb.Stores.Add(st);
                    rb.SaveChanges();
                }

                using (var rb1 = new DeiofiberEntities())
                {
                    var item = rb1.InOutTypes.FirstOrDefault(s => s.NAME == "Nhập vốn");

                    InOut io = new InOut();
                    io.IN_AMOUNT = Convert.ToDecimal(txtStartCapital.Text.Replace(",", string.Empty));
                    io.OUT_AMOUNT = 0;
                    io.CONTRACT_ID = -1;
                    io.PERIOD_ID = -1;
                    io.RENT_TYPE_ID = -1;
                    io.PERIOD_DATE = DateTime.Now;
                    io.MORE_INFO = "Vốn đầu tư ban đầu khi đăng ký cửa hàng";
                    io.STORE_ID = st.ID;
                    io.INOUT_TYPE_ID = item.ID;
                    io.INOUT_DATE = DateTime.Now;
                    io.SEARCH_TEXT = string.Format("{0} {1} ngày {2}", io.MORE_INFO, io.IN_AMOUNT, io.INOUT_DATE);
                    io.CREATED_BY = Session["username"].ToString();
                    io.CREATED_DATE = DateTime.Now;
                    io.UPDATED_BY = Session["username"].ToString();
                    io.UPDATED_DATE = DateTime.Now;
                    rb1.InOuts.Add(io);
                    rb1.SaveChanges();
                }

                WriteLog(CommonList.ACTION_CREATE_STORE);

                scope.Complete();
            }
        }

        private void UpdateStore(string id)
        {
            using (var db = new DeiofiberEntities())
            {
                int storeid = Convert.ToInt32(id);
                var st = (from s in db.Stores
                          where s.ID == storeid
                          select s).FirstOrDefault();

                st.NAME = txtName.Text.Trim();
                st.ADDRESS = txtAddress.Text.Trim();
                st.CITY_ID = Convert.ToInt32(ddlCity.SelectedValue);
                st.PHONE = txtPhone.Text.Trim();
                st.FAX = string.Empty;
                st.START_CAPITAL = Convert.ToDecimal(txtStartCapital.Text);
                st.CURRENT_CAPITAL = Convert.ToDecimal(txtCurrentCapital.Text);
                st.ACTIVE = rdbActive.Checked;
                st.NOTE = txtNote.Text.Trim();
                st.SEARCH_TEXT = string.Format("{0} {1} {2}", st.NAME, st.ADDRESS, st.PHONE);
                st.UPDATED_BY = Session["username"].ToString();
                st.UPDATED_DATE = DateTime.Now;
                db.SaveChanges();
            }

            WriteLog(CommonList.ACTION_UPDATE_STORE);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormStoreManagement.aspx");
        }

        private void WriteLog(string action)
        {
            Log lg = new Log();
            lg.ACCOUNT = Session["username"].ToString();
            string strStoreName = string.Empty;
            if (CheckAdminPermission())
            {
                DropDownList drpStore = this.Master.FindControl("ddlStore") as DropDownList;
                strStoreName = drpStore.SelectedItem.Text;
            }
            else
            {
                strStoreName = Session["store_name"].ToString();
            }
            lg.STORE = strStoreName;
            lg.LOG_ACTION = action;
            lg.LOG_DATE = DateTime.Now;
            lg.LOG_MSG = string.Format("Tài khoản {0} {1} thực hiện {2} vào lúc {3}", lg.ACCOUNT, strStoreName, lg.LOG_ACTION, lg.LOG_DATE);
            lg.SEARCH_TEXT = lg.LOG_MSG;

            using (var db = new DeiofiberEntities())
            {
                db.Logs.Add(lg);
                db.SaveChanges();
            }
        }

        public bool CheckAdminPermission()
        {
            string acc = Convert.ToString(Session["username"]);
            using (var db = new DeiofiberEntities())
            {
                var item = db.Accounts.FirstOrDefault(s => s.ACC == acc);

                if (item.PERMISSION_ID == 1)
                    return true;
                return false;
            }
        }
    }
}