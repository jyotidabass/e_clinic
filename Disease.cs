//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AyurvedOnCall.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Disease
    {
        public long DiseaseId { get; set; }
        public long SpecialityId { get; set; }
        public string Name { get; set; }
    
        public virtual Speciality Speciality { get; set; }
    }
}
