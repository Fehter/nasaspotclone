<?php
class User
{ 
    // The following variables are the properties of the User class.
    
    // This variable specifies a connection to an SQL database.
    // It is set when the User class is constructed (the constructor requires that a connection is passed to it).
    private $connection;
    
    // This variable stores the name of the SQL database table that holds user credentials.
    private $user_table_name = "users";
    
    // This variable stores the name of the SQL database view that holds presentation information.
    private $presentations_view_name = "getallpresentations";
    
    // The following two variables will contain the email/password combination of this user.
    // I suppose we will need to encrypt these somehow.
    public $email;
    private $password;

    // The constructor for this User class.
    public function __construct($databaseConnection)
    {
        $this->connection = $databaseConnection;
    }
    
    // This function gets data from a test table on an SQL database I set up.
    // This function returns this data.
    function test_getData()
    {
        // Define the query that should be executed on the database.
        // This query just gets all of the rows from my "test_user_table".
        $query = "SELECT * FROM mysql_database_01.test_user_table";
        
        try
        {        
            // Prepare the query by calling the "prepare()" function of the PDO object "connection".
            // This prepare() function returns an executable "statement" object.
            $statement = $this->connection->prepare($query);
        
            // Execute the statement -- the results/database rows are stored in the same "$statement" variable.
            // This is what actually causes the query to be run on the database.
            $statement->execute();
            
            // Set the "fetch mode" so that, when the fetch() function is called, it will return an associative array.
            // What this means is that, if we call fetchAll(), it will return an array of key value pairs where the key will match the column name in the database.
            // The value(s) of a corresponding key are then defined by the values in the row cells corresponding to that column.
            $statement->setFetchMode(PDO::FETCH_ASSOC);
            
            echo "PHP web service: user.php: test_getData(): Successfully got data".PHP_EOL;
        }
        
        catch(PDOException $e)
        {
            echo "PHP web service: user.php: test_getData(): Failed to get data" . $e->getMessage().PHP_EOL;
        }
        
        // Return the results of the query to the file that called this function.
        return $statement;            
    }
    
    // This function will insert data from the POST associative array superglobal (superglobal means that you can access its contents from any scope).
    function test_setData()
    {
        // Define the query that should be executed on the database.
        // NOTE: the "." indicates a concatenation -- I am concatenating values from the POST superglobal into the query string.
        $query = "INSERT INTO test_user_table (username, password, extra_info) VALUES ('".$_POST["Name"]."', '".$_POST["OrganizationName"]."', '".$_POST["Email"]."')";

        try
        {
            // Prepare the query by calling the "prepare()" function of the PDO object "connection".
            // This prepare() function returns an executable "statement" object.
            $statement = $this->connection->prepare($query);        

            // Execute the statement -- this causes the values from the POST to be inserted into the SQL database.
            // To be sure that this statement actually did something, you should look at the database table you attempted to insert data into.
            $statement->execute();
            
            echo "PHP web service: user.php: test_setData(): Successfully set data".PHP_EOL;
        }
        
        catch(PDOException $e)
        {
            echo "PHP web service: user.php: test_setData(): Failed to set data" . $e->getMessage().PHP_EOL;
        }
    }
    
    function getRequestData($maxNumRowsToGet, $startRowOffset)
    {        
        // Get data from the starting row (where starting row = row 0 + the value stored in $startRowOffset) and limit the number of rows returned by the value in $maxNumRowsToGet.
        $query = "SELECT * FROM spot.getallpresentations LIMIT ".$maxNumRowsToGet." OFFSET ".$startRowOffset;
        
        try
        {           
            // Prepare and execute the query.
            // The query results are stored in the $statement variable.
            $statement = $this->connection->prepare($query);        
            
            $statement->execute();      
            
            //echo "PHP web service: user.php: getRequestData(): Successfully got data".PHP_EOL;
        }
        
        catch(PDOException $e)
        {
            echo "PHP web service: user.php: getRequestData(): Failed to get data" . $e->getMessage().PHP_EOL;
        }       
        
        return $statement;    
    }
        
