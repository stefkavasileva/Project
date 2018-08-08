﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Landmarks.Web.Areas.Operator.Controllers
{
    [Area("Operator")]
    [Authorize(Roles = "DataEntryOperator")]
    public abstract class OperatorController : Controller
    {
        
    }
}