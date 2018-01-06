<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$roomName = $_POST["room"];
$user = $_POST["user"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

//update room
$sql = "UPDATE Rooms SET playerCount = playerCount - 1 WHERE Room = '".$roomName."'";
$result = $conn->query($sql);

//check if room is empty

$sql = "SELECT playerCount FROM Rooms WHERE Room = '".$roomName."'";
$result = $conn->query($sql);

$roomIsEmpty = false;

while($row = $result->fetch_assoc()){
    if($row["playerCount"] <= 0){
	$roomIsEmpty = true;	
    }
}

if($roomIsEmpty){
    $sql = "DELETE FROM Rooms WHERE Room = '".$roomName."'";
    $result = $conn->query($sql);
}

$sql = "UPDATE UsersOnline SET RoomName = '-' WHERE UserName = '".$user."'";
$result = $conn->query($sql);

$conn->close();
?>