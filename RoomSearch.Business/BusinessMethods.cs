using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoomSearch.Data;
using RoomSearch.Common;
using System.Security.Cryptography;

namespace RoomSearch.Business
{
    public static class BusinessMethods
    {
        #region Common
        /// <summary>
        /// Calls the data layer to save the record object.
        /// The record's RecordId is used to determine if the save operation is an insert or an update.
        /// </summary>
        /// <param name="record">The record object containing the data to be saved.</param>
        public static void SaveRecord(Record record)
        {
            new DataLayer().SaveRecord(record, (record.NullableRecordId == null) ? record.CreatedBy : record.UpdatedBy);
        }       

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(Record record)
        {
            DataLayer dl = new DataLayer();
            String type = record.GetType().ToString();
            dl.DeleteRecord(Utilities.ToLong(record.RecordId), type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(long id, Type recordType)
        {
            DataLayer temp = new DataLayer();
            String type = recordType.ToString();
            temp.DeleteRecord(id, type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }
       
        #endregion

    }
}

