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
    
    public partial class SPRW_TRACK
    {
        public SPRW_TRACK()
        {
            this.SPRW_TRACK_POPULAR_DISLIKES = new HashSet<SPRW_TRACK_POPULAR_DISLIKES>();
            this.SPRW_TRACK_POPULAR_LIKES = new HashSet<SPRW_TRACK_POPULAR_LIKES>();
            this.SPRW_TRACK_POPULAR_PLAYS = new HashSet<SPRW_TRACK_POPULAR_PLAYS>();
            this.SPRW_TRACK_POPULAR_PLAY_THROUGH = new HashSet<SPRW_TRACK_POPULAR_PLAY_THROUGH>();
            this.SPRW_TRACK_POPULAR_SELECT = new HashSet<SPRW_TRACK_POPULAR_SELECT>();
            this.SPRW_TRACK_POPULAR_SKIPS = new HashSet<SPRW_TRACK_POPULAR_SKIPS>();
        }
    
        public int TRACK_ID { get; set; }
        public Nullable<int> ALBUM_ID { get; set; }
        public int ARTIST_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRP { get; set; }
        public bool ACT_IND { get; set; }
        public System.DateTime RELEASE_DATE { get; set; }
        public string LAST_MAINT_USER_ID { get; set; }
        public System.DateTime LAST_MAINT_TIME { get; set; }
    
        public virtual SPRW_ALBUM SPRW_ALBUM { get; set; }
        public virtual SPRW_ARTIST SPRW_ARTIST { get; set; }
        public virtual ICollection<SPRW_TRACK_POPULAR_DISLIKES> SPRW_TRACK_POPULAR_DISLIKES { get; set; }
        public virtual ICollection<SPRW_TRACK_POPULAR_LIKES> SPRW_TRACK_POPULAR_LIKES { get; set; }
        public virtual ICollection<SPRW_TRACK_POPULAR_PLAYS> SPRW_TRACK_POPULAR_PLAYS { get; set; }
        public virtual ICollection<SPRW_TRACK_POPULAR_PLAY_THROUGH> SPRW_TRACK_POPULAR_PLAY_THROUGH { get; set; }
        public virtual ICollection<SPRW_TRACK_POPULAR_SELECT> SPRW_TRACK_POPULAR_SELECT { get; set; }
        public virtual ICollection<SPRW_TRACK_POPULAR_SKIPS> SPRW_TRACK_POPULAR_SKIPS { get; set; }
    }
}