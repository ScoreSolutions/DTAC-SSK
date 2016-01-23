//Javascript name: My Date Time Picker
//Date created: 16-Nov-2003 23:19
//Creator: TengYong Ng
//Website: http://www.rainforestnet.com
//Copyright (c) 2003 TengYong Ng
//FileName: DateTimePicker_css.js
//Version: 2.1.1
// Note: Permission given to use and modify this script in ANY kind of applications if
//       header lines are left unchanged.
//Permission is granted to redistribute and modify this javascript under the terms of the GNU General Public License 3.0.
//New Css style version added by Yvan Lavoie (Québec, Canada) 29-Jan-2009



//Global variables
var expDate;
var pwdDate;
var winCal;
var dtToday;
var Cal;
var MonthName;
var WeekDayName1;
var WeekDayName2;
var exDateTime;//Existing Date and Time
var selDate;//selected date. version 1.7
var calSpanID = "calBorder"; // span ID
var domStyle=null; // span DOM object with style
var cnLeft="0";//left coordinate of calendar span
var cnTop="0";//top coordinate of calendar span
var xpos=0; // mouse x position
var ypos=0; // mouse y position
var calHeight=0; // calendar height
var CalWidth=208;// calendar width
var CellWidth=30;// width of day cell.
var TimeMode=24;// TimeMode value. 12 or 24
var StartYear =2005; //First Year in drop down year selection
var EndYear = 5; //End Year offset. i.e. Current Year + 5


//Configurable parameters

//var WindowTitle="DateTime Picker";//Date Time Picker title.

var SpanBorderColor = "#FFFFFF";//span border color
var SpanBgColor = "#FFFFFF";//span background color
var WeekChar=5;//number of character for week day. if 2 then Mo,Tu,We. if 3 then Mon,Tue,Wed.
var DateSeparator="/";//Date Separator, you can change it to "-" if you want.
var ShowLongMonth=true;//Show long month name in Calendar header. example: "January".
var ShowMonthYear=true;//Show Month and Year in Calendar header.
var MonthYearColor="#cc0033";//Font Color of Month and Year in Calendar header.
var WeekHeadColor = "#627aad"; //var WeekHeadColor="#18861B";//Background Color in Week header.
var SundayColor = "#dbe6ec"; //var SundayColor="#C0F64F";//Background color of Sunday.
var SaturdayColor = "#dbe6ec"; //Background color of Saturday.
var WeekDayColor="white";//Background color of weekdays.
var FontColor="blue";//color of font in Calendar day cell.
var TodayColor = "#627aad"; //var TodayColor="#FFFF33";//Background color of today.
var SelDateColor = "#E0FF38"; //var SelDateColor="#8DD53C";//Backgrond color of selected date in textbox.
var YrSelColor="#cc0033";//color of font of Year selector.
var MthSelColor="#cc0033";//color of font of Month selector if "MonthSelector" is "arrow".
var HoverColor = "#c0dcf2"; //color when mouse move over.
var ThemeBg="";//Background image of Calendar window.
var CalBgColor="";//Backgroud color of Calendar window.
var PrecedeZero=true;//Preceding zero [true|false]
var MondayFirstDay=true;//true:Use Monday as first day; false:Sunday as first day. [true|false]  //added in version 1.7
var UseImageFiles = true;//Use image files with "arrows" and "close" button

//use the Month and Weekday in your preferred language.
//มกราคม ·์ · มีนาคม · เมษายน · พฤษภาคม · มิถุนายน · กรกฎาคม · สิงหาคม · กันยายน · ตุลาคม · พฤศจิกายน · ธันวาคม
var MonthName=["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน","กรกฎาคม","สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"];
var WeekDayName1=["อา","จ","อ","พ","พฤ","ศ","ส"];
//var WeekDayName2=["จันทร์","อังคาร","พุธ","พฤหัสบดี","ศุกร์","เสาร์","อาทิตย์"];

//end Configurable parameters

//end Global variable

// Default events configuration

document.onmousedown = pickIt;
document.onmousemove = dragIt;
document.onmouseup = dropIt;

