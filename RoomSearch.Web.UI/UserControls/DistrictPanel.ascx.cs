using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
using RoomSearch.Common;
using System.Drawing.Imaging;
using System.Drawing;


namespace RoomSearch.Web.UI
{
    public partial class DistrictPanel : System.Web.UI.UserControl
    {
        public int PostType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }


        void InitData()
        {
            List<District> districts = Business.BusinessMethods.ListDistrict(1, null, false);
            List<DistrictVM> vmList = new List<DistrictVM>();
            foreach (District item in districts)
            {
                DistrictVM vm = new DistrictVM();
                int name;
                if (int.TryParse(item.Name, out name))
                {
                    vm.Name = "Quận " + name;
                }
                else
                {
                    vm.Name = item.Name;
                }

                string currentURL = HttpContext.Current.Request.Url.AbsoluteUri;
                if (currentURL.Contains("SearchHousePage"))
                {
                    vm.URL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/SearchHousePage.aspx?DistrictId=" + item.DistrictId;
                }
                else if (currentURL.Contains("SearchStayWithPage"))
                {
                    vm.URL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/SearchStayWithPage.aspx?DistrictId=" + item.DistrictId;
                }
                else
                {
                    vm.URL = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/SearchRoomPage.aspx?DistrictId=" + item.DistrictId;
                }
                vmList.Add(vm);
            }
            dataListLinks.DataSource = vmList;
            dataListLinks.DataBind();
        }

    }
}
