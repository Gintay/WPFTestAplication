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
    
    public partial class History_answer
    {
        public int test_id { get; set; }
        public int question_id { get; set; }
        public int answer_id { get; set; }
    
        public virtual Answers Answers { get; set; }
        public virtual Questions Questions { get; set; }
        public virtual Test_history Test_history { get; set; }
    }
}