function NewCssCal(pCtrl,pFormat,pScroller,pShowTime,pTimeMode,pHideSeconds, putExpDate, putPwdDate) {
	// get current date and time
    expDate = "";
    pwdDate = "";
	dtToday = new Date();
	Cal=new Calendar(dtToday);

	if ((pShowTime!=null) && (pShowTime)) {
		Cal.ShowTime=true;
		if ((pTimeMode!=null) &&((pTimeMode=='12')||(pTimeMode=='24')))	{
			TimeMode=pTimeMode;
		}

		else TimeMode='24';

        if (pHideSeconds!=null)
        {
            if (pHideSeconds)
            {Cal.ShowSeconds=false;}
            else
            {Cal.ShowSeconds=true;}

        }

        else
        {
            Cal.ShowSeconds=false;
        }

	}

	if (pCtrl!=null)

		Cal.Ctrl=pCtrl;


	if (pFormat!=null)
		Cal.Format=pFormat.toUpperCase();
	else
	    Cal.Format="MMDDYYYY";

	if (pScroller!=null) {
		if (pScroller.toUpperCase()=="ARROW") {
			Cal.Scroller="ARROW";
		}
		else {
			Cal.Scroller="DROPDOWN";
		}
    }

	exDateTime=document.getElementById(pCtrl).value;

	if (exDateTime!="")	{ //Parse existing Date String
		var Sp1;//Index of Date Separator 1
		var Sp2;//Index of Date Separator 2
		var tSp1;//Index of Time Separator 1
		var tSp1;//Index of Time Separator 2
		var strMonth;
		var strDate;
		var strYear;
		var intMonth;
		var YearPattern;
		var strHour;
		var strMinute;
		var strSecond;
		var winHeight;
		//parse month

		Sp1=exDateTime.indexOf(DateSeparator,0)
		Sp2=exDateTime.indexOf(DateSeparator,(parseInt(Sp1)+1));
		var offset=parseInt(Cal.Format.toUpperCase().lastIndexOf("M"))-parseInt(Cal.Format.toUpperCase().indexOf("M"))-1;

		if ((Cal.Format.toUpperCase()=="DDMMYYYY") || (Cal.Format.toUpperCase()=="DDMMMYYYY")) {
			if (DateSeparator=="") {
				strMonth=exDateTime.substring(2,4+offset);
				strDate=exDateTime.substring(0,2);
				strYear=exDateTime.substring(4+offset,8+offset);
				strYear=String(parseInt(strYear,10)-543)
			}
			else {
				if(exDateTime.indexOf("D*") != -1) {   //DTG
					strMonth = exDateTime.substring(8, 11);
					strDate  = exDateTime.substring(0, 2);
					strYear  = "20" + exDateTime.substring(11, 13);  //Hack, nur für Jahreszahlen ab 2000
					strYear=String(parseInt(strYear,10)-543)

				} else {
					strMonth=exDateTime.substring(Sp1+1,Sp2);
					strDate=exDateTime.substring(0,Sp1);
					strYear=exDateTime.substring(Sp2+1,Sp2+5);
					strYear=String(parseInt(strYear,10)-543)
				}
			}

		}

		else if ((Cal.Format.toUpperCase()=="MMDDYYYY") || (Cal.Format.toUpperCase()=="MMMDDYYYY")) {

			if (DateSeparator=="") {
				strMonth=exDateTime.substring(0,2+offset);
				strDate=exDateTime.substring(2+offset,4+offset);
				strYear=exDateTime.substring(4+offset,8+offset);
			}

			else {

				strMonth=exDateTime.substring(0,Sp1);
				strDate=exDateTime.substring(Sp1+1,Sp2);
				strYear=exDateTime.substring(Sp2+1,Sp2+5);
			}

		}

		else if ((Cal.Format.toUpperCase()=="YYYYMMDD") || (Cal.Format.toUpperCase()=="YYYYMMMDD")) {

			if (DateSeparator=="") {
				strMonth=exDateTime.substring(4,6+offset);
				strDate=exDateTime.substring(6+offset,8+offset);
				strYear=exDateTime.substring(0,4);
			}

			else {
				strMonth=exDateTime.substring(Sp1+1,Sp2);
				strDate=exDateTime.substring(Sp2+1,Sp2+3);
				strYear=exDateTime.substring(0,Sp1);
			}

		}

		else if ((Cal.Format.toUpperCase()=="YYMMDD") || (Cal.Format.toUpperCase()=="YYMMMDD")) {

			if (DateSeparator=="") {
				strMonth=exDateTime.substring(2,4+offset);
				strDate=exDateTime.substring(4+offset,6+offset);
				strYear=exDateTime.substring(0,2);
			}

			else {
				strMonth=exDateTime.substring(Sp1+1,Sp2);
				strDate=exDateTime.substring(Sp2+1,Sp2+3);
				strYear=exDateTime.substring(0,Sp1);
			}

		}

		if (isNaN(strMonth))
			intMonth=Cal.GetMonthIndex(strMonth);
		else
			intMonth=parseInt(strMonth,10)-1;

		if ((parseInt(intMonth,10)>=0) && (parseInt(intMonth,10)<12))
			Cal.Month=intMonth;

		//end parse month

		//parse Date

		if ((parseInt(strDate,10)<=Cal.GetMonDays()) && (parseInt(strDate,10)>=1))
			Cal.Date=strDate;
		//end parse Date

		//parse year

		YearPattern=/^\d{4}$/;
		if (YearPattern.test(strYear))
			Cal.Year=parseInt(strYear,10);

		//end parse year

		//parse time

		if (Cal.ShowTime==true)	{

			//parse AM or PM

			if (TimeMode==12) {
				strAMPM=exDateTime.substring(exDateTime.length-2,exDateTime.length)
				Cal.AMorPM=strAMPM;

			}

			tSp1=exDateTime.indexOf(":",0)
			tSp2=exDateTime.indexOf(":",(parseInt(tSp1)+1));
			if (tSp1>0)	{

				strHour=exDateTime.substring(tSp1,(tSp1)-2);
				Cal.SetHour(strHour);

				strMinute=exDateTime.substring(tSp1+1,tSp1+3);
				Cal.SetMinute(strMinute);

				strSecond=exDateTime.substring(tSp2+1,tSp2+3);
				Cal.SetSecond(strSecond);

			} else if(exDateTime.indexOf("D*") != -1) {   //DTG
				strHour = exDateTime.substring(2, 4);
				Cal.SetHour(strHour);
				strMinute = exDateTime.substring(4, 6);
				Cal.SetMinute(strMinute);

			}
		}

	}

	selDate = new Date(Cal.Year, Cal.Month, Cal.Date); //version 1.7
	expDate = putExpDate;
	pwdDate = putPwdDate;
	RenderCssCal(true);
}


