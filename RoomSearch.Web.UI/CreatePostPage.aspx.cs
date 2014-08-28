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
    public partial class PostRoomPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComboboxData();

                int postTypeId = GetPostTypeId();
                if (postTypeId == (int)PostTypes.Room)
                {
                    lblGender.Visible = radFemale.Visible = radMale.Visible = lblRealestate.Visible = cbbRealestateType.Visible = false;
                    btnSearchPost.Text = "Tìm phòng";
                    this.Page.Title = "Đăng tin cho thuê phòng";
                }
                else if (postTypeId == (int)PostTypes.StayWith)
                {
                    lblGender.Visible = radFemale.Visible = radMale.Visible = true;
                    lblRealestate.Visible = cbbRealestateType.Visible = false;
                    btnSearchPost.Text = "Tìm ở ghép";
                    lblAvailableRooms.Visible = txtAvailableRooms.Visible = false;
                    this.Page.Title = "Đăng tin cần tìm ở ghép";
                }
                else if (postTypeId == (int)PostTypes.House)
                {
                    lblRoomType.Visible = cbbRoomType.Visible = false;
                    lblGender.Visible = radFemale.Visible = radMale.Visible = true;
                    lblGender.Text = "Cần";
                    radMale.Text = "Mua";
                    radFemale.Text = "Bán";
                    lblRealestate.Visible = cbbRealestateType.Visible = true;
                    btnSearchPost.Text = "Tìm nhà/đất";
                    lblAvailableRooms.Visible = txtAvailableRooms.Visible = false;
                    lblMeterSquare.Text = "Rộng";
                    lblPrice.Text = "Giá";
                    lblUnit.Text = "triệu đồng";
                    txtPrice.Value = 500;
                    txtMeterSquare.Value = 100;
                    this.Page.Title = "Đăng tin cho mua bán nhà đất";

                }

                string[] allowedFileExtensions = new string[4] {".jpg", ".jpeg", ".gif", ".png"};
                radUploadMulti.AllowedFileExtensions = allowedFileExtensions;

                divAfterPost.Visible = false;
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

            cbbRealestateType.DataValueField = "RealestateTypeId";
            cbbRealestateType.DataTextField = "Name";
            cbbRealestateType.DataSource = Business.BusinessMethods.ListRealestateType();
            cbbRealestateType.DataBind();
        }


        protected void OnMyAjaxManagerAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument.IndexOf("RebindDistrictListByCity") != -1)
            {
                string[] param = e.Argument.Split('-');
                if (param.Length == 2)
                {
                    PostRoomAjaxManager.AjaxSettings.AddAjaxSetting(PostRoomAjaxManager, cbbDistrict);
                    BindDistrictListByCity(int.Parse(param[1]));
                }
            }
        }

        private void BindDistrictListByCity(int cityId)
        {
            cbbDistrict.DataTextField = "Name";
            cbbDistrict.DataValueField = "DistrictId";            
            cbbDistrict.DataSource = Business.BusinessMethods.ListDistrict(cityId, null, false);
            cbbDistrict.DataBind();
            cbbDistrict.SelectedIndex = 0;
        }

        private Post GetSavePost()
        {
            Post saveItem = new Post();
            saveItem.PersonName = txtPersonName.Text;
            saveItem.PhoneNumber = txtPhoneNumber.Text;
            saveItem.Email = txtEmail.Text;
            saveItem.CountryId = 232;
            saveItem.CityId = Convert.ToInt32(cbbCity.SelectedValue);
            saveItem.DistrictId = Convert.ToInt32(cbbDistrict.SelectedValue);
            saveItem.Address = txtAddress.Text;
            saveItem.MeterSquare = Convert.ToDecimal(txtMeterSquare.Value);
            saveItem.AvailableRooms = txtAvailableRooms.Value.HasValue ? Convert.ToInt32(txtAvailableRooms.Value) : 1;
            saveItem.Price = Convert.ToDecimal(txtPrice.Value);
            saveItem.Description = txtDescription.Text;

            saveItem.PostTypeId = GetPostTypeId();

            if (saveItem.PostTypeId == (int)PostTypes.Room)
            {
                saveItem.RoomTypeId = Convert.ToInt32(cbbRoomType.SelectedValue);
            }
            if (saveItem.PostTypeId == (int)PostTypes.StayWith)
            {
                saveItem.RoomTypeId = Convert.ToInt32(cbbRoomType.SelectedValue);
                saveItem.Gender = radMale.Checked ? 1 : 0;
            }
            else if (saveItem.PostTypeId == (int)PostTypes.House)
            {
                saveItem.Gender = radMale.Checked ? 1 : 0;
                saveItem.RealestateTypeId = Convert.ToInt32(cbbRealestateType.SelectedValue); ;
            }

            saveItem.ImageList = new List<Common.Image>();
            int displayIndex = 1;
            foreach (UploadedFile file in radUploadMulti.UploadedFiles)
            {
                string fileName = file.GetName();
                System.Web.UI.WebControls.Image imageSource = new System.Web.UI.WebControls.Image();
                Stream imageStream = file.InputStream;                
                int bufferSize = Convert.ToInt32(imageStream.Length);
                byte[] byteArray = new byte[bufferSize];
                imageStream.Read(byteArray, 0, bufferSize);

                MemoryStream resizedStream = UtilityHelper.ResizeFromStream(400, imageStream);
                
                Common.Image newImage = new Common.Image();
                newImage.ImageContent = byteArray;
                newImage.ImageSmallContent = resizedStream.ToArray(); ;
                newImage.DisplayIndex = displayIndex++;
                newImage.ImageTypeId = (int)Common.ImageType.Room;
                newImage.FileName = fileName;
                newImage.CreatedBy = newImage.UpdatedBy = saveItem.PersonName;
                saveItem.ImageList.Add(newImage);
            }

            saveItem.CreatedBy = saveItem.UpdatedBy = saveItem.PersonName;

            return saveItem;

        }


        protected void OnBtnSave_Clicked(object sender, EventArgs e)
        {
            string message = string.Empty;
            string script1 = " alert(\"" + message + "\")";
            if (string.IsNullOrEmpty(txtPersonName.Text) 
                || string.IsNullOrEmpty(txtPhoneNumber.Text)
                || string.IsNullOrEmpty(txtAddress.Text))
            {
                message = "Xin vui lòng điền thông tin có dấu sao.";
                script1 = " alert(\"" + message + "\")";
                PostRoomAjaxManager.ResponseScripts.Add(script1);
                return;
            }

            Post savePost = GetSavePost();
            Business.BusinessMethods.SavePost(savePost);

            message = "Tin của bạn đã được đăng thành công.";
            script1 = " alert(\"" + message + "\")";
            PostRoomAjaxManager.ResponseScripts.Add(script1);

            divMain.Visible = false;
            divAfterPost.Visible = true;    
        }



        protected void OnBtnContinuePost_Clicked(object sender, EventArgs e)
        {
            divMain.Visible = true;
            divAfterPost.Visible = false;
            txtDescription.Text = string.Empty;
            txtAddress.Text = string.Empty;
            radUploadMulti.UploadedFiles.Clear();
        }

        protected void OnBtnSearchPost_Clicked(object sender, EventArgs e)
        {
            int postTypeId = GetPostTypeId();
            if (postTypeId == (int)PostTypes.Room)
            {
                Response.Redirect("~/SearchRoomPage.aspx");
            }
            else if (postTypeId == (int)PostTypes.StayWith)
            {
                Response.Redirect("~/SearchStayWithPage.aspx");
            }
            else if (postTypeId == (int)PostTypes.House)
            {
                Response.Redirect("~/SearchHousePage.aspx");
            }
            
        }

        protected int GetPostTypeId()
        {
            int postTypeId = 1; //Room
            if (!string.IsNullOrEmpty(Request.QueryString["PostType"]))
            {
                postTypeId = Convert.ToInt32(Request.QueryString["PostType"]);
            }
            return postTypeId;
        }
    }
}