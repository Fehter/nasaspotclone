<?php
// Include the PHP files that define objects or functions we need in this file.
include_once '../application_files/database.php';
include_once '../application_files/user.php';  
include_once '../application_files/utility_functions.php';

// If the POST request doesn't have values for the keys "email" and "password", don't do anything.
if (isset($_POST['email']) && isset($_POST['password']))
{
    // Declare two variables that will contain the rows of data we want from the database.
    $email = $password = '';

    // Set those variables to the values contained in the POST array.
    $email = $_POST['email'];
    $password = $_POST['password'];   

    // Run the user input through the cleanseInput() function.
    // See utility_function.php for a definition of cleanseInput().
    $email = cleanseInput($email);
    $password = cleanseInput($password); 
    
    // Declare and construct the database object.
    $database = new Database();
    
    // Get a connection to the SQL database.
    $databaseConnection = $database->getConnection();

    // Construct a User based on the connection to the database.
    $user = new User($databaseConnection);       
    
    // Set the user's email and password data members.
    $user->email = $email;
    $user->password = $password; // We might base64 encode this (by calling base64_encode()) ... not sure yet. See: http://php.net/manual/en/function.base64-encode.php
    
    // Call the login() function of the User class and store the subsequent query result in the $statement variable.
    $statement = $user->login();
    
    // If the email/password combination of the user exists in the database, then $statement will have at least one row.
    // If it has zero rows, then that email/password combination does not belong to a registered user.
    if($statement->rowCount() == 1)
    {
        // Set the fetch mode to an associative array.
        $statement->setFetchMode(PDO::FETCH_ASSOC);
        
        // Get the query result row from the statement as an associative array.
        $row = $statement->fetch();
        
        // Construct a associative array, "$login_response", with keys that match the LoginResponse class in the Xamarin application (see LoginResponse.cs for the class definition).
        // This array is for a successful login attempt.
        $login_response = array(
            "Status" => true,
            "Message" => "Login successful",
            "Email" => $row['email']);
    }
    
    // Return a "failed login" array.
    else if ($statement->rowCount() == 0)
    {        
        $login_response = array(
            "Status" => false,
            "Message" => "Login unsuccessful: invalid email and password combination");
    }
    
    // Return a "failed login" array.
    else if ($statement->rowCount() > 1)
    {        
        $login_response = array(
            "Status" => false,
            "Message" => "Login unsuccessful: more than one user with the same email and password combination!");
    }
    
    // Return a "failed login" array.
    else if ($statement->rowCount() < 0)
    {       
        $login_response = array(
            "Status" => false,
            "Message" => "Login unsuccessful: somehow the statement's rowCount() was less than 0 ... this should never happen.");
    }
    
    // Send the JSON encoded login response associative array back to the Xamarin application.
    echo json_encode($login_response);
}
?>