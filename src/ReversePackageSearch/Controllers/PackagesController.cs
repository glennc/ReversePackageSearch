using Microsoft.AspNet.Mvc;
using System;

namespace ReversePackageSearch
{
    /// <summary>
    /// Summary description for PackagesController
    /// </summary>
    public class PackagesController : Controller
    {
	    public IActionResult Index()
        {
            return View();
        }
    }
}