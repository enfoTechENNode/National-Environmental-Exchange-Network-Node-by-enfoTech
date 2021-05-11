
//********************************************************************
//*	 This module is only for Texas notobi Ajax module
//*  The gridValidation.js include all validation functions for Grid.
//*
//********************************************************************


//********************************************************************************
//*
//*  This funciton validate SubmissionYear for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateSubmissionYear(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();
  var myValue = myGridCell.getValue();
  if (myNewValue.length > 4) {
    nitobi.callout.destroyLast();
    var myCallout = new nitobi.callout.Callout("xp");
    var cellId = myGridCell.getDomNode().id;
    myCallout.attachToElement(cellId);
    myCallout.setTitle('Please try again');
    myCallout.setBody('Your entry must be no longer than 4 letters long.');
    myCallout.show();
    setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  }
  else {
    nitobi.callout.destroyAll();
  }
  return true;
}


//**************************************************************************
//*
//*  This funciton validate TRIFID for Grid of ReconciliationEntry.jsp.
//*
//**************************************************************************

function validateTRIFID(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 15) {
  // first we delete the last callout (if any)
  nitobi.callout.destroyLast();
  // now we create a new one
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  // we attach it to the element with ID myName
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 15 letters long.');
  // now we show it.
  myCallout.show();
  // However, if I want to either revert the value or set some new value I can
  // use setValue() and use a timeout to make put it outside the cell validation
  // execution context like this:
  // setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
  // it passed validation.. lets destroy all the callouts
    nitobi.callout.destroyAll();
  }
  // If I wanted to prevent the cell from blurring I can return true like this:
  // return true;
  return true;
}

//********************************************************************************
//*
//*  This funciton validate FacilityName for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateFacilityName(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 65) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 65 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate LocationAddress for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateLocationAddress(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 30) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 30 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate ZIP for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateZIP(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 9) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 9 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate TechContact for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateTechContact(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 60) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 60 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate Phone for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validatePhone(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 20) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 20 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate Chemical for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateChemical(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 20) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 20 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate TCEQRecDate for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateTCEQRecDate(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  var r = myNewValue.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);

  if (r==null) {
    nitobi.callout.destroyLast();
    var myCallout = new nitobi.callout.Callout("xp");
    var cellId = myGridCell.getDomNode().id;
    myCallout.attachToElement(cellId);
    myCallout.setTitle('Please try again');
    myCallout.setBody('Your entry must be YYYY-MM-DD.');
    myCallout.show();
    var d = new Date(r[1], r[3]-1, r[4]);
    return (d.getFullYear()==r[1]&&(d.getMonth()+1)==r[3]&&d.getDate()==r[4]);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate SubmitMethod for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateSubmitMethod(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 20) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 20 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate RevisionInd for Grid of ReconciliationEntry.jsp.
//*
//********************************************************************************

function validateRevisionInd(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 10) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 10 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate ReconcileComments for Grid of ReconciliationReport.jsp.
//*
//********************************************************************************

function validateReconcileComments(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 200) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 200 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton validate CustomerNumber for Grid of ReconciliationReport.jsp.
//*
//********************************************************************************

function validateCustomerNumber(eventArgs)
{
  var myNewValue = eventArgs.newValue;
  var myOldValue = eventArgs.oldValue;

  var myGridCell = eventArgs.getCell();

  if (myNewValue.length > 10) {
  nitobi.callout.destroyLast();
  var myCallout = new nitobi.callout.Callout("xp");
  var cellId = myGridCell.getDomNode().id;
  myCallout.attachToElement(cellId);
  myCallout.setTitle('Please try again');
  myCallout.setBody('Your entry must be no longer than 10 letters long.');
  myCallout.show();
  setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
  } else {
    nitobi.callout.destroyAll();
  }
  return true;
}

//********************************************************************************
//*
//*  This funciton will show a callout box prompt diableEdit information.
//*
//********************************************************************************

function disableEdit(eventArgs)
{
 var myNewValue = eventArgs.newValue;
 var myOldValue = eventArgs.oldValue;

 var myGridCell = eventArgs.getCell();

 nitobi.callout.destroyAll();
 var myCallout = new nitobi.callout.Callout("xp");
 var cellId = myGridCell.getDomNode().id;
 myCallout.attachToElement(cellId);
 myCallout.setTitle('This column can not be edited');
 myCallout.setBody('');
 myCallout.show();
 myNewValue=myOldValue;
 //setTimeout(function() {myGridCell.setValue(myOldValue);}, 0);
}
