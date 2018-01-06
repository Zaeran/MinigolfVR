<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT * FROM UsersOnline";
$result = $conn->query($sql);

while($row = $result->fetch_assoc()){
    echo $row["UserName"].",".$row["RoomName"]."\n";
}

$conn->close();
?>