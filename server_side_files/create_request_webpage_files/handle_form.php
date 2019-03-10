<html>
    <head>
        <!-- <link rel="stylesheet" type="text/css" href="style_02.css"> -->
        <!-- <script type="text/javascript" src="jquery-3.3.1.min.js"></script> -->
    
        <title>Handle Request Page</title>

        <h1><center><font color=#000000><i>Handle Request Page</i></font></center></h1>
    </head>
    <body> 
        <?php
        //print_r($_POST);
        //var_dump($_POST); // Prints more detailed information on what is in the POST than print_r

        //echo $_POST["name"];        
       
        if ($_SERVER["REQUEST_METHOD"] == "POST")
            echo "POST REQUEST METHOD<br><br>";        
        
        echo "<br>-------------------------------------------------------------------------------------------------------------<br>";
        echo "POST contents from create request form as follows:";
        echo "<br>-------------------------------------------------------------------------------------------------------------<br>";
        
        $name = $orgName = $email = $primaryPhoneNumber = $altPhoneNumber = $contactTimes = "";
        $schoolAddress = $requestedPresentation = $requestedHandsOnActivity = $gradeLevels = "";
        $numStudents = $date = $time = $days = $supplies = $canPayFee = $ambassadorStatus = $concerns = $method = "";
        
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
            $name = test_input($_POST["name"]);
            $orgName = test_input($_POST["orgName"]);
            $email = test_input($_POST["email"]);
            $primaryPhoneNumber = test_input($_POST["primaryPhoneNumber"]);
            $altPhoneNumber = test_input($_POST["altPhoneNumber"]);
            $contactTimes = test_input($_POST["contactTimes"]);
            $schoolAddress = test_input($_POST["schoolAddress"]);
            $requestedPresentation = test_input($_POST["requestedPresentation"]);
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
        
        echo "<br>";
        echo $name."<br>";
        echo $orgName."<br>";
        echo $email."<br>";
        echo $primaryPhoneNumber."<br>";
        echo $altPhoneNumber."<br>";
        echo $contactTimes."<br>";
        echo $schoolAddress."<br>";
        
        foreach ($requestedPresentation as $subItem)
        {
            echo $subItem."<br>";
        }
        
        foreach ($requestedHandsOnActivity as $subItem)
        {
            echo $subItem."<br>";
        }

        echo $gradeLevels."<br>";
        echo $numStudents."<br>";
        echo $date."<br>";
        echo $time."<br>";
        
        foreach ($days as $subItem)
        {
            echo $subItem."<br>";
        }
        
        foreach ($supplies as $subItem)
        {
            echo $subItem."<br>";
        }

        echo $canPayFee."<br>";
        echo $ambassadorStatus."<br>";
        echo $concerns."<br>";
        
        foreach ($method as $subItem)
        {
            echo $subItem."<br>";
        }

        echo "<br>-------------------------------------------------------------------------------------------------------------<br><br>";

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
        $dbName = "mysql_database_01";
        $link = mysqli_connect($server, $user, $pass) or die ("Can't connect to SQL server");

        // Check connection to database
        if (mysqli_connect_errno()) {
            printf("Connect failed: %s\n", mysqli_connect_error());
            exit();
        }

        else
            echo "Successfully created connection to SQL database<br><br>";

        if (mysqli_select_db($link, $dbName))
            echo "Successfully set default database to: ".$dbName."<br><br>";

        //$query_result = mysqli_query($link, "SELECT * FROM test_user_table");

        /*
        // The following queries insert serveral values into various columns on the "test_user_table" in the SQL database -- this is for debugging purposes.
        mysqli_query($link, "INSERT INTO test_user_table (username, password, extra_info) VALUES ('testusername_01', 'testpassword_01', 'testextra_info_01')");
        mysqli_query($link, "INSERT INTO test_user_table (username, password, extra_info) VALUES ('testusername_02', 'testpassword_02', 'testextra_info_02')");
        mysqli_query($link, "INSERT INTO test_user_table (username, password, extra_info) VALUES ('testusername_03', 'testpassword_03', 'testextra_info_03')");
        mysqli_query($link, "INSERT INTO test_user_table (username, password, extra_info) VALUES ('testusername_04', 'testpassword_04', 'testextra_info_04')");
        mysqli_query($link, "INSERT INTO test_user_table (username, password, extra_info) VALUES ('testusername_05', 'testpassword_05', 'testextra_info_05')");
        mysqli_query($link, "INSERT INTO test_user_table (username, password, extra_info) VALUES ('testusername_06', 'testpassword_06', 'testextra_info_06')");
        */

        echo "Some content from a query on ".$dbName.".test_user_table:<br>";
        $query_result = mysqli_query($link, "SELECT * FROM test_user_table");

        while($query_result_row = mysqli_fetch_array($query_result))
        {
            echo "- ";
            print_r($query_result_row);
            echo("<br>");
            //var_dump($query_result_row);
            //echo "<br>----<br>";
            
            /*
            foreach ($query_result_row as &$value)
            {
                var_dump($value);
                echo("<br>");
            }
            echo "<br>----<br>";
            */
        }

        echo "<br>";

        //var_dump($query_result);

        mysqli_close($link)
        ?>
    </body>
</html>