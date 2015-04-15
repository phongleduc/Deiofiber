using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deiofiber.Common;

namespace Deiofiber
{
    public partial class FormBadContractReport : System.Web.UI.Page
    {
        int pageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["store_id"] == null)
            {
                Response.Redirect("FormLogin.aspx");
            }
            if (!IsPostBack)
            {
                using (var db = new DeiofiberEntities())
                {
                    List<CONTRACT_FULL_VW> result = GetResultList(db);
                    LoadGeneralInfo(result);
                    LoadData(string.Empty, 0, result, db);
                }
            }
        }

        private List<CONTRACT_FULL_VW> GetResultList(DeiofiberEntities db)
        {
            var data = new List<CONTRACT_FULL_VW>();
            if (CheckAdminPermission())
            {
                DropDownList ddlStore = Master.FindControl("ddlStore") as DropDownList;
                if (ddlStore != null && !string.IsNullOrEmpty(ddlStore.SelectedValue))
                {
                    int storeid = Helper.parseInt(ddlStore.SelectedValue);
                    data = db.CONTRACT_FULL_VW.ToList().Where(c => c.CONTRACT_STATUS == true && c.STORE_ID == storeid)
                   .OrderByDescending(c => c.ID).ToList();
                }
                else
                {
                    data = db.CONTRACT_FULL_VW.ToList().Where(c => c.CONTRACT_STATUS == true)
                        .OrderByDescending(c => c.ID).ToList();
                }
            }
            else
            {
                int storeid = Helper.parseInt(Session["store_id"].ToString());
                data = db.CONTRACT_FULL_VW.ToList().Where(c => c.CONTRACT_STATUS == true && c.STORE_ID == storeid)
                    .OrderByDescending(c => c.ID).ToList();
            }

            var result = new List<CONTRACT_FULL_VW>();
            var lstPeriod = db.PayPeriods.Where(s => s.STATUS == true).ToList();
            foreach (CONTRACT_FULL_VW c in data)
            {
                var lstTempPeriod = lstPeriod.Where(s => s.CONTRACT_ID == c.ID).ToList();
                decimal totalPayed = lstTempPeriod.Select(s => s.ACTUAL_PAY).DefaultIfEmpty().Sum();
                foreach (PayPeriod pp in lstTempPeriod)
                {
                    if (pp.AMOUNT_PER_PERIOD > totalPayed)
                    {
                        c.PAY_DATE = pp.PAY_DATE;
                        c.OVER_DATE = DateTime.Now.Subtract(c.PAY_DATE).Days;
                        if (c.OVER_DATE > 50)
                        {
                            result.Add(c);
                        }
                        break;
                    }
                    totalPayed -= pp.AMOUNT_PER_PERIOD;
                }
            }
            return result.OrderByDescending(c => c.OVER_DATE).ToList();
        }

        private void LoadData(string strSearch, int page, List<CONTRACT_FULL_VW> data, DeiofiberEntities db)
        {
            int totalRecord = 0;
            totalRecord = data.Count();
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
            int skip = page * pageSize;

            List<CONTRACT_FULL_VW> dataList = data.Skip(skip).Take(pageSize).ToList();
            rptContract.DataSource = dataList;
            rptContract.DataBind();

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormContractUpdate.aspx");
        }


        protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new DeiofiberEntities())
            {
                List<CONTRACT_FULL_VW> result = GetResultList(db);
                LoadGeneralInfo(result);
                LoadData(string.Empty, Helper.parseInt(ddlPager.SelectedValue) - 1, result, db);
            }
        }

        private void LoadGeneralInfo(List<CONTRACT_FULL_VW> lstContract)
        {

            decimal DeiofiberNo = 0;
            decimal rentEquipNo = 0;
            decimal rentOtherNo = 0;
            decimal totalBadContract = 0;

            IEnumerable<CONTRACT_FULL_VW> ieDeiofiber = lstContract.Where(x => x.RENT_TYPE_ID == 1 && x.CONTRACT_STATUS == true);
            if (ieDeiofiber.Any())
            {
                DeiofiberNo = ieDeiofiber.Sum(x => x.CONTRACT_AMOUNT);
            }

            IEnumerable<CONTRACT_FULL_VW> ieRentEquiq = lstContract.Where(x => x.RENT_TYPE_ID == 2 && x.CONTRACT_STATUS == true);
            if (ieRentEquiq.Any())
            {
                rentEquipNo = ieRentEquiq.Sum(x => x.CONTRACT_AMOUNT);
            }

            IEnumerable<CONTRACT_FULL_VW> ieRentOther = lstContract.Where(x => x.RENT_TYPE_ID == 3 && x.CONTRACT_STATUS == true);
            if (ieDeiofiber.Any())
            {
                rentOtherNo = ieRentOther.Sum(x => x.CONTRACT_AMOUNT);
            }

            lblDeiofiberCount.Text = ieDeiofiber.Count().ToString();
            lblRentEquipCount.Text = ieRentEquiq.Count().ToString();
            lblRentOtherCount.Text = ieRentOther.Count().ToString();

            lblTotalFeeBikeContract.Text = DeiofiberNo == 0 ? "0" : string.Format("{0:0,0}", DeiofiberNo) + " VNĐ";
            lblTotalFeeEquiqContract.Text = DeiofiberNo == 0 ? "0" : string.Format("{0:0,0}", rentEquipNo) + " VNĐ";
            lblTotalFeeOtherContract.Text = DeiofiberNo == 0 ? "0" : string.Format("{0:0,0}", rentOtherNo) + " VNĐ";
            totalBadContract = lstContract.Sum(x => x.CONTRACT_AMOUNT);
            lblNumberOfBadContract.Text = lstContract.Count() + "/" + lstContract.Count();
            if (lstContract.Count() > 0)
                lblPercentBadContract.Text = String.Format("{0:P2}", lstContract.Count() / lstContract.Count());
            else
                lblPercentBadContract.Text = String.Format("{0:P2}", 0);

            lblTotalBadContract.Text = totalBadContract == 0 ? "0" : string.Format("{0:0,0}", totalBadContract) + " VNĐ";
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