function RenderCssCal(bNewCal) {

	if (typeof bNewCal == "undefined" || bNewCal != true) {bNewCal = false;}
	var vCalHeader;
	var vCalData;
	var vCalTime="";

	var i;
	var j;

	var SelectStr;
	var vDayCount=0;
	var vFirstDay;

	calHeight = 0; // reset the window height on refresh

	// Set the default cursor for the calendar

	winCalData = "<span onblur='self.close()' style='cursor:auto;'>\n";

	if (ThemeBg==""){CalBgColor="bgcolor='"+WeekDayColor+"'"}
	vCalHeader="<table "+CalBgColor+" background='"+ThemeBg+"' border=1 cellpadding=1 cellspacing=1 width='200px' valign='top'>\n";

	//Table for Month & Year Selector

	vCalHeader+="<tr>\n<td colspan='7'>\n<table border='0' width='200px' cellpadding='0' cellspacing='0'>\n<tr>\n";
	//******************Month and Year selector in dropdown list************************

	if (Cal.Scroller=="DROPDOWN") {
		vCalHeader+="<td align='center'><select name=\"MonthSelector\" onChange=\"javascript:Cal.SwitchMth(this.selectedIndex);RenderCssCal();\">\n";
		for (i=0;i<12;i++) {
			if (i==Cal.Month)
				SelectStr="Selected";
			else
				SelectStr="";
			    vCalHeader+="<option "+SelectStr+" value="+i+">"+MonthName[i]+"</option>\n";

		}

		vCalHeader+="</select></td>\n";
		//Year selector
		StartYear=dtToday.getFullYear()-80;

		vCalHeader+="<td align='center'><select name=\"YearSelector\" size=\"1\" onChange=\"javascript:Cal.SwitchYear(this.value);RenderCssCal();\">\n";
		for (i = StartYear,j=StartYear+543; i <= (dtToday.getFullYear() + EndYear);i++,j++)	{
			if (i==Cal.Year)
				SelectStr="Selected";
			else
				SelectStr="";
			vCalHeader+="<option "+SelectStr+" value="+i+">"+j+"</option>\n";

		}

		vCalHeader+="</select></td>\n";
		calHeight += 30;
	}

	//******************End Month and Year selector in dropdown list*********************

	//******************Month and Year selector in arrow*********************************

    else if (Cal.Scroller=="ARROW")
    {

    if (UseImageFiles)
    {
  		vCalHeader+="<td><img onmousedown='javascript:Cal.DecYear();RenderCssCal();' src='images/cal_fastreverse.gif' width='13px' height='9' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td>\n";//Year scroller (decrease 1 year)
  		vCalHeader+="<td><img onmousedown='javascript:Cal.DecMonth();RenderCssCal();' src='images/cal_reverse.gif' width='13px' height='9' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td>\n";//Month scroller (decrease 1 month)
  		vCalHeader+="<td width='70%' class='calR'><font color='"+YrSelColor+"'>"+Cal.GetMonthName(ShowLongMonth)+" "+Cal.Year+"</font></td>\n"//Month and Year
  		vCalHeader+="<td><img onmousedown='javascript:Cal.IncMonth();RenderCssCal();' src='images/cal_forward.gif' width='13px' height='9' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td>\n";//Month scroller (increase 1 month)
  		vCalHeader+="<td><img onmousedown='javascript:Cal.IncYear();RenderCssCal();' src='images/cal_fastforward.gif' width='13px' height='9' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td>\n";//Year scroller (increase 1 year)

  	    calHeight += 22;
	  }
	  else
	  {
	  	vCalHeader+="<td><span id='dec_year' title='reverse year' onmousedown='javascript:Cal.DecYear();RenderCssCal();' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white; color:"+YrSelColor+"'>-</span></td>";//Year scroller (decrease 1 year)
	  	vCalHeader+="<td><span id='dec_month' title='reverse month' onmousedown='javascript:Cal.DecMonth();RenderCssCal();' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'>&lt;</span></td>\n";//Month scroller (decrease 1 month)
  		vCalHeader+="<td width='70%' class='calR'><font color='"+YrSelColor+"'>"+Cal.GetMonthName(ShowLongMonth)+" "+Cal.Year+"</font></td>\n"//Month and Year
  		vCalHeader+="<td><span id='inc_month' title='forward month' onmousedown='javascript:Cal.IncMonth();RenderCssCal();' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'>&gt;</span></td>\n";//Month scroller (increase 1 month)
  		vCalHeader+="<td><span id='inc_year' title='forward year' onmousedown='javascript:Cal.IncYear();RenderCssCal();'  onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white; color:"+YrSelColor+"'>+</span></td>\n";//Year scroller (increase 1 year)
  	    calHeight += 22;
	  }
	}

	vCalHeader+="</tr>\n</table>\n</td>\n</tr>\n"

  //******************End Month and Year selector in arrow******************************

	//Calendar header shows Month and Year
	if ((ShowMonthYear)&&(Cal.Scroller=="DROPDOWN")) {
		vCalHeader+="<tr><td colspan='7' class='calR'>\n<font color='"+MonthYearColor+"'>"+Cal.GetMonthName(ShowLongMonth)+" "+(543+parseInt(Cal.Year,10))+"</font>\n</td></tr>\n";
	    calHeight += 19;
	}

	//Week day header

	vCalHeader+="<tr><td colspan=\"7\"><table cellspacing=1><tr>\n";
	var WeekDayName=new Array();//Added version 1.7

		WeekDayName=WeekDayName1;
	for (i=0;i<7;i++) {
		vCalHeader+="<td bgcolor="+WeekHeadColor+" width='"+CellWidth+"px' class='calTD'><font color='white'>"+WeekDayName[i].substr(0,WeekChar)+"</font></td>\n";
	}

	calHeight += 19;
	vCalHeader+="</tr>\n";
	//Calendar detail
	CalDate=new Date(Cal.Year,Cal.Month);
	CalDate.setDate(1);

	vFirstDay=CalDate.getDay()+1;

	//Added version 1.7

	if (MondayFirstDay==true) {
		vFirstDay-=1;
		if (vFirstDay==-1)
			vFirstDay=6;
	}

	//Added version 1.7

	vCalData="<tr>";
	calHeight += 19;
	for (i=0;i<vFirstDay;i++) {
		vCalData=vCalData+GenCell();
		vDayCount=vDayCount+1;
	}

	//Added version 1.7

	for (j=1;j<=Cal.GetMonDays();j++) {
		var strCell;
		if((vDayCount%7==0)&&(j > 1)) {
			vCalData=vCalData+"\n<tr>";
		}

		vDayCount=vDayCount+1;
		if ((j==dtToday.getDate())&&(Cal.Month==dtToday.getMonth())&&(Cal.Year==dtToday.getFullYear()))
			strCell=GenCell(j,true,TodayColor);//Highlight today's date
		else {
			if ((j==selDate.getDate())&&(Cal.Month==selDate.getMonth())&&(Cal.Year==selDate.getFullYear())) { //modified version 1.7
				strCell=GenCell(j,true,SelDateColor);
			}
			else {
				if (MondayFirstDay==true) {
					if ((vDayCount)%7==0)
						strCell=GenCell(j,false,SundayColor);
					else if ((vDayCount-1)%7==0)
						strCell=GenCell(j,false,SaturdayColor);
					else
						strCell=GenCell(j,null,WeekDayColor);
				}
				else {
					if (vDayCount%7==0)
						strCell=GenCell(j,false,SaturdayColor);
					else if ((vDayCount+5)%7==0)
						strCell=GenCell(j,false,SundayColor);
					else
						strCell=GenCell(j,null,WeekDayColor);
				}
			}
		}

		vCalData=vCalData+strCell;

		if((vDayCount%7==0)&&(j<Cal.GetMonDays())) {
			vCalData=vCalData+"\n</tr>";
			calHeight += 19;
		}
	}

	// finish the table proper

	if(!(vDayCount%7) == 0) {
		while(!(vDayCount % 7) == 0) {
			vCalData=vCalData+GenCell();
			vDayCount=vDayCount+1;
		}
	}

	vCalData=vCalData+"\n</table></td></tr>";


	//Time picker
	if (Cal.ShowTime)
	{
		var showHour;
		var ShowArrows=false;
		var HourCellWidth="35px"; //cell width with seconds.
		showHour=Cal.getShowHour();

		if (Cal.ShowSeconds==false && TimeMode==24 )
        {
		   ShowArrows=true;
		   HourCellWidth="10px";
		}

		vCalTime="\n<tr>\n<td colspan='7' align='center'><center>\n<table border='0' width='199px' cellpadding='0' cellspacing='2'>\n<tr>\n<td height='5px' width='"+HourCellWidth+"px'>&nbsp;</td>\n";

		if (ShowArrows && UseImageFiles)
		{
            vCalTime+="<td align='center'><table cellspacing='0' cellpadding='0' style='line-height:0pt'><tr><td><img onmousedown='startSpin(\"Hour\", \"plus\");' onmouseup='stopSpin();' src='images/cal_plus.gif' width='13px' height='9px' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td></tr><tr><td><img onmousedown='startSpin(\"Hour\", \"minus\");' onmouseup='stopSpin();' src='images/cal_minus.gif' width='13px' height='9px' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td></tr></table></td>\n";
		}

		vCalTime+="<td align='center' width='22px'><input type='text' name='hour' maxlength=2 size=1 style=\"WIDTH:22px\" value="+showHour+" onChange=\"javascript:Cal.SetHour(this.value)\">";
		vCalTime+="</td><td align='center' style='font-size:150%; font-weight:bold;'>:</td><td align='center' width='22px'>";
		vCalTime+="<input type='text' name='minute' maxlength=2 size=1 style=\"WIDTH: 22px\" value="+Cal.Minutes+" onChange=\"javascript:Cal.SetMinute(this.value)\">";

		if (Cal.ShowSeconds) {
			vCalTime+="</td><td align='center'>:</td><td align='center' width='22px'>";
			vCalTime+="<input type='text' name='second' maxlength=2 size=1 style=\"WIDTH: 22px\" value="+Cal.Seconds+" onChange=\"javascript:Cal.SetSecond(parseInt(this.value,10))\">";
		}

		if (TimeMode==12) {
			var SelectAm =(Cal.AMorPM=="AM")? "Selected":"";
			var SelectPm =(Cal.AMorPM=="PM")? "Selected":"";

            vCalTime+="</td><td>";
			vCalTime+="<select name=\"ampm\" onChange=\"javascript:Cal.SetAmPm(this.options[this.selectedIndex].value);\">\n";
			vCalTime+="<option "+SelectAm+" value=\"AM\">AM</option>";
			vCalTime+="<option "+SelectPm+" value=\"PM\">PM<option>";
			vCalTime+="</select>";
		}

		if (ShowArrows && UseImageFiles) {
		   vCalTime+="</td>\n<td align='center'><table cellspacing='0' cellpadding='0' style='line-height:0pt'><tr><td><img onmousedown='startSpin(\"Minute\", \"plus\");' onmouseup='stopSpin();' src='images/cal_plus.gif' width='13px' height='9px' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td></tr><tr><td><img onmousedown='startSpin(\"Minute\", \"minus\");' onmouseup='stopSpin();' src='images/cal_minus.gif' width='13px' height='9px' onmouseover='changeBorder(this, 0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'></td></tr></table>";
		}

		vCalTime+="</td>\n<td align='right' valign='bottom' width='"+HourCellWidth+"px'>";
	}

	else
		{vCalTime+="\n<tr>\n<td colspan='7' align='right'>";}
    if (UseImageFiles)
    {
        //vCalTime+="<a onmousedown='javascript:closewin(\"" + Cal.Ctrl + "\"); stopSpin();'  width='16px' height='14px' onmouseover='changeBorder(this,0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'>ปิดหน้านี้</a></td>";
        vCalTime += "<table width='100%'><tr><td width='50%'><a onmousedown='javascript:clearValue(\"" + Cal.Ctrl + "\");closewin(\"" + Cal.Ctrl + "\"); stopSpin();' width='16px' height='14px' onmouseover='changeBorder(this,0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'>ลบ</a></td><td width='50%' align='right'><a onmousedown='javascript:closewin(\"" + Cal.Ctrl + "\"); stopSpin();'  width='16px' height='14px' onmouseover='changeBorder(this,0)' onmouseout='changeBorder(this, 1)' style='border:1px solid white'>ปิด</a></td></tr></table></td>";
    }
    else
    {
       vCalTime+="<span id='close_cal' title='close'onmousedown='javascript:closewin(\"" + Cal.Ctrl + "\");' onmouseover='changeBorder(this, 0)'onmouseout='changeBorder(this, 1)' style='border:1px solid white; font-family: Arial;font-size: 10pt;'>x</span></td>";
    }
    vCalTime+="</tr>\n</table></center>\n</td>\n</tr>";
    calHeight += 31;
	vCalTime+="\n</table>\n</span>";

	//end time picker
    var funcCalback="function callback(id, datum) {\n";
    funcCalback += " var CalId = document.getElementById(id); if (datum== 'undefined') { var d = new Date(); datum = d.getDate() + '/' +(d.getMonth()+1) + '/' + (d.getFullYear()); } window.calDatum=datum;CalId.value=datum;\n";
    funcCalback += " if(document.getElementById(expDate) != null && document.getElementById(pwdDate) != null){ \n";
    funcCalback += "    document.getElementById(expDate).value=dateAdd('d',90,datum);\n";
    funcCalback += "    document.getElementById(pwdDate).value=dateAdd('yyyy',1,datum);\n";
    //funcCalback += "    dateAdd('d',90,datum);\n";
    //dateAdd(p_Interval, p_Number, p_Date) {
    funcCalback += " }\n";
    //funcCalback+=" alert('document.getElementById(age) = '+document.getElementById('age')); \n";
    funcCalback+=" if(document.getElementById('age')){; \n";
    funcCalback+=" var arr = CalId.value.split ('/');\n";
    funcCalback+=" calBirthday('age',parseInt(arr[2]),parseInt(arr[1]),parseInt(arr[0]));\n";
    //funcCalback+=" document.getElementById('age').innerHTML='79';\n";
    funcCalback+="}\n";
    funcCalback+=" if (Cal.ShowTime) {\n";
    funcCalback+=" CalId.value+=' '+Cal.getShowHour()+':'+Cal.Minutes;\n";
    funcCalback+=" if (Cal.ShowSeconds)\n  CalId.value+=':'+Cal.Seconds;\n";
    funcCalback+=" if (TimeMode==12)\n  CalId.value+=''+Cal.getShowAMorPM();\n";
    funcCalback += "}\n winCal.style.visibility='hidden';\n}\n";
    funcCalback += "\n";


	// determines if there is enough space to open the cal above the position where it is called
	if (ypos > calHeight) {
	   ypos = ypos - calHeight;
	}

	if (winCal == undefined) {
	   var headID = document.getElementsByTagName("head")[0];

     // add javascript function to the span cal
       var e = document.createElement("script");
       e.type = "text/javascript";
       e.language = "javascript";
       e.text = funcCalback;
       headID.appendChild(e);
	   // add stylesheet to the span cal

	   var cssStr = ".calTD {font-family: verdana; font-size: 12px; text-align: center; border:0 }\n";
	   //cssStr+= ".calR {font-family: verdana; font-size: 12px; text-align: center; font-weight: bold; color: red;}"
		cssStr+= ".calR {font-family: verdana; font-size: 12px; text-align: center; font-weight: bold;}"

	   var style = document.createElement("style");
       style.type = "text/css";
       style.rel = "stylesheet";
       if(style.styleSheet) { // IE
          style.styleSheet.cssText = cssStr;
        }

	   else { // w3c
          var cssText = document.createTextNode(cssStr);
          style.appendChild(cssText);
		}

       headID.appendChild(style);
	   // create the outer frame that allows the cal. to be moved
	   var span = document.createElement("span");
       span.id = calSpanID;

       with (span.style) { position = "absolute"; left = (xpos + 8) + 'px'; top = (ypos - 8) + 'px'; width = CalWidth + 'px'; border = "solid 2pt " + SpanBorderColor; padding = "1pt"; cursor = "move"; backgroundColor = SpanBgColor; zIndex = 100000; }
       document.body.appendChild(span);
       winCal=document.getElementById(calSpanID);
    }

    else {
	  winCal.style.visibility = "visible";
	  winCal.style.Height = calHeight;

	  // set the position for a new calendar only
	  if(bNewCal==true){
	     winCal.style.left = (xpos+8)+'px';
	     winCal.style.top = (ypos-8)+'px';
	   }
	}

	winCal.innerHTML=winCalData + vCalHeader + vCalData + vCalTime;
	return true;
}

