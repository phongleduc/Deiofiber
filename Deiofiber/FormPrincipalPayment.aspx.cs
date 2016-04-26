using Deiofiber.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deiofiber
{
    public partial class FormPrincipalPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["store_id"] == null)
            {
                Response.Redirect("FormLogin.aspx");
            }
            if (!IsPostBack)
            {
                // LOAD PAYPERIOD
                int contractId = Convert.ToInt32(Request.QueryString["id"]);
                using (var db = new DeiofiberEntities())
                {
                    var contract = db.CONTRACT_FULL_VW.Where(s => s.ID == contractId && s.CONTRACT_STATUS == true).FirstOrDefault();
                    if (contract != null)
                    {
                        LoadGrid(contract);
                        LoadPaidAmountAndTheLeft(contract);
                        // DISPLAY SREEN
                        hplContract.NavigateUrl = string.Format("FormContractUpdate.aspx?ID={0}", contractId);
                        hplContract.Text = "Xem chi tiết hợp đồng";

                        var item = db.Stores.Where(s => s.ID == contract.STORE_ID).FirstOrDefault();
                        if (item != null)
                        {
                            txtStore.Text = item.NAME;
                            txtStore.Enabled = false;
                        }
                        var inouttypelist = db.InOutTypes.Where(s => s.IS_CONTRACT == true && s.ACTIVE == true).ToList();

                        ddInOutType.DataSource = inouttypelist;
                        ddInOutType.DataTextField = "NAME";
                        ddInOutType.DataValueField = "ID";
                        ddInOutType.DataBind();
                        ddInOutType.SelectedValue = "31";
                    }
                }
            }
        }

        private void LoadGrid(CONTRACT_FULL_VW con)
        {
            using (var db = new DeiofiberEntities())
            {
                List<InOut> payList = db.InOuts.Where(s => s.CONTRACT_ID == con.ID && s.INOUT_TYPE_ID == 31).ToList();
                rptContractInOut.DataSource = payList;
                rptContractInOut.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int contractId = Convert.ToInt32(Request.QueryString["ID"]);
            // SAVE INOUT
            using (var db = new DeiofiberEntities())
            {
                var contract = db.Contracts.FirstOrDefault(c => c.ID == contractId && c.CONTRACT_STATUS == true);
                contract.UNABLE_PAY_INTEREST = true;

                InOut io = new InOut();
                io.IN_AMOUNT = Convert.ToDecimal(txtIncome.Text);
                io.OUT_AMOUNT = 0;
                io.CONTRACT_ID = contractId;
                io.RENT_TYPE_ID = contract.RENT_TYPE_ID;
                io.INOUT_TYPE_ID = Convert.ToInt32(ddInOutType.SelectedValue);
                io.MORE_INFO = txtMoreInfo.Text.Trim();
                io.STORE_ID = Convert.ToInt32(Session["store_id"]);
                io.SEARCH_TEXT = string.Format("{0} ", io.MORE_INFO);
                io.INOUT_DATE = DateTime.Now;
                io.CREATED_BY = Session["username"].ToString();
                io.CREATED_DATE = DateTime.Now;
                io.UPDATED_BY = Session["username"].ToString();
                io.UPDATED_DATE = DateTime.Now;

                db.InOuts.Add(io);
                db.SaveChanges();
            }

            Response.Redirect("FormContractUpdate.aspx?ID=" + contractId);
        }

        private void LoadPaidAmountAndTheLeft(CONTRACT_FULL_VW con)
        {
            decimal total = 0;
            decimal amountLeft = 0;

            using (var db = new DeiofiberEntities())
            {
                var result = db.InOuts.Where(itm => itm.CONTRACT_ID == con.ID && itm.INOUT_TYPE_ID == 31).ToList();
                total = result.Select(c => c.IN_AMOUNT).DefaultIfEmpty().Sum();
                amountLeft = con.CONTRACT_AMOUNT - total;
            }

            Label lblContractAmout = (Label)rptContractInOut.Controls[rptContractInOut.Controls.Count - 1].Controls[0].FindControl("lblContractAmout");
            lblContractAmout.Text = string.Format("{0:0,0}", con.CONTRACT_AMOUNT);
            Label lblTotalPaid = (Label)rptContractInOut.Controls[rptContractInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalPaid");
            lblTotalPaid.Text = string.Format("{0:0,0}", total);
            Label lblAmountLeft = (Label)rptContractInOut.Controls[rptContractInOut.Controls.Count - 1].Controls[0].FindControl("lblAmountLeft");
            lblAmountLeft.Text = string.Format("{0:0,0}", amountLeft);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("FormContractUpdate.aspx?ID={0}", Request.QueryString["ID"]));
        }
    }
}