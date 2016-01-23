// JScript File
function chkAllBox(cmb, name_f, name_b)
{
    var i = 2;
    var itxt = '00' + i;
    //alert(name_f + twonum(i) + name_b);
    while ( document.getElementById(name_f + (i < 100 ? itxt.substring(itxt.length - 2, itxt.length) : itxt) + name_b) != null)
    {
        zctl = document.getElementById(name_f + (i < 100 ? itxt.substring(itxt.length - 2, itxt.length) : itxt) + name_b)
        if (zctl.disabled == '') zctl.checked = cmb.checked;
        i++;
        itxt = (i < 100 ? '00' : '') + i;
    }
}
function ChkMinusDbl(ctl, e) {
    var evt = e ? e : window.event;
    var zz = evt.keyCode || evt.charCode;
    
    if (zz < 48 || zz > 57) {
        if (zz == 46) {
            if (ctl.value.indexOf(".", 0) >= 0) {
                if(window.event)//IE
                    event.returnValue = false;
                else //Firefox
                    e.preventDefault();
            }
        }
        else {
            if (window.event)//IE
                event.returnValue = false;
            else  //Firefox
                e.preventDefault();
        }

    }
}

function ChkMinusInt(ctl, e) {
    var evt = e ? e : window.event;
    var zz = evt.keyCode || evt.charCode;

    if (zz < 48 || zz > 57) {
        if(window.event)
            event.returnValue = false;
        else
            e.preventDefault();
    }
}

function CheckKeyNumber(e) {
    //ป้องกันการกดปุ่ม  ctrl และปุ่มตัว V
    var evt = e ? e : window.event;
    var keyCode = evt.keyCode || evt.charCode;
    
    if ((keyCode == 17) || (keyCode == 86)) {
        if (window.event)//IE
            event.returnValue = false;
        else if (e)//Firefox
            e.preventDefault();
    }
}

function DisableText(ctl)
{
    event.returnValue = false;
}

function txtTime_OnKeyPress(sender,e) {
    var evt = e ? e : window.event;
    var charCode = evt.keyCode || evt.charCode;

    //var charCode = (event.which) ? event.which : event.keyCode
    //alert(charCode);
    if ((charCode != 8) && (charCode != 46)) {
        
        var myTime = sender.value;
        if (myTime.length > 4) {
            if (window.event)
                event.returnValue = false;
            else
                e.preventDefault();
        }

        switch (myTime.length) {
            case 0:
                if (charCode < 48 || charCode > 50) {
                    if (window.event)
                        event.returnValue = false;
                    else
                        e.preventDefault();
                }
                break;
            case 1:
                //alert("charCode=" + charCode + "  $$$$ " + myTime);
                if (myTime == 2) {
                    if (charCode > 51 || charCode < 48) {
                        if (window.event)
                            event.returnValue = false;
                        else
                            e.preventDefault();
                    }
                } else {
                    if (charCode < 48 || charCode > 57) {
                        if (window.event)
                            event.returnValue = false;
                        else
                            e.preventDefault();
                    }
                }
                break;
            case 2:
                {
                    if (charCode < 48 || charCode > 53) {
                        if (window.event)
                            event.returnValue = false;
                        else
                            e.preventDefault();
                    }
                    sender.value = sender.value + ':';
                }
                break;
            case 3:
                if (charCode < 48 || charCode > 53) {
                    if (window.event)
                        event.returnValue = false;
                    else
                        e.preventDefault();
                }
                //alert(3);
                break;
            default:
                if (charCode < 48 || charCode > 57) {
                    if (window.event)
                        event.returnValue = false;
                    else
                        e.preventDefault();
                }
        }
    }
}

function ValidateTime(sender) {
    if (sender.value.length == 0) return false;
    var regEx = /^(\d{2}):(\d{2})$/;
    var arrMatch = sender.value.match(regEx);
    if (arrMatch == null) {
        alert("Invalid time.");
        sender.value = "";
        return false;
    }
    var hh = arrMatch[1];
    var mm = arrMatch[2];
    if (hh >= 24 || mm >= 60) {
        alert("Invalid time.");
        sender.value = "";
        return false;
    }
    return true;
}


