<?php
// Include the PHP files that define objects or functions we need in this file.
include_once '../application_files/database.php';
include_once '../application_files/user.php';  
include_once '../application_files/utility_functions.php';

// If the POST request doesn't have values for the keys "startRow" and "endRow", don't do anything.
if (isset($_POST['startRow']) && isset($_POST['endRow']))
{
    // Declare two variables that will contain the rows of data we want from the database.
    $startRow = $endRow = 0;

    // Set those variables to the values contained in the POST array.
    $startRow = $_POST['startRow'];
    $endRow = $_POST['endRow'];   

    // This is technically unnecessary, but call the cleanseInput() function to remove whitespace, slashes, and convert HTML special characters.
    $startRow = cleanseInput($startRow);
    $endRow = cleanseInput($endRow); 
    
    // Declare and construct the database object.
    $database = new Database();
    
    // Get a connection to the database.
    $databaseConnection = $database->getConnection();

    // Construct a User based on the connection to the database.
    $user = new User($databaseConnection);
    
    // Call the getRequestData() function with the specified start and end row indexes.
    $statement = $user->getRequestData($startRow, $endRow);
    
    // Set the fetch mode to an associative array.
    $statement->setFetchMode(PDO::FETCH_ASSOC);
    
    // Declare the array that will contain request information arrays.
    $requests = array();
    
    // Fetch individual rows from the query results and construct a "request" array of key/value pairs for each row.
    while($row = $statement->fetch())
    {
        // Construct an array that represents a single request from a single row of the query results.
        // NOTE: the name of the key (for example the key "Email") MUST match the name of the property of the RequestViewModel class back in the Xamarin application.
        // This is because, when the JsonConvert.DeserializeObject() function is called in the application, it will look for keys in this array that correspond to properties of the RequestViewModel object.
        // If the array key matches the property of the object, then it will set the property of the object to the value associated with the matching key.
        $request = array("Email" => $row['email'],
            "FirstName" => $row['first_name'],
            "LastName" => $row['last_name'],
            "PrimaryPhoneNumber" => $row['phone_number'],
            "notes" => $row['notes'],
            "OrganizationName" => $row['organization_name'],
            "GradeLevels" => $row['grade_level'],
            "number_of_presentations" => $row['number_of_presentations'],
            "number_of_students_per_presentation" => $row['number_of_students_per_presentation'],
            "subject_requested" => $row['subject_requested'],
            "OtherConcerns" => $row['concerns'],
            "isMonday_preferred" => $row['isMonday_preferred'],
            "isTuesday_preferred" => $row['isTuesday_preferred'],
            "isWednesday_preferred" => $row['isWednesday_preferred'],
            "isThursday_preferred" => $row['isThursday_preferred'],
            "isFriday_preferred" => $row['isFriday_preferred'],
            "isSaturday_preferred" => $row['isSaturday_preferred'],
            "isSunday_preferred" => $row['isSunday_preferred'],
            "proposed_time_date" => $row['proposed_time_date'],
            "organization_street_address" => $row['organization_street_address'],
            "organization_zip" => $row['organization_zip'],
            "organization_city" => $row['organization_city'],
            "organization_state" => $row['organization_state']);
        
        // Add the request array to the requests array.
        array_push($requests, $request);
    }
    
    // Send a JSON encoded version of the requests array back to the Xamarin application.
    // The following two lines of code (from the Xamarin application) are responsible for decoding this JSON string:
    //
    //      HttpResponseMessage response = await client.PostAsync(uri, formUrlEncodedContent); // Get the HTTP response from the PHP file (this contains this encoded JSON array).
    //
    //      string responseContent = await response.Content.ReadAsStringAsync(); // Pull the encoded JSON array out of the HTTP response.
    //
    //      List<RequestViewModel> rvmList = JsonConvert.DeserializeObject<List<RequestViewModel>>(responseContent); // Deserialize (as in unencode) the JSON array and construct a List of RequestViewModel objects from it.
    echo json_encode($requests);
}
?>