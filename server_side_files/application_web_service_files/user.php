<?php
class User
{ 
    // The following variables are the properties of the User class.
    
    // This variable specifies a connection to an SQL database.
    // It is set when the User class is constructed (the constructor requires that a connection is passed to it).
    private $connection;
    
    // The following two variables will contain the email/password combination of this user.
    // I suppose we will need to encrypt these somehow.
    public $email;
    public $password;

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
    
    function getRequestData($startRow, $endRow)
    {
        // Get data from rows $startRow to $endRow from the "getallpresentations" view of the "spot" database.
        $query = "SELECT * FROM spot.getallpresentations LIMIT ".$startRow.", ".$endRow;
        
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
            //echo "PHP web service: user.php: getRequestData(): Failed to get data" . $e->getMessage().PHP_EOL;
        }       
        
        return $statement;    
    }
}
?>