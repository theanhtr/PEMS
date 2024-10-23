﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Base.Model {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Base.Model.Resource.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &lt;{0}&gt; object already exists in the system..
        /// </summary>
        public static string Code_Exist {
            get {
                return ResourceManager.GetString("Code_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &lt;{0}&gt; object does not exist in the system..
        /// </summary>
        public static string Code_Not_Exist {
            get {
                return ResourceManager.GetString("Code_Not_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[A-Z]+\d*-\d+$.
        /// </summary>
        public static string Code_Regex {
            get {
                return ResourceManager.GetString("Code_Regex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot delete from an empty list!.
        /// </summary>
        public static string Delete_Empty_Error {
            get {
                return ResourceManager.GetString("Delete_Empty_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The department &lt;{0}&gt; already exists in the system..
        /// </summary>
        public static string Department_Exist {
            get {
                return ResourceManager.GetString("Department_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The department &lt;{0}&gt; does not exist in the system..
        /// </summary>
        public static string Department_Not_Exist {
            get {
                return ResourceManager.GetString("Department_Not_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employee_Basic_Template.xlsx.
        /// </summary>
        public static string Employee_Basic_Excel_Template_File_Name {
            get {
                return ResourceManager.GetString("Employee_Basic_Excel_Template_File_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employee_List.xlsx.
        /// </summary>
        public static string Employee_Excel_Export_File_Name {
            get {
                return ResourceManager.GetString("Employee_Excel_Export_File_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The employee &lt;{0}&gt; already exists in the system..
        /// </summary>
        public static string Employee_Exist {
            get {
                return ResourceManager.GetString("Employee_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employee_Full_Template.xlsx.
        /// </summary>
        public static string Employee_Full_Excel_Template_File_Name {
            get {
                return ResourceManager.GetString("Employee_Full_Excel_Template_File_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The employee &lt;{0}&gt; does not exist in the system..
        /// </summary>
        public static string Employee_Not_Exist {
            get {
                return ResourceManager.GetString("Employee_Not_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employee Code.
        /// </summary>
        public static string Excel_Employee_Code_Header_Name {
            get {
                return ResourceManager.GetString("Excel_Employee_Code_Header_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employee List.
        /// </summary>
        public static string Excel_Employee_Title_Export {
            get {
                return ResourceManager.GetString("Excel_Employee_Title_Export", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The header &lt;{0}&gt; is duplicated..
        /// </summary>
        public static string Excel_Header_Duplicate {
            get {
                return ResourceManager.GetString("Excel_Header_Duplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The header name cannot be empty..
        /// </summary>
        public static string Excel_Header_Empty {
            get {
                return ResourceManager.GetString("Excel_Header_Empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot proceed with data import if required columns are not mapped to corresponding columns in the Excel file. Please check again..
        /// </summary>
        public static string Excel_Header_Required_Not_Map {
            get {
                return ResourceManager.GetString("Excel_Header_Required_Not_Map", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data row is a duplicate of row number &lt;{0}&gt;..
        /// </summary>
        public static string Excel_Row_Duplicate {
            get {
                return ResourceManager.GetString("Excel_Row_Duplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resource not found..
        /// </summary>
        public static string Exception_NotFound_Default {
            get {
                return ResourceManager.GetString("Exception_NotFound_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to System error, please contact MISA for assistance..
        /// </summary>
        public static string Exception_System_Default {
            get {
                return ResourceManager.GetString("Exception_System_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid data, please check again..
        /// </summary>
        public static string Exception_Validate_Default {
            get {
                return ResourceManager.GetString("Exception_Validate_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid attachment file, only .xlsx files are allowed..
        /// </summary>
        public static string Format_Excel_Error {
            get {
                return ResourceManager.GetString("Format_Excel_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Female.
        /// </summary>
        public static string Gender_Female {
            get {
                return ResourceManager.GetString("Gender_Female", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Male.
        /// </summary>
        public static string Gender_Male {
            get {
                return ResourceManager.GetString("Gender_Male", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Other.
        /// </summary>
        public static string Gender_Other {
            get {
                return ResourceManager.GetString("Gender_Other", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The system has recorded the import command..
        /// </summary>
        public static string Import_Excel_Request_Success {
            get {
                return ResourceManager.GetString("Import_Excel_Request_Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to en.
        /// </summary>
        public static string Lang_Code {
            get {
                return ResourceManager.GetString("Lang_Code", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exceeded the allowed file size..
        /// </summary>
        public static string Max_Size_File_Error {
            get {
                return ResourceManager.GetString("Max_Size_File_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your session has expired, please reload the page to start a new session..
        /// </summary>
        public static string Session_Is_Over {
            get {
                return ResourceManager.GetString("Session_Is_Over", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The sheet has no data..
        /// </summary>
        public static string Sheet_Is_Empty {
            get {
                return ResourceManager.GetString("Sheet_Is_Empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The full name must not be empty..
        /// </summary>
        public static string Validate_ThreadName_NotNull {
            get {
                return ResourceManager.GetString("Validate_ThreadName_NotNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User input error..
        /// </summary>
        public static string Validate_User_Input_Error {
            get {
                return ResourceManager.GetString("Validate_User_Input_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Code &lt;{0}&gt; has an incorrect format. The correct format is: NV-1001, AB2-999..
        /// </summary>
        public static string Wrong_Format_Code {
            get {
                return ResourceManager.GetString("Wrong_Format_Code", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incorrect date format, the correct format is: &lt;{0}&gt;..
        /// </summary>
        public static string Wrong_Format_Date {
            get {
                return ResourceManager.GetString("Wrong_Format_Date", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid email format..
        /// </summary>
        public static string Wrong_Format_Email {
            get {
                return ResourceManager.GetString("Wrong_Format_Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;{0}&gt; has a length greater than {1} characters..
        /// </summary>
        public static string Wrong_Length {
            get {
                return ResourceManager.GetString("Wrong_Length", resourceCulture);
            }
        }
    }
}