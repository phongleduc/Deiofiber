using Deiofiber.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deiofiber
{
    public partial class FormIncomeOutcomeSummary : System.Web.UI.Page
    {
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
                
                //CommonList.LoadStore(ddlStore);
                int permissionid = Convert.ToInt32(Session["permission"]);
                LoadStore(permissionid);
            }
            LoadMiddle();
            LoadData();
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStore.SelectedValue = drpStore.SelectedValue;
        }

        private void LoadData()
        {
            DropDownList drpStore = this.Master.FindControl("ddlStore") as DropDownList;
            int storeId =  Helper.parseInt(drpStore.SelectedValue);
            using (var db = new DeiofiberEntities())
            {
                if (CheckAdminPermission())
                {
                    List<InOut> lstInOut = db.InOuts.ToList();
                    if (storeId != 0)
                    {
                        lstInOut = lstInOut.Where(c => c.STORE_ID == storeId).ToList();
                    }
                    var data = from d in lstInOut
                                group d by d.INOUT_DATE into g
                                select new
                                {
                                    Period = g.Key,
                                    Record = from o in g
                                             select new
                                             {
                                                 ID = o.STORE_ID,
                                                 Period = o.INOUT_DATE,
                                                 InAmount = o.IN_AMOUNT,
                                                 OutAmount = o.OUT_AMOUNT,
                                                 TotalIn = g.Sum(x => x.IN_AMOUNT),
                                                 TotalOut = g.Sum(x => x.OUT_AMOUNT),
                                                 BeginAmount = 0,
                                                 EndAmount = 0 ,
                                                 ContractFeeCar = 0,
                                                 RentFeeCar = 0,
                                                 CloseFeeCar = 0,
                                                 ContractFeeEquip = 0,
                                                 RentFeeEquip = 0,
                                                 CloseFeeEquip = 0,
                                                 ContractFeeOther = 0,
                                                 RentFeeOther = 0,
                                                 CloseFeeOther = 0,
                                                 RemainEndOfDay = 0,
                                                 InOutTypeId = o.INOUT_TYPE_ID,
                                                 RentTypeId = o.RENT_TYPE_ID,
                                                 InCapital = 0,
                                                 OutCapital = 0,
                                                 InOther = 0,
                                                 OutOther = 0

                                             }
                                };
                    List<SummaryInfo> lst = new List<SummaryInfo>();
                    foreach (var g in data)
                    {
                        SummaryInfo si = new SummaryInfo();
                        si.StoreId = g.Record.ToList()[0].ID;
                        si.Period = g.Record.ToList()[0].Period.Value;
                        si.TotalIn = g.Record.ToList()[0].TotalIn;
                        si.TotalOut = g.Record.ToList()[0].TotalOut;
                        si.BeginAmount = 0;
                        si.EndAmount = g.Record.ToList()[0].TotalIn - g.Record.ToList()[0].TotalOut;

                        var inout = g.Record.Where(x => x.InOutTypeId == 17);
                        if (inout.Any())
                        {
                            si.ContractFeeCar = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 22);
                        if (inout.Any())
                        {
                            si.ContractFeeEquip = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 25);
                        if (inout.Any())
                        {
                            si.ContractFeeStudent = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 26);
                        if (inout.Any())
                        {
                            si.ContractFeeLoan = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 23);
                        if (inout.Any())
                        {
                            si.ContractFeeStudent = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 14);
                        if (inout.Any())
                        {
                            si.RentFeeCar = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 15);
                        if (inout.Any())
                        {
                            si.RentFeeEquip = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 28);
                        if (inout.Any())
                        {
                            si.RentFeeStudent = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 30);
                        if (inout.Any())
                        {
                            si.RentFeeLoan = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 10);
                        if (inout.Any())
                        {
                            si.InCapital = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 11);
                        if (inout.Any())
                        {
                            si.OutCapital = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 12);
                        if (inout.Any())
                        {
                            si.InOther = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 13);
                        if (inout.Any())
                        {
                            si.OutOther = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 1);
                        if (inout.Any())
                        {
                            si.CloseFeeCar = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 2);
                        if (inout.Any())
                        {
                            si.CloseFeeEquip = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 3);
                        if (inout.Any())
                        {
                            si.CloseFeeStudent = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 4);
                        if (inout.Any())
                        {
                            si.CloseFeeLoan = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 1);
                        if (inout.Any())
                        {
                            si.RedundantFeeCar = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 2);
                        if (inout.Any())
                        {
                            si.RedundantFeeEquip = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 3);
                        if (inout.Any())
                        {
                            si.RedundantFeeStudent = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 4);
                        if (inout.Any())
                        {
                            si.RedundantFeeLoan = inout.Sum(x => x.OutAmount);
                        }
                        
                        lst.Add(si);
                    }

                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (i > 0)
                        {
                            lst[i].BeginAmount = lst[i - 1].EndAmount;
                            lst[i].EndAmount += lst[i].BeginAmount;
                        }
                    }


                    rptInOut.DataSource = lst.OrderByDescending(c => c.Period);
                    rptInOut.DataBind();
                    decimal sumIn = 0;
                    decimal sumOut = 0;
                    decimal sumBegin = 0;
                    decimal sumEnd = 0;

                    if (lst.Any())
                    {
                        sumBegin = lst[0].BeginAmount;
                        foreach (SummaryInfo itm in lst)
                        {
                            sumIn += itm.TotalIn;
                            sumOut += itm.TotalOut;
                        }
                        sumEnd = sumIn - sumOut;

                        Label lblTotalIn = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalIn");
                        Label lblTotalOut = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalOut");
                        Label lblTotalBegin = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalBegin");
                        Label lblTotalEnd = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalEnd");
                        lblTotalIn.Text = string.Format("{0:0,0}", sumIn);
                        lblTotalOut.Text = string.Format("{0:0,0}", sumOut);
                        lblTotalBegin.Text = string.Format("{0:0,0}", sumBegin);
                        lblTotalEnd.Text = string.Format("{0:0,0}", sumEnd);
                    }
                }
                else
                {
                    int storeid = Convert.ToInt32(Session["store_id"]);
                    var data = from d in db.InOuts
                               where d.STORE_ID == storeid
                               group d by d.INOUT_DATE into g
                               select new
                               {
                                   Period = g.Key,
                                   Record = from o in g
                                            select new
                                            {
                                                ID = o.STORE_ID,
                                                Period = o.INOUT_DATE,
                                                InAmount = o.IN_AMOUNT,
                                                OutAmount = o.OUT_AMOUNT,
                                                TotalIn = g.Sum(x => x.IN_AMOUNT),
                                                TotalOut = g.Sum(x => x.OUT_AMOUNT),
                                                BeginAmount = 0,
                                                EndAmount = 0,
                                                ContractFeeCar = 0,
                                                RentFeeCar = 0,
                                                CloseFeeCar = 0,
                                                ContractFeeEquip = 0,
                                                RentFeeEquip = 0,
                                                CloseFeeEquip = 0,
                                                ContractFeeOther = 0,
                                                RentFeeOther = 0,
                                                CloseFeeOther = 0,
                                                RemainEndOfDay = 0,
                                                InOutTypeId = o.INOUT_TYPE_ID,
                                                RentTypeId = o.RENT_TYPE_ID,
                                                InCapital = 0,
                                                OutCapital = 0,
                                                InOther = 0,
                                                OutOther = 0

                                            }
                               };

                    List<SummaryInfo> lst = new List<SummaryInfo>();
                    foreach (var g in data)
                    {
                        SummaryInfo si = new SummaryInfo();
                        si.StoreId = g.Record.ToList()[0].ID;
                        si.Period = g.Record.ToList()[0].Period.Value;
                        si.TotalIn = g.Record.ToList()[0].TotalIn;
                        si.TotalOut = g.Record.ToList()[0].TotalOut;
                        si.BeginAmount = 0;
                        si.EndAmount = g.Record.ToList()[0].TotalIn - g.Record.ToList()[0].TotalOut;

                        var inout = g.Record.Where(x => x.InOutTypeId == 17);
                        if (inout.Any())
                        {
                            si.ContractFeeCar = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 22);
                        if (inout.Any())
                        {
                            si.ContractFeeEquip = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 25);
                        if (inout.Any())
                        {
                            si.ContractFeeStudent = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 26);
                        if (inout.Any())
                        {
                            si.ContractFeeLoan = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 23);
                        if (inout.Any())
                        {
                            si.ContractFeeStudent = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 14);
                        if (inout.Any())
                        {
                            si.RentFeeCar = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 15);
                        if (inout.Any())
                        {
                            si.RentFeeEquip = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 28);
                        if (inout.Any())
                        {
                            si.RentFeeStudent = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 30);
                        if (inout.Any())
                        {
                            si.RentFeeLoan = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 10);
                        if (inout.Any())
                        {
                            si.InCapital = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 11);
                        if (inout.Any())
                        {
                            si.OutCapital = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 12);
                        if (inout.Any())
                        {
                            si.InOther = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 13);
                        if (inout.Any())
                        {
                            si.OutOther = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 1);
                        if (inout.Any())
                        {
                            si.CloseFeeCar = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 2);
                        if (inout.Any())
                        {
                            si.CloseFeeEquip = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 3);
                        if (inout.Any())
                        {
                            si.CloseFeeStudent = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 18 && x.RentTypeId == 4);
                        if (inout.Any())
                        {
                            si.CloseFeeLoan = inout.Sum(x => x.InAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 1);
                        if (inout.Any())
                        {
                            si.RedundantFeeCar = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 2);
                        if (inout.Any())
                        {
                            si.RedundantFeeEquip = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 3);
                        if (inout.Any())
                        {
                            si.RedundantFeeStudent = inout.Sum(x => x.OutAmount);
                        }

                        inout = g.Record.Where(x => x.InOutTypeId == 19 && x.RentTypeId == 4);
                        if (inout.Any())
                        {
                            si.RedundantFeeLoan = inout.Sum(x => x.OutAmount);
                        }

                        lst.Add(si);
                    }

                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (i > 0)
                        {
                            lst[i].BeginAmount = lst[i - 1].EndAmount;
                            lst[i].EndAmount += lst[i].BeginAmount;
                        }
                    }


                    rptInOut.DataSource = lst.OrderByDescending(c =>c.Period);
                    rptInOut.DataBind();
                    decimal sumIn = 0;
                    decimal sumOut = 0;
                    decimal sumBegin = 0;
                    decimal sumEnd = 0;
                    if (lst.Any())
                    {
                        sumBegin = lst[0].BeginAmount;
                        foreach (SummaryInfo itm in lst)
                        {
                            sumIn += itm.TotalIn;
                            sumOut += itm.TotalOut;
                        }
                        sumEnd = sumIn - sumOut;

                        Label lblTotalIn = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalIn");
                        Label lblTotalOut = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalOut");
                        Label lblTotalBegin = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalBegin");
                        Label lblTotalEnd = (Label)rptInOut.Controls[rptInOut.Controls.Count - 1].Controls[0].FindControl("lblTotalEnd");
                        lblTotalIn.Text = string.Format("{0:0,0}", sumIn);
                        lblTotalOut.Text = string.Format("{0:0,0}", sumOut);
                        lblTotalBegin.Text = string.Format("{0:0,0}", sumBegin);
                        lblTotalEnd.Text = string.Format("{0:0,0}", sumEnd);
                    }
                }
            }

        }

        private List<Contract> GetContractFeeByDay(DateTime date, DeiofiberEntities db)
        {
            var data = from d in db.Contracts
                       where EntityFunctions.TruncateTime(d.RENT_DATE) == EntityFunctions.TruncateTime(date)
                             select d;
              return data.ToList();
        }

        private decimal GetRentFeeByDay(int rentType, DateTime date, DeiofiberEntities db)
        {
            var data = from d in db.Contracts
                       where d.RENT_TYPE_ID == rentType
                       && EntityFunctions.TruncateTime(d.RENT_DATE) == EntityFunctions.TruncateTime(date)
                       select d;
            if (data.Any())
            {
                return data.Sum(x => x.FEE_PER_DAY);
            }
            return 0;
        }


        private void LoadMiddle()
        {
            using (var db = new DeiofiberEntities())
            {
                if (CheckAdminPermission())
                {
                    DropDownList drpStore = this.Master.FindControl("ddlStore") as DropDownList;
                    int storeId = Helper.parseInt(drpStore.SelectedValue);
                    List<CONTRACT_FULL_VW> contrList;

                    if (storeId != 0)
                    {
                        var item1 = from itm1 in db.CONTRACT_FULL_VW
                                    where itm1.CONTRACT_STATUS == true && itm1.STORE_ID == storeId 
                                    select itm1;
                        contrList = item1.ToList();
                    }
                    else
                    {
                        var item1 = from itm1 in db.CONTRACT_FULL_VW
                                    where itm1.CONTRACT_STATUS == true
                                    select itm1;
                        contrList = item1.ToList();
                    }

                    decimal bikeAmount = contrList.Where(c => c.RENT_TYPE_ID == 1).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();
                    decimal equipAmount = contrList.Where(c => c.RENT_TYPE_ID == 2).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();
                    decimal studentAmount = contrList.Where(c => c.RENT_TYPE_ID == 3).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();
                    decimal loanAmount = contrList.Where(c => c.RENT_TYPE_ID == 4).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();

                    lblDeiofiberAmount.Text = bikeAmount == 0 ? "0" : string.Format("{0:0,0}", bikeAmount);
                    lblRentEquipAmount.Text = equipAmount == 0 ? "0" : string.Format("{0:0,0}", equipAmount);
                    lblRentStudentAmount.Text = studentAmount == 0 ? "0" : string.Format("{0:0,0}", studentAmount);
                    lblLoanAmount.Text = loanAmount == 0 ? "0" : string.Format("{0:0,0}", loanAmount);
                    lblRentAll.Text = bikeAmount + equipAmount + studentAmount == 0 ? "0" : string.Format("{0:0,0}", (bikeAmount + equipAmount + studentAmount + loanAmount));


                    //============================================================
                    List<InOut> ioList;
                    if (storeId != 0)
                    {
                        var item2 = from itm2 in db.InOuts
                                    where itm2.STORE_ID == storeId
                                    select itm2;

                        ioList = item2.ToList();
                    }
                    else
                    {
                        var item2 = from itm2 in db.InOuts
                                    select itm2;

                        ioList = item2.ToList();
                    }
                    decimal totalIn = 0;
                    decimal totalOut = 0;
                    foreach (InOut io in ioList)
                    {
                        totalIn += io.IN_AMOUNT;
                        totalOut += io.OUT_AMOUNT;
                    }
                    lblSumAllIn.Text = totalIn == 0 ? "0" : string.Format("{0:0,0}", totalIn);
                    lblSumAllOut.Text = totalOut == 0 ? "0" : string.Format("{0:0,0}", totalOut);

                    decimal totalCapital = 0;
                    List<Store> storeList;
                    if (storeId != 0)
                    {
                        var item3 = from itm3 in db.Stores
                                    where itm3.ID == storeId
                                    select itm3;
                        storeList = item3.ToList();
                    }
                    else
                    {
                        var item3 = from itm3 in db.Stores
                                    select itm3;
                        storeList = item3.ToList();
                    }
                    foreach (Store st in storeList)
                    {
                        totalCapital += st.START_CAPITAL;
                    }
                    lblTotalInvest.Text = totalCapital == 0 ? "0" : string.Format("{0:0,0}", totalCapital);
                }
                else // NOT ADMIN
                {
                    int storeid = Convert.ToInt32(Session["store_id"]);
                    var item1 = from itm1 in db.CONTRACT_FULL_VW
                                where itm1.STORE_ID == storeid
                                select itm1;

                    List<CONTRACT_FULL_VW> contrList = item1.Where(c => c.CONTRACT_STATUS == true).ToList();
                    decimal bikeAmount = contrList.Where(c => c.RENT_TYPE_ID == 1).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();
                    decimal equipAmount = contrList.Where(c => c.RENT_TYPE_ID == 2).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();
                    decimal studentAmount = contrList.Where(c => c.RENT_TYPE_ID == 3).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();
                    decimal loanAmount = contrList.Where(c => c.RENT_TYPE_ID == 4).Select(c => c.CONTRACT_AMOUNT).DefaultIfEmpty().Sum();

                    lblDeiofiberAmount.Text = bikeAmount == 0 ? "0" : string.Format("{0:0,0}", bikeAmount);
                    lblRentEquipAmount.Text = equipAmount == 0 ? "0" : string.Format("{0:0,0}", equipAmount);
                    lblRentStudentAmount.Text = studentAmount == 0 ? "0" : string.Format("{0:0,0}", studentAmount);
                    lblLoanAmount.Text = loanAmount == 0 ? "0" : string.Format("{0:0,0}", loanAmount);
                    lblRentAll.Text = bikeAmount + equipAmount + studentAmount == 0 ? "0" : string.Format("{0:0,0}", (bikeAmount + equipAmount + studentAmount + loanAmount));


                    //============================================================
                    var item2 = from itm2 in db.InOuts
                                where itm2.STORE_ID == storeid
                                select itm2;

                    List<InOut> ioList = item2.ToList();
                    decimal totalIn = 0;
                    decimal totalOut = 0;
                    foreach (InOut io in ioList)
                    {
                        totalIn += io.IN_AMOUNT;
                        totalOut += io.OUT_AMOUNT;
                    }
                    lblSumAllIn.Text = totalIn == 0 ? "0" : string.Format("{0:0,0}", totalIn);
                    lblSumAllOut.Text = totalOut == 0 ? "0" : string.Format("{0:0,0}", totalOut);

                    decimal totalCapital = 0;
                    var item3 = from itm3 in db.Stores
                                where itm3.ID == storeid
                                select itm3;
                    List<Store> storeList = item3.ToList();
                    foreach (Store st in storeList)
                    {
                        totalCapital += st.START_CAPITAL;
                    }
                    lblTotalInvest.Text = totalCapital == 0 ? "0" : string.Format("{0:0,0}", totalCapital);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //int storeid = Convert.ToInt32(ddlStore.SelectedValue);
            //DateTime dt = Convert.ToDateTime(txtViewDate.Text);
            ////int year = Convert.ToDateTime(txtViewDate.Text).Year;
            ////int month = Convert.ToDateTime(txtViewDate.Text).Month;
            //using (var db = new DeiofiberEntities())
            //{
            //    var data = from d in db.InOuts
            //               where d.STORE_ID == storeid && EntityFunctions.TruncateTime(d.INOUT_DATE) == EntityFunctions.TruncateTime(dt)
            //               select d;

            //    List<InOut> ioList = data.ToList();

            //    decimal sumIn = 0;
            //    decimal sumOut = 0;
            //    foreach (InOut io in ioList)
            //    {
            //        sumIn += io.IN_AMOUNT;
            //        sumOut += io.OUT_AMOUNT;
            //    }
            //    lblViewDate.Text = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(txtViewDate.Text));
            //    lblTotalIn.Text = sumIn == 0 ? "0" : string.Format("{0:0,0}", sumIn);
            //    lblTotalOut.Text = sumOut == 0 ? "0" : string.Format("{0:0,0}", sumOut);
            //    lblStoreName.Text = ddlStore.SelectedItem.Text;

            //    var databefore = from d in db.InOuts
            //                     where d.STORE_ID == storeid && EntityFunctions.TruncateTime(d.INOUT_DATE) > EntityFunctions.TruncateTime(dt)
            //                     select d;
            //    List<InOut> beforeList = databefore.ToList();
            //    decimal sumInBefore = 0;
            //    decimal sumOutBefore = 0;
            //    foreach (InOut io in ioList)
            //    {
            //        sumInBefore += io.IN_AMOUNT;
            //        sumOutBefore += io.OUT_AMOUNT;
            //    }
            //    decimal startAmount = sumInBefore - sumOutBefore;
            //    decimal endAmount = startAmount + sumIn - sumOut;
            //    lblStartAmount.Text = startAmount == 0 ? "0" : string.Format("{0:0,0}", startAmount);
            //    lblEndAmount.Text = endAmount == 0 ? "0" : string.Format("{0:0,0}", endAmount);
            //}


            //if (CheckAdminPermission())
            LoadDetailData(Convert.ToInt32(ddlStore.SelectedValue), txtViewDate.Text, 0);
            //else
            //btnSearch.Enabled = false;
        }

        int pageSize = 20;
        private void LoadDetailData(int storeid, string searchDate, int page)
        {
            // LOAD PAGER
            int totalRecord = 0;
            using (var db = new DeiofiberEntities())
            {
                if (!string.IsNullOrEmpty(searchDate))
                {
                    DateTime sDate = Convert.ToDateTime(searchDate);
                    totalRecord = (from c in db.InOuts
                                   where EntityFunctions.TruncateTime(c.INOUT_DATE) == EntityFunctions.TruncateTime(sDate) && c.STORE_ID == storeid
                                   select c).Count();
                }
                else 
                {
                    totalRecord = (from c in db.InOuts
                                   where c.STORE_ID == storeid
                                   select c).Count();
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
            List<INOUT_FULL_VW> dataList;
            int skip = page * pageSize;
            using (var db = new DeiofiberEntities())
            {
                if (!string.IsNullOrEmpty(searchDate))
                {
                    DateTime sDate = Convert.ToDateTime(searchDate);
                    var st = from s in db.INOUT_FULL_VW
                             where EntityFunctions.TruncateTime(s.INOUT_DATE) == EntityFunctions.TruncateTime(sDate) && s.STORE_ID == storeid
                             orderby s.ID descending
                             select s;
                    dataList = st.Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    var st = from s in db.INOUT_FULL_VW
                             where s.STORE_ID == storeid
                             orderby s.ID descending
                             select s;
                    dataList = st.Skip(skip).Take(pageSize).ToList();
                }

            }

            rptInOutDetail.DataSource = dataList;
            rptInOutDetail.DataBind();
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

        protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDetailData(Convert.ToInt32(ddlStore.SelectedValue), txtViewDate.Text, Convert.ToInt32(ddlPager.SelectedValue) - 1);
        }

        private void LoadStore(int permissionid)
        {
            ddlStore.Items.Add(new ListItem("--Tất cả cửa hàng--", ""));
            CommonList.LoadStore(ddlStore);
            if (permissionid != 1)
            {
                ddlStore.SelectedValue = Session["store_id"].ToString();
                ddlStore.Enabled = false;
            }
        }
    }

    public class SummaryInfo
    {
        public SummaryInfo()
        { }

        public int StoreId { get; set; }
        public DateTime Period { get; set; }
        public decimal BeginAmount { get; set; }
        public decimal EndAmount { get; set; }
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public decimal ContractFeeCar { get; set; }
        public decimal RentFeeCar { get; set; }
        public decimal CloseFeeCar { get; set; }
        public decimal ContractFeeEquip { get; set; }
        public decimal RentFeeEquip { get; set; }
        public decimal CloseFeeEquip { get; set; }
        public decimal ContractFeeStudent { get; set; }
        public decimal RentFeeStudent { get; set; }
        public decimal CloseFeeStudent { get; set; }
        public decimal ContractFeeLoan { get; set; }
        public decimal RentFeeLoan { get; set; }
        public decimal CloseFeeLoan { get; set; }
        public decimal RemainEndOfDay { get; set; }
        public decimal InCapital { get; set; }
        public decimal OutCapital { get; set; }
        public decimal InOther { get; set; }
        public decimal OutOther { get; set; }
        public decimal RedundantFeeCar { get; set; }
        public decimal RedundantFeeEquip { get; set; }
        public decimal RedundantFeeStudent { get; set; }
        public decimal RedundantFeeLoan { get; set; }
        

    }
}