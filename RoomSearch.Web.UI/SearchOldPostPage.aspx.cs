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

namespace RoomSearch.Web.UI
{
    public partial class SearchOldPostPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
            }
        }

        protected void OnMyAjaxManagerAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument.IndexOf("RebindSearchResults") != -1)
            {
                SearcRoomPostAjaxManager.AjaxSettings.AddAjaxSetting(SearcRoomPostAjaxManager, gridRoomResult);
                GetGridRoomResultDataSource(null);
                gridRoomResult.DataBind();
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

            
            if (!string.IsNullOrEmpty(txtEmail.Text) || !string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                string email = txtEmail.Text.ToLower();
                string phoneNumber = txtPhoneNumber.Text.ToLower();

                int postTypeId = GetPostTypeId();
                gridRoomResult.VirtualItemCount = Business.BusinessMethods.CountPost(postTypeId, null, null, null, null, null, null, phoneNumber, email, null,
                    null, null, null, null, null, null, null, true);
                List<Post> searchResults = Business.BusinessMethods.SearchPostPaging(postTypeId, null, null, null, null, null, null, phoneNumber, email, null,
                    null, null, null, null, null, null, null, true, gridRoomResult.PageSize, pageNumber, sortExpress, sortExpressInvert);
                gridRoomResult.DataSource = searchResults;
            }

        }

        protected int GetPostTypeId()
        {
            int postTypeId = 1; //Room
            string rawURL = Request.RawUrl;
            string sub = rawURL.Substring(rawURL.LastIndexOf("/"));
            if (int.TryParse(sub, out postTypeId))
            {
                return postTypeId;
            }
            //if (!string.IsNullOrEmpty(Request.QueryString["PostType"]))
            //{
            //    postTypeId = Convert.ToInt32(Request.QueryString["PostType"]);
            //}
            return postTypeId;
        }
        #endregion

    }
}
