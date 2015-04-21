using Deiofiber.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deiofiber
{
    public partial class FormContractUpdate : System.Web.UI.Page
    {
        int pageSize = 10;
        protected string ContractID { get; set; }
        protected int RentTypeID { get; set; }
        protected bool IsNewContract { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["store_id"] == null)
            {
                Response.Redirect("FormLogin.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    CommonList.LoadRentType(ddlRentType);
                    CommonList.LoadStore(ddlStore);
                    hdfFeeRate.Value = (GetFeeRate(Convert.ToInt32(Session["store_id"])) / 10000).ToString();
                    string id = Request.QueryString["ID"];
                    string copy = Request.QueryString["copy"];
                    int storeId = Convert.ToInt32(Session["store_id"]);

                    if (!string.IsNullOrEmpty(id) && string.IsNullOrEmpty(copy)) // EDIT
                    {
                        using (var db = new DeiofiberEntities())
                        {
                            int contractId = Helper.parseInt(id);
                            var contract = db.Contracts.Where(c => c.ID == contractId && c.CONTRACT_STATUS == true).FirstOrDefault();
                            if (contract != null)
                            {
                                IsNewContract = false;
                                ContractID = id;
                                List<CONTRACT_FULL_VW> lst;
                                int contractid = Convert.ToInt32(id);

                                Store stor = new Store();
                                stor = db.Stores.FirstOrDefault(s => s.ID == storeId);

                                var st = from s in db.CONTRACT_FULL_VW
                                         where s.ID == contractid
                                         select s;

                                lst = st.ToList<CONTRACT_FULL_VW>();
                                bool bAdmin = CheckAdminPermission();
                                if (bAdmin)
                                {
                                    storeId = lst[0].STORE_ID;
                                }
                                ddlStore.SelectedValue = storeId.ToString();
                                if (!bAdmin)
                                {
                                    ddlStore.SelectedValue = storeId.ToString();
                                    ddlStore.Enabled = false;
                                }
                                pnlTable.Enabled = lst[0].CONTRACT_STATUS;
                                rptPayFeeSchedule.Visible = lst[0].CONTRACT_STATUS;

                                CONTRACT_FULL_VW cntrct = lst[0];
                                txtLicenseNumber.Text = cntrct.LICENSE_NO;
                                txtCustomerName.Text = cntrct.CUSTOMER_NAME;
                                if (cntrct.BIRTH_DAY.HasValue)
                                    txtBirthDay.Text = string.Format("{0:dd/MM/yyyy}", cntrct.BIRTH_DAY);
                                if (cntrct.LICENSE_RANGE_DATE.HasValue)
                                    txtRangeDate.Text = string.Format("{0:dd/MM/yyyy}", cntrct.LICENSE_RANGE_DATE);
                                txtPlaceDate.Text = cntrct.LICENSE_RANGE_PLACE;
                                txtPhone.Text = cntrct.PHONE;
                                txtPermanentResidence.Text = cntrct.PERMANENT_RESIDENCE;
                                txtCurrentResidence.Text = cntrct.CURRENT_RESIDENCE;
                                txtContractNo.Text = cntrct.CONTRACT_NO;
                                var rentType = db.RentTypes.Where(c => c.NAME == cntrct.RENT_TYPE_NAME).FirstOrDefault();
                                ddlRentType.SelectedValue = rentType.ID.ToString();
                                RentTypeID = cntrct.RENT_TYPE_ID;
                                txtAmount.Text = string.Format("{0:0,0}", cntrct.CONTRACT_AMOUNT);
                                txtFeePerDay.Text = string.Format("{0:0,0}", cntrct.FEE_PER_DAY);
                                txtRentDate.Text = string.Format("{0:dd/MM/yyyy}", cntrct.RENT_DATE);
                                txtEndDate.Text = string.Format("{0:dd/MM/yyyy}", cntrct.END_DATE);
                                txtNote.Text = cntrct.NOTE;

                                txtReferencePerson.Text = cntrct.REFERENCE_NAME;
                                txtItemName.Text = cntrct.ITEM_TYPE;
                                txtItemLicenseNo.Text = cntrct.ITEM_LICENSE_NO;
                                txtSerial1.Text = cntrct.SERIAL_1;
                                txtSerial2.Text = cntrct.SERIAL_2;
                                txtImplementer.Text = cntrct.IMPLEMENTER;
                                txtBackDocument.Text = cntrct.BACK_TO_DOCUMENTS;
                                txtItemDetail.Text = cntrct.DETAIL;

                                txtContractNo.Enabled = ddlRentType.Enabled = txtAmount.Enabled = txtFeePerDay.Enabled = txtRentDate.Enabled = txtEndDate.Enabled = false;
                                BuildPhotoLibrary(cntrct);
                                LoadPayFeeSchedule();
                            }
                        }
                    }
                    else // NEW
                    {
                        IsNewContract = true;
                        btnFinishContract.Visible = false;
                        txtContractNo.Visible = false;
                        using (var db = new DeiofiberEntities())
                        {
                            Store stor = new Store();
                            if (storeId != 0)
                            {
                                stor = db.Stores.FirstOrDefault(s => s.ID == storeId);
                                if (!CheckAdminPermission())
                                {
                                    ddlStore.SelectedValue = storeId.ToString();
                                    ddlStore.Enabled = false;
                                }
                            }
                            RentTypeID = Convert.ToInt32(ddlRentType.SelectedValue);
                            txtRentDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                            txtEndDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddDays(29));

                            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(copy))
                            {
                                int contractid = Convert.ToInt32(id);
                                CONTRACT_FULL_VW cntrct = db.CONTRACT_FULL_VW.Where(s => s.ID == contractid).FirstOrDefault();
                                if (cntrct != null)
                                {
                                    bool bAdmin = CheckAdminPermission();
                                    if (bAdmin)
                                    {
                                        storeId = cntrct.STORE_ID;
                                    }
                                    ddlStore.SelectedValue = storeId.ToString();
                                    if (!bAdmin)
                                    {
                                        ddlStore.SelectedValue = storeId.ToString();
                                        ddlStore.Enabled = false;
                                    }
                                    txtLicenseNumber.Text = cntrct.LICENSE_NO;
                                    txtCustomerName.Text = cntrct.CUSTOMER_NAME;
                                    if (cntrct.BIRTH_DAY.HasValue)
                                        txtBirthDay.Text = string.Format("{0:dd/MM/yyyy}", cntrct.BIRTH_DAY);
                                    if (cntrct.LICENSE_RANGE_DATE.HasValue)
                                        txtRangeDate.Text = string.Format("{0:dd/MM/yyyy}", cntrct.LICENSE_RANGE_DATE);
                                    txtPlaceDate.Text = cntrct.LICENSE_RANGE_PLACE;
                                    txtPhone.Text = cntrct.PHONE;
                                    txtPermanentResidence.Text = cntrct.PERMANENT_RESIDENCE;
                                    txtCurrentResidence.Text = cntrct.CURRENT_RESIDENCE;
                                    txtContractNo.Text = cntrct.CONTRACT_NO;
                                    var rentType = db.RentTypes.Where(c => c.NAME == cntrct.RENT_TYPE_NAME).FirstOrDefault();
                                    ddlRentType.SelectedValue = rentType.ID.ToString();
                                    RentTypeID = cntrct.RENT_TYPE_ID;
                                    txtAmount.Text = string.Format("{0:0,0}", cntrct.CONTRACT_AMOUNT);
                                    txtFeePerDay.Text = string.Format("{0:0,0}", cntrct.FEE_PER_DAY);
                                    txtNote.Text = cntrct.NOTE;

                                    txtReferencePerson.Text = cntrct.REFERENCE_NAME;
                                    txtItemName.Text = cntrct.ITEM_TYPE;
                                    txtItemLicenseNo.Text = cntrct.ITEM_LICENSE_NO;
                                    txtSerial1.Text = cntrct.SERIAL_1;
                                    txtSerial2.Text = cntrct.SERIAL_2;
                                    txtImplementer.Text = cntrct.IMPLEMENTER;
                                    txtBackDocument.Text = cntrct.BACK_TO_DOCUMENTS;
                                    txtItemDetail.Text = cntrct.DETAIL;

                                    BuildPhotoLibrary(cntrct);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.CssClass = "text-center text-danger";
                }
            }
        }

        private void BuildPhotoLibrary(CONTRACT_FULL_VW cntrct)
        {
            // Initialize StringWriter instance.
            StringWriter stringWriter = new StringWriter();

            // Put HtmlTextWriter in using block because it needs to call Dispose.
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                if (!string.IsNullOrEmpty(cntrct.PHOTO_1) && !string.IsNullOrEmpty(cntrct.THUMBNAIL_PHOTO_1))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "fancybox-buttons");
                    writer.AddAttribute("data-fancybox-group", "button");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, cntrct.PHOTO_1);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    writer.AddAttribute("src", cntrct.THUMBNAIL_PHOTO_1);
                    writer.AddAttribute("alt", "");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag(); //End of Img

                    writer.RenderEndTag(); //End of A
                }
                if (!string.IsNullOrEmpty(cntrct.PHOTO_2) && !string.IsNullOrEmpty(cntrct.THUMBNAIL_PHOTO_2))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "fancybox-buttons");
                    writer.AddAttribute("data-fancybox-group", "button");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, cntrct.PHOTO_2);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    writer.AddAttribute("src", cntrct.THUMBNAIL_PHOTO_2);
                    writer.AddAttribute("alt", "");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag(); //End of Img

                    writer.RenderEndTag(); //End of A
                }
                if (!string.IsNullOrEmpty(cntrct.PHOTO_3) && !string.IsNullOrEmpty(cntrct.THUMBNAIL_PHOTO_3))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "fancybox-buttons");
                    writer.AddAttribute("data-fancybox-group", "button");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, cntrct.PHOTO_3);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    writer.AddAttribute("src", cntrct.THUMBNAIL_PHOTO_3);
                    writer.AddAttribute("alt", "");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag(); //End of Img

                    writer.RenderEndTag(); //End of A
                }
                if (!string.IsNullOrEmpty(cntrct.PHOTO_4) && !string.IsNullOrEmpty(cntrct.THUMBNAIL_PHOTO_4))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "fancybox-buttons");
                    writer.AddAttribute("data-fancybox-group", "button");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, cntrct.PHOTO_4);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    writer.AddAttribute("src", cntrct.THUMBNAIL_PHOTO_4);
                    writer.AddAttribute("alt", "");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag(); //End of Img

                    writer.RenderEndTag(); //End of A
                }
                if (!string.IsNullOrEmpty(cntrct.PHOTO_5) && !string.IsNullOrEmpty(cntrct.THUMBNAIL_PHOTO_5))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "fancybox-buttons");
                    writer.AddAttribute("data-fancybox-group", "button");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, cntrct.PHOTO_5);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    writer.AddAttribute("src", cntrct.THUMBNAIL_PHOTO_5);
                    writer.AddAttribute("alt", "");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag(); //End of Img

                    writer.RenderEndTag(); //End of A
                }
                writer.RenderEndTag();  //End of P

                litPhoto.Text = stringWriter.ToString();
            }
        }
        private StringWriter BuildPhotoData(string photo, string thumbnailPhoto)
        {
            // Initialize StringWriter instance.
            StringWriter stringWriter = new StringWriter();

            // Put HtmlTextWriter in using block because it needs to call Dispose.
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                if (!string.IsNullOrEmpty(photo) && !string.IsNullOrEmpty(thumbnailPhoto))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "fancybox-buttons");
                    writer.AddAttribute("data-fancybox-group", "button");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, photo);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);

                    writer.AddAttribute("src", thumbnailPhoto);
                    writer.AddAttribute("alt", "");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag(); //End of Img

                    writer.RenderEndTag(); //End of A
                }
                return stringWriter;
            }
        }

        private List<CONTRACT_FULL_VW> LoadData(string strSearch, int page)
        {
            // LOAD PAGER
            int totalRecord = 0;
            using (var db = new DeiofiberEntities())
            {
                var count = (from c in db.Contracts
                             where c.SEARCH_TEXT.Contains(strSearch)
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
            if (pageList.Count > 0)
            {
                ddlPager.SelectedIndex = page;
            }

            // LOAD DATA WITH PAGING
            List<CONTRACT_FULL_VW> dataList;
            int skip = page * pageSize;
            using (var db = new DeiofiberEntities())
            {
                var st = from s in db.CONTRACT_FULL_VW
                         where s.SEARCH_TEXT.Contains(strSearch)
                         orderby s.ID
                         select s;

                dataList = st.Skip(skip).Take(pageSize).ToList();
            }

            rptCustomer.DataSource = dataList;
            rptCustomer.DataBind();
            return dataList;
        }

        protected string ValidateFields()
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                return "Bạn cần phải nhập tên khách hàng.";
            }
            if (string.IsNullOrEmpty(txtLicenseNumber.Text.Trim()))
            {
                return "Bạn phải nhập số CMT.";
            }
            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                return "Bạn phải nhập số điện thoại.";
            }
            if (string.IsNullOrEmpty(txtAmount.Text.Trim()))
            {
                return "Bạn phải nhập số tiền hợp đồng.";
            }
            if (string.IsNullOrEmpty(txtFeePerDay.Text.Trim()))
            {
                return "Bạn phải nhập phí thuê ngày.";
            }
            if (string.IsNullOrEmpty(txtRentDate.Text.Trim()))
            {
                return "Bạn phải nhập ngày bắt đầu hợp đồng.";
            }
            if (string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                return "Bạn phải nhập ngày kết thúc hợp đồng.";
            }
            if (ddlRentType.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(txtReferencePerson.Text.Trim()))
                {
                    return "Bạn phải nhập tên người xác minh.";
                }
                if (string.IsNullOrEmpty(txtItemName.Text.Trim()))
                {
                    return "Bạn phải nhập loại xe.";
                }
                if (string.IsNullOrEmpty(txtItemLicenseNo.Text.Trim()))
                {
                    return "Bạn phải nhập biến kiểm soát.";
                }
                //if (string.IsNullOrEmpty(txtSerial1.Text.Trim()))
                //{
                //    return "Bạn phải nhập số khung.";
                //}
                //if (string.IsNullOrEmpty(txtSerial2.Text.Trim()))
                //{
                //    return "Bạn phải nhập số máy.";
                //}
            }
            if (Request.Files.Count > 5)
            {
                return "Bạn không thể chọn quá 5 file ảnh.";
            }
            return string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    string id = Request.QueryString["ID"];
                    string copy = Request.QueryString["copy"];
                    string result = ValidateFields();
                    int cusid = 0;
                    if (string.IsNullOrEmpty(id) || (!string.IsNullOrEmpty(copy) && copy == "1")) // NEW
                    {
                        if (!string.IsNullOrEmpty(result))
                        {
                            lblMessage.Text = result;
                            return;
                        }
                        using (var db = new DeiofiberEntities())
                        {
                            bool IsNewCust = false;
                            Customer cusItem = db.Customers.FirstOrDefault(c => c.LICENSE_NO == txtLicenseNumber.Text.Trim() && c.NAME == txtCustomerName.Text.Trim());
                            if (cusItem == null)
                            {
                                IsNewCust = true;
                                cusItem = new Customer();
                            }

                            cusItem.NAME = txtCustomerName.Text.Trim();
                            if (!string.IsNullOrEmpty(txtBirthDay.Text))
                            {
                                cusItem.BIRTH_DAY = DateTime.ParseExact(txtBirthDay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            cusItem.LICENSE_NO = txtLicenseNumber.Text.Trim();
                            if (!string.IsNullOrEmpty(txtRangeDate.Text))
                            {
                                cusItem.LICENSE_RANGE_DATE = DateTime.ParseExact(txtRangeDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            cusItem.LICENSE_RANGE_PLACE = txtPlaceDate.Text.Trim();
                            cusItem.PHONE = txtPhone.Text.Trim();
                            cusItem.PERMANENT_RESIDENCE = txtPermanentResidence.Text.Trim();
                            cusItem.CURRENT_RESIDENCE = txtCurrentResidence.Text.Trim();

                            if (IsNewCust)
                            {
                                db.Customers.Add(cusItem);
                            }
                            db.SaveChanges();
                            cusid = cusItem.ID;
                        }

                        // New Contract
                        Contract item = new Contract();
                        item.RENT_TYPE_ID = Convert.ToInt32(ddlRentType.SelectedValue);
                        item.FEE_PER_DAY = Math.Round(Convert.ToDecimal(txtFeePerDay.Text.Replace(",", string.Empty)));
                        if (!string.IsNullOrEmpty(txtRentDate.Text))
                        {
                            item.RENT_DATE = DateTime.ParseExact(txtRentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                            item.RENT_DATE = DateTime.Now;

                        if (!string.IsNullOrEmpty(txtEndDate.Text))
                        {
                            item.END_DATE = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                            item.END_DATE = item.RENT_DATE.AddDays(29);
                        item.CLOSE_CONTRACT_DATE = new DateTime(1, 1, 1);
                        item.NOTE = txtNote.Text;
                        item.REFERENCE_NAME = txtReferencePerson.Text.Trim();
                        item.ITEM_TYPE = txtItemName.Text.Trim();
                        item.ITEM_LICENSE_NO = txtItemLicenseNo.Text.Trim();
                        item.SERIAL_1 = txtSerial1.Text.Trim();
                        item.SERIAL_2 = txtSerial2.Text.Trim();
                        item.IMPLEMENTER = txtImplementer.Text.Trim();
                        item.BACK_TO_DOCUMENTS = txtBackDocument.Text.Trim();
                        item.DETAIL = txtItemDetail.Text.Trim();
                        item.CUSTOMER_ID = cusid;
                        item.CONTRACT_STATUS = true;
                        item.CONTRACT_AMOUNT = Convert.ToDecimal(txtAmount.Text.Replace(",", string.Empty));
                        item.CREATED_BY = Session["username"].ToString();
                        item.CREATED_DATE = DateTime.Now;
                        item.UPDATED_BY = Session["username"].ToString();
                        item.UPDATED_DATE = DateTime.Now;
                        //item.CONTRACT_NO = txtContractNo.Text.Trim();
                        if (ddlStore.Enabled == true)
                            item.STORE_ID = Convert.ToInt32(ddlStore.SelectedValue);
                        else
                            item.STORE_ID = Convert.ToInt32(Session["store_id"]);
                        item.SEARCH_TEXT = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                                        txtCustomerName.Text.Trim(),
                                                        txtBirthDay.Text.Trim(),
                                                        txtLicenseNumber.Text.Trim(),
                                                        txtRangeDate.Text.Trim(),
                                                        txtPermanentResidence.Text.Trim(),
                                                        txtCurrentResidence.Text.Trim(),
                                                        txtPhone.Text.Trim(),
                                                        item.CONTRACT_NO,
                                                        item.RENT_DATE.ToString("dd/MM/yyyy"));
                        SavePhoto(item);
                        using (var db = new DeiofiberEntities())
                        {
                            db.Contracts.Add(item);
                            db.SaveChanges();
                        }

                        DateTime periodTime = DateTime.Now;
                        if (!string.IsNullOrEmpty(txtRentDate.Text))
                        {
                            periodTime = DateTime.ParseExact(txtRentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        PayPeriod pp1 = new PayPeriod();
                        pp1.CONTRACT_ID = item.ID;
                        pp1.PAY_DATE = periodTime;
                        pp1.AMOUNT_PER_PERIOD = item.FEE_PER_DAY * 10;
                        pp1.STATUS = true;
                        pp1.ACTUAL_PAY = 0;

                        PayPeriod pp2 = new PayPeriod();
                        pp2.CONTRACT_ID = item.ID;
                        pp2.PAY_DATE = periodTime.AddDays(9);
                        pp2.AMOUNT_PER_PERIOD = item.FEE_PER_DAY * 10;
                        pp2.STATUS = true;
                        pp2.ACTUAL_PAY = 0;

                        PayPeriod pp3 = new PayPeriod();
                        pp3.CONTRACT_ID = item.ID;
                        pp3.PAY_DATE = periodTime.AddDays(19);
                        pp3.AMOUNT_PER_PERIOD = item.FEE_PER_DAY * 10;
                        pp3.STATUS = true;
                        pp3.ACTUAL_PAY = 0;

                        using (var rbdb = new DeiofiberEntities())
                        {
                            rbdb.PayPeriods.Add(pp1);
                            rbdb.PayPeriods.Add(pp2);
                            rbdb.PayPeriods.Add(pp3);
                            rbdb.SaveChanges();
                        }

                        InOut io = new InOut();
                        io.CONTRACT_ID = item.ID;
                        io.IN_AMOUNT = 0;
                        io.OUT_AMOUNT = Convert.ToDecimal(txtAmount.Text);
                        io.RENT_TYPE_ID = Convert.ToInt32(ddlRentType.SelectedValue);
                        io.PERIOD_DATE = DateTime.Now;
                        io.MORE_INFO = string.Format("Cho khách {0} thuê: {1} ngày {2} trị giá {3}", txtCustomerName.Text.Trim(), txtItemName.Text.Trim(), DateTime.Now.ToString("dd/MM/yyyy"), txtAmount.Text.Trim());
                        if (ddlStore.Enabled == true)
                            io.STORE_ID = Convert.ToInt32(ddlStore.SelectedValue);
                        else
                            io.STORE_ID = Convert.ToInt32(Session["store_id"]);
                        io.SEARCH_TEXT = string.Format("{0} ", io.MORE_INFO);
                        io.INOUT_DATE = DateTime.Now;
                        io.CREATED_BY = Session["username"].ToString();
                        io.CREATED_DATE = DateTime.Now;
                        io.UPDATED_BY = Session["username"].ToString();
                        io.UPDATED_DATE = DateTime.Now;
                        switch (ddlRentType.SelectedValue)
                        {
                            case "1":
                                io.INOUT_TYPE_ID = 17;
                                break;
                            case "2":
                                io.INOUT_TYPE_ID = 22;
                                break;
                            case "3":
                                io.INOUT_TYPE_ID = 25;
                                break;
                            case "4":
                                io.INOUT_TYPE_ID = 26;
                                break;
                            default:
                                List<InOutType> lstiot = new List<InOutType>();
                                using (var db = new DeiofiberEntities())
                                {
                                    var iot = from itm in db.InOutTypes
                                              where itm.IS_CONTRACT == true && itm.ACTIVE == false && itm.IS_INCOME == false
                                              select itm;

                                    lstiot = iot.ToList();
                                    if (lstiot.Count > 0)
                                    {
                                        io.INOUT_TYPE_ID = lstiot[0].ID;
                                    }
                                }
                                break;
                        }
                        using (var rbdb = new DeiofiberEntities())
                        {
                            rbdb.InOuts.Add(io);
                            rbdb.SaveChanges();
                        }

                        WriteLog(CommonList.ACTION_CREATE_CONTRACT, false);
                        ts.Complete();
                    }
                    else // EDIT
                    {
                        int contractId = Convert.ToInt32(id);
                        using (var db = new DeiofiberEntities())
                        {
                            var item = db.Contracts.FirstOrDefault(itm => itm.ID == contractId);

                            item.NOTE = txtNote.Text;
                            item.REFERENCE_NAME = txtReferencePerson.Text.Trim();
                            item.ITEM_TYPE = txtItemName.Text.Trim();
                            item.ITEM_LICENSE_NO = txtItemLicenseNo.Text.Trim();
                            item.SERIAL_1 = txtSerial1.Text.Trim();
                            item.SERIAL_2 = txtSerial2.Text.Trim();
                            item.IMPLEMENTER = txtImplementer.Text.Trim();
                            item.BACK_TO_DOCUMENTS = txtBackDocument.Text.Trim();
                            item.DETAIL = txtItemDetail.Text.Trim();
                            item.CONTRACT_STATUS = true;
                            item.CONTRACT_AMOUNT = Convert.ToDecimal(txtAmount.Text.Replace(",", string.Empty));
                            item.UPDATED_BY = Session["username"].ToString();
                            item.UPDATED_DATE = DateTime.Now;
                            if (ddlStore.Enabled == true)
                                item.STORE_ID = Convert.ToInt32(ddlStore.SelectedValue);
                            else
                                item.STORE_ID = Convert.ToInt32(Session["store_id"]);
                            item.SEARCH_TEXT = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                                            txtCustomerName.Text.Trim(),
                                                            txtBirthDay.Text.Trim(),
                                                            txtLicenseNumber.Text.Trim(),
                                                            txtRangeDate.Text.Trim(),
                                                            txtPermanentResidence.Text.Trim(),
                                                            txtCurrentResidence.Text.Trim(),
                                                            txtPhone.Text.Trim(),
                                                            item.CONTRACT_NO,
                                                            item.RENT_DATE.ToString("dd/MM/yyyy"));
                            SavePhoto(item);
                            var cusItem = db.Customers.FirstOrDefault(c => c.ID == item.CUSTOMER_ID);
                            if (cusItem != null)
                            {
                                cusItem.NAME = txtCustomerName.Text.Trim();
                                if (!string.IsNullOrEmpty(txtBirthDay.Text))
                                {
                                    cusItem.BIRTH_DAY = DateTime.ParseExact(txtBirthDay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                }
                                cusItem.LICENSE_NO = txtLicenseNumber.Text.Trim();
                                if (!string.IsNullOrEmpty(txtRangeDate.Text))
                                {
                                    cusItem.LICENSE_RANGE_DATE = DateTime.ParseExact(txtRangeDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                }
                                cusItem.LICENSE_RANGE_PLACE = txtPlaceDate.Text.Trim();
                                cusItem.PHONE = txtPhone.Text.Trim();
                                cusItem.PERMANENT_RESIDENCE = txtPermanentResidence.Text.Trim();
                                cusItem.CURRENT_RESIDENCE = txtCurrentResidence.Text.Trim();
                            }
                            db.SaveChanges();
                        }

                        WriteLog(CommonList.ACTION_UPDATE_CONTRACT, false);
                        ts.Complete();
                    }
                    Response.Redirect("FormContractManagement.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.CssClass = "text-center text-danger";
            }
        }

        private void SavePhoto(Contract item)
        {
            string filepath = Server.MapPath("\\upload");
            HttpFileCollection uploadedFiles = Request.Files;
            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                HttpPostedFile userPostedFile = uploadedFiles[i];
                try
                {
                    if (userPostedFile.ContentLength > 0)
                    {
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(userPostedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(userPostedFile.ContentLength);
                        }
                        byte[] photo = ImageHelper.CreateThumbnail(fileData, 612, 612);
                        byte[] thumbnailPhoto = ImageHelper.CreateThumbnail(fileData, 128, 128);

                        string guid = Guid.NewGuid().ToString();
                        string datetime = DateTime.Now.ToString("yyyyMMddhhmmssfff");

                        string photoPath = filepath + "\\" + guid + "-" + datetime + "-" + (i + 1) + "_b" + ".png";
                        string thumbnailPhotoPath = filepath + "\\" + guid + "-" + datetime + "-" + (i + 1) + "_s" + ".png";

                        using (var fs = new BinaryWriter(new FileStream(photoPath, FileMode.Append, FileAccess.Write)))
                        {
                            fs.Write(photo);
                        }
                        using (var fs = new BinaryWriter(new FileStream(thumbnailPhotoPath, FileMode.Append, FileAccess.Write)))
                        {
                            fs.Write(thumbnailPhoto);
                        }

                        switch (i)
                        {
                            case 0:
                                DeletePhoto(item.PHOTO_1);
                                DeletePhoto(item.THUMBNAIL_PHOTO_1);
                                item.PHOTO_1 = photoPath.Replace(Server.MapPath("~"), string.Empty);
                                item.THUMBNAIL_PHOTO_1 = thumbnailPhotoPath.Replace(Server.MapPath("~"), string.Empty);
                                break;
                            case 1:
                                DeletePhoto(item.PHOTO_2);
                                DeletePhoto(item.THUMBNAIL_PHOTO_2);
                                item.PHOTO_2 = photoPath.Replace(Server.MapPath("~"), string.Empty);
                                item.THUMBNAIL_PHOTO_2 = thumbnailPhotoPath.Replace(Server.MapPath("~"), string.Empty);
                                break;
                            case 2:
                                DeletePhoto(item.PHOTO_3);
                                DeletePhoto(item.THUMBNAIL_PHOTO_3);
                                item.PHOTO_3 = photoPath.Replace(Server.MapPath("~"), string.Empty);
                                item.THUMBNAIL_PHOTO_3 = thumbnailPhotoPath.Replace(Server.MapPath("~"), string.Empty);
                                break;
                            case 3:
                                DeletePhoto(item.PHOTO_4);
                                DeletePhoto(item.THUMBNAIL_PHOTO_4);
                                item.PHOTO_4 = photoPath.Replace(Server.MapPath("~"), string.Empty);
                                item.THUMBNAIL_PHOTO_4 = thumbnailPhotoPath.Replace(Server.MapPath("~"), string.Empty);
                                break;
                            case 4:
                                DeletePhoto(item.PHOTO_5);
                                DeletePhoto(item.THUMBNAIL_PHOTO_5);
                                item.PHOTO_5 = photoPath.Replace(Server.MapPath("~"), string.Empty);
                                item.THUMBNAIL_PHOTO_5 = thumbnailPhotoPath.Replace(Server.MapPath("~"), string.Empty);
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void DeletePhoto(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(Server.MapPath("~") + path))
                    File.Delete(Server.MapPath("~") + path);
            }
            catch { ; }
        }

        private int ExistingContract(string licenseNo)
        {
            using (var db = new DeiofiberEntities())
            {
                int storeId = Convert.ToInt32(Session["store_id"]);
                CONTRACT_FULL_VW cntrct = db.CONTRACT_FULL_VW.Where(s => s.LICENSE_NO == licenseNo).FirstOrDefault();
                if (cntrct != null && cntrct.CONTRACT_STATUS == true)
                {
                    return 1;
                }
                else
                    return 0;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormContractManagement.aspx");
        }

        protected void btnPrincipalPay_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormPrincipalPayment.aspx?id=" + Request.QueryString["ID"]);
        }

        private void LoadPayFeeSchedule()
        {
            string id = Request.QueryString["ID"];
            if (!string.IsNullOrEmpty(id))
            {
                int contractID = Convert.ToInt32(id);
                List<PayPeriod> lst;
                using (var db = new DeiofiberEntities())
                {
                    var st = from s in db.PayPeriods
                             where s.CONTRACT_ID == contractID
                             select s;

                    lst = st.ToList<PayPeriod>();

                    foreach (PayPeriod pp in lst)
                    {
                        var payList = db.InOuts.Where(s => s.PERIOD_ID == pp.ID);
                        if (pp != null)
                        {
                            var sumPay = payList.Select(c => c.IN_AMOUNT).DefaultIfEmpty(0).Sum();
                            if (sumPay > 0)
                            {
                                pp.ACTUAL_PAY = sumPay;
                                db.SaveChanges();
                            }
                        }
                    }

                    lst = (from s in db.PayPeriods
                           where s.CONTRACT_ID == contractID
                           select s).ToList();


                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (lst[i].ACTUAL_PAY > lst[i].AMOUNT_PER_PERIOD)
                        {
                            lst[i + 1].ACTUAL_PAY += lst[i].ACTUAL_PAY - lst[i].AMOUNT_PER_PERIOD;
                        }
                    }
                }

                rptPayFeeSchedule.DataSource = lst;
                rptPayFeeSchedule.DataBind();

            }
            else
            {
                List<PayPeriod> lst = new List<PayPeriod>();
                PayPeriod p1 = new PayPeriod();
                p1.PAY_DATE = DateTime.Now;
                PayPeriod p2 = new PayPeriod();
                p2.PAY_DATE = DateTime.Now.AddDays(9);
                PayPeriod p3 = new PayPeriod();
                p3.PAY_DATE = DateTime.Now.AddDays(19);

                lst.Add(p1);
                lst.Add(p2);
                lst.Add(p3);

                rptPayFeeSchedule.DataSource = lst;
                rptPayFeeSchedule.DataBind();
            }
        }

        protected void btnFinishContract_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["ID"];
            Response.Redirect(string.Format("FormCloseContract.aspx?ID={0}", id));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim(), 0);
        }

        protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim(), Convert.ToInt32(ddlPager.SelectedValue) - 1);
        }

        private decimal GetFeeRate(int storeId)
        {
            decimal fee = 0;
            List<StoreFee> lst = new List<StoreFee>();
            using (var db = new DeiofiberEntities())
            {
                var item = from s in db.StoreFees
                           where s.STORE_ID == storeId
                           select s;
                lst = item.ToList();
                if (lst.Count > 0)
                {
                    fee = Convert.ToDecimal(lst[0].FEE_PERCENT);
                }
            }

            return fee;
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

        // NOT USE ANYMORE
        private void LoadStore(List<Store> lst)
        {
            ddlStore.DataSource = lst;
            ddlStore.DataValueField = "ID";
            ddlStore.DataTextField = "NAME";
            ddlStore.DataBind();
        }

        private static List<Store> GetStoreByCity(int city_id)
        {
            List<Store> lst;
            using (var db = new DeiofiberEntities())
            {
                var st = from s in db.Stores
                         where s.CITY_ID == city_id
                         select s;

                lst = st.ToList<Store>();
            }
            return lst;
        }

        private static List<Customer> GetCustomerByCity(int city_id)
        {
            List<Customer> lst;
            using (var db = new DeiofiberEntities())
            {
                var st = from s in db.Customers
                         where s.CITY_ID == city_id
                         orderby s.NAME
                         select s;

                lst = st.ToList<Customer>();
            }
            return lst;
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

        public bool ShowBlueImage(decimal amountPer, decimal actualPay)
        {
            if (amountPer <= actualPay)
                return true;
            return false;
        }

        public bool ShowOrangeImage(decimal amountPer, decimal actualPay)
        {
            if (actualPay > 0 && amountPer > actualPay)
                return true;
            return false;
        }
    }
}