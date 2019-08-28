
using System;
using System.Collections.Generic;
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
            const int verionPartCount = 3; 

            var software = GetAllSoftware();
            var newerSoftware = new List<Software>();

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
                        throw new Exception("Invalid version");
                    }
                }

                // filling out parts user didn't enter to make comparison easier
                while (intParts.Count < verionPartCount) {
                    intParts.Add(0);
                }

                return intParts;
            };

            var version = getVersion(versionStr);
            
            foreach(var item in software) {
                var softwareVersion = getVersion(item.Version);

                var isOlder = false; 
                for (var i = 0; i < verionPartCount; i++) {
                    if (version[i] > softwareVersion[i]) {
                        isOlder = true; 
                        break;
                    }
                }

                if (!isOlder) {
                    newerSoftware.Add(item);
                }
            }

            return newerSoftware;
        }
    }
}
