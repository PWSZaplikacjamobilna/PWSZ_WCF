//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PWSZ_WCF
{
    using System;
    using System.Collections.Generic;
    
    public partial class DniZajec
    {
        public int DniZajecID { get; set; }
        public Nullable<int> ZajeciaID { get; set; }
        public Nullable<System.DateTime> Dzien { get; set; }
    
        public virtual Zajecia Zajecia { get; set; }
    }
}
