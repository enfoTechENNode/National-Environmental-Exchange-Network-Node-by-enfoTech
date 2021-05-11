
function AjaxSelectBox(id,url)
{
    this.ClientID = id;
    this.Url = url;
    return this;
};

AjaxSelectBox.prototype.callback = function(parm)
{
    Utils.runCallback(this.Url, this.callbackHandler, parm, this.ClientID);
}

AjaxSelectBox.prototype.callbackHandler = function(txtResult, domObj, context)
{
    Utils.clearSelect(context);
    Utils.populateSelect(context, domObj);
}


//=======================================================================================================

function AjaxTextBox(id,url)
{
    this.ClientID = id;
    this.Url = url;
    return this;
};

AjaxTextBox.prototype.callback = function(parm)
{
    Utils.runCallback(this.Url, this.callbackHandler, parm, this.ClientID);
}

AjaxTextBox.prototype.callbackHandler = function(txtResult, domObj, context)
{
    document.getElementById(context).value = txtResult;
}

//=======================================================================================================

function AjaxSpan(id,url)
{
    this.ClientID = id;
    this.Url = url;
    return this;
};

AjaxSpan.prototype.callback = function(parm)
{
    Utils.runCallback(this.Url, this.callbackHandler, parm, this.ClientID);
}

AjaxSpan.prototype.callbackHandler = function(txtResult, domObj, context)
{
    document.getElementById(context).innerText = txtResult;
}


//=======================================================================================================

function AjaxContentHolder(id,url)
{
    this.ClientID = id;
    this.Url = url;
    return this;
};

AjaxContentHolder.prototype.callback = function(parm)
{
    var wDiv = document.getElementById('ajaxWaitDiv_'+this.ClientID);
    if(wDiv) document.getElementById('ajaxCntDiv_'+this.ClientID).innerHTML = wDiv.innerHTML;
    Utils.runCallback(this.Url, this.callbackHandler, parm, this.ClientID);
}

AjaxContentHolder.prototype.callbackHandler = function(txtResult, domObj, context)
{
    document.getElementById('ajaxCntDiv_'+context).innerHTML = txtResult;
}
