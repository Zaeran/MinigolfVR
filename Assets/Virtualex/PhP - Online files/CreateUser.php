<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$user = $_POST["user"];
$pass = $_POST["pass"];
$email = $_POST["email"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$hpass = password_hash($pass, PASSWORD_DEFAULT);

$sql = "INSERT INTO UserLogin (UserName, Password, Email)
VALUES ('".$user."', '".$hpass."', '".$email."')";

if ($conn->query($sql) === TRUE) {
    echo "ADDED";
} else {
    echo "FAIL";
}

$conn->close();
?>