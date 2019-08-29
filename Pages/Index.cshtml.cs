using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Versions.Exceptions;
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
        [Required]
        public string Version { get; set; }

        public string ErrorMessage { get; set; }

        public IList<Software> NewerSoftware { get; set; }

        public void OnPost()
        {
            if (!(ModelState.IsValid))
            {
                return;
            }

            try 
            {
                NewerSoftware = SoftwareManager.GetNewerSoftware(Version).ToList();
            } 
            catch (InvalidSoftwareVersionException exception) 
            {
                ErrorMessage = exception.Message;
                NewerSoftware = new List<Software>();
            }
            catch
            {
                ErrorMessage = "An error occurred while processing your request";
                NewerSoftware = new List<Software>();
                // would log this error
            }
        }
    }
}
