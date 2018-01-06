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

$sql = "SELECT * FROM Rooms WHERE Room = '".$roomName."'";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

if ($num_rows < 1) {
  $sql = "INSERT INTO Rooms (Room, playerCount, isLobby)
  VALUES ('".$roomName."', '1', '1')";
}
else {
  $sql = "UPDATE Rooms SET playerCount = playerCount + 1 WHERE Room = '".$roomName."'";
}

if ($conn->query($sql) === TRUE){
    $sql = "UPDATE UsersOnline SET RoomName = '".$roomName."' WHERE UserName = '".$user."'";
    if ($conn->query($sql) === TRUE){
      echo "VALID";
    }
    else{
      echo "1";
    }
}
else{
    echo "0";
}
echo "END";

$conn->close();
?>