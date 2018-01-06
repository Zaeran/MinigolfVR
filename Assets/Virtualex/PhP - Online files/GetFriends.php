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

$sql = "SELECT * FROM FriendList WHERE UserName = '".$user."'";
$result = $conn->query($sql);

while($row = $result->fetch_assoc()){
    echo $row["FriendName"].",";
}

$conn->close();
?>