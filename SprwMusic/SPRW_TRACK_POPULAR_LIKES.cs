//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SprwMusic
{
    using System;
    using System.Collections.Generic;
    
    public partial class SPRW_TRACK_POPULAR_LIKES
    {
        public int TRACK_ID { get; set; }
        public System.DateTime LIKE_DATE { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public int ID { get; set; }
    
        public virtual SPRW_TRACK SPRW_TRACK { get; set; }
        public virtual SPRW_USER SPRW_USER { get; set; }
    }
}
