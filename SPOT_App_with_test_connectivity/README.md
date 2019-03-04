This folder contains TESTING CODE for connecting from the Xamarin application to the database.  
NOTE: I did NOT write all of this code myself -- I was primarily following this tutorial: https://codinginfinite.com/restful-web-services-php-example-php-mysql-source-code/  
And, to some degree, this tutorial: https://www.w3schools.com/php/php_mysql_connect.asp  


There are two subfolders "client_side_files" and "server_side_files" in this folder.  

"client_side_files" contains the files for the Xamarin application -- for connectivity purposes the only files that matter are as follows:  
- App.xaml.cs: this file constructs the "RestService" class which is responsible for connecting to the correct PHP files.  
- RestService.cs: this file instantiates the HttpClient class and connects to a PHP file on the Apache server.  
- RequestViewModel.cs: this file defines a class that contains information relating to a request. The important method of this file is "GetFormUrlEncodedContent()".
This method is used by the HttpClient instance back in the RestService.cs file to correctly sent a POST to a PHP file.
This is necessary because a POST must be in a certain format (it is essentially an array with key value pairs), and the GetFormUrlEncodedContent() method returns an object with those key value pairs.
In general, GetFormUrlEncodedContent() was used to test sending data from the Xamarin application to the PHP file and, potentially, inserting that data into the database.

"server_side_files" contains the files that will be hosted on the Apache HTTP server -- the files are as follows:  
- database.php: this file defines a class that represents a Database. It has one method: getConnection() which uses the credentials of the data members of the Database class to create and return a connection.  
- user.php: this file defines a class that represents a User. It has several methods -- get_request_data() and set_request_data() are used by the Xamarin application.  
- get_request_data.php: this file uses both the User and Database classes to retrieve test data from the database.  
- test_set_request_data.php: this file uses the User and Database classes to set/insert test data into the database.  

NOTE: "get_request_data.php" and "test_set_request_data.php" are the PHP files that are directly referenced in the Xamarin application.  
Example (this code can be found in "RestService.cs"):  

var uri = new Uri("http://192.168.1.126:80/test/application_files/test_set_request_data.php");  // "192.168.1.126:80" references the local Apache server I have installed on my computer. "test_set_request_data.php" tells it which file to look for.  
To be clear: 192.168.1.126 is my IP address and 80 is the port number on which the Apache server is running.  

HttpResponseMessage response = await client.PostAsync(uri, rvm.GetFormUrlEncodedContent()); // POST the rvm.GetFormUrlEncodedContent() to the Apache server / PHP file combination referenced by the URI and store the PHP file's response in the "response" variable.  

string responseContent = await response.Content.ReadAsStringAsync(); // Get the content from the response.  

Debug.WriteLine(responseContent); // Print the response content to the console.  

NOTE: In case you try to use something like: "http://localhost/test/application_files/test_set_request_data.php" -- this will fail because the Xamarin application is run on an emulator.  
As I understand it, when it looks for the "localhost" it will do so on the emulator NOT your computer where you the Apache server installed.  
So you must identify the computer that has the Apache server software on it with the IP/port number combination.  

So, in general, if you want to perform a particular action like logging a user in, you must write a "login.php" file and reference that file in the Xamarin application.  
This "login.php" file will then handle the credentials sent to be the Xamarin application and perform the necessary queries/checks on the database to log the user in.  