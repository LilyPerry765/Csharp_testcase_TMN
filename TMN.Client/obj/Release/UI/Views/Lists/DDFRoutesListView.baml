   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf 1clr-namespace:TMN.Views.ListsTMN.Views.Lists '   !TMN.Views.Lists.DDFRoutesListView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     -ucclr-namespace:TMN.UserControls     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     +vclr-namespace:TMN.Views.Lists     #  TMN.Views.Lists.ItemsListBase    ListView  ��  �� View �� .P���  �� Width$ 25��$	��V/L�� �� CellTemplate �� .R��� $��16��$��16������ *Image+�� $��آدرس لينک��$
 Auto�� �� DisplayMemberBinding �� *Link.Address+�� $��Timeslot��$
 Auto�� �� *
TimeSlot+�� $��
کانال��$
 Auto�� �� *LNO+�� $��نام مسیر��$
 Auto�� �� *Route.RouteName+�� $��پروتکل��$
 Auto�� �� *Route.Protocol+'  !TMN.Converters.ProtocolsConverter   Instance �� 	Converter# Z �� $��نوع مسير��$
 Auto�� �� *Route.Destination.CenterType+)  #TMN.Converters.CenterTypesConverter   Instance# Z �� $��Route��$
 Auto�� �� *
Route.TGNO+�� $	��OPC��$
 Auto�� �� *	Route.OPC+�� $	��DPC��$
 Auto�� �� *	Route.DPC+