function  calBirthday(ctrl_name,year,mon,date){
	var newCurrent =new Date();
	var Current_year = newCurrent.getFullYear();
    if(Current_year < 2400)
        Current_year = Current_year;
	var Current_month = newCurrent.getMonth()+1;
	year_birthday = Current_year-year;
	if(mon>Current_month){
		//alert("month = "+newCurrent.getMonth());
		year_birthday =year_birthday-1;
	}
	if((Current_month-mon)==0){
		if( date>newCurrent.getDate()){
			year_birthday =year_birthday-1;
		}
	}

	year_birthday=(year_birthday <0)?0:year_birthday;

	//year_birthday = newCurrent.getMonth()-mon;
	//newCurrent.setDate(newCurrent.getDate()-date+1);
	//newCurrent.setMonth(newCurrent.getMonth()-mon);
	//newCurrent.setFullYear(Current_year-year);
	//year_birthday=(newCurrent.getFullYear() <0)?0:newCurrent.getFullYear();
	//$('age').value=year_birthday
	//$('showAge').innerHTML=$F('age');
	document.getElementById(ctrl_name).innerHTML =year_birthday;
	}


function GenCell(pValue,pHighLight,pColor) { //Generate table cell with value
	var PValue;
	var PCellStr;
	var vColor;

	var vHLstr1;//HighLight string
	var vHlstr2;
	var vTimeStr;

	if (pValue==null)
		PValue="";
	else
		PValue=pValue;
	if (pColor!=null)
		vColor="bgcolor=\""+pColor+"\"";
	else
		vColor=CalBgColor;
	    if ((pHighLight!=null)&&(pHighLight)) {
		   vHLstr1="<font class='calR'>";vHLstr2="</font>";
		 }
	    else {
		   vHLstr1="";vHLstr2="";
		 }

	if (Cal.ShowTime) {
		vTimeStr=' '+Cal.Hours+':'+Cal.Minutes;
		if (Cal.ShowSeconds)
			vTimeStr+=':'+Cal.Seconds;
		if (TimeMode==12)
			vTimeStr+=' '+Cal.AMorPM;
	}

	else
		vTimeStr="";

	if (PValue!="") {
		//PCellStr="\n<td "+vColor+" class='calTD' style='cursor: pointer;' onmouseover='changeBorder(this, 0);' onmouseout='changeBorder(this, 1);' onClick=\"javascript:callback('"+Cal.Ctrl+"','"+Cal.FormatDate(PValue)+"');\">"+vHLstr1+PValue+vHLstr2+"</td>";

		if(pColor == SaturdayColor || pColor == SundayColor || pColor == SelDateColor || pColor == TodayColor) {
			PCellStr="\n<td "+vColor+" class='calTD' style='cursor: pointer;' onmouseover='changeBorder(this, 0);' onmouseout=\"changeBorder(this, 1, '"+pColor+"');\" onClick=\"javascript:callback('"+Cal.Ctrl+"','"+Cal.FormatDate(PValue)+"');\">"+vHLstr1+PValue+vHLstr2+"</td>";

		}
		else {
			PCellStr="\n<td "+vColor+" class='calTD' style='cursor: pointer;' onmouseover='changeBorder(this, 0);' onmouseout='changeBorder(this, 1);' onClick=\"javascript:callback('"+Cal.Ctrl+"','"+Cal.FormatDate(PValue)+"');\">"+vHLstr1+PValue+vHLstr2+"</td>";
		}
	}
	else

		PCellStr="\n<td "+vColor+" class='calTD'>&nbsp;</td>";

	return PCellStr;

}



