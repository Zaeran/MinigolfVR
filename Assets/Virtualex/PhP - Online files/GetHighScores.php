<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$userName = $_POST["user"];
$course = $_POST["course"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT * FROM Highscore WHERE Course = '".$course."' ORDER BY Score Limit 10";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

while($row = $result->fetch_assoc()){
  echo $row["Username"].",".$row["Score"]."\n";
}

$conn->close();
?>