<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$user = $_POST["user"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "UPDATE UsersOnline SET OnlineTimer = 2 WHERE UserName = '".$user."'";
$result = $conn->query($sql);

$conn->close();
?>