// Initialize
function init() {
	preloadImages();
}

// Form
function printVersion() {
	document.frmAnalyzer.target = "_blank";
	__doPostBack("mnuFilePrintAction","");
	document.frmAnalyzer.target = "_self";
}

// Menu
var menuOpened = '';
function openMenu(menu) {
	if (menuOpened != menu) {
		if (menuOpened != '') {
			closeMenu(menuOpened);
		}
		document.getElementById(menu).style.visibility = 'visible';
		document.getElementById(menu + "Cell").className = 'activeMenu';
		menuOpened = menu;
	}
}
function closeMenu(menu) {
	document.getElementById(menu).style.visibility = 'hidden';
	document.getElementById(menu + "Cell").className = 'menu';
	menuOpened = '';
}

// Preload images
function newImage(arg) {
	if (document.images) {
		rslt = new Image();
		rslt.src = arg;
		return rslt;
	}
}

function changeImages() {
	if (document.images && (preloadFlag == true)) {
		for (var i=0; i<changeImages.arguments.length; i+=2) {
			document[changeImages.arguments[i]].src = changeImages.arguments[i+1];
		}
	}
}

var preloadFlag = false;
function preloadImages() {
	if (document.images) {
		apply_over = newImage("images/apply-over.gif");
		clear_over = newImage("images/clear-over.gif");
		preloadFlag = true;
	}
}

// Formatting
function filterNonNumeric(element) {
	var result = new String();
	var numbers = "0123456789";
	var chars = element.value.split("");
	for (i = 0; i < chars.length; i++) {
		if (numbers.indexOf(chars[i]) != -1) result += chars[i];
	}
	if (element.value != result) element.value = result;
}

function formatStation(element) {
	var objRegExp = /^\d{8}$|^\d{10}$|^\d{15}$/;
	if (!objRegExp.test(element.value) && element.value != '') {
		element.focus();
		element.select();
	}
}

function formatVariable(element) {
	var objRegExp = /^(\d{1,5})$/;
	if (objRegExp.test(element.value)) {
		var iterations = 5 - element.value.length;
		for (i = 0; i < iterations; i++) {
			element.value = "0" + element.value
		}
	} else if (!objRegExp.test(element.value) && element.value != '') {
		element.focus();
		element.select();
	}
}

// Launch Search Pages
function searchstations() {
	window.open("http://waterdata.usgs.gov/nwis/inventory");
}

function searchvariables() {
	window.open("http://nwis.waterdata.usgs.gov/usa/nwis/pmcodes");
}

//Launch a new window with a particular size
function NewWindow(url) {
	var windowProperties

	if (screen.height <= 600) {
		windowProperties = 'width=400,height=150,left=0,top=0,resizable=yes,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no,status=no';			
	} else {
		windowProperties = 'width=400,height=150,left=0,top=0,resizable=no,scrollbars=no,toolbar=no,menubar=no,location=no,directories=no,status=no';
	}

	popupWindow = window.open(url,'popupWindow',windowProperties);
	if (window.focus) { popupWindow.focus() }
}