function CheckKeyBackSpace(e) {
//ป้องกันการกดปุ่ม Delete , ปุ่ม Backspace , ปุ่ม ctrl และปุ่มตัว V
    //var keyCode = (event.which) ? event.which : event.keyCode;
    var evt = e ? e : window.event;
    var keyCode = evt.keyCode;
    if ((keyCode == 8) || (keyCode == 46) || (keyCode == 17) || (keyCode == 86)) {
        if(window.event)//IE
            event.returnValue = false;
        else if(e)//Firefox
            e.preventDefault();
    }
}


function txtKeyPress(e) {
    if(window.event)//For IE
        event.returnValue = false;
    else if(e)//For Firefox
        e.preventDefault();
}

function clickButton(e, buttonid) {
    var evt = e ? e : window.event;
    var bt = document.getElementById(buttonid);
    if (bt) {
        if (evt.keyCode == 13) {
            bt.click();
            return false;
        }
    }
}



//Password Validation
function ValidPassword(txtPwd) {
    var txtPwd = document.getElementById(txtPwd);
    if (txtPwd.value != "") {
        if (txtPwd.value.length < 6) {
            alert("รหัสผ่านจะต้องไม่น้อยกว่า 6 ตัวอักษร \r\n( Password is contains at least 6 characters. )");
            txtPwd.select();
        }
    }
}

function ValidPasswordKeyPress(e, txtPwd) {
    var txtPwd = document.getElementById(txtPwd);
    var evt = e ? e : window.event;
    var zz = evt.keyCode || evt.charCode;
    //alert(zz);
    //ถ้ากดปุ่ม Backspace หรือปุ่ม Delete ก็ให้ใช้งานได้
    //(zz != 46) && && (zz != 39) && (zz != 37)
    
    if ((zz != 8) && (zz != 13) && (zz != 9)) {
        //รหัสผ่านตัวแรกจะต้องเป็นตัวอักษรเท่านั้น
        if (txtPwd.value.length == 0) {
            if ((zz >= 65 && zz <= 90) || (zz >= 97 && zz <= 122)) {
                null;
            } else {
                alert('ตัวอักษรตัวแรกจะต้องเป็นตัวอักษรภาษาอังกฤษเท่านั้น \r\n ( The first charactrs must be in English letter. )');
                if (window.event)
                    event.returnValue = false;
                else
                    e.preventDefault();
            }
        } else {
            //รหัสผ่านจะต้องเป็นตัวเลขหรือตัวอักษรภาษาอังกฤษเท่านั้น
        if ((zz < 48 || zz > 57) && (zz < 65 || zz > 90) && (zz < 97 || zz > 122)) {
                alert('รหัสผ่านต้องเป็นตัวอักษรผสมตัวเลข หรือเป็นตัวอักษรอย่างเดียว \r\n ( Composes of letters and number combination, or pure letter. )');
                if (window.event)
                    event.returnValue = false;
                else
                    e.preventDefault();
            } else {
                var str = txtPwd.value;  //รหัสผ่านจะต้องซ้ำกันได้ไม่เกิน 2 ตัว
                if (String.fromCharCode(str.charCodeAt(str.length - 1)) == String.fromCharCode(zz) && String.fromCharCode(str.charCodeAt(str.length - 2)) == String.fromCharCode(zz)) {
                    alert('ห้ามมีตัวอักษรหรือตัวเลขตัวเดียวกันเรียงกันมากกว่า 2 ตัว \r\n ( Not allow more than 2 repeated letter or numbers. )');
                    if (window.event)
                        event.returnValue = false;
                    else
                        e.preventDefault();
                }
            }
        }
    }
}

function ValidPwdKeyDown(e) {
    var evt = e ? e : window.event;
    var zz = evt.keyCode || evt.charCode;
    //alert(zz);
    if ((zz == 17) || (zz == 86) || (zz == 45)) {
        if (window.event)//IE
            event.returnValue = false;
        else if (e)//Firefox
            e.preventDefault();
    }
}

function ValidRightClick(e) {
    if (navigator.appName == 'Netscape' && e.which == 3) {
        alert("no right click please");
        return false;
    }
    else {
        if (navigator.appName == 'Microsoft Internet Explorer' && event.button == 2)
        alert("no right click please");
        return false;
    }
    return true;
}