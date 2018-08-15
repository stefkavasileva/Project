using Landmarks.Web.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Areas.Operator.Controllers
{
    [Area(NamesConstants.OperatorArea)]
    [Authorize(Roles = NamesConstants.RoleAdminAndOperator)]
    public abstract class OperatorController : Controller
    {
    }
}