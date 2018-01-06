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

$sql = "SET @row_num = 0";
$result = $conn->query($sql);

$sql = "SELECT * FROM(SELECT @row_num := @row_num + 1 as row_number, Username, Score, Course FROM Highscore ORDER BY Score) d WHERE Username = '".$userName."' AND Course = '".$course."'";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

//username field needed as well, otherwise it doesn't work -_-
while($row = $result->fetch_assoc()){
  echo $row["row_number"].",".$row["Username"];
}

$conn->close();
?>