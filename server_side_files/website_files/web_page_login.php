<!doctype html>
<html>

    <head>
        <link rel="stylesheet" type="text/css" href="style.css">
        <script type="text/javascript" src="jquery-3.3.1.min.js"></script>
    
        <title>2018-2019 SPOT Login</title>

		<h1><center><font color=#000000><i>2018-2019 Presentation Request Form</i></font></center></h1>
    </head>

    <body>
	
		<?php
			$displayForm = TRUE;
			
			//Check if user previously submitted a request on this session. 
			//Returns false if the user has not logged in to get a session yet,
			//meaning no session exists.
			if(isset($_SESSION['has_submitted']))
			{
				
				$_SESSION['has_submitted'] = FALSE;
				
			}
			
			//checks if user has attempted to login yet. If they haven't, 
			//show them the form to log in. if they have, show them the
			//request form
			if(isset($_POST['submit']))
			{
				$displayForm = FALSE;
			}
			
			if($displayForm)
			{
				//This seems like a super Jank work around. Maybe try to improve? perhaps seperate php and html to seperate files?
				?>
				<div class="requestFormContainer">
				
					<form action="web_page_login.php" id="loginFormID" class="requestFormClass" method="post">
						<p>Welcome! Please login to continue.</p>
            
					<label for="email">Email</label>
						<input type="text" id="email" name="email" placeholder="example@gmail.com" required>
	
						<label for="password">Password</label>
						<input type="password" id="password" name="password" placeholder="" required>
						<input type="submit" name ="submit" value="Login">
					</form>
				</div>
				<?php
			}
			
			if (isset($_POST['email']) && isset($_POST['password']))
			{	
			
				$email = $_POST['email'];
				$password = $_POST['password'];
		
				$url = "http://localhost/Test/server_side_files/application_files/login.php";

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
				curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);

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
					
					echo "The login information was incorrect. Please try again.";
					
				}
		
			}

		?>
	
	</body>
</html>