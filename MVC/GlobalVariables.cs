﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace MVC
{
    public class GlobalVariables
    {
        public static HttpClient WebApiClient = new HttpClient();


       static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44334/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}