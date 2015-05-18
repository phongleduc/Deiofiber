//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Deiofiber
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contract
    {
        public int ID { get; set; }
        public int RENT_TYPE_ID { get; set; }
        public int STORE_ID { get; set; }
        public System.DateTime RENT_DATE { get; set; }
        public System.DateTime END_DATE { get; set; }
        public Nullable<System.DateTime> EXTEND_END_DATE { get; set; }
        public Nullable<System.DateTime> CLOSE_CONTRACT_DATE { get; set; }
        public string NOTE { get; set; }
        public string REFERENCE_NAME { get; set; }
        public string ITEM_TYPE { get; set; }
        public string SERIAL_1 { get; set; }
        public string SERIAL_2 { get; set; }
        public string DETAIL { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string SEARCH_TEXT { get; set; }
        public bool CONTRACT_STATUS { get; set; }
        public string ITEM_LICENSE_NO { get; set; }
        public decimal CONTRACT_AMOUNT { get; set; }
        public string REFERENCE_PHONE { get; set; }
        public string SCHOOL_NAME { get; set; }
        public string CLASS_NAME { get; set; }
        public string IMPLEMENTER { get; set; }
        public string BACK_TO_DOCUMENTS { get; set; }
        public string CONTRACT_NO { get; set; }
        public string PHOTO_1 { get; set; }
        public string THUMBNAIL_PHOTO_1 { get; set; }
        public string PHOTO_2 { get; set; }
        public string THUMBNAIL_PHOTO_2 { get; set; }
        public string PHOTO_3 { get; set; }
        public string THUMBNAIL_PHOTO_3 { get; set; }
        public string PHOTO_4 { get; set; }
        public string THUMBNAIL_PHOTO_4 { get; set; }
        public string PHOTO_5 { get; set; }
        public string THUMBNAIL_PHOTO_5 { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public decimal FEE_PER_DAY { get; set; }
        public bool UNABLE_PAY_INTEREST { get; set; }
    }
}