    // This function checks (via a query to the SQL database) whether the email/password combination contained in this User instance exists in the SQL spot.users table.
    // It will return the query results to the PHP file that called this function.
    function login()
    {        
        $query = "SELECT email, password FROM spot.".$this->user_table_name." WHERE email='".$this->email."' AND password='".$this->password."'";
        // The above query is equivalent to this following example query assuming email == "testuseremail1@test.com" and password == "password1":
        // SELECT email, password FROM spot.users WHERE email='testuseremail1@test.com' AND password='password1'
        
        try
        {           
            // Prepare and execute the query.
            // The query results are stored in the $statement variable.
            $statement = $this->connection->prepare($query);        
            
            $statement->execute();
        }
        
        catch(PDOException $e)
        {
            echo "PHP web service: user.php: login(): PDOException: " . $e->getMessage().PHP_EOL;
        }  
        
        return $statement;
    }  

    function setPassword($password)
    {
        $this->password = $password;
    }    
	
	//This function will query the database for the remaining user information, and return the query results
	
	function getUserData()
    {        
        $query = "SELECT email,first_name,last_name,phone_number,IsAmbassador,isTeacher,isAdministrator FROM spot.users WHERE email='".$this->email."'";
        // The above query is equivalent to this following example query assuming email == "testuseremail1@test.com" and password == "password1":
        // SELECT email,first_name,last_name,phone_number,IsAmbassador,isPresenter,isAdministrator WHERE email=$_SESSION['logged_in_user_email']
        
        try
        {           
            // Prepare and execute the query.
            // The query results are stored in the $statement variable.
            $statement = $this->connection->prepare($query);        
            
            $statement->execute();
        }
        
        catch(PDOException $e)
        {
            //echo "PHP web service: user.php: login(): PDOException: " . $e->getMessage().PHP_EOL;
        }  
        
        return $statement;
    }  
	function addUser($email, $password, $firstName, $lastName, $phoneNumber, $isAmbassador, $isTeacher, $isAdmin)
	{
		// Define the query that should be executed on the database.
        // NOTE: the "." indicates a concatenation -- I am concatenating values from the POST superglobal into the query string.
		try
        {
            // Prepare the query by calling the "prepare()" function of the PDO object "connection".
            // This prepare() function returns an executable "statement" object.
			$preparedStatement = $this->connection->prepare("INSERT INTO spot.users (`email`, `password`, `first_name`, `last_name`, `phone_number`, `isAmbassador`, `isTeacher`, `isAdministrator`) VALUES (?, ?, ?, ?, ?, ?,?, ?)");
				
			$preparedStatement->bindParam(1, $email, PDO::PARAM_STR);
			$preparedStatement->bindParam(2, $password, PDO::PARAM_STR);
			$preparedStatement->bindParam(3, $firstName, PDO::PARAM_STR);
			$preparedStatement->bindParam(4, $lastName, PDO::PARAM_STR);
			$preparedStatement->bindParam(5, $phoneNumber, PDO::PARAM_STR);
			$preparedStatement->bindParam(6, $isAmbassador, PDO::PARAM_INT);
			$preparedStatement->bindParam(7, $isTeacher, PDO::PARAM_INT);
			$preparedStatement->bindParam(8, $isAdmin, PDO::PARAM_INT);
				
            // Execute the statement -- this causes the values from the POST to be inserted into the SQL database.
            // To be sure that this statement actually did something, you should look at the database table you attempted to insert data into.
            $preparedStatement->execute();
			$preparedStatement->closeCursor();
			//echo "User has been created";
           return "Success";
        }
        
        catch(PDOException $e)
        {
          //  echo "PHP web service: user.php: addUser(): Failed to add user" . $e->getMessage().PHP_EOL;
			return "Failed";
        }
	}
}
?>