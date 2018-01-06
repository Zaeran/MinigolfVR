<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$user = $_POST["user"];
$friend =$_POST["friend"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "DELETE FROM FriendList WHERE UserName = '".$user."' AND FriendName = '".$friend."'";
$result = $conn->query($sql);

$conn->close();
?>