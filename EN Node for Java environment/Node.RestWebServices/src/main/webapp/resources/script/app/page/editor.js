	// Handle menu collapse
$(function() {
	$(window).load(function() {
		$('.parent').click(function() {
			var subMenu = $(this).siblings('ul');
			var subMenuOpenSymbol = $(this).find('i');
			if ($(subMenu).hasClass('open')) {
				$(subMenu).fadeOut();
				$(subMenu).removeClass('open').addClass('closed');
				$(subMenuOpenSymbol).removeClass('icon-minus').addClass('icon-plus');
			} else {
				$(subMenu).fadeIn();
				$(subMenu).removeClass('closed').addClass('open');
				$(subMenuOpenSymbol).removeClass('icon-plus').addClass('icon-minus');
			}
		});
	});
		
	// Handle portlet
	$(".portal-column").sortable({
		connectWith : ".portal-column"
	});

	$(".portlet")
			.addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
			.find(".portlet-header")
			.addClass("ui-widget-header ui-corner-all")
			.prepend("<span class='ui-icon ui-icon-close'></span><span class='ui-icon ui-icon-minusthick'></span>")
			.end().find(".portlet-content");

	$(".portlet-header .ui-icon-minusthick").click(
		function() {
			$(this).toggleClass("ui-icon-minusthick")
					.toggleClass("ui-icon-plusthick");
			
			$(this).parents(".portlet:first")
					.find(".portlet-content")
					.toggle();
	});
	
	$(".portlet-header .ui-icon-plusthick").click(
			function() {
				$(this).toggleClass("ui-icon-minusthick")
						.toggleClass("ui-icon-plusthick");
				
				$(this).parents(".portlet:first")
						.find(".portlet-content")
						.toggle();
	});
	$(".portlet-header .ui-icon-close").click(
		function() {
			$(this).parents(".portlet:first").hide();
	});

//	$( ".portal-column" ).disableSelection();

	// Get operation content
	$("a").click(function(event) {
		var id = event.target.id;
		var opID =  event.target.name;
		var url = document.URL;
		// Handle operation content
		if(id.indexOf('link_') != -1){
			var jid = '#'+id.substr(id.indexOf('link_')+5);
			var jportlet = '#portlet_'+id.substr(id.indexOf('link_')+5);
			var herowidth = $('.hero-unit').width();
			$(jid).width(herowidth-15);
			$(jportlet).width(herowidth);
			
			$.get("/RESTServices/getOperation?opID="+opID, function() {
				  //alert( "success" );
			}).done(function(data, textStatus, jqXHR) {
				//show content
				var format=data.opName+'_format';
				var rowId=data.opName+'_row';
				var maxRowId=data.opName+'_maxRow';
				var urlStr = url.substring(0,url.indexOf('RESTServices')+12) + '/Query?Dataflow='+data.domain+'&Request='+data.opName+'&RowId=0'+'&MaxRows=200&format=xml&Params='; 
				var paramStr='';
				var webParamsArr = data.webServiceParameters;
				
				for (var i in webParamsArr){
					var paramArr =webParamsArr[i];
					paramStr = paramStr +
'<tr><td id="'+data.opName+'_'+paramArr[0]+'">' + paramArr[1] + '</td>' + '<td><input id="'+ data.opName + '_' + paramArr[1]+'"' + 'type="text" /></td></tr>';
					urlStr = urlStr + paramArr[1] + '|;';
				};
				var htmlCnt = 	'<p><strong>Domain: '+data.domain+'</strong></p>'+
				'<div class="page-header">'+
					'<p>Service Name: '+data.opName+'</p>'+
				'</div>'+
				'<div class="row-fluid">'+
					'<span class="span9">'+
						'<div>Description: '+data.description+'</div>'+				
					'</span>'+
				'</div>'+
				'<hr/>'+
				'<div class="row-fluid">'+
					'<span class="span9">'+
						'<div>Parameters:</div>'+				
					'</span>'+
				'</div>'+
	    		'<div class="row-fluid">'+
					'<span class="span9">'+
						'<table class="table table-hover">'+
							'<thead>'+
								'<tr style="background-color:#98D9CD;">'+
									'<th>Parameter Name:</th>'+
									'<th>Parameter Value:</th>'+
								'<tr>'+
							'</thead>'+
							'<tbody>'+
								paramStr +
							'</tbody>'+
						'</table>'+
					'</span>'+
				'</div>'+
				'<hr/>'+
				'<div class="row-fluid">'+
					'<div class="span9">'+
						'Select Row: '+'<input id="'+ rowId + '"' + 'type="text" value="0" /> * Minimal is 0'+
						'<br />'+
					'</div>'+
				'</div>'+
				'<div class="row-fluid">'+
					'<div class="span9">'+
						'Select Maximum Rows to: '+'<input id="'+ maxRowId + '"' + 'type="text" value="200" /> * Set -1 to return all rows'+
						'<br />'+
					'</div>'+
				'</div>'+
				'<div class="row-fluid">'+
					'<div class="span9">'+
						'Select format: '+'<select id="'+ format + '" >'+
											'<option>xml</option>'+
											'<option>zip</option>'+
										  '</select>'+
						'<br />'+
					'</div>'+
				'</div>'+
				'<hr/>'+
				'<div class="row-fluid">'+
					'<div class="span9">'+
						'<p><strong>REST URL:</strong></p>'+
						'<textarea id="'+ data.opName + '_url"' + 'rows="10" class="field span12" onclick="this.style.borderColor = \'lightred\'; this.select();" style="cursor:pointer;" rel="popover" readonly>'+ urlStr+'</textarea>'+
						'<br />'+
					'</div>'+
				'</div>'+
    			'<button id="'+ data.opName + '_btn-invoke" class="btn btn-primary" type="button">Invoke</button>' +
    			'<br/>'+
    			'<br/>'+
    			'<p><a href="#">Top</a></p>';

		        $(jid).empty().html(htmlCnt);
		        // Change the current portlet position to first
		        var idsInOrder = $('.portal-column').sortable("toArray");
		        var index = $.inArray(jportlet.substring(1), idsInOrder);
			    if(index != 0){
			    	$(jportlet).insertBefore('[id^=portlet_]:first');
			    }
		        
		    	$(jportlet).show(); 			
//		    	$('textarea').popover({ title: 'Click and press CTRL+C to copy.', trigger: "hover" }); 
			  })
			  .fail(function() {
			    alert( "error" );
			  });			
		}
	});
	
//	$(".parent").prepend("<span class='icon-minusthick'></span>");
	
	// Handle the input event and change the url automatically
	$("body").on('keyup','input',function (event) {
		var inputId = event.target.id;
		var opName = inputId.substr(0,inputId.lastIndexOf('_'));
		var textAreaName = '#'+opName+'_url';
		$(textAreaName).val(changeUrlStr(event));
	});
	
	// Handle the select event and change the url automatically
	$("body").on('change','select',function (event) {
		var inputId = event.target.id;
		var opName = inputId.substr(0,inputId.lastIndexOf('_'));
		var textAreaName = '#'+opName+'_url';
		$(textAreaName).val(changeUrlStr(event));
	});

	$("body").on('click','button',function(event) {
		var inputId = event.target.id;
		var opName = inputId.substr(0,inputId.lastIndexOf('_'));
		var textAreaName = '#'+opName+'_url';
		window.open($(textAreaName).val());
		//$.get($(textAreaName).val());
	});

});

