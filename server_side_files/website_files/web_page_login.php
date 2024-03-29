<?php

	// Include the PHP files that define objects or functions we need in this file.
	include_once '../application_files/database.php';
	include_once '../application_files/user.php';  
	include_once '../application_files/utility_functions.php';


	//Check if user previously submitted a request on this session. 
	//Returns false if the user has not logged in to get a session yet,
	//meaning no session exists.
	if(isset($_SESSION['has_submitted']))
	{
		
		$_SESSION['has_submitted'] = FALSE;
		
	}
	if (isset($_POST['email']) && isset($_POST['password']))
	{	
		
		$result = json_decode(login($_POST['email'], $_POST['password']), true);
		
		//if user is successfully logged in and they are either a teacher or an admin, take them to the 
		//presentations page.
		if($result['Status'] == true && ($_SESSION['isTeacher'] == true || $_SESSION['isAdministrator'] == true))
		{
			
			$_SESSION['has_submitted']= FALSE; 
			include 'request_form.html';
					
		}
				
		else if ($result['Status'] == false)
		{
			
			echo "The login information was incorrect. Please try again.";
					
		}

		else if ($_SESSION['isTeacher'] == false && $_SESSION['isAdministrator'] == false)
		{
			
			echo "You are not authorized to make presentation requests";
			
		}
	}
	
	function login($email, $password)
	{
		
		// If the POST request doesn't have values for the keys "email" and "password", don't do anything.

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
			$user->setPassword($password); // We might need to base64 encode this (by calling base64_encode()) ... not sure yet. See: http://php.net/manual/en/function.base64-encode.php
    
			// Call the login() function of the User class and store the subsequent query result in the $statement variable.
			$statement = $user->login();
    
			try
			{
				// If the email/password combination of the user exists in the database, then $statement will have at least one row.
				// If it has zero rows, then that email/password combination does not belong to a registered user.
				if($statement->rowCount() == 1)
				{
					// Since the attempted login was a success, create a new session for the user.
					// See: http://php.net/manual/en/function.session-start.php
					session_start();
            
					// Set the fetch mode to an associative array.
					$statement->setFetchMode(PDO::FETCH_ASSOC);
            
					// Get the query result row from the statement as an associative array.
					$row = $statement->fetch();
            
					// Set the key "logged_in_user_email" in the \$_SESSION superglobal to the email of the newly logged-in user.
					// When another page is loaded by this logged-in user, the \$_SESSION superglobal will still the user's email as the value for its "logged_in_user_email" key.
					// This is what we use to identify if the user is logged in or not in separate files (like in the get_request_data.php file).
					$_SESSION['logged_in_user_email'] = $row['email'];
					$_SESSION['isAdministrator'] = $row['isAdministrator'];
					$_SESSION['isTeacher'] = $row['isTeacher'];
            
					// Construct a associative array (by calling the createLoginResponse() function) with keys that
					// match the LoginResponse class in the Xamarin application (see LoginResponse.cs for the class definition).
					// This array is for a successful login attempt.                
					$login_response = createLoginResponse(true, "Login successful: session created.", $row['email']);
				}
        
				// Create a "failed login" array.
				else if ($statement->rowCount() == 0)
				{        
					$login_response = createLoginResponse(false, "Login unsuccessful: invalid email and password combination.");
				}
        
				// Create a "failed login" array.
				else if ($statement->rowCount() > 1)
				{       
					$login_response = createLoginResponse(false, "Login unsuccessful: more than one user with the same email and password combination!");
				}
        
				// Create a "failed login" array.
				else if ($statement->rowCount() < 0)
				{       
					$login_response = createLoginResponse(false, "Login unsuccessful: somehow the statement's rowCount() was less than 0 ... this should never happen.");
				}
			}
    
			catch (Exception $e)
			{
				echo $e->getMessage().PHP_EOL;
			}
    
			// Send the JSON encoded login response associative array back to the Xamarin application.
			try
			{
				return json_encode($login_response);
			}
    
			catch(PDOException $e)
			{
				echo $e->getMessage().PHP_EOL;
			}
		}

?>