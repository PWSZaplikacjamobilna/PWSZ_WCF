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
    
    public partial class GrupaWykladowa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GrupaWykladowa()
        {
            this.Rejestracja = new HashSet<Rejestracja>();
            this.Terminarz = new HashSet<Terminarz>();
            this.Zajecia = new HashSet<Zajecia>();
        }
    
        public int GrupaWykladowaID { get; set; }
        public string GrupaWykładowa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rejestracja> Rejestracja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Terminarz> Terminarz { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zajecia> Zajecia { get; set; }
    }
}
