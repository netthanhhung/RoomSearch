using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RoomSearch.Common;
using Telerik.Web.UI;

namespace RoomSearch.Web.UI
{
    public partial class ImagePanel : System.Web.UI.UserControl
    {

        public Common.Image ImageDataItem { get; set; }
        public bool CanEdit { get; set; }
        public event EventHandler DeleteClicked;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ImageDataItem.ImageContent != null)
                {
                    imgImage.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ImageDataItem.ImageContent);
                }
                else if (ImageDataItem.ImageSmallContent != null)
                {
                    imgImage.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ImageDataItem.ImageSmallContent);
                }
                btnDelete.Visible = this.CanEdit;                
            }
        }


        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //imgImage.Visible = false;
            //btnDelete.Visible = false;
            this.Visible = false;
            if (DeleteClicked != null)
                DeleteClicked(this, EventArgs.Empty);
        }
    }
}