var CopyToClipboard = function (text) {
    Copied = text.createTextRange();
    Copied.execCommand("Copy");
};

var changeUrlStr = function(event){
	var ret='';
	if(event != null){
		var inputId = event.target.id;
		var opName = inputId.substr(0,inputId.lastIndexOf('_'));
		var paramName = inputId.substr(inputId.lastIndexOf('_')+1);
		var paramVal = $(event.target).val();
		var textAreaName = '#'+opName+'_url';
		var url = $(textAreaName).val();
		var formatName = 'format';
		var formatId = opName+'_format';
		var rowName = 'RowId';
		var rowId = opName+'_row';
		var maxRowName = 'MaxRows';
		var maxRowId = opName+'_maxRow';
		
		if(inputId != '' && inputId === rowId){ 		// handle row
			var indexRowStart = url.indexOf(rowName+'=');
			var urlSecond = url.substr(url.indexOf(rowName+'=')+rowName.length+1);
			var oldRowValue = urlSecond.substr(0,urlSecond.indexOf('&'));
			var indexRowEnd = indexRowStart+rowName.length+1;
			if(oldRowValue != ''){
				indexRowEnd = url.indexOf(rowName+'='+oldRowValue) + rowName.length+1 + oldRowValue.length;
			}
			ret = url.substr(0,indexRowStart+rowName.length+1) + paramVal + url.substr(indexRowEnd);
		}else if(inputId != '' && inputId === maxRowId){ 		// handle maxrow
			var indexMaxRowStart = url.indexOf(maxRowName+'=');
			var urlSecond = url.substr(url.indexOf(maxRowName+'=')+maxRowName.length+1);
			var oldMaxRowValue = urlSecond.substr(0,urlSecond.indexOf('&'));
			var indexMaxRowEnd = indexMaxRowStart+maxRowName.length+1;
			if(oldMaxRowValue != ''){
				indexMaxRowEnd = url.indexOf(maxRowName+'='+oldMaxRowValue) + maxRowName.length+1 + oldMaxRowValue.length;
			}
			ret = url.substr(0,indexMaxRowStart+maxRowName.length+1) + paramVal + url.substr(indexMaxRowEnd);
		}else if(inputId != '' && inputId === formatId){ 		// handle format
			var indexFormatStart = url.indexOf(formatName+'=');
			var urlSecond = url.substr(url.indexOf(formatName+'=')+formatName.length+1);
			var oldFormatValue = urlSecond.substr(0,urlSecond.indexOf('&'));
			var indexFormatEnd = indexFormatStart+formatName.length+1;
			if(oldFormatValue != ''){
				indexFormatEnd = url.indexOf(formatName+'='+oldFormatValue) + formatName.length+1 + oldFormatValue.length;
			}
			ret = url.substr(0,indexFormatStart+formatName.length+1) + paramVal + url.substr(indexFormatEnd);
		}else{		// handle parameters
			if(url.indexOf(paramName)!=-1){
				var indexParamStart = url.indexOf(paramName+'|');
				var urlSecond = url.substr(url.indexOf(paramName+'|')+paramName.length+1);
				var oldParamValue = urlSecond.substr(0,urlSecond.indexOf(';'));
				var indexParamEnd = indexParamStart+paramName.length+1;
				if(oldParamValue != ''){
					indexParamEnd = url.indexOf(paramName+'|'+oldParamValue) + paramName.length+1 + oldParamValue.length;
				}
				ret = url.substr(0,indexParamStart+paramName.length+1) + paramVal + url.substr(indexParamEnd);
			}			
		}
	}
	return ret;
};
