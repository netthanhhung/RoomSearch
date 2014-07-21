using System;
using System.Web;
using RoomSearch.Common;
using System.Configuration;

namespace RoomSearch.Web.UI
{
    public partial class CommonSettings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool IsEnableAutoLoginComplete()
        {         
            return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableAutoLoginComplete"]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static string GloblaCulture()
        {
            return Convert.ToString(ConfigurationManager.AppSettings["GloblaCulture"]);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static string NumberFormatCulture()
        {
            return Convert.ToString(ConfigurationManager.AppSettings["NumberFormatCulture"]);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static string DateTimeFormatCulture()
        {
            return Convert.ToString(ConfigurationManager.AppSettings["DateTimeFormatCulture"]);

        }

    }
}
