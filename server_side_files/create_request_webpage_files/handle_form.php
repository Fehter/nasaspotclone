<html>
    <head>
        <!-- <dbConnection rel="stylesheet" type="text/css" href="style_02.css"> -->
        <!-- <script type="text/javascript" src="jquery-3.3.1.min.js"></script> -->
    
        <title>Handle Request Page</title>

        <h1><center><font color=#000000><i>Handle Request Page</i></font></center></h1>
    </head>
    <body> 
        <?php
        //print_r($_POST);
        //var_dump($_POST); // Prints more detailed information on what is in the POST than print_r

        //echo $_POST["name"];        
       
        //if ($_SERVER["REQUEST_METHOD"] == "POST")
            //echo "POST REQUEST METHOD<br><br>";        
        
        //echo "<br>-------------------------------------------------------------------------------------------------------------<br>";
        //echo "POST contents from create request form as follows:";
        //echo "<br>-------------------------------------------------------------------------------------------------------------<br>";
        
        $firstName = $lastName = $orgName = $email = $primaryPhoneNumber = $contactTimes = "";
        $streetAddress = $city = $state = $zip = $requestedPresentation = $requestedHandsOnActivity = $gradeLevels = "";
        $numStudents = $date = $time = $mySQLDateTimeFormat = $days = $supplies = $canPayFee = $ambassadorStatus = $concerns = $method = "";
        $concatRequestedPresentations = $concatRequestedHandsOnActivity = $concatDays = $concatSupplies = $concatMethod = "";
		$numberOfPresentations = $preparedStatement = $time_date_created = "";
		
        // Loop through POST contents and echo everything.
        foreach ($_POST as $key => $value)
        {
            echo $key.":<br>";

            if (is_array($value))
            {
                foreach ($value as $arrayItem)
                {
					
                    echo $arrayItem."<br>";
                
				}
            }
            
            else
            {
                echo $value."<br>";
            }
            
            echo "----<br>";
        }
        
        echo "<br>-------------------------------------------------------------------------------------------------------------<br>";

        if ($_SERVER["REQUEST_METHOD"] == "POST")
        {
            $firstName = test_input($_POST["firstName"]);
			$lastName = test_input($_POST["lastName"]);
            $orgName = test_input($_POST["orgName"]);
            $email = test_input($_POST["email"]);
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
        
        //echo "<br>";
        //echo $firstName."<br>";
		//echo $lastName."<br>";
        //echo $orgName."<br>";
        //echo $email."<br>";
        //echo $primaryPhoneNumber."<br>";
        //echo $contactTimes."<br>";
        //echo $streetAddress."<br>";
		//echo $city."<br>";
		//echo $state."<br>";
		//echo $zip."<br>";
		
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
        
		if(!(empty($requestedPresentation)))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateStringSubitems($requestedPresentation, $concatRequestedPresentations);
		}
		
		else
		{
			$concatRequestedPresentations = "Any Presentation.";
		}
		
		//echo "**************************<br>".$concatRequestedPresentations."<br>";
        
		if(!(empty($requestedHandsOnActivity)))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateStringSubitems($requestedHandsOnActivity, $concatRequestedHandsOnActivity);
		}
		
		else
		{
			$concatRequestedHandsOnActivity = "No hands-on component requested.";
		}
		
		//echo $concatRequestedHandsOnActivity."<br>";
		
       // echo $gradeLevels."<br>";
        //echo $numStudents."<br>";
       // echo $date."<br>";
       // echo $time."<br>";
		
		$mySQLDateTimeFormat .= $date;
		$mySQLDateTimeFormat .= " ";
		$mySQLDateTimeFormat .= $time;
		$mySQLDateTimeFormat .= ":00";
		
		//echo $mySQLDateTimeFormat."<br>";
        
		if(!(empty($days)))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateCommaSeperatedSubitems($days, $concatDays);
        }
		
		//if days is empty, every day is a preferred day.
		else
		{
			$concatDays = "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday";	
		}
		
		//echo $concatDays."<br>";
		
		if(!(empty($supplies)) && (!(trim(end($supplies)) == "")))//if not empty. empty() returns true if the variable is empty.
		{
			
			
				concatenateStringSubitems($supplies, $concatSupplies);
			
		}
		
		else
		{
			$concatSupplies = "No supplies will be provided.";
		}
		
		//echo $concatSupplies."<br>";

        //echo "can pay fee: ".$canPayFee."<br>";
        //echo $ambassadorStatus."<br>";
        
		if(trim($concerns) == "")
		{
			$concerns = "No Concerns Reported.";
		}
		
		//echo $concerns."<br>";
        
		if(!(empty($method)) && (!(trim(end($method)) == "")))//if not empty. empty() returns true if the variable is empty.
		{
			concatenateStringSubitems($method, $concatMethod);
		}
		
		else
		{
			$concatMethod = "No alternative presentation methods requested.";
		}
		
		//echo $concatMethod."<br>";

        //echo "<br>-------------------------------------------------------------------------------------------------------------<br><br>";

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
        
        //phpinfo(); // Prints a LOT of information about the current PHP installation on the Apache server.

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

        else
            echo "Successfully created connection to SQL database<br><br>";

       // if (mysqli_select_db($dbConnection, $dbName))
           // echo "Successfully set default database to: ".$dbName."<br><br>";
	   
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
		
		//mysqli_query($dbConnection, "INSERT INTO test_user_table (username, password, extra_info) VALUES ('testusername_02', 'testpassword_02', 'testextra_info_02')");
		
		
		$preparedStatement->close();
        mysqli_close($dbConnection)
        ?>
    </body>
</html>