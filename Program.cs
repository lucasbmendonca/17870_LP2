using _17870_LP2.Controllers;
using _17870_LP2.Models;
using Amazon.Runtime.Internal;
using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace _17870_LP2
{
    /*
       Main program containing the call to the main application controller.
    */
    class Program
    {
        static void Main(string[] args)
        {
            //Create master controller instance.
            HospitalController hospitalController = new HospitalController();
        }
    }
}
