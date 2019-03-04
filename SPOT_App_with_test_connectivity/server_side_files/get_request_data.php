<?php    
    include_once '../application_files/database.php';
    include_once '../application_files/user.php';     
    
    $database = new Database();
    $databaseConnection = $database->getConnection();     
    
    $user = new User($databaseConnection);
    
    //$user->username = isset($_GET['username']) ? $_GET['username'] : die();
    //$user->password = base64_encode(isset($_GET['password']) ? $_GET['password'] : die());
    
    $statement_results = $user->get_request_data();
    
    // Note: when calling the echo function (or print_r or var_dump), the results of those functions are sent back to the *thing* that called this php page.
    // This means that since the Xamarin application called this file, whatever is echoed will be sent back to the Xamarin application.
    // So, by calling the echoKeyValuePair() function below, I am sending the key value pairs from the $statement_results variable back to the application.
    foreach($statement_results->fetchAll() as $key => $value)
    {   
        echoKeyValuePair($key, $value);
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