function Calendar(pDate,pCtrl) {

	//Properties
	this.Date=pDate.getDate();//selected date
	this.Month=pDate.getMonth();//selected month number
	this.Year=pDate.getFullYear();//selected year in 4 digits
	this.Hours=pDate.getHours();

	if (pDate.getMinutes()<10)
		this.Minutes="0"+pDate.getMinutes();
	else
		this.Minutes=pDate.getMinutes();

	if (pDate.getSeconds()<10)
		this.Seconds="0"+pDate.getSeconds();
	else
		this.Seconds=pDate.getSeconds();


	this.MyWindow=winCal;
	this.Ctrl=pCtrl;
	this.Format="ddMMyyyy";
	this.Separator=DateSeparator;
	this.ShowTime=false;
	this.Scroller="DROPDOWN";
	if (pDate.getHours()<12)
		this.AMorPM="AM";
	else
		this.AMorPM="PM";

	this.ShowSeconds=true;
}



function GetMonthIndex(shortMonthName) {
	for (i=0;i<12;i++) {
		if (MonthName[i].substring(0,3).toUpperCase()==shortMonthName.toUpperCase())
		   {return i;}
	}
}

Calendar.prototype.GetMonthIndex=GetMonthIndex;

function IncYear() {
	Cal.Year++;}
	Calendar.prototype.IncYear=IncYear;

