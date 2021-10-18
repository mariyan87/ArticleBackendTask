
using System.ComponentModel;

namespace Common
{
    public enum Activity
    {
        [Description("Can view articles")]
        ViewArticles,

        [Description("Can manage articles")]
        ManageArticles
    }
}
