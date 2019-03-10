﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using SPOT_App.ViewModels; // This is necessary because the RequestViewModel class is defined in the "SPOT_App.ViewModels" namespace.
                           // Without this line, attempting to declare/initialize a RequestViewModel object would fail.

namespace SPOT_App
{
    // This class will serve as the middleman between the application and the PHP web service that is hosted on the Apache server.
    // The idea is that this class will be instantiated once and then used to communicated to the web service.
    // Depending on the type of communication we want to do (maybe we want to pull request data from the database, maybe we want to log someone in, etc.),
    // we will need to define individual functions for this RestService class.
    // Currently, there are two such functions "test_getData()" and "test_setData()".
    // Each of these functions serves a different purpose and, as a consequence, connects to a different PHP web service file on the Apache server.
    // See the comments on/in these functions for more information.
    public class RestService
    {
        // This class will serve as our method of communication to the PHP web service and, by extension, the SQL database.
        HttpClient client; 

        // Default constructor.
        public RestService()
        {
            client = new HttpClient();
        }
        
        // This function will send a GET request to the "test_get_data.php" file on the Apache server.
        // The function will then wait for a response from the PHP file -- the contents of the response and whether we get a response at all
        // is defined entirely by the PHP file.
        public async void test_getData()
        {
            // Print a message to the Debug console in Visual Studio to indicate that the test_getData() function has been called.
            Debug.WriteLine("********** RestService.test_getData() START **********"); 

            // Create a "Uniform Resource Identifier" that holds the IP, port number, and directory location of the PHP web service file "test_get_data.php".
            var uri = new Uri("http://192.168.1.126:80/test/application_files/test_get_data.php"); 

            try
            {
                // Send a GET request to the "test_get_data.php" file (which it identified by the argument "uri" variable) on the Apache server and "await" a reponse.
                // The response will be stored in the "response" string.
                string response = await client.GetStringAsync(uri);

                // Print the type of the response to the console -- it should print "System.String".
                Debug.WriteLine(response.GetType());

                Debug.WriteLine("RestService.test_getData(): RESPONSE CONTENT AS FOLLOWS:");

                // Print the contents of the response string to the console.
                // The the format and contents of the response is defined by code in the "test_get_data.php" file.
                Debug.WriteLine(response);

                Debug.WriteLine("RestService.test_getData(): RESPONSE CONTENT END");
            }

            // If something went wrong: catch the Exception, print the Exception, and print an error message.
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_getData(): something went wrong while testing connection to web service!");
            }

            Debug.WriteLine("********** RestService.test_getData() END **********");
        }

        // This function will send a POST request to the "test_set_data.php" file on the Apache server.
        // The function will then wait for a response from the PHP file -- the contents of the response and whether we get a response at all
        // is defined entirely by the PHP file.
        public async void test_setData(RequestViewModel requestViewModel)
        {
            // Print a message to the Debug console in Visual Studio to indicate that the test_setData() function has been called.
            Debug.WriteLine("********** RestService.test_setData() START **********");

            // Create a "Uniform Resource Identifier" that holds the IP, port number, and directory location of the PHP web service file "test_set_data.php".
            var uri = new Uri("http://192.168.1.126:80/test/application_files/test_set_data.php");

            try
            {
                // Send a POST request to the "test_get_data.php" file (which it identified by the argument "uri" variable) on the Apache server and "await" a reponse.
                // The response will be stored in the "response" HttpResponseMessage variable.
                // The contents of the POST request is defined by the GetFormUrlEncodedContent() method of the RequestViewModel object class.
                HttpResponseMessage response = await client.PostAsync(uri, requestViewModel.GetFormUrlEncodedContent());

                //Print the type of the response to the console -- it should be "Xamarin.Android.Net.AndroidHttpResponseMessage".
                Debug.WriteLine(response.GetType());

                // Because the response is an HttpResponseMessage object, it has a property called "Content".
                // Print the type of the "Content" property to the console -- it should be "System.Net.Http.StreamContent".
                Debug.WriteLine(response.Content.GetType());

                // Read the contents of the Content property as a string and store it in the "responseContent" string.
                string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RestService.test_setData(): RESPONSE CONTENT AS FOLLOWS:");

                // Print the contents of the responseContent string to the console.
                // The the format and contents of the response is defined by code in the "test_set_data.php" file. 
                Debug.WriteLine(responseContent);

                Debug.WriteLine("RestService.test_setData(): RESPONSE CONTENT END");
            }

            // If something went wrong: catch the Exception, print the Exception, and print an error message.
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_setData(): something went wrong while testing connection to web service!");
            }

            Debug.WriteLine("********** RestService.test_setData() END **********");
        }
    }
}
