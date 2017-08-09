function CompressPageData() {
    var zip = new JSZip();   
    var theForm = document.forms[0];
    var elem = [];  
    var hiddenObjects = [];

    if (typeof theForm.elements !== 'undefined') {
        for (var i = 0; i < theForm.elements.length; i++) {
            if (typeof theForm.elements[i].type !== 'undefined'
                && theForm.elements[i].type == 'hidden'
                && typeof theForm.elements[i].name !== 'undefined'
                && theForm.elements[i].name.length > 0
                && theForm.elements[i].id != "__EVENTTARGET"
                && theForm.elements[i].id != "__EVENTARGUMENT"
                && theForm.elements[i].id != "__VIEWSTATE"
                && theForm.elements[i].id != "__VIEWSTATEGENERATOR"
                && theForm.elements[i].id != "__EVENTVALIDATION"
                ){
                    elem.push(theForm.elements[i]);
            }
        }

        for (var i = 0; i < elem.length; i++) {
            hiddenObject = new Object();
            hiddenObject.id = elem[i].name;
            hiddenObject.value = elem[i].value;
            hiddenObjects.push(hiddenObject);
        }

        var dataJson = JSON.stringify(hiddenObjects);
        zip.file('json.txt', dataJson);

        var base64Pom = zip.generate({
            type: 'base64',
            compression: 'DEFLATE'
        });

        document.getElementById('HiddenFieldCompressedData').value = base64Pom;

        $("input[type=hidden]").not('form #HiddenFieldCompressedData').not('form #__EVENTTARGET').not('form #__EVENTARGUMENT').not('form #__VIEWSTATE').not('form #__VIEWSTATEGENERATOR').not('form #__EVENTVALIDATION').each(function () {    
            $(this).remove();
        });
    }
};

function clientValidate() {
    if (document.getElementById('CheckBox1').checked) {
        CompressPageData();
    }
    return true;
}

function executeAfter() {
    debugger;
    window.location.href = "/Summary.aspx";
}
