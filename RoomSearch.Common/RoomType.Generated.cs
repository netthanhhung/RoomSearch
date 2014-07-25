using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace RoomSearch.Common
{
    public enum RoomTypes : int
    {
        Standard = 1,
        Exclusive = 2,
    }

    [DataContract]
    public partial class RoomType : Record
    {
        #region Public Constructors

        public RoomType() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string RoomTypeId = "RoomTypeId";
            public const string Name = "Name";
            public const string Description = "Description";
        }

        #endregion

        #region Properties

        private int _roomTypeId;
        [DataMember]
        public int RoomTypeId { get { return _roomTypeId; } set { _roomTypeId = value; RaisePropertyChanged("RoomTypeId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        #endregion
    }
}




