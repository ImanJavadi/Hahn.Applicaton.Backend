using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Api.Utilities
{
       // a singleton class
        public sealed class Http
        {
           

            private static readonly Lazy<HttpClient> lazyClient = new Lazy<HttpClient> (()=> new HttpClient());

            // a private constructor
            private Http()
            {
            }

            // the method that returns an instance
            public static HttpClient GetHttpClientInstance()
            {
               
                return lazyClient.Value;
            }
        }
}
