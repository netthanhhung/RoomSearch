using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace RoomSearch.Common
{
    public enum PostTypes : int
    {
        Room = 1,
        StayWith = 2,
        House = 3
    }

    [DataContract]
    public partial class PostType : Record
    {
        #region Public Constructors

        public PostType() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string PostTypeId = "PostTypeId";
            public const string Name = "Name";
        }

        #endregion

        #region Properties

        private int _postTypeId;
        [DataMember]
        public int PostTypeId { get { return _postTypeId; } set { _postTypeId = value; RaisePropertyChanged("PostTypeId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        #endregion
    }
}




