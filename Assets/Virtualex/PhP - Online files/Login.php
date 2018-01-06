<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$user = $_POST["user"];
$pass = $_POST["pass"];
$validLogin = false;
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT UserName, Password FROM UserLogin WHERE UserName = '".$user."'";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

if ($num_rows > 0) {
  while($row = $result->fetch_assoc()){
    if(password_verify($pass, $row["Password"])){
	  $validLogin = true;
    }
    else{
      echo "1";
    }
  }
}
else {
  echo "0";
}

if($validLogin){
  $sql = "SELECT * FROM UsersOnline WHERE UserName = '".$user."'";
  $result = $conn->query($sql);
  if($result->num_rows == 0){
    $sql = "INSERT INTO UsersOnline (UserName, RoomName, OnlineTimer)
    VALUES ('".$user."', '-', 2)";

    if ($conn->query($sql) === TRUE) {
      echo "VALID";
    } else {
      echo "2";
    }
  }
}
$conn->close();
?>