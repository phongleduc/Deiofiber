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
    
    public partial class INOUT_FULL_VW
    {
        public int ID { get; set; }
        public decimal IN_AMOUNT { get; set; }
        public decimal OUT_AMOUNT { get; set; }
        public int CONTRACT_ID { get; set; }
        public int PERIOD_ID { get; set; }
        public int RENT_TYPE_ID { get; set; }
        public System.DateTime PERIOD_DATE { get; set; }
        public string MORE_INFO { get; set; }
        public int STORE_ID { get; set; }
        public string SEARCH_TEXT { get; set; }
        public string STORE_NAME { get; set; }
        public string RENT_TYPE_NAME { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public Nullable<bool> ISINCOME { get; set; }
        public Nullable<bool> IS_CONTRACT { get; set; }
        public Nullable<bool> RENT_TYPE_ACTIVE { get; set; }
        public string INOUT_TYPE_NAME { get; set; }
        public int INOUT_TYPE_ID { get; set; }
        public Nullable<System.DateTime> INOUT_DATE { get; set; }
        public string CONTRACT_NO { get; set; }
    }
}
