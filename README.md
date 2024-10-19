# LaserMarking
Local Development Getting Started:

To develop this application, the local user also needs Marking Builder Plus. The installation for this can be found @ U:\IT\Computer Programs\Keyence Laser Marker. 

Install the SolidWorks API @ U:\IT\Computer Programs\SolidWorks Downloads\SW 2023\SOLIDWORKS 2023 SP05\apisdk. 

Add a project reference to the ActiveX file "MBActX.ocx" found in whatever file Marking Builder Plus was installed to. By default this should be C:\Program Files (x86)\KEYENCE\MarkingBuilderPlus_Ver2.

Add a project reference to EPDM.Interop.epdm.dll, found in the directory that the SolidWorks API SDK was installed to. By default this is C:\Program Files (x86)\SOLIDWORKS PDM.

Note: There are similar Nuget packages for the above two references but these will NOT work. the actual files must be added as project references by right-clicking the project file -> Add -> Reference -> Browse

The user manual for ActiveX controls (MBPLib2, etc) can be found at "U:\IT\Computer Programs\Keyence Laser Marker\User's Manual\MD-X-ActiveX_UM_A99GB_252105_GB_2051-1.pdf".
