﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Landmarks.Common.Resources.Admin {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Landmark {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Landmark() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Landmarks.Common.Resources.Admin.Landmark", typeof(Landmark).Assembly);
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
        ///   Looks up a localized string similar to Add a new landmark.
        /// </summary>
        public static string lblAdd {
            get {
                return ResourceManager.GetString("lblAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Edit landmark.
        /// </summary>
        public static string lblEdit {
            get {
                return ResourceManager.GetString("lblEdit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The maximum length of the name is 200 characters.
        /// </summary>
        public static string msgMaxLen {
            get {
                return ResourceManager.GetString("msgMaxLen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The name must be at least five characters long.
        /// </summary>
        public static string msgMinLen {
            get {
                return ResourceManager.GetString("msgMinLen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter a name of landmark.
        /// </summary>
        public static string msgRequiredName {
            get {
                return ResourceManager.GetString("msgRequiredName", resourceCulture);
            }
        }
    }
}