﻿#pragma checksum "..\..\..\Views\Login.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A57B71D7CC4B51498F0225E94321809489F819FBEBEB8353A9344D297DBCD602"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using AdonisUI;
using AdonisUI.Controls;
using AdonisUI.Extensions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using kunze_prüfer.Views;


namespace kunze_prüfer.Views {
    
    
    /// <summary>
    /// Login
    /// </summary>
    public partial class Login : AdonisUI.Controls.AdonisWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BoxMitNr;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxPassword;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonLogin;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockDatabaseConfig;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageDatabaseOk;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockDatabaseStatus;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/kunze-prüfer;component/views/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\Login.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BoxMitNr = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.TextBoxPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 45 "..\..\..\Views\Login.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ToggleButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonLogin = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Views\Login.xaml"
            this.ButtonLogin.Click += new System.Windows.RoutedEventHandler(this.ButtonLogin_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TextBlockDatabaseConfig = ((System.Windows.Controls.TextBlock)(target));
            
            #line 49 "..\..\..\Views\Login.xaml"
            this.TextBlockDatabaseConfig.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlockDatabaseConfig_OnMouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ImageDatabaseOk = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.TextBlockDatabaseStatus = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

