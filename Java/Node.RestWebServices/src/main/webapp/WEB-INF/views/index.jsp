<%@ page language="java" contentType="text/html;charset=utf-8" pageEncoding="ISO-8859-1" session="false"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html>
<html lang="en">
<head>
	<title>Index</title>
	<!--<meta name="viewport" content="width=device-width, initial-scale=1.0"> -->
	<!--<link href="/RESTServices/resources/bootstrap/css/bootstrap-responsive.css" rel="stylesheet"> -->
	<link rel="stylesheet" type="text/css" href="/RESTServices/resources/bootstrap/css/bootstrap.min.css">
<!--	<link rel="stylesheet" type="text/css" href="/RESTServices/resources/css/docs.css">-->
	<link rel="stylesheet" type="text/css" href="/RESTServices/resources/css/core.css" />
	<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
	<script src="http://code.jquery.com/jquery-1.10.2.js"></script>
	<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
	<script type="text/javascript" src="/RESTServices/resources/bootstrap/js/tooltip.js"></script>
	<script type="text/javascript" src="/RESTServices/resources/bootstrap/js/popover.js"></script>
	<script type="text/javascript" src="/RESTServices/resources/script/app/page/editor.js"></script>
</head>
<body>
	<!-- Navbar ================================================== -->
<!--	<div class="navbar navbar-inverse navbar-fixed-top">-->
<!--		<div class="navbar-inner">-->
<!--			<div class="container-fluid">-->
<!--				<button type="button" class="btn btn-navbar" data-toggle="collapse"-->
<!--					data-target=".nav-collapse">-->
<!--					<span class="icon-bar"></span> <span class="icon-bar"></span> <span-->
<!--						class="icon-bar"></span>-->
<!--				</button>-->
<!--				<a class="brand" href="./index">enfoTech EnNode RESTful Web Services</a>-->
<!--				<div class="nav-collapse collapse">-->
<!--					<ul class="nav">-->
<!--						<li class="active"><a href="./index">Home</a></li>-->
<!--					</ul>-->
<!--				</div>-->
<!--			</div>-->
<!--		</div>-->
<!--	</div>-->

	<!-- Header -->
	<div class="navbar navbar-inverse navbar-fixed-top">
		<div class="navbar-inner">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td colspan="2" bgcolor="#0055A6" height="58px" width="300px"><a href="http://www.enfotech.com/"><img src="/RESTServices/resources/images/node/header.gif" style="border-width:0px;" /></a></td>
				</tr>
				<tr class="HeaderText" valign="top" style="background-color: #396EA0">
					<td style="color: white; padding:0 0 0 10px;">Node RESTful Web Services</td>
					<td align="right"><a href="http://www.enfotech.com/" target="_blank" style="color:White; ">enfoTech & Consulting, Inc. Web Policy</a> - <a href="http://www.enfotech.com/enfoWebApp/pages/company/Contact.aspx" style="color:White;padding:0 10px 0 0;" target="_blank">Contact Us</a></td>
				</tr>
			</table>
		</div>
	</div>

	<div class="container-fluid">
		<div class="row-fluid">
			<div class="span3">
				<div class="well sidebar-nav">
						<c:forEach var="domain" items="${domainHt}">
							<c:set var="opList" value="${domain.value}"/>
							<li class="nav-header"><a href="#" class="parent" style="font-size: 120%; color: black;"><i class='icon-minus'></i>${domain.key}</a>
								<ul class="nav nav-list open" style="text-indent:7px;">
									<c:forEach var="operationHt" items="${opList}">
										<c:forEach items="${operationHt}" var="operationEntry">
											<li><a id="link_${operationEntry.value}" name="${operationEntry.key}" href="#">${operationEntry.value}</a></li>								
										</c:forEach>
									</c:forEach>
								</ul>
							</li>				
						</c:forEach>
				</div>
				<!--/.well -->
			</div>
			<!--/span-->
			<div class="span9">
				<!--  Content -->
				<div class="hero-unit">
					<h4>${introduction[0]}</h4>
					<p>${introduction[1]}</p>
				</div>
				<div class="portal-column">
					<c:forEach var="domain" items="${domainHt}">
						<c:set var="opList" value="${domain.value}"/>
						<c:forEach var="operationHt" items="${opList}">
							<c:forEach items="${operationHt}" var="operationEntry">
								<div id="portlet_${operationEntry.value}" class="portlet" style="display: none">
									<div class="portlet-header">${operationEntry.value}</div>
									<div class="portlet-content">
										<div class="row-fluid">
											<div class="span8">
												<div id="${operationEntry.value}"></div>
											</div>
										</div>
									</div>
								</div>
							</c:forEach>
						</c:forEach>							
					</c:forEach>
				</div>
			</div>
			<!--/span-->
		</div>
		<!--/row-->

	</div>
	<!--/.fluid-container-->
</body>
</html>
