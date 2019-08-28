using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Versions.Models;

namespace Versions.Pages
{
    public class NewerSoftwareModel : PageModel
    {
        public void OnGet()
        {
            
        }   

        public IList<Software> NewerSoftware { get; set; }
    }
}
