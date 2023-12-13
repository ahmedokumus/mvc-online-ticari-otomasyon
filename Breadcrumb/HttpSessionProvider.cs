using System.Web;

namespace MvcOnlineTicariOtomasyon.Breadcrumb;

public class HttpSessionProvider : IProvideBreadCrumbsSession
{
    public string SessionId
    {
        get
        {
            var id = HttpContext.Current.Session.SessionID;
            var sessionKey = id + "-SessionId.MvcBreadCrumbs";

            // Apparently you need to actually ad something to session in order to
            // stabilize the SessionID between requests, who knew.  Just adding SessionID,
            // as a dummy.
            // Modified Session key to minimize the possibility of hitting existant key
            HttpContext.Current.Session[sessionKey] = id;
            return id;
        }
    }
    
}