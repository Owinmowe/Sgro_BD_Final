<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<title>Ejemplo1</title>
</head>
<body>

<?php
	$con = mysqli_connect("localhost","root","root","Ej1");

// Check that connection happened
	if(mysqli_connect_error())
	{
		echo "1: Connection Failed";													// Error code #1 = Error de coneccion.
		exit();
	}

// Variables
	$username = $POST["Username"];
	$score = $POST["Score"];

// Check if name exist
	$nameCheckQuery = "SELECT username FROM player WHERE Username = '".$username."';";
	$nameCheck = mysqli_query($con,$nameCheckQuery)or Die("2: Name Check Querry Failed"); // Error code #2 = Ya existe una variable con ese dato.

	if(mysqli_num_rows($nameCheck)>0)
	{
		echo "3: Name Already exist";													// Error code #3 = Nombre ya existente.
		exit();
	}


// Add user to table
	$insertUserQuerry = "INSERT INTO player (username,score)VALUES('".$username."','".$score."');";
	mysqli_query($con,$insertUserQuerry)or die("4: Insert Player Quert Failed");		// Error code #4 = Falló la suibida de los datos.

	echo("1");
?>
</body>
</html>