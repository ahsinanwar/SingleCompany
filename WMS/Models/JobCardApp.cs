//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobCardApp
    {
        public int JobCardID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateStarted { get; set; }
        public Nullable<System.DateTime> DateEnded { get; set; }
        public Nullable<short> CardType { get; set; }
        public Nullable<int> UserID { get; set; }
        public string JobCardCriteria { get; set; }
        public Nullable<int> CriteriaData { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> TimeIn { get; set; }
        public Nullable<System.DateTime> TimeOut { get; set; }
        public Nullable<short> WorkMin { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public string Remarks { get; set; }
        public Nullable<short> OtherValue { get; set; }
    }
}
