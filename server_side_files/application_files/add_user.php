<?php
// Include the PHP files that define objects or functions we need in this file.
include_once '../application_files/database.php';
include_once '../application_files/user.php';  
include_once '../application_files/utility_functions.php';
// In order to check if user is logged in, we have to have access to the $_SESSION variable.
// We can get this by calling the following function.
session_start();

// If the key "logged_in_user_email" is not set or is empty, then the user isn't logged in and shouldn't be able to get request data.
if (!isset($_SESSION['logged_in_user_email']) || empty($_SESSION['logged_in_user_email']))
{       
    exit("get_request_data.php: error: User attempted to get request data but wasn't logged in.");
}
// If the POST request doesn't have values for the keys "email" and "password", don't do anything.
else if (isset($_POST['email']) && isset($_POST['password']) && isset($_POST['firstName']) && 
    isset($_POST['lastName']) && isset($_POST['phoneNumber']) && isset($_POST['isAmbassador']) && 
	isset($_POST['isTeacher']) && isset($_POST['isAdmin']))
{
	
    // Declare two variables that will contain the rows of data we want from the database.
    $email = $password = $firstName = $lastName = $phoneNumber = $isAdmin = $isPresenter = $isTeacher = '';

    // Set those variables to the values contained in the POST array.
    $email = $_POST['email'];
    $password = $_POST['password'];   
	$firstName = $_POST['firstName'];
	$lastName = $_POST['lastName'];
	$phoneNumber = $_POST['phoneNumber'];
	$isAmbassador = (int) $_POST['isAmbassador'];
	$isTeacher = (int) $_POST['isTeacher'];
	$isAdmin = (int) $_POST['isAdmin'];

    // Run the user input through the cleanseInput() function.
    // See utility_functions.php for a definition of cleanseInput().
    $email = cleanseInput($email);
    $password = cleanseInput($password); 
	$firstName = cleanseInput($firstName);
	$lastName = cleanseInput($lastName);
	$phoneNumber = cleanseInput($phoneNumber);
	//$isAmbassador = cleanseInput ($isAmbassador);
	//$isTeacher = cleanseInput ($isTeacher);
	//$isAdmin= cleanseInput ($isAdmin);

    
    // Declare and construct the database object.
    $database = new Database();
    
    // Get a connection to the database.
    $databaseConnection = $database->getConnection();

    // Construct a User based on the connection to the database.
    $user = new User($databaseConnection);
    
	
    // Set the user's email based on the value stored in current session.
    $user->email = $_SESSION['logged_in_user_email'];
	
	//Call the user.AddUser function
	$statement = $user->addUser($email, $password, $firstName, $lastName, $phoneNumber, $isAmbassador, $isTeacher, $isAdmin);
	echo $statement;
	//close the PDO connection to the database. Yes, this is how it is closed according to the manual located
	//here. https://www.php.net/manual/en/pdo.connections.php Example#3 demonstrates closing a connection.
	$databaseConnection = null;
	
}

else
{
	
	echo "Not all parameters have been set for add_user.php";
	
}
?>