function DecYear() {
	Cal.Year--;}
	Calendar.prototype.DecYear=DecYear;

function IncMonth() {
	Cal.Month++;
	if (Cal.Month>=12) {
		Cal.Month=0;
		Cal.IncYear();
	}
}

Calendar.prototype.IncMonth=IncMonth;

function DecMonth() {
	Cal.Month--;
	if (Cal.Month<0) {
		Cal.Month=11;
		Cal.DecYear();
	}
}

Calendar.prototype.DecMonth=DecMonth;

function SwitchMth(intMth) {
	Cal.Month=intMth;}
	Calendar.prototype.SwitchMth=SwitchMth;

function SwitchYear(intYear) {
	Cal.Year=intYear;}
	Calendar.prototype.SwitchYear=SwitchYear;

function SetHour(intHour) {
	var MaxHour;
	var MinHour;
	if (TimeMode==24) {
		MaxHour=23;MinHour=0}
	else if (TimeMode==12) {
		MaxHour=12;MinHour=1}
	else
		alert("TimeMode can only be 12 or 24");

	var HourExp=new RegExp("^\\d\\d");
	var SingleDigit=new RegExp("\\d");

	if ((HourExp.test(intHour) || SingleDigit.test(intHour)) && (parseInt(intHour,10)>MaxHour)) {
	    intHour = MinHour;
	}

	else if ((HourExp.test(intHour) || SingleDigit.test(intHour)) && (parseInt(intHour,10)<MinHour)) {
  	    intHour = MaxHour;
	}

	if (SingleDigit.test(intHour)) {
		intHour="0"+intHour+"";
	}

	if (HourExp.test(intHour) && (parseInt(intHour,10)<=MaxHour) && (parseInt(intHour,10)>=MinHour)) {
		if ((TimeMode==12) && (Cal.AMorPM=="PM")) {
			if (parseInt(intHour,10)==12)
				Cal.Hours=12;
			else
				Cal.Hours=parseInt(intHour,10)+12;
		}

		else if ((TimeMode==12) && (Cal.AMorPM=="AM")) {
			if (intHour==12)
				intHour-=12;

			Cal.Hours=parseInt(intHour,10);
		}

		else if (TimeMode==24)
			Cal.Hours=parseInt(intHour,10);
	}

}

Calendar.prototype.SetHour=SetHour;

function SetMinute(intMin) {
	var MaxMin=59;
	var MinMin=0;

	var SingleDigit=new RegExp("\\d");
	var SingleDigit2=new RegExp("^\\d{1}$");
	var MinExp=new RegExp("^\\d{2}$");

	if ((MinExp.test(intMin) || SingleDigit.test(intMin)) && (parseInt(intMin,10)>MaxMin)) {
		intMin = MinMin;
	}

	else if ((MinExp.test(intMin) || SingleDigit.test(intMin)) && (parseInt(intMin,10)<MinMin))	{
		intMin = MaxMin;
	}

	var strMin = intMin + "";
	if (SingleDigit2.test(intMin)) {
		strMin="0"+strMin+"";
	}

	if ((MinExp.test(intMin) || SingleDigit.test(intMin))
	 && (parseInt(intMin,10)<=59) && (parseInt(intMin,10)>=0)) {

	 	Cal.Minutes=strMin;
	}
}

Calendar.prototype.SetMinute=SetMinute;

