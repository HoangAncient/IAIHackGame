<?php
    $servername = "localhost";
    $username = "root";
    $password = "";
    $dbname = "unitygame";


    $conn = new mysqli($servername, $username, $password, $dbname);

    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

    $sql = "SELECT * FROM questions JOIN MC2 on questions.QuestionID = MC2.QuestionID";

    $result = $conn->query($sql);

    if($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            if($row["QuestionID"] != "") {
                echo $row["Question"] . "," .$row["choice1"] . ",".$row["choice2"] . ",".$row["answer"]. "*";
            }
            else {
                echo "Failed";
            }
        }
    } else {
        echo "0 questions exist";
        
    }
    $conn->close();



?>