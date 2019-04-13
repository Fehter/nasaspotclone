using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Xamarin.Forms;
using System.Security.Cryptography; // This library gives us access to several different hashing algorithms. Hopefully one of those algorithms will match whatever is available in PHP (for the create request web page).
using Newtonsoft.Json;
using SPOT_App.Models; // This is necessary because the LoginResponse class is defined in the SPOT_App.Models namespace.
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
        User user;
        LoginResponse loginResponse;
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
            var uri = new Uri("http://10.0.2.2:80/test/application_files/test_get_data.php");

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
            var uri = new Uri("http://10.0.2.2:80/test/application_files/test_set_data.php");

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

        // This function sends a POST request to the "get_request_data.php" file on the Apache server.
        // It uses the integer arguments "startRow" and "endRow" to tell the "get_request_data.php" file which rows (effectively which requests) should be queried from the database.
        public async Task<List<RequestViewModel>> GetRequestData(int maxNumRowsToGet, int startRowOffset)
        {
            Debug.WriteLine("********** RestService.GetRequestData() START **********");

            // Identify the target PHP file.
            var uri = new Uri("http://10.0.2.2:80/test/application_files/get_request_data.php");

            try
            {
                // Create the content that will be posted to the target PHP file.
                // This content will be used by the PHP file to determine which rows should be queried from the database.
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("maxNumRowsToGet", maxNumRowsToGet.ToString()),
                    new KeyValuePair<string, string>("startRowOffset", startRowOffset.ToString())
                });

                // Send the POST request to the PHP file at the specified URI.
                HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);

                //byte[] tempArray = await response.Content.ReadAsByteArrayAsync();
                //string responseContent = Encoding.UTF8.GetString(tempArray);

                // This line, for our purposes, seemingly has the same effect as the above two commented-out lines.
                // Read the HttpResponseMessage's Content property as a string.                
                string responseContent = await response.Content.ReadAsStringAsync();
                
                Debug.WriteLine("RestService.GetRequestData(): RESPONSE CONTENT AS FOLLOWS:");

                // This will print the JSON string itself -- this is what is "deserialized" by the JsonConvert.DeserializeObject() function.
                Debug.WriteLine(responseContent);

                Debug.WriteLine("RestService.GetRequestData(): RESPONSE CONTENT END");

                // Because the PHP file sends back a JSON encoded associative array, we can use the "Newtonsoft.Json" package to deserialize the JSON string and, thereby, create a list of RequestViewModel objects (which I've called rvmList).
                List<RequestViewModel> rvmList = JsonConvert.DeserializeObject<List<RequestViewModel>>(responseContent);

                // To make sure that the JsonConvert.DeserializeObject() function was successful, loop through the list and call the GetContents() function on each object.
                // If the JsonConvert.DeserializeObject() was successful, then the values of some of properties of the RequestViewModel objects should match what is stored in the SQL database.
                foreach (RequestViewModel rvm in rvmList)
                {
                    Debug.WriteLine(rvm.GetContents());
                }
                Debug.WriteLine("********** RestService.GetRequestData() END **********");
                return rvmList;
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.GetRequestData(): something went wrong while testing connection to web service!");
                return null;
            }

        }

        // This is a TEST login function -- it will send the argument email and password combination to the login.php file on the Apache server.
        // That login.php file will return one of two associative arrays:
        // 1: A "login successful" array that will have the following key/value pairs (note: the format is "key" -> "value"):
        //      - "Status" -> "true" // This is a boolean in the PHP file but gets converted to a string when the LoginResponse is constructed.
        //      - "Message" -> "Login successful" // This is a string
        //      - "Email" -> *Email* // This the Email of the user as a string
        //
        // 2: A "login unsuccessful" array that will have the following key/value pairs:
        //      - "Status" -> "false" // This is a boolean in the PHP file but gets converted to a string when the LoginResponse is constructed.
        //      - "Message" -> "Login unsuccessful: invalid email and password combination" // This is a string
        //
        // These associative arrays are then converted into a LoginResponse object via a call to JsonConvert.DeserializeObject().
        // A LoginResponse object has get/set Status, Message, and Email functions -- these allow you to easily see if the attempted login was successful.
        public async Task<LoginResponse> login(string email, string password)
        {
            Debug.WriteLine("********** RestService.test_login() START **********");

            // Identify the target PHP file.
            var uri = new Uri("http://10.0.2.2:80/test/application_files/login.php");

            try
            {
                // Create the content that will be posted to the target PHP file.
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password)
                });

                // Send the POST request to the PHP file at the specified URI.
                HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);

                // Read the HttpResponseMessage's Content property as a string.                
                string responseContent = await response.Content.ReadAsStringAsync();

                // Because the PHP file sends back a JSON encoded associative array, we can use the "Newtonsoft.Json" package to deserialize the JSON string and, thereby, create LoginResponse object.
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                Debug.WriteLine("RestService.test_login(): RESPONSE CONTENT AS FOLLOWS:");

                // This will print the JSON string itself -- this is what is "deserialized" by the JsonConvert.DeserializeObject() function.
                Debug.WriteLine(responseContent);

                Debug.WriteLine("RestService.test_login(): RESPONSE CONTENT END");

                // Print the contents of the LoginResponse to see if the attempted login was successful.
                // In this case "successful" just means that the email/password combination given to this test_login() function was in the SQL database.
                Debug.WriteLine(loginResponse.GetContents());
                return loginResponse;
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_login(): something went wrong!");
                return null;
            }
            //Debug.WriteLine("********** RestService.test_login() END **********");
        }

        // This function takes an argument string of a password and returns the hashed result as a lowercase hexadecimal string.
        // For more information, see: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?view=netstandard-2.0
        public string getPasswordHash(string password)
        {
            // Choose which hashing algorithm to use and store it in a variable.
            var hashingAlgorithm = MD5.Create();            
            
            // Compute the hash of the password and store the resulting byte array in a variable.
            byte[] hashedPasswordBytes = hashingAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Construct the StringBuilder instance that we will use to convert the byte array form of the hashed password into a hex string.
            StringBuilder stringBuilder = new StringBuilder();

            // Individually convert each byte in the password byte array into a lowercase hex string an append the result to the StringBuilder instance.
            foreach (byte hashByte in hashedPasswordBytes)
            {
                // "hashByte.ToString("x2")" converts the byte into a lowercase hexadecimal number (indicated by the format string "x2").
                // That converted byte is then appended onto the StringBuilder instance.
                // For more information on "x2" see: https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
                stringBuilder.Append(hashByte.ToString("x2")); 
            }

            // Now that the StringBuilder contains the complete hex string of the hashed password, call ToString() again and return the resulting string hash of the password.
            return stringBuilder.ToString();
        }

        // This function tests the compatibility of the hash algorithm used in the getPasswordHash() function of this RestService class and the other hash algorithm used in the "test_password_hashing.php" file.
        // It takes a password and computes the corresponding hash of that password by calling the getPasswordHash() function of the RestService.
        // It then sends (in a POST request) a FormUrlEncodedObject with key/value pairs of the email, the password, and the hashed password to the "test_password_hashing.php" file.
        // As a response, the "test_password_hashing.php" file echos:
        // 1: the unaltered email and unaltered password.
        // 2: the unaltered email and the password hashed by the getPasswordHash() function of the RestService.
        // 3: the unaltered email and the password hashed by an algorithm in the "test_password_hashing.php" file.
        // 4: the result of comparing the Xamarin hashed password with the PHP hashed password.
        public async void testPasswordHashing()
        {
            Debug.WriteLine("********** RestService.testPasswordHashing() START **********");

            // Set the email/password combination that will be hashed and tested.
            string email = "testEmail";
            string password = "testPassword";

            // Hash the password with an algorithm defined in the hashPassword() function.
            // Subsequently store the string hash of the password in the hashedPassword variable.
            string hashedPassword = getPasswordHash(password);

            var uri = new Uri("http://10.0.2.2:80/test/application_files/test_password_hashing.php");

            try
            {
                // Construct the FormUrlEncodedContent object with the email, password, and hashed password as key/value pairs.
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),                    
                    new KeyValuePair<string, string>("hashedPassword", hashedPassword)
                });

                // Send the FormUrlEncodedContent object to the PHP file in a POST request and await a response.
                HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);
                   
                // Read the response's content as a string.
                string responseContent = await response.Content.ReadAsStringAsync();                

                Debug.WriteLine("RestService.testPasswordHashing(): RESPONSE CONTENT AS FOLLOWS:");

                // Print the response from the PHP file.
                Debug.WriteLine(responseContent);

                Debug.WriteLine("RestService.testPasswordHashing(): RESPONSE CONTENT END");              
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.testPasswordHashing(): something went wrong!");
            }
            
            Debug.WriteLine("********** RestService.testPasswordHashing() END **********");
        }

        // This function sends a POST request to the "get_user_data.php" file on the Apache server.
        // It uses the email of the loginResponse to load the rest of the user data
        public async Task<User> GetUserData(string email)
        {
            Debug.WriteLine("** RestService.GetUserData() START **");

            // Identify the target PHP file.
            var uri = new Uri("http://10.0.2.2:80/test/application_files/get_user_data.php");

            try
            {
                // Create the content that will be posted to the target PHP file.
                // This content will be used by the PHP file to determine which rows should be queried from the database.
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("email", email),

                });
                // Send the POST request to the PHP file at the specified URI.
                HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);

                // Read the HttpResponseMessage's Content property as a string.                
                string responseContent = await response.Content.ReadAsStringAsync();
                // Because the PHP file sends back a JSON encoded associative array, we can use the "Newtonsoft.Json" package to deserialize the JSON string and, thereby, create LoginResponse object.
                user = JsonConvert.DeserializeObject<User>(responseContent);

                Debug.WriteLine("RestService.GetUserData(): RESPONSE CONTENT AS FOLLOWS:");

                // This will print the JSON string itself -- this is what is "deserialized" by the JsonConvert.DeserializeObject() function.
                Debug.WriteLine(responseContent);

                Debug.WriteLine("RestService.GetUserData(): RESPONSE CONTENT END");

                // Print the contents of the UserResponse to see if the user object was loaded correctly
                Debug.WriteLine(user.GetContents());
                return user;
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
                Debug.Fail("RestService.GetUserData(): something went wrong while testing connection to web service!");
                return null;
            }


        }
        //==============================================================================================================================================================================//

        // This is a test function. It is meant to connect to a PHP web service that remembers the username of the user who logs in (this is done via $_SESSION in the PHP file).
        // This function will perform four actions:
        // 1: it will attempt to login.
        // 2: it will attempt to get request data -- this should only succeed if the previous login attempt succeeded.
        // 3: it will attempt to logout.
        // 4: it will attempt to get request data again -- this should fail provided the previous logout was successful.
        public async void test_Login_GetRequestData_Logout_GetRequestData_WithSession()
        {
            Debug.WriteLine("********** RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession() START **********");

            // The user to login as.
            string email = "testuseremail1@test.com";
            string password = "password1";

            // Get at most two rows from the database.
            int maxNumRowsToGet = 2;

            // Skip the first two rows.
            int startRowOffset = 2;

            //----------------------------------------------------------------------------------------------------------//
            // Attempt login.

            var uri = new Uri("http://10.0.2.2:80/test/application_files/login.php");

            try
            {
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password)
                });

                HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);
                string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): LOGIN: RESPONSE CONTENT AS FOLLOWS:");

                Debug.WriteLine(responseContent);

                Debug.WriteLine("-----------------------------------------------------------------------------------------");

                try
                {
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                    Debug.WriteLine(loginResponse.GetContents());
                }

                catch (Exception e)
                {
                    Debug.Fail("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): LOGIN: something went wrong while deserializing \"responseContent\":");
                    Debug.WriteLine(e);
                }

                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): LOGIN: RESPONSE CONTENT END");
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): LOGIN: something went wrong while logging in!");
            }

            //----------------------------------------------------------------------------------------------------------//
            // Attempt to get request data.

            uri = new Uri("http://10.0.2.2:80/test/application_files/get_request_data.php");

            try
            {
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("maxNumRowsToGet", maxNumRowsToGet.ToString()),
                    new KeyValuePair<string, string>("startRowOffset", startRowOffset.ToString())
                });

                HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);
                string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: RESPONSE CONTENT AS FOLLOWS:");

                Debug.WriteLine(responseContent);

                Debug.WriteLine("-----------------------------------------------------------------------------------------");

                try
                {
                    List<RequestViewModel> rvmList = JsonConvert.DeserializeObject<List<RequestViewModel>>(responseContent);

                    foreach (RequestViewModel rvm in rvmList)
                    {
                        Debug.WriteLine(rvm.GetContents());
                    }
                }

                catch (Exception e)
                {
                    Debug.Fail("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: something went wrong while deserializing \"responseContent\":");
                    Debug.WriteLine(e);
                }

                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: RESPONSE CONTENT END");
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: something went wrong while trying to get request data!");
            }

            //----------------------------------------------------------------------------------------------------------//
            // Attempt to logout.

            uri = new Uri("http://10.0.2.2:80/test/application_files/logout.php");

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): LOGOUT: RESPONSE CONTENT AS FOLLOWS:");
                Debug.WriteLine(responseContent);
                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): LOGOUT: RESPONSE CONTENT END");
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): LOGOUT: something went wrong while attempting to logout!");
            }

            //----------------------------------------------------------------------------------------------------------//
            // Attempt to get request data again. This should fail since we've already logged out.

            uri = new Uri("http://10.0.2.2:80/test/application_files/get_request_data.php");

            try
            {
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("maxNumRowsToGet", maxNumRowsToGet.ToString()),
                    new KeyValuePair<string, string>("startRowOffset", startRowOffset.ToString())
                });

                HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent);
                string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: RESPONSE CONTENT AS FOLLOWS:");

                Debug.WriteLine(responseContent);

                Debug.WriteLine("-----------------------------------------------------------------------------------------");

                try
                {
                    List<RequestViewModel> rvmList = JsonConvert.DeserializeObject<List<RequestViewModel>>(responseContent);

                    foreach (RequestViewModel rvm in rvmList)
                    {
                        Debug.WriteLine(rvm.GetContents());
                    }
                }

                catch (Exception e)
                {
                    Debug.Fail("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: something went wrong while deserializing \"responseContent\":");
                    Debug.WriteLine(e);
                }

                Debug.WriteLine("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: RESPONSE CONTENT END");
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession(): GET REQUEST DATA: something went wrong while trying to get request data!");
            }

            //----------------------------------------------------------------------------------------------------------//

            Debug.WriteLine("********** RestService.test_Login_GetRequestData_Logout_GetRequestData_WithSession() END **********");
        }

        //==============================================================================================================================================================================// 

        // This test function will send POST request to the echo_POST.php file.
        // That PHP file will echo the contents of the POST back to this function.
        // The results are printed to the console.
        public async void test_POST(RequestViewModel requestViewModel)
        {
            Debug.WriteLine("********** RestService.test_POST() START **********");

            var uri = new Uri("http://10.0.2.2:80/test/application_files/echo_POST.php");

            try
            {
                HttpContent httpContent = requestViewModel.GetFormUrlEncodedContent();
                HttpResponseMessage response = await client.PostAsync(uri, httpContent);
                string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RestService.test_POST(): RESPONSE CONTENT AS FOLLOWS:");
                Debug.WriteLine(responseContent);
                Debug.WriteLine("RestService.test_POST(): RESPONSE CONTENT END");
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.test_POST(): something went wrong!");
            }

            Debug.WriteLine("********** RestService.test_POST() END **********");
        }

        //==============================================================================================================================================================================// 

        // Utility function -- accepts an argument string and returns the same string with white space characters removed.
        public string RemoveWhiteSpace(String str)
        {
            string stringWithNoWhiteSpace = "";

            foreach (Char character in str)
            {
                if (!Char.IsWhiteSpace(character))
                    stringWithNoWhiteSpace += character.ToString();
            }

            return stringWithNoWhiteSpace;
        }

        // Utility function -- accepts an argument string and returns the same string with extra "+" characters removed.
        // As an example, the argument string "a++++b+c" would become "a+b+c".
        public string RemoveExtraPlusCharacters(String str)
        {
            string formattedString = "";

            // For documentation see: https://docs.microsoft.com/en-us/dotnet/api/system.string.split?view=netframework-4.7.2#System_String_Split_System_Char___System_StringSplitOptions_
            string[] strArray = str.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

            formattedString = string.Join("+", strArray);

            return formattedString;
        }

        // This function is called by the DisplayGoogleMapsDirections() function of the RestService class.
        // It converts and argument string into a format that can be inserted into a Google Maps URI.
        // An example Google Maps URI is as follows:
        // http://maps.google.com/maps?saddr=fairmont+wv&daddr=new+york+8th+avenue&directionsmode=driving
        public string ConvertToGoogleMapsUriFormat(String str)
        {
            // Remove white space characters from beginning and end of the string.
            str = str.Trim();
            Debug.WriteLine("RestService.ConvertToGoogleMapsUriFormat(): " + str);

            // Replace " " and "," characters with a "+" character.
            str = str.Replace(' ', '+');
            str = str.Replace(',', '+');
            Debug.WriteLine("RestService.ConvertToGoogleMapsUriFormat(): " + str);

            // Remove all white space characters from the string.
            str = this.RemoveWhiteSpace(str);
            Debug.WriteLine("RestService.ConvertToGoogleMapsUriFormat(): " + str);

            // Remove any extra "+" characters from the string.
            str = this.RemoveExtraPlusCharacters(str);
            Debug.WriteLine("RestService.ConvertToGoogleMapsUriFormat(): " + str);

            return str;
        }

        // This function uses a Google Maps URI to display the how far away the presentation location is from the user's current location.
        // It assumes that the location string stored in the PresentationLocation() get/set function of the argument RequestViewModel object uses a "," character as a delimiter.
        // Note that, on an emulator, this function will not be able to identify the user's location -- this will need to be tested on an actual phone.
        public void DisplayGoogleMapsDirections(RequestViewModel rvm)
        {
            try
            {
                Debug.WriteLine("********** RestService.DisplayGoogleMapsDirections() START **********");
                                
                string locationContent = this.ConvertToGoogleMapsUriFormat(rvm.PresentationLocation);

                //string startingLocation = "my+location"; // This should work when this function is called on a phone -- on an emulator it cannot figure out the user's location.                
                string startingLocation = "fairmont+wv"; // This is a placeholder so that this function will operate correctly on an emulator -- when we test this on a phone, this line must be replaced with the above commented-out line.
                string destinationLocation = locationContent;

                // Create the URI that will be opened with Google Maps.
                var uri = new Uri("http://maps.google.com/maps?" + "saddr=" + startingLocation + "&daddr=" + destinationLocation + "&directionsmode=driving");

                // Open the URI.
                Device.OpenUri(uri);
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("RestService.DisplayGoogleMapsDirections(): something went wrong!");
            }

            Debug.WriteLine("********** RestService.DisplayGoogleMapsDirections() END **********");
        }
    }
}