<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$userName = $_POST["user"];
$score = $_POST["score"];
$course = $_POST["course"];
$verification = $_POST["vfc"];

if($verification == "QWERTYTREWQ"){

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT * FROM Highscore WHERE Username = '".$userName."' AND Course = '".$course."'";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

if ($num_rows < 1) {
  //user doesn't have a high score yet
  //ADD HIGH SCORE
  $sql = "INSERT INTO Highscore (Username, Score, Course)
  VALUES ('".$userName."', '".$score."','".$course."')";

  if ($conn->query($sql) === TRUE){
    echo "1";
  }
}
else {
  while($row = $result->fetch_assoc()){
    if($row["Score"] > $score){
      $sql = "UPDATE Highscore SET Score='".$score."' WHERE Username='".$userName."' AND Course='".$course."'";
      if ($conn->query($sql) === TRUE){
        echo "2";
      }
    }
  }
}
$conn->close();
}
?>