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
    
    public partial class Log
    {
        public int ID { get; set; }
        public string LOG_MSG { get; set; }
        public string ACCOUNT { get; set; }
        public string STORE { get; set; }
        public System.DateTime LOG_DATE { get; set; }
        public string LOG_ACTION { get; set; }
        public bool IS_CRASH { get; set; }
        public string SEARCH_TEXT { get; set; }
        public Nullable<int> STORE_ID { get; set; }
    }
}
