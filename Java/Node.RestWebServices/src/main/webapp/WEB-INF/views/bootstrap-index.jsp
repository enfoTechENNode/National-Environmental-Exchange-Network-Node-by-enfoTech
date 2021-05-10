<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ page session="false"%>
<!DOCTYPE html>
<html lang="en">
<head>
	<title>Index</title>
	<!--<meta name="viewport" content="width=device-width, initial-scale=1.0"> -->
	<!--<link href="/RESTServices/resources/bootstrap/css/bootstrap-responsive.css" rel="stylesheet"> -->
	<link rel="stylesheet" type="text/css" href="/RESTServices/resources/bootstrap/css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="/RESTServices/resources/css/docs.css">
	<link rel="stylesheet" type="text/css" href="/RESTServices/resources/css/core.css" />
	<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
	<script src="http://code.jquery.com/jquery-1.10.2.js"></script>
	<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
	<script type="text/javascript" src="/RESTServices/resources/script/app/page/bootstrap-editor.js"></script>
</head>
<body>

	<!-- Navbar
    ================================================== -->
	<div class="navbar navbar-inverse navbar-fixed-top">
		<div class="navbar-inner">
			<div class="container-fluid">
				<button type="button" class="btn btn-navbar" data-toggle="collapse"
					data-target=".nav-collapse">
					<span class="icon-bar"></span> <span class="icon-bar"></span> <span
						class="icon-bar"></span>
				</button>
				<a class="brand" href="./index">enfoTech EnNode Restful Web Services</a>
				<div class="nav-collapse collapse">
					<ul class="nav">
						<li class="active"><a href="./index">Home</a></li>
					</ul>
				</div>
			</div>
		</div>
	</div>

	<div class="container-fluid">
		<div class="row-fluid">
			<div class="span3">
				<div class="well sidebar-nav">
					<ul class="nav nav-list">
						<li class="nav-header">AQDE</li>
						<li id="Li_AQSMonitoring_v2"><a href="#">AQSMonitoring_v2</a></li>
						<li id="Li_AQDERawdata_v2"><a href="#">AQDERawdata_v2</a></li>
						<li><a href="#">Link</a></li>
						<li><a href="#">Link</a></li>
						<li class="nav-header">WQX</li>
						<li><a href="#">WQDEQURY</a></li>
						<li><a href="#">Link</a></li>
						<li><a href="#">Link</a></li>
						<li><a href="#">Link</a></li>
						<li><a href="#">Link</a></li>
						<li><a href="#">Link</a></li>
						<li class="nav-header">RCRA</li>
						<li><a href="#">RCRAv1</a></li>
					</ul>
				</div>
				<!--/.well -->
			</div>
			<!--/span-->
			<div class="span9">
				<!--  Content -->
				<div class="hero-unit">
					<h3>Introduction</h3>
					<p>This is a template for enfoTech EnNode Restful WebService</p>
				</div>
				<div class="column">

					<div id="portlet-AQSMonitoring_v2" class="portlet">
						<div class="portlet-header">AQSMonitoring_v2</div>
						<div class="portlet-content">
							<div class="row-fluid">
								<div class="span8">
									<div id="AQSMonitoring_v2">hhhh</div>
								</div>
							</div>
						</div>
					</div>

					<div id="portlet-AQDERawdata_v2" class="portlet">
						<div class="portlet-header">AQDERawdata_v2</div>
						<div class="portlet-content">
							<div class="row-fluid">
								<div class="span8">
									<div id="AQDERawdata_v2">kkk</div>
								</div>
							</div>
						</div>
					</div>

				</div>
				<br/>
			</div>
			<!--/span-->
		</div>
		<!--/row-->

	</div>
	<!--/.fluid-container-->
</body>
</html>
