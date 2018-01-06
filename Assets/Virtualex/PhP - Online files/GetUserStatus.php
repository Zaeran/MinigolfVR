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

$sql = "SELECT * FROM UsersOnline WHERE UserName = '".$user."'";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

if ($num_rows == 0) {
  echo "-OFFLINE";
}
else{
    while($row = $result->fetch_assoc()){
        echo $row["RoomName"];
    }
}
$conn->close();
?>