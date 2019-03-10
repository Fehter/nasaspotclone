<?php
class Database
{
    // The following variables are the properties of the Database class.
    // $server_name, $database_name, $username, and $password are used to connect to a specific SQL database with a specific connection (in this case the connection is "test1").
    private $server_name = "localhost";
    private $database_name = "mysql_database_01";
    private $username = "test1";
    private $password = "test1";
    
    // This data member will contain the connection to the SQL database.
    public $connection;

    // A function that attempts to create a connection to the SQL database.
    // If this function is successful, it will return the connection.
    public function getConnection()
    {
        try 
        {
            // Create the connection.
            $this->connection = new PDO("mysql:host=" . $this->server_name . ";dbname=" . $this->database_name, $this->username, $this->password);
            
            // Set ERRMODE attribute of the PDO such that, if something goes wrong, it will throw an Exception.
            $this->connection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            
            // Echo a message if the connection was successfully created.
            echo "PHP web service: database.php: getConnection(): Connected successfully".PHP_EOL; 
        }
        
        // Catch the Exception that is thrown if the connection creation was unsuccessful and echo an error message.
        // NOTE: the error message should be visible in the Debug console of Visual Studio.
        catch(PDOException $e)
        {
            echo "PHP web service: database.php: getConnection(): Connection failed: " . $e->getMessage().PHP_EOL;
        }
        
        return $this->connection;
    }
}
?>