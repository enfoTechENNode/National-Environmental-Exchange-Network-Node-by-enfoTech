var LocalHostName = "http://localhost/RESTFulService/Query?";

$(document).ready(function () {


    $.get("Clients/GetRESTfulPageDesc", function (data, status) {
        if (status == "success")
            BuildPageInfo(data);
    });


    $.get("Clients/GetDataFlow", function (data, status) {
        if (status == "success")
            BuildServicesPanel(data);
    });

}); 



function GetServiceDtl(data)
{
    var divcontent = $(data);
    var sID = "#SerDtl-" + data.id.substring(data.id.lastIndexOf("-") + 1);
    //    alert(sID);
    var serdtl = $("#MainContent").find(sID);

    if (serdtl.length > 0) {

        $(serdtl).remove();
        $("#MainContent").prepend(serdtl);
        wireServiceDtlEvent();

    } else {

    $.post("Clients/GetServiceDetail?serviceID=" + data.id,
                { serviceID: data.id },
                function (data, status) {
                    if (status == "success")
                         buildServiceDetail(data);
                });
    }

};

function ConstructRESTFul(data) {

//    alert($(data).prop('tagName'));
//    return;
    
    var paraList = $(data).parent().parent().parent();
    var servicedtl = $(data).parent().parent().parent().parent();
    var resultstring = LocalHostName;

    var request = $(servicedtl).find(".FieldRowService").find(".FieldInput").html();
    resultstring = resultstring + "Request=" + request.trim() + "&";

    $(paraList).children().each(function () {

        var paraName = $(this).find(".FieldLabel").html();
        var paraValue = $(this).find(".ParameterInput").val();

//        alert(paraName + "=" + paraValue);
        if (paraValue != "") 
        {
            resultstring = resultstring + paraName.trim() + "=" + paraValue.trim() + "&";
        }
    });

    if (resultstring.lastIndexOf("&") == resultstring.length-1)
        resultstring = resultstring.substring(0, resultstring.length - 1);

    //    resultstring = "<p>" + resultstring + "</p>";
    $(servicedtl).find(".RESTFulResul").val(resultstring);
};

function RemoveServiceDtl(data) {
    $(data).parent().parent().remove();
}

function ToggleServiceDtl(data) {
    $(data).parent().parent().find(".CtlBLock").slideToggle("slow");

    if ($(data).attr("SRC") == "App_Images/zoom-out-4.png")
        $(data).attr("SRC", "App_Images/zoom-in-4.png");
    else
        $(data).attr("SRC","App_Images/zoom-out-4.png");

}

function ToggleDataFlow(data) {

//    alert($(data).parent().attr("class"));
//    alert($(data).next().attr("class"));

    $(data).next().slideToggle();

    var ctlToggle = $(data).find(".ctlToggle");

//    alert($(ctlToggle).html());

    if ($(ctlToggle).html() == "-")
        $(ctlToggle).html("+");
    else
        $(ctlToggle).html("-");
}

function BuildServicesPanel(data)
{
    var leftPanel = $(".LeftColumn");
    var dataBlock="";
    var dataflow ="";
    var service="";

    for (var i = 0; i < data.length; i++) {

        dataflow = "<div class='DataFlow'><span class='ctlToggle'>-</span>" + data[i].DataFlowName + "</div>";
        service = "";
        for (var j = 0; j < data[i].Services.length; j++) {
           service = service + "<div class='Service' id='ServiceID-" + data[i].Services[j].ServiceID + "'>" + data[i].Services[j].ServiceName + "</div>"
       }

       dataBlock = "<div class='DFBlock'>"+ dataflow + "<div class='ctlBox'>"+service+"</div></div>";

       leftPanel.append(dataBlock);

    }

    $(".DataFlow").click(function () {
        ToggleDataFlow(this);
    });

    $(".Service").click(function () {
        GetServiceDtl(this);
    });

//    $(".Service").mouseenter(function () {
//        $(this).addClass("HighLightService");
//    });

//    $(".Service").mouseleave(function () {
//        $(this).removeClass("HighLightService");
//    });

//    var test = $(leftPanel).html();
//    alert(test);

}

