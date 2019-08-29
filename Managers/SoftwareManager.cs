
using System;
using System.Collections.Generic;
using System.Linq;
using Versions.Exceptions;
using Versions.Models;

namespace Versions.Managers 
{
    public static class SoftwareManager
    {
        public static IEnumerable<Software> GetAllSoftware()
        {
            return new List<Software>
            {
                new Software
                {
                    Name = "MS Word",
                    Version = "13.2.1."
                },
                new Software
                {
                    Name = "AngularJS",
                    Version = "1.7.1"
                },
                new Software
                {
                    Name = "Angular",
                    Version = "8.1.13"
                },
                new Software
                {
                    Name = "React",
                    Version = "0.0.5"
                },
                new Software
                {
                    Name = "Vue.js",
                    Version = "2.6"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2017.0.1"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2019.1"
                },
                new Software
                {
                    Name = "Visual Studio Code",
                    Version = "1.35"
                },
                new Software
                {
                    Name = "Blazor",
                    Version = "0.7"
                }
            };
        }

        public static IEnumerable<Software> GetNewerSoftware(string versionStr) 
        {
            // define in a single place, easier to update if we add software that has more than three parts
            const int versionPartCount = 3; 

            var software = GetAllSoftware();
            var newerSoftware = new List<Software>();

            // keeping local for now, may need to be in a more reusable place in the future
            Func<string, IList<int>> getVersion = delegate(string ver) 
            {
                // account for when version starts with a period
                // assuming that means that means 0
                var realVer = ver; 
                if (ver.StartsWith('.')) {
                    realVer = $"0{ver}";
                }

                var parts = realVer.Split('.');
                var intParts = new List<int>();

                foreach (var part in parts) {
                    // we might have empty parts because of trailing periods
                    if (string.IsNullOrWhiteSpace(part)) {
                        continue; 
                    }

                    if (int.TryParse(part, out var intPart)) 
                    {
                        intParts.Add(intPart);
                    }
                    else 
                    {
                        throw new InvalidSoftwareVersionException($"Invalid version: {ver}");
                    }
                }

                // filling out parts user didn't enter to make comparison easier
                while (intParts.Count < versionPartCount) {
                    intParts.Add(0);
                }

                return intParts;
            };

            var version = getVersion(versionStr);
            
            foreach(var item in software) {
                var softwareVersion = new List<int>();
                try 
                {
                    softwareVersion = getVersion(item.Version).ToList();
                }
                catch 
                {
                    // no op for now, bad data was entered, let's just show the user the data we can parse
                    // would log this error
                    continue; 
                }

                var isSoftwareNewer = false; 
                for (var i = 0; i < versionPartCount; i++) {
                    // check to see if it is newer AND older
                    // break out either way
                    // only compare next version number if they are equal
                    if (version[i] < softwareVersion[i]) {
                        isSoftwareNewer = true; 
                        break;
                    } 
                    else if (version[i] > softwareVersion[i]) 
                    {
                        break;
                    }
                }

                if (isSoftwareNewer) {
                    newerSoftware.Add(item);
                }
            }

            return newerSoftware;
        }
    }
}
