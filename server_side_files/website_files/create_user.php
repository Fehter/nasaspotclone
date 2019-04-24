<?php
// Include the PHP files that define objects or functions we need in this file.
include_once '../application_files/database.php';
include_once '../application_files/user.php';  
include_once '../application_files/utility_functions.php';

// If the POST request doesn't have values for the keys "email" and "password", don't do anything.
if (isset($_POST['email']) && isset($_POST['password']) && isset($_POST['firstName']) && 
    isset($_POST['lastName']) && isset($_POST['phoneNumber']))
{
	
    // Declare two variables that will contain the rows of data we want from the database.
    $email = $password = $firstName = $lastName = $phoneNumber = "";
	$isAdmin = $isAmbassador = $isTeacher = 0;

    // Set those variables to the values contained in the POST array.
    $email = $_POST['email'];
    $password = $_POST['password'];   
	$firstName = $_POST['firstName'];
	$lastName = $_POST['lastName'];
	$phoneNumber = $_POST['phoneNumber'];
	
	if(isset($_POST['isAdmin']))
	{
		
		$isAdmin = 1;

	}
	
	if(isset($_POST['isTeacher']))
	{
		
		$isTeacher = 1;
		
	}
	
	if(isset($_POST['isAmbassador']))
	{
		
		$isAmbassador = 1;
		
	}

    // Run the user input through the cleanseInput() function.
    // See utility_functions.php for a definition of cleanseInput().
    $email = cleanseInput($email);
    $password = cleanseInput($password); 
	$firstName = cleanseInput($firstName);
	$lastName = cleanseInput($lastName);
	$phoneNumber = cleanseInput($phoneNumber);
    
    // Declare and construct the database object.
    $database = new Database();
    
    // Get a connection to the database.
    $databaseConnection = $database->getConnection();

    // Construct a User based on the connection to the database.
    $user = new User($databaseConnection);
    
	session_start();
	
    // Set the user's email based on the value stored in current session.
    $user->email = $_SESSION['logged_in_user_email'];
	
	//Call the user.AddUser function
	$statement = $user->addUser($email, $password, $firstName, $lastName, $phoneNumber, $isAmbassador, $isTeacher, $isAdmin);
	
	//close the PDO connection to the database. Yes, this is how it is closed according to the manual located
	//here. https://www.php.net/manual/en/pdo.connections.php Example#3 demonstrates closing a connection.
	$databaseConnection = null;
	
	include 'create_user.html';
	
}

else
{
	
	echo "Not all parameters have been set for create_user.php";
	
}
?>