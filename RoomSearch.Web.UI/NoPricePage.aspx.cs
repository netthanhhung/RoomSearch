using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using RoomSearch.Common;
using System.Collections;
using System.Text;

namespace RoomSearch.Web.UI
{
    public partial class NoPricePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
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

            cbbRoomType.DataValueField = "RoomTypeId";
            cbbRoomType.DataTextField = "Name";
            cbbRoomType.DataSource = Business.BusinessMethods.ListRoomType();
            cbbRoomType.DataBind();
            cbbRoomType.SelectedValue = "1";
            cbbRoomType.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cbbRoomType_SelectedIndexChanged);

            DateTime today = DateTime.Today;
            datDateFrom.SelectedDate = today.AddMonths(-3);
            datDateTo.SelectedDate = today;
        }

        
        void cbbRoomType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int roomTypeId = Convert.ToInt32(cbbRoomType.SelectedValue);
            if (roomTypeId == (int)RoomTypes.Standard)
            {
                txtPriceFrom.Value = 1.5;
                txtPriceTo.Value = 3;
            }
            else
            {
                txtPriceFrom.Value = 3;
                txtPriceTo.Value = 10;
            }
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
            List<District> districts = Business.BusinessMethods.ListDistrict(cityId, null, true);
            cbbDistrict.DataSource = districts;
            cbbDistrict.DataBind();
            cbbDistrict.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["DistrictId"]))
            {
                int districtId = Convert.ToInt32(Request.QueryString["DistrictId"]);
                District foundDis = districts.FirstOrDefault(i => i.DistrictId == districtId);
                if (foundDis != null)
                {
                    cbbDistrict.SelectedValue = districtId.ToString();
                    int name;
                    if (int.TryParse(foundDis.Name, out name))
                    {
                        this.Page.Title += " Quận " + name;
                    }
                    else
                    {
                        this.Page.Title += " " + foundDis.Name;
                    }

                    this.Page.MetaKeywords = this.Page.Title;
                }
            }
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


            gridRoomResult.VirtualItemCount = Business.BusinessMethods.CountNoPricePost();
            List<Post> searchResults = Business.BusinessMethods.SearchNoPricePostPaging(gridRoomResult.PageSize, pageNumber, sortExpress, sortExpressInvert);
            gridRoomResult.DataSource = searchResults;

            //Build Meta Description for Google Search Engine :
            StringBuilder builder = new StringBuilder();
            foreach (Post post in searchResults)
            {
                builder.Append(post.Address + " giá : " + post.PriceString + ". \n");
            }
            this.Page.MetaDescription = builder.ToString();
            
        }

        #endregion

    }
}
