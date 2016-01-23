Namespace ConnectDB
    Public Class MessageResources
        Public Shared ReadOnly Property CultureName() As String
            Get
                Try
                    'If System.Web.HttpContext.Current.Session(Constant.CultureSessionID) = Nothing Then
                    '    System.Web.HttpContext.Current.Session(Constant.CultureSessionID) = Constant.CultureName.Defaults
                    'End If
                    'Return System.Web.HttpContext.Current.Session(Constant.CultureSessionID).ToString()
                    Return Constant.CultureName.Defaults
                Catch ex As Exception
                    Return Constant.CultureName.Defaults
                End Try
            End Get
            'Set(ByVal value As String)
            '    System.Web.HttpContext.Current.Session(Constant.CultureSessionID) = value
            'End Set
        End Property
        Public Shared ReadOnly Property GetCultureText(ByVal EngName As String, ByVal ThaiName As String) As String
            Get
                Dim ret As String = ""
                If CultureName = Constant.CultureName.Eng Then
                    ret = EngName
                Else
                    ret = ThaiName
                End If
                Return ret
            End Get
        End Property
        Public Shared ReadOnly Property GetCultureDigit() As String
            Get
                Dim Cul As String = CultureName()
                Return IIf(Cul = Constant.CultureName.Eng, "E", "T")
            End Get
        End Property


        'Message
        Private Shared Function GenerateMessage(ByVal message As String)
            Return message
        End Function

        'Error Message
        Public Shared ReadOnly Property MSGEC001() As String
            Get
                Return GenerateMessage(GetCultureText("Cannot not retrieve a System.Configuration.ConnectionStringSettingsCollection object.", "ไม่พบค่าการเชื่อมต่อฐานข้อมูลใน Configuration"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC002() As String
            Get
                Return GenerateMessage(GetCultureText("The connection does not exist.-or- The connection is not open.", "ไม่สามารถเชื่อมต่อฐานข้อมูลได้"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC003() As String
            Get
                Return GenerateMessage(GetCultureText("Cannot set the connection used by the instance of the command.", "ไม่สามารถกำหนดการเชื่อมต่อสำหรับคำสั่งทำรายการได้"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC004() As String
            Get
                Return GenerateMessage(GetCultureText("The value was not a valid System.Data.CommandType.", "ค่าของ System.Data.CommandType ไม่ถูกต้อง"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC005() As String
            Get
                Return GenerateMessage(GetCultureText("The value of parameter is null.", "ไม่ได้กำหนดพารามิเตอร์"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC006() As String
            Get
                Return GenerateMessage(GetCultureText("The parameter specified is already added to this or another parameter collection.", "พารามิเตอร์ที่ระบุซ้ำกับที่มีอยู่"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC007() As String
            Get
                Return GenerateMessage(GetCultureText("No column with the {0} name was found in {1}.", "ไม่พบคอลัมน์ {0} ในตาราง {1}"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC008() As String
            Get
                Return GenerateMessage(GetCultureText("The transaction has already been committed or rolled back.-or- The connection is broken.", "มีการยืนยันหรือยกเลิกข้อมูลระหว่างทำรายการ หรือ การเชื่อมต่อฐานข้อมูลขัดข้อง"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC009() As String
            Get
                Return GenerateMessage(GetCultureText("Parallel transactions are not supported.", "ไม่สนับสนุนการทำรายการคู่ขนาน"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC010() As String
            Get
                Return GenerateMessage(GetCultureText("Error when execute sql statement.", "เกิดความผิดพลาดขณะรันคำสั่ง sql"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC011() As String
            Get
                Return GenerateMessage(GetCultureText("Error when build DataReader object.", "เกิดความผิดพลาดขณะสร้าง DataReader"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC012() As String
            Get
                Return GenerateMessage(GetCultureText("Error when build DataTable object.", "เกิดความผิดพลาดขณะสร้าง DataTable"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC013() As String
            Get
                Return GenerateMessage(GetCultureText("Error in ExecuteScalar method.", "เกิดความผิดพลาดขณะรันคำสั่ง ExecuteScalar"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC014() As String
            Get
                Return GenerateMessage(GetCultureText("{0} does not exist in database.", "ไม่พบตารางหรือวิว {0} ในฐานข้อมูล"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC015() As String
            Get
                Return GenerateMessage(GetCultureText("Cannot download the resource as a System.Uri.", "ไม่สามารถดาวน์โหลดข้อมูลจากปลายทางได้"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC101() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while insert data.", "เกิดความผิดพลาดขณะเพิ่มข้อมูลใหม่"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC102() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while update data.", "เกิดความผิดพลาดขณะบันทึกแก้ไขข้อมูล"))
            End Get
        End Property
        Public Shared ReadOnly Property MSGEC103() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while delete data.", "เกิดความผิดพลาดขณะลบข้อมูล"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC104() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while select data.", "เกิดความผิดพลาดขณะเรียกดูข้อมูล"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC105() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while importing excel data.", "เกิดความผิดพลาดขณะนำเข้าข้อมูล จาก Excel"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC106() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while initial process.", "เกิดความผิดพลาดในการกำหนดค่าเริ่มต้นเพื่อเข้าสู่กระบวนการ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGES001() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while loging in", "เกิดความผิดพลาดขณะเข้าสู่ระบบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGES002() As String
            Get
                Return GenerateMessage(GetCultureText("There are errors occur while changing password", "เกิดความผิดพลาดขณะเปลี่ยนรหัสผ่าน"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC901() As String
            Get
                Return GenerateMessage(GetCultureText("Database error occur {0} -> {1}", "เกิดความผิดพลาดในการติดต่อฐานข้อมูล {0} -> {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEC902() As String
            Get
                Return GenerateMessage(GetCultureText("Undefined error occur {0}", "เกิดความผิดพลาดขณะทำรายการ {0}"))
            End Get
        End Property




        Public Shared ReadOnly Property MSGEA001() As String
            Get
                Return GenerateMessage(GetCultureText("Your user name is incorrect. Please try again.", "ชื่อเข้าระบบไม่ถูกต้อง กรุณาลองอีกครั้ง"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEA002() As String
            Get
                Return GenerateMessage(GetCultureText("Your password is incorrect. Please try again.", "รหัสผ่านไม่ถูกต้อง กรุณาลองอีกครั้ง"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEA003() As String
            Get
                Return GenerateMessage(GetCultureText("You are not allowed entering to this system. Please contact your administrator.", "คุณไม่มีสิทธิเข้าใช้ระบบนี้ กรุณาติดต่อเจ้าหน้าที่ผู้ดูแลระบบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEA004() As String
            Get
                Return GenerateMessage(GetCultureText("You are not allowed temporarily. Please contact your administrator.", "คุณไม่มีสิทธิเข้าใช้ระบบในขณะนี้ กรุณาติดต่อเจ้าหน้าที่ผู้ดูแลระบบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGED001() As String
            Get
                Return GenerateMessage(GetCultureText("There are no any records be deleted.", "ไม่พบรายการที่ถูกลบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGED002() As String
            Get
                Return GenerateMessage(GetCultureText("Can not delete non-exist data.", "ไม่สามารถลบข้อมูลได้ เนื่องจากไม่พบข้อมูล"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGED003() As String
            Get
                Return GenerateMessage(GetCultureText("Can not delete data without conditions", "ไม่สามารถลบข้อมูลได้ เนื่องจากไม่ได้ระบุเงื่อนไข"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI001() As String
            Get
                Return GenerateMessage(GetCultureText("{0} is required.", "กรุณาระบุ {0}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI002() As String
            Get
                Return GenerateMessage(GetCultureText("{0} is required.", "กรุณาเลือก {0}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI003() As String
            Get
                Return GenerateMessage(GetCultureText("{0} must be equal to {1}.", "{0} ต้องเท่ากับ {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI004() As String
            Get
                Return GenerateMessage(GetCultureText("{0} must be not equal to {1}.", "{0} ต้องไม่เท่ากับ {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI005() As String
            Get
                Return GenerateMessage(GetCultureText("{0} must be more than {1}.", "{0} ต้องมากกว่า {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI006() As String
            Get
                Return GenerateMessage(GetCultureText("{0} must be more than or equal to {1}.", "{0} ต้องมากกว่าหรือเท่ากับ {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI007() As String
            Get
                Return GenerateMessage(GetCultureText("{0} must be less than {1}.", "{0} ต้องน้อยกว่า {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI008() As String
            Get
                Return GenerateMessage(GetCultureText("{0} must be less than or equal to {1}.", "{0} ต้องน้อยกว่าหรือเท่ากับ {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI009() As String
            Get
                Return GenerateMessage(GetCultureText("The length of {0} must be equal to {1}.", "{0} ต้องมีความยาว {1} ตัวอักษร"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI010() As String
            Get
                Return GenerateMessage(GetCultureText("The length of {0} must be more than {1}.", "{0} ต้องมีความยาวมากกว่า {1} ตัวอักษร"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI011() As String
            Get
                Return GenerateMessage(GetCultureText("The length of {0} must be more than or equal to {1}.", "{0} ต้องมีความยาวมากกว่าหรือเท่ากับ {1} ตัวอักษร"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI012() As String
            Get
                Return GenerateMessage(GetCultureText("The length of {0} must be less than {1}.", "{0} ต้องมีความยาวน้อยกว่า {1} ตัวอักษร"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI013() As String
            Get
                Return GenerateMessage(GetCultureText("The length of {0} must be less than or equal to {1}.", "{0} ต้องมีความยาวน้อยกว่าหรือเท่ากับ {1} ตัวอักษร"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI014() As String
            Get
                Return GenerateMessage(GetCultureText("{0} must be same as {1}.", "{0} ต้องตรงกับ {1}"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI015() As String
            Get
                Return GenerateMessage(GetCultureText("{0} '{1}' has already existed in system.", "{0} '{1}' ซ้ำกับที่มีอยู่ในระบบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI016() As String
            Get
                Return GenerateMessage(GetCultureText("{0} '{1}' and {2} '{3}' has already existed in the system", "{0} '{1}' และ {2} '{3}' ซ้ำกับที่มีอยู่ในระบบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI017() As String
            Get
                Return GenerateMessage(GetCultureText("{0} '{1}', {2} '{3}' and {4} '{5}' has already existed in the system", "{0} '{1}', {2} '{3}' และ {4} '{5}' ซ้ำกับที่มีอยู่ในระบบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEI018() As String
            Get
                Return GenerateMessage(GetCultureText("{0} '{1}', {2} '{3}', {4} '{5}' and {6} '{7}' has already existed in the system", "{0} '{1}', {2} '{3}', {4} '{5}' และ {6} '{7}' ซ้ำกับที่มีอยู่ในระบบ"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEN001() As String
            Get
                Return GenerateMessage(GetCultureText("There are no any records be inserted.", "ไม่พบรายการที่เพิ่มใหม่"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEN002() As String
            Get
                Return GenerateMessage(GetCultureText("Can not insert data with duplicate key.", "ไม่สามารถเพิ่มข้อมูลซ้ำกับที่มีอยู่"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEU001() As String
            Get
                Return GenerateMessage(GetCultureText("There are no any records be updated.", "ไม่พบรายการที่แก้ไข"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEU002() As String
            Get
                Return GenerateMessage(GetCultureText("Can not update non-exist data.", "ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากไม่พบข้อมูล"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEU003() As String
            Get
                Return GenerateMessage(GetCultureText("Can not update single record data without primary key conditions.", "ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากไม่ได้ระบุคีย์หลัก"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEV001() As String
            Get
                Return GenerateMessage(GetCultureText("There are no primary key condition for single record select statement.", "ข้อมูลจากการค้นหาอาจมีมากกว่า 1 รายการ กรุณาระบุคีย์หลัก"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEV002() As String
            Get
                Return GenerateMessage(GetCultureText("Data not found.", "ไม่พบข้อมูลตามเงื่อนไขที่กำหนด"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGEV003() As String
            Get
                Return GenerateMessage(GetCultureText("The search result has more one rows. Please select other primary key.", "ข้อมูลจากการค้นหามีมากกว่า 1 รายการ กรุณาเลือกคีย์หลักใหม่"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGCN001() As String
            Get
                Return GenerateMessage(GetCultureText("Are you sure you want to insert new data?", "ต้องการเพิ่มรายการใช่หรือไม่?"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGCD001() As String
            Get
                Return GenerateMessage(GetCultureText("Are you sure you want to delete this record?", "ต้องการลบรายการใช่หรือไม่?"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGCD002() As String
            Get
                Return GenerateMessage(GetCultureText("Are you sure you want to delete all records?", "ต้องการลบรายการทั้งหมดใช่หรือไม่?"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGCD003() As String
            Get
                Return GenerateMessage(GetCultureText("Are you sure you want to delete selected records?", "ต้องการลบรายการที่เลือกใช่หรือไม่?"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGCD004() As String
            Get
                Return GenerateMessage(GetCultureText("Are you sure you want to delete the record {0} '{1}'?", "ต้องการลบรายการ {0} '{1}' ใช่หรือไม่?"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGCC001() As String
            Get
                Return GenerateMessage(GetCultureText("Are you sure you want to copy the record {0} '{1}'?", "ต้องการคัดลอกรายการ {0} '{1}' ใช่หรือไม่?"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGCS001() As String
            Get
                Return GenerateMessage(GetCultureText("Are you sure you want to log out?", "ต้องการออกจากระบบใช่หรือไม่?"))
            End Get
        End Property

        '#region Information
        '/// <summary>Inserting data is completed successfully.</summary>
        Public Shared ReadOnly Property MSGIN001()
            Get
                Return GenerateMessage(GetCultureText("Inserting data is completed successfully.", "เพิ่มข้อมูลเรียบร้อยแล้ว"))
            End Get
        End Property

        '/// <summary>Updating data is completed successfully.</summary>
        Public Shared ReadOnly Property MSGIU001() As String
            Get
                Return GenerateMessage(GetCultureText("Updating data is completed successfully.", "แก้ไขข้อมูลเรียบร้อยแล้ว"))
            End Get
        End Property

        '/// <summary>Deleting data is completed successfully.</summary>
        Public Shared ReadOnly Property MSGIR001() As String
            Get
                Return GenerateMessage(GetCultureText("Updating data is completed successfully.", "ส่งเรื่องเรียบร้อยแล้ว"))
            End Get
        End Property

        '/// <summary>Changing password is completed successfully.</summary>
        Public Shared ReadOnly Property MSGID001() As String
            Get
                Return GenerateMessage(GetCultureText("Deleting data is completed successfully.", "ลบข้อมูลเรียบร้อยแล้ว"))
            End Get
        End Property

        Public Shared ReadOnly Property MSGIS001() As String
            Get
                Return GenerateMessage(GetCultureText("Changing password is completed successfully.", "เปลี่ยนรหัสผ่านเรียบร้อยแล้ว"))
            End Get
        End Property



        '#region Constant
        'public Shared ReadOnly Property Anonymous As String
        '{
        '    get { return GetCultureText("Anonymous", "บุคคลทั่วไป"); }
        '}
        'public Shared ReadOnly Property Cancel As String
        '{
        '    get { return GetCultureText("Cancel", "ยกเลิก"); }
        '}
        'public Shared ReadOnly Property ChangePassword As String
        '{
        '    get { return GetCultureText("Change Password", "เปลียนรหัสผ่าน"); }
        '}
        'public Shared ReadOnly Property CopyRight As String
        '{
        '    get { return GetCultureText("Copyright &copy; All Rights Reserved.<br>Dept. of Intellectual Property, Ministry of Commerce<br>44/100 Nonthaburi 1 Rd., Bangkrasor, Muang, Nonthaburi, Thailand 11000.", "สงวนลิขสิทธิ์ &copy; โดย กรมทรัพย์สินทางปัญญา<br>44/100 ถนนนนทบุรี 1 ต.บางกระสอ อ.เมือง จ.นนทบุรี 11000"); }
        '}
        'public Shared ReadOnly Property EditProfile As String
        '{
        '    get { return GetCultureText("Edit Profile", "แก้ไขข้อมูลส่วนตัว"); }
        '}
        'public Shared ReadOnly Property EmptyDataText As String
        '{
        '    get { return GetCultureText("<center>***Data Not Found***</center>", "<center>***ไม่พบข้อมูล***</center>"); }
        '}
        'public Shared ReadOnly Property Home As String
        '{
        '    get { return GetCultureText("Home", "กลับหน้าหลัก"); }
        '}
        'public Shared ReadOnly Property LogIn As String
        '{
        '    get { return GetCultureText("Log In", "ลงชื่อเข้าใช้"); }
        '}
        'public Shared ReadOnly Property LoginRequire As String
        '{
        '    get { return GetCultureText("You are not logged in.", "กรุณาลงชื่อเข้าใช้ระบบ"); }
        '}
        'public Shared ReadOnly Property OK As String
        '{
        '    get { return GetCultureText("OK", "ตกลง"); }
        '}
        'public Shared ReadOnly Property AutherizeRequire As String
        '{
        '    get { return GetCultureText("You are not allowed to access this page.", "คุณไม่มีสิทธิ์เข้าใช้งานในส่วนนี้"); }
        '}
        'public Shared ReadOnly Property Login_OfficerCheckBox As String
        '{
        '    get { return GetCultureText("For officer only", "สำหรับเจ้าหน้าที่"); }
        '}
        'public Shared ReadOnly Property Login_Password As String
        '{
        '    get { return GetCultureText("Password  :&nbsp;&nbsp;", "รหัสผ่าน  :&nbsp;&nbsp;"); }
        '}
        'public Shared ReadOnly Property Login_UserID As String
        '{
        '    get { return GetCultureText("User ID  :&nbsp;&nbsp;", "ชื่อผู้ใช้  :&nbsp;&nbsp;"); }
        '}
        'public Shared ReadOnly Property LogOut As String
        '{
        '    get { return GetCultureText("Log Out", "ออกจากระบบ"); }
        '}
        'public Shared ReadOnly Property LoggedOut As String
        '{
        '    get { return GetCultureText("You are logged out.", "ออกจากระบบเรียบร้อยแล้ว"); }
        '}
        'public Shared ReadOnly Property Register As String
        '{
        '    get { return GetCultureText("Register", "สมัครสมาชิก"); }
        '}
        'public Shared ReadOnly Property SearchHeader As String
        '{
        '    get { return GetCultureText("Search Criteria", "ค้นหา"); }
        '}
        'public Shared ReadOnly Property Thai As String
        '{
        '    get { return "ไทย"; }
        '}
        '/// <summary>Please wait while processing ...</summary>
        'public Shared ReadOnly Property Wait As String
        '{
        '    get { return GetCultureText("Please wait while processing ...", "กรุณารอสักครู่..."); }
        '}
        'public Shared ReadOnly Property Welcome As String
        '{
        '    get { return GetCultureText("Welcome", "ยินดีต้อนรับ"); }
        '}
        'public Shared ReadOnly Property DownloadManual As String
        '{
        '    get { return GetCultureText("Download User Manual", "ดาวน์โหลดคู่มือการใช้งาน"); }
        '}
        'public Shared ReadOnly Property ContactAdmin As String
        '{
        '    get { return GetCultureText("Contact System Administrator", "ติดต่อผู้ดูแลระบบ"); }
        '}
        '#endregion

    End Class
End Namespace

