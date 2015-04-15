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
    public partial class FormDailyIncomeOutcomeUpdate : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["store_id"] == null)
            {
                Response.Redirect("FormLogin.aspx");
            }
            if (!IsPostBack)
            {
                LoadStore();
                LoadInOutType();
            }
        }

        private void LoadStore()
        {
            List<Store> lst = new List<Store>();
            using (var db = new DeiofiberEntities())
            {
                var data = from itm in db.Stores
                           select itm;

                if (data.Any())
                {
                    foreach (Store store in data)
                    {
                        ddlStore.Items.Add(new ListItem(store.NAME, store.ID.ToString()));
                    }
                    ddlStore.SelectedValue = Convert.ToString(Session["store_id"]);
                    if (!CheckAdminPermission())
                    {
                        ddlStore.Enabled = false;
                    }
                }
            }
        }

        private void LoadInOutType()
        {
            List<InOutType> lst = new List<InOutType>();
            using (var db = new DeiofiberEntities())
            {
                var item = from itm in db.InOutTypes
                           where itm.IS_CONTRACT == false && itm.ACTIVE == true
                           select itm;

                lst = item.ToList();
            }

            ddlInOutFee.DataSource = lst;
            if (lst.Count > 0)
            {
                ddlInOutFee.DataValueField = "ID";
                ddlInOutFee.DataTextField = "NAME";
            }
            ddlInOutFee.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                InOut io = new InOut();
                io.INOUT_TYPE_ID = Convert.ToInt32(ddlInOutFee.SelectedValue);
                io.MORE_INFO = txtMoreInfo.Text.Trim();
                io.SEARCH_TEXT = string.Format("");
                io.CONTRACT_ID = -1;
                io.PERIOD_ID = -1;
                io.RENT_TYPE_ID = -1;
                io.PERIOD_DATE = new DateTime(1, 1, 1);
                if (ddlStore.Enabled == false)
                {
                    io.STORE_ID = Convert.ToInt32(Session["store_id"]);
                }
                else
                {
                    io.STORE_ID = Convert.ToInt32(ddlStore.SelectedValue);
                }


                using (var db = new DeiofiberEntities())
                {
                    var item = db.InOutTypes.FirstOrDefault(s => s.ID == io.INOUT_TYPE_ID);

                    if (item.IS_INCOME)
                    {
                        io.IN_AMOUNT = Convert.ToDecimal(txtFeeAmount.Text.Trim());
                        io.OUT_AMOUNT = 0;
                    }
                    else
                    {
                        io.IN_AMOUNT = 0;
                        io.OUT_AMOUNT = Convert.ToDecimal(txtFeeAmount.Text.Trim().Replace(",",string.Empty));
                    }

                    io.INOUT_DATE = DateTime.Now;
                    io.SEARCH_TEXT = string.Format("{0} {1} {2}", io.INOUT_DATE, io.MORE_INFO, item.NAME);
                    io.CREATED_BY = Session["username"].ToString();
                    io.CREATED_DATE = DateTime.Now;
                    io.UPDATED_BY = Session["username"].ToString();
                    io.UPDATED_DATE = DateTime.Now;

                    db.InOuts.Add(io);
                    db.SaveChanges();
                }

                WriteLog(CommonList.ACTION_CREATE_INOUT, false);

                ts.Complete();
            }
            Response.Redirect("FormDailyIncomeOutcome.aspx", false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormDailyIncomeOutcome.aspx", false);
        }

        private void WriteLog(string action, bool isCrashed)
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
            lg.IS_CRASH = isCrashed;
            lg.LOG_MSG = string.Format("Tài khoản {0} {1}thực hiện {2} vào lúc {3}", lg.ACCOUNT, strStoreName, lg.LOG_ACTION, lg.LOG_DATE);
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