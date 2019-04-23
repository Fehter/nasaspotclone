<?php
	//Check if user previously submitted a request on this session. 
	//Returns false if the user has not logged in to get a session yet,
	//meaning no session exists.
	if(isset($_SESSION['has_submitted']))
	{
		
		$_SESSION['has_submitted'] = FALSE;
		
	}
	if (isset($_POST['email']) && isset($_POST['password']))
	{	
	
		$email = $_POST['email'];
		$password = $_POST['password'];
		$url = "http://localhost/server_side_files/application_files/database.php";

		$fields = array(
			'email' => $email,
			'password' => $password,
		);

		$postvars = http_build_query($fields);

		$ch = curl_init();

		// set the url, number of POST vars, POST data
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_POST, TRUE);
		curl_setopt($ch, CURLOPT_POSTFIELDS, $postvars);
		//curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);

		$result = curl_exec($ch);
		curl_close($ch);
		$result = json_decode($result, true);
		if($result['Status'] == true)
		{
			session_start();
			$_SESSION['logged_in_user_email'] = $result['Email'];
			$_SESSION['has_submitted']= FALSE; 
			include 'request_form.html';
					
		}
				
		else
		{
			
			echo getValue("Username")."<br>";
			echo getValue("Password")."<br>";
			echo "The login information was incorrect. Please try again.";
			echo $result;
					
		}
		
		
		
	}
	
	function getValue($key)
		{
			$ini_array = parse_ini_file("../application_files/database.properties");
			$value =  $ini_array[$key];
			return $value;
		}

?>