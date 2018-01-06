<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";
$user = $_POST["user"];
$friend = $_POST["friend"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT * FROM FriendList WHERE UserName = '".$user."' AND FriendName = '".$friend."'";
$result = $conn->query($sql);
$num_rows = $result->num_rows;

if ($num_rows > 0) {
  echo "EXISTS";
}
else {
    $sql = "INSERT INTO FriendList (UserName, FriendName) VALUES ('".$user."', '".$friend."')";
    if ($conn->query($sql) === TRUE) {
        echo "ADDED";
    } else {
        echo "FAIL";
    }
}

$conn->close();
?>