using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RoomSearch.Web.UI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();

            //lblFBShare.Text = "<a name=\"fb_share\" type=\"button\"></a>" +
            //        "<script " +
            //        "src=\"http://static.ak.fbcdn.net/connect.php/js/FB.Share\" " +
            //        "type=\"text/javascript\"></script>";

            //HtmlMeta tag = new HtmlMeta();
            //tag.Name = "name";
            //tag.Content = "Tìm phòng trọ - nhà đất";
            //Page.Header.Controls.Add(tag);

            //tag = new HtmlMeta();
            //tag.Name = "description";
            //tag.Content = "Tìm phòng trọ - nhà đất";
            //Page.Header.Controls.Add(tag);

            //HtmlLink link = new HtmlLink();
            //link.Href = "http://www.murrayhilltech.com/images/LogoColorNoText.jpg";
            //link.Attributes["rel"] = "image_src";
            //Page.Header.Controls.Add(link);

        }
    }
}