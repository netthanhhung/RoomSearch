using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace RoomSearch.Common
{
    [Serializable]
    [DataContract]
    public partial class Post : Record
    {
        #region Public Constructors

        public Post() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string PostId = "PostId";
            public const string PostTypeId = "PostTypeId";
            public const string PersonName = "PersonName";
            public const string PhoneNumber = "PhoneNumber";
            public const string Email = "Email";
            public const string Gender = "Gender";

            public const string Floor = "Floor";
            public const string Address = "Address";
            public const string District = "District";
            public const string DistrictId = "DistrictId";
            public const string City = "City";
            public const string CityId = "CityId";
            public const string Country = "Country";
            public const string CountryId = "CountryID";

            public const string AvailableRooms = "AvailableRooms";
            public const string RoomTypeId = "RoomTypeId";
            public const string RoomType = "RoomType";
            public const string RealestateTypeId = "RealestateTypeId";
            public const string RealestateType = "RealestateType";
            public const string Description = "Description";
            public const string MeterSquare = "MeterSquare";            
            public const string Price = "Price";
            public const string IsLegacy = "IsLegacy";
        }

        #endregion

        #region Properties

        private int _postId;
        [DataMember]
        public int PostId { get { return _postId; } set { _postId = value; RaisePropertyChanged("PostId"); } }

        private int _postTypeId;
        [DataMember]
        public int PostTypeId { get { return _postTypeId; } set { if (!this.PostTypeId.Equals(value)) { _postTypeId = value; RaisePropertyChanged("PostTypeId"); } } }

        private string _personName;
        [DataMember]
        public string PersonName { get { return _personName; } set { if (!object.ReferenceEquals(this.PersonName, value)) { _personName = value; RaisePropertyChanged("PersonName"); } } }

        private string _phoneNumber;
        [DataMember]
        public string PhoneNumber { get { return _phoneNumber; } set { if (!object.ReferenceEquals(this.PhoneNumber, value)) { _phoneNumber = value; RaisePropertyChanged("PhoneNumber"); } } }

        private string _email;
        [DataMember]
        public string Email { get { return _email; } set { if (!object.ReferenceEquals(this.Email, value)) { _email = value; RaisePropertyChanged("Email"); } } }

        private int? _gender;
        [DataMember]
        public int? Gender { get { return _gender; } set { if (!this.Gender.Equals(value)) { _gender = value; RaisePropertyChanged("Gender"); } } }

        private string _address;
        [DataMember]
        public string Address { get { return _address; } set { if (!object.ReferenceEquals(this.Address, value)) { _address = value; RaisePropertyChanged("Address"); } } }

        private int? _floor;
        [DataMember]
        public int? Floor { get { return _floor; } set { if (!this.Floor.Equals(value)) { _floor = value; RaisePropertyChanged("Floor"); } } }

        private string _district;
        [DataMember]
        public string District { get { return _district; } set { if (!object.ReferenceEquals(this.District, value)) { _district = value; RaisePropertyChanged("District"); } } }

        private int? _districtId;
        [DataMember]
        public int? DistrictId { get { return _districtId; } set { if (!this.DistrictId.Equals(value)) { _districtId = value; RaisePropertyChanged("DistrictId"); } } }

        private string _city;
        [DataMember]
        public string City { get { return _city; } set { if (!object.ReferenceEquals(this.City, value)) { _city = value; RaisePropertyChanged("City"); } } }

        private int? _cityId;
        [DataMember]
        public int? CityId { get { return _cityId; } set { if (!this.CityId.Equals(value)) { _cityId = value; RaisePropertyChanged("CityId"); } } }

        private string _country;
        [DataMember]
        public string Country { get { return _country; } set { if (!object.ReferenceEquals(this.Country, value)) { _country = value; RaisePropertyChanged("Country"); } } }

        private int? _countryId;
        [DataMember]
        public int? CountryId { get { return _countryId; } set { if (!this.CountryId.Equals(value)) { _countryId = value; RaisePropertyChanged("CountryId"); } } }

        private int? _availableRooms;
        [DataMember]
        public int? AvailableRooms { get { return _availableRooms; } set { if (!this.AvailableRooms.Equals(value)) { _availableRooms = value; RaisePropertyChanged("AvailableRooms"); } } }

        private int? _roomTypeId;
        [DataMember]
        public int? RoomTypeId { get { return _roomTypeId; } set { if (!this.RoomTypeId.Equals(value)) { _roomTypeId = value; RaisePropertyChanged("RoomTypeId"); } } }

        private string _roomType;
        [DataMember]
        public string RoomType { get { return _roomType; } set { if (!object.ReferenceEquals(this.RoomType, value)) { _roomType = value; RaisePropertyChanged("RoomType"); } } }

        private int? _realestateTypeId;
        [DataMember]
        public int? RealestateTypeId { get { return _realestateTypeId; } set { if (!this.RealestateTypeId.Equals(value)) { _realestateTypeId = value; RaisePropertyChanged("RealestateTypeId"); } } }

        private string _realestateType;
        [DataMember]
        public string RealestateType { get { return _realestateType; } set { if (!object.ReferenceEquals(this.RealestateType, value)) { _realestateType = value; RaisePropertyChanged("RealestateType"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private decimal? _meterSquare;
        [DataMember]
        public decimal? MeterSquare { get { return _meterSquare; } set { if (!this.MeterSquare.Equals(value)) { _meterSquare = value; RaisePropertyChanged("MeterSquare"); } } }

        private decimal? _price;
        [DataMember]
        public decimal? Price { get { return _price; } set { if (!this.Price.Equals(value)) { _price = value; RaisePropertyChanged("Price"); } } }

        [DataMember]
        public string PriceString
        {
            get
            {
                return Price.HasValue ? (Price.Value.ToString("0.0") + " (tr đ)") : string.Empty;
            }
        }

        [DataMember]
        public string ShortDescription
        {
            get
            {
                return !string.IsNullOrEmpty(this.Description) && this.Description.Length > 170 ? (Description.Substring(0, 170) + "....") : Description;
            }
        }

        [DataMember]
        public string ShortTitle
        {
            get
            {
                return !string.IsNullOrEmpty(this.Description) && this.Description.Length > 100 ? (Description.Substring(0, 100) + "....") : Description;
            }
        }

        private List<Image> _imageList;
        [DataMember]
        public List<Image> ImageList { get { return _imageList; } set { if (!object.ReferenceEquals(this.ImageList, value)) { _imageList = value; RaisePropertyChanged("ImageList"); } } }

        #endregion
    }
}




