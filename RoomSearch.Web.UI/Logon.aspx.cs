using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Resources;
using Telerik.WebControls;
using System.Collections.Generic;
using System.Collections.Specialized;
using RoomSearch.Business;
using RoomSearch.Common;

namespace RoomSearch.Web.UI
{
    public partial class Logon : System.Web.UI.Page
    {
        #region Private Attributes

        private HtmlTable _loginTable;
        private RadComboBox _userNameCombo;
        private TextBox _userNameText;
        private RequiredFieldValidator _userValidator;
        private Button _changeButton;
        private Label _organisationName;
        private Button _loginButton;

        private HtmlTable _authorisationCodeTable;
        private TextBox _authorisationCode;
        private Button _updateButton;
        private Button _cancelButton;
        private CustomValidator _organisationUnique;
        
        //private static readonly string _autoCompleteUserNameInRoles = MimosaSettings.GetValue<string>(MimosaSettings.Setting.AutoCompleteUserNamesInRole);

        #endregion

        #region Overrides

        protected override void OnInit(EventArgs e)
        {
            this.Login1.LoginError += new EventHandler(Login1_LoginError);
            base.OnInit(e);

            // Determine the configuration setting so we know which username control is visible and for validation
            bool enableAutoLoginComplete = CommonSettings.IsEnableAutoLoginComplete();

            AssignLocalPointersToControls();

            // Assign the handlers as not exposed by login control.
            _updateButton.Click += new EventHandler(UpdateButton_Click);
            _changeButton.Click += new EventHandler(ChangeButton_Click);
            _cancelButton.Click += new EventHandler(CancelButton_Click);

            // If an authentication timeout occurs, and the user performs an action that results in an AJAX callback
            // the browser hangs or errors.  To deal with this, we have to force the browser to properly reload the page
            // However, as we also have AJAX and postbacks on this page itself, we have to allow this and not force a redirect
            // Hence the check before setting redirect location.
            if (!_userNameCombo.IsCallBack && !IsPostBack)
                Response.RedirectLocation = Request.Url.OriginalString;

            _userNameCombo.Visible = enableAutoLoginComplete;
            _userNameText.Visible = !enableAutoLoginComplete;
            _userValidator.ControlToValidate = enableAutoLoginComplete ? "UserNameComboBox" : "UserName";

            if (enableAutoLoginComplete)
            {
                _userNameCombo.ItemsRequested += new RadComboBoxItemsRequestedEventHandler(UserNameCombo_ItemsRequested);
                Login1.LoggingIn += new LoginCancelEventHandler(Login1_LoggingIn);
            }
        }

        #endregion

        #region Private Methods

        private List<string> ListMatchingUserName(string startsWith)
        {
            // 2. Do we have an entry in the dictionary already or do we have to fetch and store?
            List<string> resultList = null;

            return resultList;
        }

        /// <summary>
        /// Gain access to our controls in the template control.
        /// </summary>
        private void AssignLocalPointersToControls()
        {
            _loginTable = Login1.FindControl("LoginTable") as HtmlTable;
            _userNameCombo = Login1.FindControl("UserNameComboBox") as RadComboBox;
            _userNameText = Login1.FindControl("UserName") as TextBox;
            _userValidator = Login1.FindControl("UserNameRequired") as RequiredFieldValidator;
            _changeButton = Login1.FindControl("ChangeOrganisationButton") as Button;
            _organisationName = Login1.FindControl("OrganisationName") as Label;
            _loginButton = Login1.FindControl("LoginButton") as Button;

            _authorisationCodeTable = Login1.FindControl("AuthorisationCodeTable") as HtmlTable;
            _authorisationCode = Login1.FindControl("AuthorisationCode") as TextBox;
            _updateButton = Login1.FindControl("UpdateButton") as Button;
            _cancelButton = Login1.FindControl("CancelButton") as Button;
            _organisationUnique = Login1.FindControl("OrganisationUnique") as CustomValidator;
        }

        
        /// <summary>
        /// Toggle visibility between 'login details entry mode' and 'auth code entry mode'.
        /// </summary>
        /// <param name="visible"></param>
        private void ToggleControlVisibility(bool authCodeValidated)
        {
            // Switching between either mode means the combo list gets invalidated.
            if (_userNameCombo.Items != null && _userNameCombo.Items.Count > 0)
            {
                _userNameCombo.Items.Clear();
            }

            _loginTable.Visible = authCodeValidated;
            _loginTable.Attributes.Add("display", authCodeValidated ? "block" : "none");

            _authorisationCodeTable.Visible = !authCodeValidated;
            _authorisationCodeTable.Attributes.Add("display", authCodeValidated ? "none" : "block");
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            // Workaround for the stupid telerik "light" callback.
            // http://www.telerik.com/help/aspnet/combobox/combo_iscallback.html
            bool isComboCallBack = (_userNameCombo != null && _userNameCombo.IsCallBack);
            if (!IsPostBack && !isComboCallBack)
            {
                if (Request.QueryString["Logout"] != null)
                {
                    FormsAuthentication.SignOut();
                    Roles.DeleteCookie();
                    
                    Response.Redirect("Logon.aspx");
                }
                else
                {
                    Button loginButton = Login1.FindControl("LoginButton") as Button;
                    if (loginButton != null)
                    {
                        Page.Form.DefaultButton = loginButton.UniqueID;
                    }

                }
            }
        }

        private void UserNameCombo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox combo = (RadComboBox)o;
            if (combo != null)
            {
                if (combo.Items != null && combo.Items.Count > 0)
                {
                    combo.Items.Clear();
                }

                if (!string.IsNullOrEmpty(e.Text))
                {
                    List<string> userNames = ListMatchingUserName(e.Text.Replace("'", "''"));
                    foreach (string name in userNames)
                    {
                        RadComboBoxItem item = new RadComboBoxItem(name);
                        item.Value = name;
                        combo.Items.Add(item);
                    }
                }
            }
        }

        // If we're using the combo, put combo value into the original username textbox so normal processing can occur
        private void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            Login1.UserName = _userNameCombo.AllowCustomText && _userNameCombo.Value == string.Empty ? _userNameCombo.Text : _userNameCombo.Value;
            MembershipUser userInfo = Membership.GetUser(Login1.UserName);
            if (userInfo != null)
            {
            }
            Response.Cookies["ActiveModule"].Value = string.Empty;
        }

        private void Login1_LoginError(object sender, EventArgs e)
        {
            // There was a problem logging in the user
            // See if this user exists in the database
            MembershipUser userInfo = Membership.GetUser(Login1.UserName);

            if (null == userInfo)
                this.ExtraErrorInformation.Text = Properties.Resources.UserDoesNotExist + Login1.UserName;
            else
            {
                // See if the user is locked out or not approved
                if (!userInfo.IsApproved)
                    this.ExtraErrorInformation.Text = Properties.Resources.AccountNotApproved;
                else if (userInfo.IsLockedOut)
                    this.ExtraErrorInformation.Text = Properties.Resources.AccountLocked;
                else
                    this.ExtraErrorInformation.Text = string.Empty;
            }
        }

        /// <summary>
        /// Toggle the visibility of the auth code controls on to allow entry of a different auth code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeButton_Click(object sender, EventArgs e)
        {
            ToggleControlVisibility(false);
        }

        /// <summary>
        /// If the auth code entered is valid, toggle the visibility of the login controls on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// If the machine has a valid cookie, cancel reverts to that org.
        /// Else there is no login ability other than to show the auth code controls again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CancelButton_Click(object sender, EventArgs e)
        {
        }

        #endregion

    }
}
