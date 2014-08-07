using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace RoomSearch.Common
{
    public enum RealestateTypes : int
    {
        Standard = 1,
        Exclusive = 2,
    }

    [Serializable]
    [DataContract]
    public partial class RealestateType : Record
    {
        #region Public Constructors

        public RealestateType() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string RealestateTypeId = "RealestateTypeId";
            public const string Name = "Name";
            public const string Description = "Description";
        }

        #endregion

        #region Properties

        private int _realestateTypeId;
        [DataMember]
        public int RealestateTypeId { get { return _realestateTypeId; } set { _realestateTypeId = value; RaisePropertyChanged("RealestateTypeId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        #endregion
    }
}




