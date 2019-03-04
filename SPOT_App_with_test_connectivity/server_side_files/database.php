<?php
    class Database
    {
        private $server_name = "localhost";
        private $database_name = "mysql_database_01";
        private $username = "test1";
        private $password = "test1";
        public $conn;

        //$this->conn = new PDO("mysql:host=" . $this->server_name . ";dbname=" . $this->database_name, $this->username, $this->password);
        //$this->conn->exec("set names utf8");

        public function getConnection()
        {
            try 
            {
                $this->conn = new PDO("mysql:host=" . $this->server_name . ";dbname=" . $this->database_name, $this->username, $this->password);
                               
                $this->conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
                
                echo "Connected successfully".PHP_EOL; 
            }
            
            catch(PDOException $e)
            {
                echo "Connection failed: " . $e->getMessage().PHP_EOL;
            }
            
            return $this->conn;
        }
    }
?>