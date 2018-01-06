<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$user = $_POST["user"];
$pass = $_POST["pass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT * FROM UserLogin WHERE UserName = '".$user."'";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

if ($num_rows > 0) {
  echo "EXISTS";
}
else {
  echo "0";
}
$conn->close();
?>