function SetSecond(intSec) {
	var MaxSec=59;
	var MinSec=0;

	var SingleDigit=new RegExp("\\d");
	var SingleDigit2=new RegExp("^\\d{1}$");
	var SecExp=new RegExp("^\\d{2}$");

	if ((SecExp.test(intSec) || SingleDigit.test(intSec)) && (parseInt(intSec,10)>MaxSec)) {
		intSec = MinSec;
	}

	else if ((SecExp.test(intSec) || SingleDigit.test(intSec)) && (parseInt(intSec,10)<MinSec))	{
		intSec = MaxSec;
	}

	var strSec = intSec + "";
	if (SingleDigit2.test(intSec)) {
		strSec="0"+strSec+"";
	}

	if ((SecExp.test(intSec) || SingleDigit.test(intSec))
	 && (parseInt(intSec,10)<=59) && (parseInt(intSec,10)>=0)) {

	 	Cal.Seconds=strSec;
	}

}

Calendar.prototype.SetSecond=SetSecond;

function SetAmPm(pvalue) {
	this.AMorPM=pvalue;
	if (pvalue=="PM") {

		this.Hours=(parseInt(this.Hours,10))+12;
		if (this.Hours==24)
  		    this.Hours=12;
	}

	else if (pvalue=="AM")
		this.Hours-=12;
}

Calendar.prototype.SetAmPm=SetAmPm;

function getShowHour() {
	var finalHour;

    if (TimeMode==12) {
    	if (parseInt(this.Hours,10)==0) {
			this.AMorPM="AM";
			finalHour=parseInt(this.Hours,10)+12;
		}

		else if (parseInt(this.Hours,10)==12) {
			this.AMorPM="PM";
			finalHour=12;
		}

		else if (this.Hours>12)	{
			this.AMorPM="PM";
			if ((this.Hours-12)<10)

				finalHour="0"+((parseInt(this.Hours,10))-12);
			else
				finalHour=parseInt(this.Hours,10)-12;
		}
		else {
			this.AMorPM="AM";
			if (this.Hours<10)

				finalHour="0"+parseInt(this.Hours,10);
			else
				finalHour=this.Hours;
		}
	}

	else if (TimeMode==24) {
		if (this.Hours<10)
			finalHour="0"+parseInt(this.Hours,10);
		else
    		finalHour=this.Hours;
	}

	return finalHour;
}

Calendar.prototype.getShowHour=getShowHour;

function getShowAMorPM() {
	return this.AMorPM;
}

Calendar.prototype.getShowAMorPM=getShowAMorPM;

function GetMonthName(IsLong) {
	var Month=MonthName[this.Month];
	if (IsLong)
		return Month;
	else
		return Month.substr(0,3);
}

Calendar.prototype.GetMonthName=GetMonthName;

