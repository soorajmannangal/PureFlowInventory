; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "PureFlow"
#define MyAppVersion "1.0"
#define MyAppPublisher "Sooraj"
#define MyAppExeName "PureFlow.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{FE85324A-927B-4507-82A2-98AF65DB9AF7}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=mysetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "E:\Repo\bin\Debug\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\CommonServiceLocator.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.Extras.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.Extras.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.Extras.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.Platform.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.Platform.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.Platform.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\GalaSoft.MvvmLight.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\Owin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\PureFlow.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\PureFlow.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\PureFlow.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\PureFlowDB.accdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\System.Windows.Interactivity.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Repo\bin\Debug\test.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

