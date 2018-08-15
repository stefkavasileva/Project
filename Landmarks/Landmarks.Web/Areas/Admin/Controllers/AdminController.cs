using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Landmarks.Web.Common.Constants;

namespace Landmarks.Web.Areas.Admin.Controllers
{
    [Area(NamesConstants.AdminArea)]
    [Authorize(Roles = NamesConstants.RoleAdmin)]
    public abstract class AdminController : Controller
    {
    }
}