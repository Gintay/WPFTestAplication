//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Test_history
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Test_history()
        {
            this.History_answer = new HashSet<History_answer>();
        }
    
        public int test_id { get; set; }
        public int user_id { get; set; }
        public int test_category { get; set; }
        public int test_level { get; set; }
        public System.DateTime test_date { get; set; }
        public double test_rating { get; set; }
    
        public virtual Question_SubCategory Question_SubCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History_answer> History_answer { get; set; }
        public virtual Users Users { get; set; }
    }
}
