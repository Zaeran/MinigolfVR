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

$sql = "INSERT INTO UsersOnline (UserName, RoomName)
VALUES ('".$user."', '-')";

if ($conn->query($sql) === TRUE) {
    echo "ADDED";
} else {
    echo "0";
}
$conn->close();
?>