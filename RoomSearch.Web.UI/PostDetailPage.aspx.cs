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
    public partial class PostDetailPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComboboxData();

                int postTypeId = GetPostTypeId();
                if (postTypeId == (int)PostTypes.Room)
                {
                    this.Page.Title = "Chi tiết phòng";
                    lblGender.Visible = radFemale.Visible = radMale.Visible = lblRealestate.Visible = cbbRealestateType.Visible = false;
                }
                else if (postTypeId == (int)PostTypes.StayWith)
                {
                    this.Page.Title = "Thong tin chi tiết ở ghép";
                    lblGender.Visible = radFemale.Visible = radMale.Visible = true;
                    lblRealestate.Visible = cbbRealestateType.Visible = false;
                    lblAvailableRooms.Visible = txtAvailableRooms.Visible = false;
                }
                else if (postTypeId == (int)PostTypes.House)
                {
                    lblRoomType.Visible = cbbRoomType.Visible = false;
                    this.Page.Title = "Thông tin chi tiết mua bán nhà đất";
                    lblGender.Visible = radFemale.Visible = radMale.Visible = true;
                    lblGender.Text = "Cần";
                    radMale.Text = "Mua";
                    radFemale.Text = "Bán";
                    lblRealestate.Visible = cbbRealestateType.Visible = true;
                    lblAvailableRooms.Visible = txtAvailableRooms.Visible = false;
                    lblMeterSquare.Text = "Rộng";
                    lblPrice.Text = "Giá";
                    lblUnit.Text = "triệu đồng";                    
                }

                string mode = "view";
                if (!string.IsNullOrEmpty(Request.QueryString["Mode"]))
                {
                    mode = Request.QueryString["Mode"];                    
                }

                if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
                {
                    int postId = Convert.ToInt32(Request.QueryString["PostId"]);
                    Post currentPost = Business.BusinessMethods.GetPost(postId, 3);
                    if (mode != "view")
                    {
                        ViewState["CurrentPost"] = currentPost;
                    }
                    else
                    {
                        BindReadonlyImageList(currentPost);
                    }
                    BindData(currentPost);
                                
                }

                DisableFields(mode);
            }

            BindEditImageList();        
        }

        void InitComboboxData()
        {
            cbbCity.DataValueField = "CityId";
            cbbCity.DataTextField = "Name";
            cbbCity.DataSource = Business.BusinessMethods.ListCity(232, null); //VietNam
            cbbCity.DataBind();
            
            BindDistrictListByCity(1);

            cbbRoomType.DataValueField = "RoomTypeId";
            cbbRoomType.DataTextField = "Name";
            cbbRoomType.DataSource = Business.BusinessMethods.ListRoomType();
            cbbRoomType.DataBind();
            //cbbRoomType.SelectedValue = this.CurrentPost != null ? CurrentPost.RoomTypeId.ToString() : "1";

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
                    PostDetailsAjaxManager.AjaxSettings.AddAjaxSetting(PostDetailsAjaxManager, cbbDistrict);
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
            //cbbDistrict.SelectedValue = CurrentPost != null ? CurrentPost.DistrictId.ToString() : "1";
        }

        void BindData(Post post)
        {
            this.Page.Title = post.ShortTitle;
            txtPersonName.Text = post.PersonName;
            txtEmail.Text = post.Email;
            txtPhoneNumber.Text = post.PhoneNumber;
            cbbCity.SelectedValue = post.CityId.ToString();
            cbbDistrict.SelectedValue = post.DistrictId.ToString();
            txtAddress.Text = post.Address;
            txtAvailableRooms.Value = post.AvailableRooms;
            txtMeterSQuare.Value = post.MeterSquare.HasValue ? Convert.ToDouble(post.MeterSquare.Value) : 0;
            txtPrice.Value = post.Price.HasValue ? Convert.ToDouble(post.Price.Value) : 0;
            cbbRoomType.SelectedValue = post.RoomTypeId.ToString();
            txtDescription.Text = post.Description;
            radMale.Checked = post.Gender == 1;
            radFemale.Checked = post.Gender == 0;
            cbbRealestateType.SelectedValue = post.RealestateTypeId.ToString();
        }

        void BindReadonlyImageList(Post post)
        {
            if (post != null && post.ImageList != null && post.ImageList.Count > 0)
            {
                foreach (Common.Image imageData in post.ImageList)
                {
                    System.Web.UI.WebControls.Image imageSource = new System.Web.UI.WebControls.Image();
                    if (imageData.ImageContent != null)
                    {
                        imageSource.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageData.ImageContent);
                    }
                    else if (imageData.ImageSmallContent != null)
                    {
                        imageSource.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageData.ImageSmallContent);
                    }
                    imageSource.Width = 400;
                    imageSource.Style.Add("margin-top", "5px");
                    imageSource.Style.Add("margin-left", "5px");
                    imageSource.Style.Add("margin-right", "5px");
                    imageSource.Style.Add("margin-bottom", "5px");
                    panelImage.Controls.Add(imageSource);
                }
            }
        }

        void BindEditImageList()
        {
            Post post = ViewState["CurrentPost"] as Post;
            if (post == null)
            {
                return;
            }

            if (post != null && post.ImageList != null && post.ImageList.Count > 0)
            {

                foreach (Common.Image imageData in post.ImageList)
                {
                    ImagePanel imagePanel = (ImagePanel)this.LoadControl("~/UserControls/ImagePanel.ascx");
                    imagePanel.ImageDataItem = imageData;
                    imagePanel.CanEdit = true;
                    imagePanel.DeleteClicked += new EventHandler(OnImagePanel_DeleteClicked);
                    panelImage.Controls.Add(imagePanel);
                }
            }
        }

        protected void OnImagePanel_DeleteClicked(object sender, EventArgs e)
        {
            ImagePanel removeItem = sender as ImagePanel;
            if (removeItem != null)
            {
                Post currentPost = ViewState["CurrentPost"] as Post;
                if (currentPost != null && currentPost.ImageList.Count(i => i.ImageId == removeItem.ImageDataItem.ImageId) > 0)
                {
                    currentPost.ImageList = currentPost.ImageList.Where(i => i.ImageId != removeItem.ImageDataItem.ImageId).ToList();
                }
                ViewState["CurrentPost"] = currentPost;
                //removeItem.DeleteClicked -= new EventHandler(OnImagePanel_DeleteClicked);
                panelImage.Controls.Remove(removeItem);
                removeItem.Visible = false;
                //BindEditImageList();
            }
        }

        void DisableFields(string mode)
        {
            bool readOnly = false;
            if (mode == "view")
            {
                readOnly = true;
            }

            txtPersonName.ReadOnly = readOnly;
            txtEmail.ReadOnly = readOnly;
            txtPhoneNumber.ReadOnly = readOnly;
            cbbCity.Enabled = !readOnly;
            cbbDistrict.Enabled = !readOnly;
            txtAddress.ReadOnly = readOnly;
            txtAvailableRooms.ReadOnly = readOnly;
            txtMeterSQuare.ReadOnly = readOnly;
            txtPrice.ReadOnly = readOnly;
            cbbRoomType.Enabled = !readOnly;
            txtDescription.ReadOnly = readOnly;
            radUploadMulti.Visible = !readOnly;
            radFemale.Enabled = radMale.Enabled = !readOnly;
            cbbRealestateType.Enabled = !readOnly;
        }

        private Post GetSavePost()
        {
            Post saveItem = ViewState["CurrentPost"] as Post;
            int displayIndex = 1;
            if (saveItem == null)
            {
                saveItem = new Post();
                saveItem.ImageList = new List<Common.Image>();
                saveItem.PostTypeId = (int)PostTypes.Room;
                saveItem.CountryId = 232;
            }
            else if (saveItem.ImageList != null)
            {
                int? max = saveItem.ImageList.Max(i => i.DisplayIndex);
                displayIndex = (max.HasValue ? max.Value : 0) + 1;
            }
            saveItem.PersonName = txtPersonName.Text;
            saveItem.PhoneNumber = txtPhoneNumber.Text;
            saveItem.Email = txtEmail.Text;            
            saveItem.CityId = Convert.ToInt32(cbbCity.SelectedValue);
            saveItem.DistrictId = Convert.ToInt32(cbbDistrict.SelectedValue);
            saveItem.Address = txtAddress.Text;
            
            saveItem.MeterSquare = Convert.ToDecimal(txtMeterSQuare.Value);
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


        protected void OnBtnSaveClicked(object sender, EventArgs e)
        {
            Post savePost = GetSavePost();
            Business.BusinessMethods.SavePost(savePost);

            string message = "Tin của bạn đã được đăng thành công.";
            string script1 = " alert(\"" + message + "\")";
            PostDetailsAjaxManager.ResponseScripts.Add(script1);

            string script = "<script type=\"text/javascript\">";
            script += " OnBtnSaveClientClicked();";
            script += " </script>";

            if (!ClientScript.IsClientScriptBlockRegistered("redirectUser"))
                ClientScript.RegisterStartupScript(this.GetType(), "redirectUser", script);
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