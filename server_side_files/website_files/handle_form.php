<html>
    <head>
		<link rel="stylesheet" type="text/css" href="style.css">
        <title>Handle Request Page</title>

        <h1><center><font color=#000000><i>Thank you for requesting a presentation!</i></font></center></h1>
    </head>
    <body> 
        <?php
        
        $email = $orgName = $primaryPhoneNumber = $contactTimes = "";
        $streetAddress = $city = $state = $zip = $requestedPresentation = $requestedHandsOnActivity = $gradeLevels = "";
        $numStudents = $date = $time = $mySQLDateTimeFormat = $days = $supplies = $canPayFee = $ambassadorStatus = $concerns = $method = "";
        $concatRequestedPresentations = $concatRequestedHandsOnActivity = $concatDays = $concatSupplies = $concatMethod = "";
		$numberOfPresentations = $preparedStatement = $time_date_created = "";
	
		function concatenateStringSubitems($array, &$concatResult)
		{
			$arrayCount = count($array);
			for($i = 0; $i < $arrayCount; $i++)
			{
				
				$concatResult.=$array[$i];
				
				if($i == ($arrayCount - 1))
				{
					$concatResult.="\n";
				}
			}
		}
		
		function concatenateCommaSeperatedSubitems($array, &$concatResult)
		{
			$arrayCount = count($array);
			for($i = 0; $i < $arrayCount; $i++)
			{
				
				$concatResult.=$array[$i];
				
				if($i < ($arrayCount - 1))
				{
					$concatResult.=", ";
				}
			}
		}
		
		// Recursive function that trims whitespace, strips slashes, and converts html special characters.
        // This is used to validate user input for security.
        function test_input(&$data)
        {
            if (is_array($data))
            {
                foreach ($data as &$subItem)
                {
                    $subItem = test_input($subItem);
                }
            }
            
            else
            {           
                $data = trim($data);
                $data = stripslashes($data);
                $data = htmlspecialchars($data);
            }
            
            return $data;
        }
        
	
		session_start();
		
		$submittedBool = $_SESSION['has_submitted'];
		
		if(!$submittedBool)
		{
        if ($_SERVER["REQUEST_METHOD"] == "POST")
        {
			
			$email = $_SESSION['logged_in_user_email'];
            $orgName = test_input($_POST["orgName"]);
            $primaryPhoneNumber = test_input($_POST["primaryPhoneNumber"]);
            $contactTimes = test_input($_POST["contactTimes"]);
            $streetAddress = test_input($_POST["streetAddress"]);
			$city = test_input($_POST["city"]);
			$state = test_input($_POST["state"]);
			$zip = test_input($_POST["zip"]);
            $requestedPresentation = test_input($_POST["requestedPresentation"]);
			$numberOfPresentations = test_input($_POST["numberOfPresentations"]);
            $requestedHandsOnActivity = test_input($_POST["requestedHandsOnActivity"]);
            $gradeLevels = test_input($_POST["gradeLevels"]);
            $numStudents = test_input($_POST["numStudents"]);
            $date = test_input($_POST["date"]);
            $time = test_input($_POST["time"]);
            $days = test_input($_POST["days"]);
            $supplies = test_input($_POST["supplies"]);
            $canPayFee = test_input($_POST["canPayFee"]);
            $ambassadorStatus = test_input($_POST["ambassadorStatus"]);
            $concerns = test_input($_POST["concerns"]);
            $method = test_input($_POST["method"]);
        }

		if(!(empty($requestedPresentation)))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateStringSubitems($requestedPresentation, $concatRequestedPresentations);
		}
		
		else
		{
			$concatRequestedPresentations = "Any Presentation.";
		}
        
		if(!(empty($requestedHandsOnActivity)))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateStringSubitems($requestedHandsOnActivity, $concatRequestedHandsOnActivity);
		}
		
		else
		{
			$concatRequestedHandsOnActivity = "No hands-on component requested.";
		}
		
		$mySQLDateTimeFormat .= $date;
		$mySQLDateTimeFormat .= " ";
		$mySQLDateTimeFormat .= $time;
		$mySQLDateTimeFormat .= ":00";
        
		if(!(empty($days)))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateCommaSeperatedSubitems($days, $concatDays);
        }
		
		//if days is empty, every day is a preferred day.
		else
		{
			$concatDays = "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday";	
		}
		
		if(!(empty($supplies)) && (!(trim(end($supplies)) == "")))//if not empty. empty() returns true if the variable is empty.
		{
			
			
				concatenateStringSubitems($supplies, $concatSupplies);
			
		}
		
		else
		{
			$concatSupplies = "No supplies will be provided.";
		}
        
		if(trim($concerns) == "")
		{
			$concerns = "No Concerns Reported.";
		}
        
		if(!(empty($method)) && (!(trim(end($method)) == "")))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateStringSubitems($method, $concatMethod);
		}
		
		else
		{
			$concatMethod = "No alternative presentation methods requested.";
		}

        $server = "localhost";
        $user = "test1";
        $pass = "test1";
        $dbName = "spot";
        $dbConnection = new mysqli($server, $user, $pass, $dbName);

        // Check connection to database
        if ($dbConnection->connect_error) {
            die("Connection Failed: ".$dbConnection->connect_error);
            exit();
        }
	   
	   $time_date_created = date("Y-m-d H:i:s");
	   
        // The following queries insert serveral values into various columns on the "test_user_table" in the SQL database -- this is for debugging purposes.
        $preparedStatement = $dbConnection->prepare("INSERT INTO Presentations (teacher_email, organization_name, grade_level, number_of_presentations,".
							" number_of_students_per_presentation, subject_requested, concerns, proposed_time_date, time_date_created,".
							" organization_street_address, organization_zip, organization_city, organization_state,".
							" contact_times, hands_on_activities, can_pay_fee, legal_agreement, supplies, preferred_days)".
							"VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");
				
		$preparedStatement->bind_param("sssiissssssssssssss", $email, $orgName, $gradeLevels, $numberOfPresentations, $numStudents,
										$concatRequestedPresentations, $concerns, $mySQLDateTimeFormat, $time_date_created,
										$streetAddress, $zip, $city, $state, $contactTimes, $concatRequestedHandsOnActivity,
										$canPayFee, $ambassadorStatus, $concatSupplies, $concatDays);
     
		$preparedStatement->execute();
		
		echo $dbConnection->error;

		$preparedStatement->close();
        mysqli_close($dbConnection);
		$_SESSION['has_submitted'] = TRUE;
		}
		
		else
		{
			
			echo "You have already submitted this request.";
			
		}
        ?>
    </body>
</html>