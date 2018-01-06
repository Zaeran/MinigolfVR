<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$roomName = $_POST["room"];

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
  //room doesn't exist
  echo "ERROR1";
}
else {
  while($row = $result->fetch_assoc()){
    if($row["isLobby"] == 1){
      echo "YES";
    }
    else{
      echo "NO";
    }
  }
}

$conn->close();
?>