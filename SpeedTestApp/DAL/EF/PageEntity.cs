//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpeedTestApp.DAL.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class PageEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PageEntity()
        {
            this.Measures = new HashSet<MeasureEntity>();
        }
    
        public int ID { get; set; }
        public int SiteID { get; set; }
        public string Page { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasureEntity> Measures { get; set; }
        public virtual SiteEntity Site { get; set; }
    }
}