using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Versions.Managers;
using Versions.Models;

namespace Versions.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            NewerSoftware = new List<Software>();
        }

        [BindProperty]
        public string Version { get; set; }

        public IList<Software> NewerSoftware { get; set; }

        public void OnPost()
        {
            NewerSoftware = SoftwareManager.GetNewerSoftware(Version).ToList();
        }
    }
}
