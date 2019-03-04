<?php
    class User
    { 
        // database connection and table name
        private $conn;
        private $table_name = "users";
     
        // object properties
        //public $id;
        public $email;
        public $password;
        //public $created;
     
        // constructor with $db as database connection
        public function __construct($databaseConnection)
        {
            $this->conn = $databaseConnection;
        }
        
        function get_request_data()
        {
            $query = "SELECT * FROM mysql_database_01.test_user_table";
            
            $statement = $this->conn->prepare($query);
        
            $statement->execute();
             
            $statement->setFetchMode(PDO::FETCH_ASSOC); 
            
            /*
            foreach($statement->fetchAll() as $key => $value)
            {     
                print_r($value);
            }
            */
            
            return $statement;            
        }
        
        function set_request_data()
        {
            $query = "INSERT INTO test_user_table (username, password, extra_info) VALUES ('".$_POST["Name"]."', '".$_POST["OrganizationName"]."', '".$_POST["Email"]."')";
            
            //$query = "INSERT INTO test_user_table (username, password, extra_info) VALUES ('sdfasdfase_02', 'asdfasdftest_02', 'asdfasdfto_02')";
            
            $statement = $this->conn->prepare($query);
        
            //$statement->execute();
            
            try
            {
                $statement->execute();
                
                echo "set_request_data() was successful".PHP_EOL;
            }
            
            catch(PDOException $e)
            {
                echo "set_request_data() FAILED" . $e->getMessage().PHP_EOL;
            }
        }
                
        // signup user
        function signup()
        {        
            if($this->isAlreadyExist())
            {
                return false;
            }
            // query to insert record
            $query = "INSERT INTO
                        " . $this->table_name . "
                    SET
                        username=:username, password=:password, created=:created";
        
            // prepare query
            $stmt = $this->conn->prepare($query);
        
            // sanitize
            $this->username=htmlspecialchars(strip_tags($this->username));
            $this->password=htmlspecialchars(strip_tags($this->password));
            $this->created=htmlspecialchars(strip_tags($this->created));
        
            // bind values
            $stmt->bindParam(":username", $this->username);
            $stmt->bindParam(":password", $this->password);
            $stmt->bindParam(":created", $this->created);
        
            // execute query
            if($stmt->execute())
            {
                $this->id = $this->conn->lastInsertId();
                return true;
            }
        
            return false;            
        }
        
        // login user
        function login()
        {
            // select all query
            $query = "SELECT
                        `id`, `username`, `password`, `created`
                    FROM
                        " . $this->table_name . " 
                    WHERE
                        username='".$this->username."' AND password='".$this->password."'";
                        
            // prepare query statement
            $stmt = $this->conn->prepare($query);
            
            // execute query
            $stmt->execute();
            
            return $stmt;
        }
        
        function isAlreadyExist()
        {
            $query = "SELECT *
                FROM
                    " . $this->table_name . " 
                WHERE
                    username='".$this->username."'";
            
            // prepare query statement
            $stmt = $this->conn->prepare($query);
            
            // execute query
            $stmt->execute();
            
            if($stmt->rowCount() > 0)
            {
                return true;
            }
            
            else
            {
                return false;
            }
        }
    }
?>