﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using RoomSearch.Common;
using System.Collections;

namespace RoomSearch.Web.UI
{
    public partial class SearchHousePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComboboxData();
            }
        }

        void InitComboboxData()
        {
            cbbCity.DataValueField = "CityId";
            cbbCity.DataTextField = "Name";
            cbbCity.DataSource = Business.BusinessMethods.ListCity(232, null); //VietNam
            cbbCity.DataBind();
            cbbCity.SelectedValue = "1"; //Ho Chi Minh

            BindDistrictListByCity(1);

            cbbRealestateType.DataValueField = "RealestateTypeId";
            cbbRealestateType.DataTextField = "Name";
            cbbRealestateType.DataSource = Business.BusinessMethods.ListRealestateType();
            cbbRealestateType.DataBind();
            cbbRealestateType.SelectedValue = "1";

            DateTime today = DateTime.Today;
            datDateFrom.SelectedDate = today.AddMonths(-3);
            datDateTo.SelectedDate = today;
        }

        protected void OnMyAjaxManagerAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument.IndexOf("RebindDistrictListByCity") != -1)
            {
                string[] param = e.Argument.Split('-');
                if (param.Length == 2)
                {
                    SearchRoomAjaxManager.AjaxSettings.AddAjaxSetting(SearchRoomAjaxManager, cbbDistrict);
                    BindDistrictListByCity(int.Parse(param[1]));
                }
            }
        }

        private void BindDistrictListByCity(int cityId)
        {
            cbbDistrict.DataTextField = "Name";
            cbbDistrict.DataValueField = "DistrictId";
            cbbDistrict.DataSource = Business.BusinessMethods.ListDistrict(cityId, null);
            cbbDistrict.DataBind();
            cbbDistrict.SelectedIndex = 0;
        }

        protected void OnBtnSearch_Clicked(object sender, EventArgs e)
        {
            GetGridRoomResultDataSource(null);
            gridRoomResult.DataBind();
        }

        #region Room Result Grid events
        protected void OnGridRoomResult_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            HttpCookie GridRoomResultPageSizeCookie = new HttpCookie("comgrdps");
            GridRoomResultPageSizeCookie.Expires.AddDays(30);
            GridRoomResultPageSizeCookie.Value = e.NewPageSize.ToString();
            Response.Cookies.Add(GridRoomResultPageSizeCookie);
        }

        protected void OnGridRoomResultItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                int postId = ((Post)e.Item.DataItem).PostId;
                LinkButton buttonDetails = dataItem["ViewDetails"].Controls[1] as LinkButton;
                if (buttonDetails != null)
                {
                    buttonDetails.OnClientClick = string.Format("return OnUserViewDetailsClientClicked('{0}')", postId);
                }
            }
        }
        
        protected void OnGridRoomResultNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GetGridRoomResultDataSource(null);
        }

        protected void OnGridRoomResultPageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            gridRoomResult.CurrentPageIndex = e.NewPageIndex;
            BindGridData();

        }

        private void BindGridData()
        {
            gridRoomResult.DataSource = new ArrayList();
            gridRoomResult.DataBind();
        }

        protected void OnGridRoomResultSortCommand(object source, GridSortCommandEventArgs e)
        {
            GetGridRoomResultDataSource(e);
        }

        private void GetGridRoomResultDataSource(GridSortCommandEventArgs sortEventArgs)
        {
            int pageNumber = gridRoomResult.CurrentPageIndex + 1;
            string sortExpress = string.Empty;
            string sortExpressInvert = string.Empty;
            foreach (GridSortExpression item in gridRoomResult.MasterTableView.SortExpressions)
            {
                GridSortOrder newSortOrder = item.SortOrder;
                if (sortEventArgs != null && item.FieldName == sortEventArgs.SortExpression)
                {
                    newSortOrder = sortEventArgs.NewSortOrder;
                }

                if (!string.IsNullOrEmpty(sortExpress) && newSortOrder != GridSortOrder.None)
                {
                    sortExpress += ", ";
                    sortExpressInvert += ", ";
                }
                if (newSortOrder == GridSortOrder.Ascending)
                {
                    sortExpress += item.FieldName + " ASC";
                    sortExpressInvert += item.FieldName + " DESC";
                }
                else if (newSortOrder == GridSortOrder.Descending)
                {
                    sortExpress += item.FieldName + " DESC";
                    sortExpressInvert += item.FieldName + " ASC";
                }
            }

            if (sortEventArgs != null && !sortExpress.Contains(sortEventArgs.SortExpression))
            {
                if (!string.IsNullOrEmpty(sortExpress) && sortEventArgs.NewSortOrder != GridSortOrder.None)
                {
                    sortExpress += ", ";
                    sortExpressInvert += ", ";
                }
                if (sortEventArgs.NewSortOrder == GridSortOrder.Ascending)
                {
                    sortExpress += sortEventArgs.SortExpression + " ASC";
                    sortExpressInvert += sortEventArgs.SortExpression + " DESC";
                }
                else if (sortEventArgs.NewSortOrder == GridSortOrder.Descending)
                {
                    sortExpress += sortEventArgs.SortExpression + " DESC";
                    sortExpressInvert += sortEventArgs.SortExpression + " ASC";
                }
            }

            if (string.IsNullOrEmpty(sortExpress))
            {
                sortExpress = "DateCreated DESC";
                sortExpressInvert = "DateCreated ASC";
            }

            int cityId = Convert.ToInt32(cbbCity.SelectedValue);
            int districtId = Convert.ToInt32(cbbDistrict.SelectedValue);
            int realestateTypeId = Convert.ToInt32(cbbRealestateType.SelectedValue);
            decimal? priceFrom = null;
            if (txtPriceFrom.Value.HasValue)
            {
                priceFrom = Convert.ToDecimal(txtPriceFrom.Value);
            }
            decimal? priceTo = null;
            if (txtPriceTo.Value.HasValue)
            {
                priceTo = Convert.ToDecimal(txtPriceTo.Value);
            }
            DateTime? dateFrom = null;
            if (datDateFrom.SelectedDate.HasValue)
            {
                dateFrom = datDateFrom.SelectedDate.Value;
            }
            DateTime? dateTo = null;
            if (datDateTo.SelectedDate.HasValue)
            {
                dateTo = datDateTo.SelectedDate.Value;
                dateTo = dateTo.Value.AddDays(1).AddSeconds(-1);
            }

            int gender = radMale.Checked ? 1 : 0;
            gridRoomResult.VirtualItemCount = Business.BusinessMethods.CountPost((int)PostTypes.House, null, realestateTypeId, 232, cityId, districtId, null, null, null, gender,
                priceFrom, priceTo, dateFrom, dateTo, null, null, false);
            List<Post> searchResults = Business.BusinessMethods.SearchPostPaging((int)PostTypes.House, null, realestateTypeId, 232, cityId, districtId, null, null, null, gender,
                priceFrom, priceTo, dateFrom, dateTo, null, null, false, gridRoomResult.PageSize, pageNumber, sortExpress, sortExpressInvert);
            gridRoomResult.DataSource = searchResults;
        }

        #endregion

    }
}