function GetMonDays() { //Get number of days in a month

	var DaysInMonth=[31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
	if (this.IsLeapYear()) {
		DaysInMonth[1]=29;
	}

	return DaysInMonth[this.Month];
}

Calendar.prototype.GetMonDays=GetMonDays;

function IsLeapYear() {
	if ((this.Year%4)==0) {
		if ((this.Year%100==0) && (this.Year%400)!=0) {
			return false;
		}
		else {
			return true;
		}
	}
	else {
		return false;
	}

}

Calendar.prototype.IsLeapYear=IsLeapYear;

function FormatDate(pDate)
{
	var MonthDigit=this.Month+1;
	if (PrecedeZero==true) {
		if (pDate<10)
			pDate="0"+pDate;
		if (MonthDigit<10)
			MonthDigit="0"+MonthDigit;
	}

	if (this.Format.toUpperCase()=="DDMMYYYY")
		return (pDate+DateSeparator+MonthDigit+DateSeparator+String(543+parseInt(this.Year,10)) );

	else if (this.Format.toUpperCase()=="DDMMMYYYY")
		return (pDate+DateSeparator+this.GetMonthName(false)+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="MMDDYYYY")
		return (MonthDigit+DateSeparator+pDate+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="MMMDDYYYY")
		return (this.GetMonthName(false)+DateSeparator+pDate+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="YYYYMMDD")
		return (this.Year+DateSeparator+MonthDigit+DateSeparator+pDate);
	else if (this.Format.toUpperCase()=="YYMMDD")
		return (String(this.Year).substring(2,4)+DateSeparator+MonthDigit+DateSeparator+pDate);
	else if (this.Format.toUpperCase()=="YYMMMDD")
		return (String(this.Year).substring(2,4)+DateSeparator+this.GetMonthName(false)+DateSeparator+pDate);
	else if (this.Format.toUpperCase()=="YYYYMMMDD")
		return (this.Year+DateSeparator+this.GetMonthName(false)+DateSeparator+pDate);
	else
		return (pDate+DateSeparator+(this.Month+1)+DateSeparator+this.Year);
}

Calendar.prototype.FormatDate=FormatDate;

function closewin(id) {
   var CalId = document.getElementById(id);
   CalId.focus();
   winCal.style.visibility='hidden';
 }



function changeBorder(element, col, oldBgColor) {
  if (col == 0) {
	element.style.background = HoverColor;
    element.style.borderColor = "black";
    element.style.cursor = "pointer";
  }

  else {
	if(oldBgColor) {
		element.style.background = oldBgColor;
	} else {
		element.style.background = "white";
	}
    element.style.borderColor = "white";
    element.style.cursor = "auto";
  }
}


function pickIt(evt) {
   // accesses the element that generates the event and retrieves its ID
   if (window.addEventListener) { // w3c
	  var objectID = evt.target.id;
      if (objectID.indexOf(calSpanID) != -1){
         var dom = document.getElementById(objectID);
         cnLeft=evt.pageX;
         cnTop=evt.pageY;

         if (dom.offsetLeft){
           cnLeft = (cnLeft - dom.offsetLeft); cnTop = (cnTop - dom.offsetTop);
          }
       }

	  // get mouse position on click
	  xpos = (evt.pageX);
	  ypos = (evt.pageY);
	}

   else { // IE
	  var objectID = event.srcElement.id;
      cnLeft=event.offsetX;
      cnTop=(event.offsetY);

	  // get mouse position on click
	  var de = document.documentElement;
      var b = document.body;

      xpos = event.clientX + (de.scrollLeft || b.scrollLeft) - (de.clientLeft || 0);
      ypos = event.clientY + (de.scrollTop || b.scrollTop) - (de.clientTop || 0);
    }

   // verify if this is a valid element to pick
   if (objectID.indexOf(calSpanID) != -1){
      domStyle = document.getElementById(objectID).style;
    }

   if (domStyle) {
      domStyle.zIndex = 100;
      return false;
    }

   else {
      domStyle = null;
      return;
    }
 }



function dragIt(evt) {
   if (domStyle) {
      if (window.event) { //for IE
         domStyle.left = (event.clientX-cnLeft + document.body.scrollLeft)+'px';
         domStyle.top = (event.clientY-cnTop + document.body.scrollTop)+'px';
       } else {  //Firefox
         domStyle.left = (evt.clientX-cnLeft + document.body.scrollLeft)+'px';
         domStyle.top = (evt.clientY-cnTop + document.body.scrollTop)+'px';
       }
    }
 }



function dropIt() {
	stopSpin();

   if (domStyle) {
      domStyle = null;
    }
 }


// starts the time spinner
function startSpin(whatSpinner, direction) {
	document.thisLoop = setInterval("nextStep('"+whatSpinner+"', '"+direction+"');", 125); //125 ms
}

// performs a single increment or decrement
function nextStep(whatSpinner, direction) {
	if(whatSpinner == "Hour") {
		if(direction == "plus") {
			Cal.SetHour(Cal.Hours + 1); RenderCssCal();
		} else if(direction == "minus") {
			Cal.SetHour(Cal.Hours - 1); RenderCssCal();
		}
	} else if(whatSpinner == "Minute") {
		if(direction == "plus") {
			Cal.SetMinute(parseInt(Cal.Minutes,10) + 1); RenderCssCal();
		} else if(direction == "minus") {
			Cal.SetMinute(parseInt(Cal.Minutes,10) - 1); RenderCssCal();
		}
	}

}

//stops the time spinner
function stopSpin() {
	clearInterval(document.thisLoop);
}

function clearValue(pCtrl) {
    document.getElementById(pCtrl).value = "";
}



function dateAdd(p_Interval, p_Number, p_Date) {
    
    var dDate = p_Date.split("/");
    //alert(dDate[0] + " #### " + new Date(p_Date) + " #### " + new Date(dDate[2],dDate[1]-1, dDate[0]));
    //if (!isDate(p_Date)) { return "invalid date: '" + p_Date + "'"; }
    if (isNaN(p_Number)) { return "invalid number: '" + p_Number + "'"; }

    p_Number = new Number(p_Number);
    //var dt = new Date(p_Date);
    var dt = new Date(dDate[2] - 543, dDate[1] - 1, dDate[0]);
    dt = newDayAdd(dt, -1);
    switch (p_Interval.toLowerCase()) {
        case "yyyy":
            {// year
                dt.setFullYear(dt.getFullYear() + p_Number);
                break;
            }
        case "q":
            { // quarter
                dt.setMonth(dt.getMonth() + (p_Number * 3));
                break;
            }
        case "m":
            { // month
                dt.setMonth(dt.getMonth() + p_Number);
                break;
            }
        case "y": // day of year
        case "d": // day
        case "w":
            { // weekday
                dt.setDate(dt.getDate() + p_Number);
                break;
            }
        case "ww":
            { // week of year
                dt.setDate(dt.getDate() + (p_Number * 7));
                break;
            }
        case "h":
            { // hour
                dt.setHours(dt.getHours() + p_Number);
                break;
            }
        case "n":
            { // minute
                dt.setMinutes(dt.getMinutes() + p_Number);
                break;
            }
        case "s":
            { // second
                dt.setSeconds(dt.getSeconds() + p_Number);
                break;
            }
        case "ms":
            { // second
                dt.setMilliseconds(dt.getMilliseconds() + p_Number);
                break;
            }
        default:
            {
                return "invalid interval: '" + p_Interval + "'";
            }
    }

    //alert(dt.getDate());
    var retDate;
    if (dt.getDate() < 10)
        retDate =  "0" + dt.getDate();
    else
        retDate = dt.getDate();

    var retMonth;
    if ((dt.getMonth() + 1) < 10)
        retMonth = "0" + (dt.getMonth() + 1);
    else
        retMonth = (dt.getMonth() + 1);

    return retDate + "/" + retMonth + "/" + (dt.getFullYear() + 543);
}

function newDayAdd(inputDate, addDay) {
    var d = new Date(inputDate);
    d.setDate(d.getDate() + addDay);
    mkMonth = d.getMonth();
    mkMonth = new String(mkMonth);
    if (mkMonth.length == 1) {
        mkMonth = "0" + mkMonth;
    }
    mkDay = d.getDate();
    mkDay = new String(mkDay);
    if (mkDay.length == 1) { mkDay = "0" + mkDay; }
    mkYear = d.getFullYear();
    return new Date(mkYear, mkMonth, mkDay);

    // แสดงผลลัพธ์ในรูปแบบ ปี-เดือน-วัน return mkMonth+"/"+mkDay+"/"+mkYear; 
    // แสดงผลลัพธ์ในรูปแบบ เดือน/วัน/ปี } 
    // การใช้งาน // รูปแบบเริ่มต้นจะใช้เป็น เดือน/วัน/ปี เช่น 01/31/2009 alert(newDayAdd("01/10/2009",100)); 
    // วันเริ่มต้นเป็น 01/10/2009 
    // จำนวนวันที่จะเพิ่มเป็น 100 วัน 
    // ผลลัพธ์จะได้วันที่ในอีก 100 วันข้างหน้า นับจากวันเริ่มต้นที่กำหนด 
    // ได้ 04/20/2009 
    // หรือจะนำค่าที่ได้ไปเก็บไว้ในตัวแปรไว้ใช้งานต่อไปก็ได้ 
    // ดังนี้ var next100day=newDayAdd("01/10/2009",100);
}
    