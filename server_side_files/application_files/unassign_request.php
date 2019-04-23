<?php
		
	$teacherEmail = $presentationID = "";
	
	if(isset($_POST["teacherEmail"]) && isset($_POST["presentationID"]))
	{
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
		
		$teacherEmail = $_POST["teacherEmail"];
		$presentationID = $_POST["presentationID"];
		
		$preparedStatement = $dbConnection->prepare("UPDATE `spot`.`presentations` SET `ambassador_email` = NULL WHERE (`presentationID` = ?) and (`teacher_email` = ?);");
				
		$preparedStatement->bind_param("is", $presentationID, $teacherEmail);
     
		$preparedStatement->execute();
		
		echo $dbConnection->error;

		$preparedStatement->close();
        mysqli_close($dbConnection);
		
	}
		
?>