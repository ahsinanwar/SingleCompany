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
    
    public partial class EmpType
    {
        public EmpType()
        {
            this.Emps = new HashSet<Emp>();
        }
    
        public byte TypeID { get; set; }
        public string TypeName { get; set; }
        public Nullable<short> CatID { get; set; }
        public Nullable<short> CompanyID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Emp> Emps { get; set; }
    }
}
