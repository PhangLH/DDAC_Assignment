<?php
	try
	{
		$connection = new PDO("sqlsrv:Server=ddacassignment2018dbserver.database.windows.net;Database=DDACAssignment2018_db", "ddacAdmin", "DdacAssignment2018");
		$connection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
		$connection->setAttribute(PDO::SQLSRV_ATTR_ENCODING, PDO::SQLSRV_ENCODING_SYSTEM);
	}catch (Exception $e)
	{
		echo $e->getMessage();
		die('Connection could not be established.<br />');
	}

	try
	{
		$sql = "UPDATE ShippingRequest SET sr_status = 'Sending' WHERE sr_status = 'Approved';";
		$query = $connection->prepare($sql);
		$query->execute();

		$result = $query->fetchAll(PDO::FETCH_ASSOC);
	}catch (Exception $e)
	{
		die('Cant fetch rows.');
	}

	foreach ($result as $r)
	{
		//print_r($r); // do what you want here
	}
?>