function buildServiceDetail(data) {

    LocalHostName = data.ServiceBaseURL + "Query?";
    var requeststring = LocalHostName + "Request=" + data.ServiceName;
    requeststring = requeststring + "&Format=XML";

    var serdtl = "<div class='ServiceDtl' id='SerDtl-" + data.ServiceID + "'>";
    serdtl = serdtl + "<div class='FieldRowContol'><span class='ServiceDtlTitle'>" + data.DataFlowName + " - " + data.ServiceName + "</span><img src='App_Images/window-close-2.png' alt='Close' class='CtlRemove'/><img src='App_Images/zoom-out-4.png' alt='Toggle' class='CtlToggle'/></div>";

    serdtl = serdtl + "<div class='CtlBLock'><hr />";

    serdtl = serdtl + "<div class='FieldRow'><div class='FieldLabel'>DataFlow Name:</div><div class='FieldInput'>" + data.DataFlowName + "</div></div>";

    serdtl = serdtl + "<div class='FieldRowService'><div class='FieldLabel'>Service Name:</div><div class='FieldInput'>" + data.ServiceName + "</div></div>";

    serdtl = serdtl + "<div class='FieldRow'><div class='FieldLabel'>Service Description:</div><div class='FieldInput'>"+data.Description+"</div></div>";
    serdtl = serdtl + "<div class='FieldRow'><div class='FieldLabel'>Parameters:</div></div>";
           
    serdtl = serdtl +"<div id='ParameterList'>";
    serdtl = serdtl + "<div class='FieldRowPara'><div class='FieldLabel'>RowId</div><div class='FieldInput'><input class='ParameterInput' type='text'/></div></div>";
    serdtl = serdtl + "<div class='FieldRowPara'><div class='FieldLabel'>MaxRows</div><div class='FieldInput'><input class='ParameterInput' type='text'/></div></div>";

    var parameters = data.Parameters;
    for (var i = 0; i < parameters.length; i++) {
        serdtl = serdtl + "<div class='FieldRowPara'><div class='FieldLabel'>" + parameters[i].ParaName + "</div><div class='FieldInput'><input class='ParameterInput' type='text'/></div></div>";
    }
    serdtl = serdtl +"<div class='FieldRowPara'><div class='FieldLabel'>Format</div><div class='FieldInput'><select class='ParameterInput'><option value='XML' selected>XML</option><option value='ZIP'>ZIP</option></select></div></div>";

    serdtl = serdtl + "</div>";

    serdtl = serdtl + "<hr><div class='FieldRow'><div class='FieldLabel'>RESTful URL:</div><div class='FieldInput'><textarea class='RESTFulResul' readonly class='RESTFulResul'>" + requeststring + "</textarea>";
    serdtl = serdtl + "<input class='ServiceInvoke' type='button' value='Invoke'></input>";
    serdtl = serdtl + "</div></div></div></div>";
    $("#MainContent").prepend(serdtl);

    wireServiceDtlEvent();

}

function wireServiceDtlEvent() {

    $(".ParameterInput").unbind();
    $(".CtlRemove").unbind();
    $(".CtlToggle").unbind();
    $(".ServiceInvoke").unbind();

    $(".ParameterInput").change(function () {
        ConstructRESTFul(this);
    });

    $(".ParameterInput").keyup(function () {
        ConstructRESTFul(this);
    });

    $(".CtlRemove").click(function () {
        RemoveServiceDtl(this);
    });

    $(".CtlToggle").click(function () {
        ToggleServiceDtl(this);
    });

    $(".ServiceInvoke").click(function () {
        var url = $(this).prev().val(); ;
        window.open(url, '_blank');
    });

}


function BuildPageInfo(data) {

    var pageDesc = $(".PageDesc");
    var result = "";
    result = "<span class='PageDescHeader'>"+data.PageTitle+"</span>";
    result = result + "<p class='PageDescDetail'>"+data.PageDescription+"</p>";
    pageDesc.append(result);

}