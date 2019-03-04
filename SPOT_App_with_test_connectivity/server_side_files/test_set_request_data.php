<?php    
    include_once '../application_files/database.php';
    include_once '../application_files/user.php';     
    
    $database = new Database();
    $databaseConnection = $database->getConnection();     
    
    $user = new User($databaseConnection);
    
    //echo "var_dump($_POST)".PHP_EOL;
    
    //var_dump($_POST);
    
    //$user->username = isset($_GET['username']) ? $_GET['username'] : die();
    //$user->password = base64_encode(isset($_GET['password']) ? $_GET['password'] : die());
    
    // TESTING
    //echo $_POST["username"].",".$_POST["password"].",".$_POST["email"];
    
    $statement_results = $user->set_request_data();
    
    
    foreach ($_POST as $key => $value)
    {
        echo $key." => ";

        if (is_array($value))
        {
            foreach ($value as $arrayItem)
            {
                echo $arrayItem.PHP_EOL;
            }
        }
        
        else
        {
            echo $value.PHP_EOL;
        }
        
        echo PHP_EOL;
    }
        
    
    function echoKeyValuePair($key, $value)
    {
        if (is_array($value))
        {
            foreach ($value as $subItem)
            {
                echoKeyValuePair($key, $subItem);
            }
        }
        
        else
        {      
            echo $key.":".$value.PHP_EOL;
        }
    } 

    function echoContentsWithEOL($data)
    {
        if (is_array($data))
        {
            foreach ($data as $subItem)
            {
                echoContents($subItem);
            }
        }
        
        else
        {      
            echo $data.PHP_EOL;
        }
    }    
    
    function echoContents($data)
    {
        if (is_array($data))
        {
            foreach ($data as $subItem)
            {
                echoContents($subItem);
            }
        }
        
        else
        {      
            echo $data;
        }
    }
    
    function cleanseInput(&$data)
    {
        if (is_array($data))
        {
            foreach ($data as &$subItem)
            {
                $subItem = cleanseInput($subItem);
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
    
    
    //return $statement_results->fetchAll(); 
?>